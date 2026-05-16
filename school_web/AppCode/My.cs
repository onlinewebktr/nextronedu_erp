using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.AppCode
{
    public class My
    {
        internal static string GenerateHmac(string msg, string keyString)
        {
            string digest = null;
            try
            {
                // Convert the key string to a byte array
                byte[] key = Encoding.UTF8.GetBytes(keyString);

                // Create an HMACSHA256 instance and initialize with the key
                using (var hmac = new HMACSHA256(key))
                {
                    // Convert the message string to bytes
                    byte[] msgBytes = Encoding.ASCII.GetBytes(msg);

                    // Compute the HMAC digest
                    byte[] hashBytes = hmac.ComputeHash(msgBytes);

                    // Convert the hash bytes to a hexadecimal string
                    StringBuilder hash = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        hash.Append(b.ToString("x2"));
                    }

                    digest = hash.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (if necessary)
                Console.WriteLine("Error: " + ex.Message);
            }

            return digest;

        }
        public Dictionary<string, object> get_icic_gateway_details()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from App_Setting   ";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["ICIC_MID"] = "";
                dc["ICIC_Agg_ID"] = "0";
                dc["ICIC_Key"] = "0";

            }
            else
            {
                dc["ICIC_MID"] = dt.Rows[0]["ICIC_MID"].ToString();
                dc["ICIC_Agg_ID"] = dt.Rows[0]["ICIC_Agg_ID"].ToString();
                dc["ICIC_Key"] = dt.Rows[0]["ICIC_Key"].ToString();

            }
            return dc;

        }
        internal void bind_all_ddl_with_id_list_select(ListBox ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        internal static Dictionary<string, object> get_admission_no_status(string old_Admision_No, string new_Admission_No)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string oldadmission_no = "";
            DataTable dt = My.dataTable("Select admissionserialnumber from admission_registor where admissionserialnumber='" + old_Admision_No + "'");
            if (dt.Rows.Count == 0)
            {
                oldadmission_no = "Old admission number does not match the data in the our database";
            }
            else
            {
                oldadmission_no = "Yes";
            }

            string newadmission_no = "";
            DataTable dt1 = My.dataTable("Select admissionserialnumber,studentname from admission_registor where admissionserialnumber='" + new_Admission_No + "' order by id desc");
            if (dt1.Rows.Count == 0)
            {
                newadmission_no = "Yes";
            }
            else
            {
                newadmission_no = "New admission number is alreday assoctaed with student Adm. " + dt1.Rows[0]["admissionserialnumber"].ToString() + " Student Name :-" + dt1.Rows[0]["studentname"].ToString();
            }
            dc["oldadmission_no"] = oldadmission_no;
            dc["newadmission_no"] = newadmission_no;
            return dc;
        }
        internal string get_marks_murge_Section(string testid)
        {
            DataTable dt = My.dataTable("Select Marks from OLINETEST_EXAM_NAME_Murge_Section where Entry_id='" + testid + "'");
            if (dt.Rows.Count == 0)
            {
                return "1";
            }
            else
            {
                return dt.Rows[0]["Marks"].ToString();
            }
        }
        internal string get_classid_from_testid(string Entry_id)
        {
            DataTable dt = FillData("Select Class_id from OLINETEST_EXAM_NAME_Murge_Section where Entry_id='" + Entry_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Class_id"].ToString();
            }
        }
        internal static string get_eg_pay_remarks(string responseCode)
        {
            try
            {
                string query1 = "select Remarks from EG_Pay_error_code  where Error_code='" + responseCode + "'   ";
                DataTable dt = My.dataTable(query1);
                if (dt.Rows.Count == 0)
                {
                    return "The user clicked the back button.";
                }
                else
                {
                    return dt.Rows[0][0].ToString();
                }

            }
            catch
            {
                return "The user clicked the back button.";

            }

        }
        internal static bool get_question_final_submit(string testid)
        {

            string query1 = "select test_id,Objective_Entry_id from question_info  where test_id='" + testid + "'  and Uploding_status='Uploaded'  ";
            DataTable dt = My.dataTable(query1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        internal static string get_test_id_from_entry_id(string Entry_id)
        {
            string query1 = "Select top 1 Exam_id from OLINETEST_EXAM_NAME where Entry_id='" + Entry_id + "' ";

            DataTable dt = FillDatastatic(query1);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public static string hostelDB = ConfigurationManager.AppSettings["HostelConn"];

        public void bind_all_ddl_with_id_otherportal(DropDownList ddl, string strQuery)
        {
            DataTable dt = dataTableHDB(strQuery, hostelDB);

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

        internal static string top_one_hostel_room_HDB(string hostel_id, string category_id)
        {
            string returnValue = "0";
            DataTable dt = dataTableHDB("select top 1   Room_id from Hostel_room_master where Hostel_id='" + hostel_id + "' and Category_id='" + category_id + "' order by Room_name asc", hostelDB);
            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["Room_id"].ToString();
            }
            return returnValue;
        }

        internal static string get_top_on_bed_HDB(string hostel_id, string room_category, string room_id, string session_id)
        {
            string returnValue = "0";
            DataTable dt = dataTableHDB("select top 1  Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + room_id + "' and Status='1' and Hostel_id=" + hostel_id + " and Category_id=" + room_category + " and Session_id='" + session_id + "' ) and Room_id='" + room_id + "' and Hostel_id='" + hostel_id + "'and Category_id='" + room_category + "' order by Id asc", hostelDB);
            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["Bed_id"].ToString();
            }
            return returnValue;
        }



        public static string Convertdate_to_pwd(string DateInString) //Format :: dd/MM/yyyy
        {
            try
            {
                DateInString = DateInString.Substring(0, 2) + DateInString.Substring(3, 2) + DateInString.Substring(6, 4);
                return DateInString;
            }
            catch
            {
                return "0";
            }
        }
        internal static string unique_id_by_user(string userid)
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
            return a + "_" + userid;
        }

        internal static void is_classTeacher_auto_assign_for_all_class(string Session_id, string teacher_id)
        {
            try
            {
                string isAutoAssign = My.get_single_column_data("select Is_class_teacher_auto_assign as Column_Name from Firm_Details");
                if (isAutoAssign == "1")
                {
                    DataTable dtc = My.dataTable("select course_id from Add_course_table order by Position asc");
                    if (dtc.Rows.Count > 0)
                    {
                        foreach (DataRow drc in dtc.Rows)
                        {
                            DataTable dts = My.dataTable("select  distinct Section from admission_registor where Session_id='" + Session_id + "' and Class_id='" + drc["course_id"].ToString() + "' and Status='1' order by Section asc");
                            if (dts.Rows.Count > 0)
                            {
                                foreach (DataRow drsec in dts.Rows)
                                {
                                    DataTable dtcheck = My.dataTable("Select Id from Ptm_class_teacher_mapping where CategoryID='" + drc["course_id"].ToString() + "' and Section='" + drsec["Section"].ToString() + "' and UserID='" + teacher_id + "' and Session_Id=" + Session_id + " ");
                                    if (dtcheck.Rows.Count == 0)
                                    {
                                        SqlCommand cmd;
                                        string query = "INSERT INTO Ptm_class_teacher_mapping (CategoryID,Section,UserID,Date,idate,time,Session_Id,Branch_id) values (@CategoryID,@Section,@UserID,@Date,@idate,@time,@Session_Id,@Branch_id)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@CategoryID", drc["course_id"].ToString());
                                        cmd.Parameters.AddWithValue("@Section", drsec["Section"].ToString());
                                        cmd.Parameters.AddWithValue("@UserID", teacher_id);
                                        cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                                        cmd.Parameters.AddWithValue("@idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                                        cmd.Parameters.AddWithValue("@time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt"));
                                        cmd.Parameters.AddWithValue("@Session_Id", Session_id);
                                        cmd.Parameters.AddWithValue("@Branch_id", "1");
                                        if (InsertUpdate.InsertUpdateData(cmd))
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



                string isAutoSubjAssign = My.get_single_column_data("select Is_subj_auto_assign_to_teacher as Column_Name from Firm_Details");
                if (isAutoSubjAssign == "1")
                {
                    DataTable dtc = My.dataTable("select course_id from Add_course_table order by Position asc");
                    if (dtc.Rows.Count > 0)
                    {
                        foreach (DataRow drc in dtc.Rows)
                        {
                            DataTable dts = My.dataTable("select  distinct Section from admission_registor where Session_id='" + Session_id + "' and Class_id='" + drc["course_id"].ToString() + "' and Status='1' order by Section asc");
                            if (dts.Rows.Count > 0)
                            {
                                foreach (DataRow drsec in dts.Rows)
                                {
                                    DataTable dtsubj = My.dataTable("select course_id,Subject_id,Subject_name,Branch_id from Subject_Master where course_id='" + drc["course_id"].ToString() + "' and Is_mandatory='1'");
                                    if (dtsubj.Rows.Count > 0)
                                    {
                                        foreach (DataRow drsubj in dtsubj.Rows)
                                        {
                                            DataTable dtcheckSubj = My.dataTable("select UserID from TeacherCourseSubjectMaping where CategoryID='" + drc["course_id"].ToString() + "' and AssignCourseID=" + drsubj["Subject_id"].ToString() + " and section='" + drsec["Section"].ToString() + "' and Session_id='" + Session_id + "' and UserID='" + teacher_id + "'");
                                            if (dtcheckSubj.Rows.Count == 0)
                                            {
                                                SqlCommand cmd;
                                                string query = "INSERT INTO TeacherCourseSubjectMaping (UserID,AssignCourseID,Istatus,Date,Idate,CategoryID,section,sync_status,Acamedic_Semester_Id,Type,Session_id,Branch_id,Created_by) values (@UserID,@AssignCourseID,@Istatus,@Date,@Idate,@CategoryID,@section,@sync_status,@Acamedic_Semester_Id,@Type,@Session_id,@Branch_id,@Created_by)";
                                                cmd = new SqlCommand(query);
                                                cmd.Parameters.AddWithValue("@UserID", teacher_id);
                                                cmd.Parameters.AddWithValue("@AssignCourseID", drsubj["Subject_id"].ToString());
                                                cmd.Parameters.AddWithValue("@Istatus", 1);
                                                cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                                                cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                                                cmd.Parameters.AddWithValue("@CategoryID", drc["course_id"].ToString());
                                                cmd.Parameters.AddWithValue("@section", drsec["Section"].ToString());
                                                cmd.Parameters.AddWithValue("@sync_status", "0");
                                                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                                                cmd.Parameters.AddWithValue("@Type", "Yearwise");
                                                cmd.Parameters.AddWithValue("@Session_id", Session_id);
                                                cmd.Parameters.AddWithValue("@Branch_id", drsubj["Branch_id"].ToString());
                                                cmd.Parameters.AddWithValue("@Created_by", "AI");
                                                if (My.InsertUpdateData(cmd))
                                                {
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        public static string create_random_no()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random();
            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }

        internal static string get_user_id_class_teacher(string session_id, string class_id, string section)
        {
            string returN = "No";
            string query = "select top 1 * from Ptm_class_teacher_mapping where Session_Id='" + session_id + "' and CategoryID='" + class_id + "' and Section='" + section + "' order by id desc  ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = dt.Rows[0]["UserID"].ToString();
                return returN;
            }
        }
        internal static string get_rendome_id()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("mmss");
            Random random = new Random(DateTime.Now.Millisecond);
            int tempo = random.Next(1, 999);
            string sl = My.auto_serial1("parentid");
            string bill = tempo.ToString() + date;
            return bill;
        }

        internal static string get_parentsid(string Student_id)
        {
            DataTable dt = find_data_static("Select  Parent_id   from Parent_Student_Mapping   where Student_id='" + Student_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Parent_id"].ToString();
            }
        }

        internal static string get_reg_apply_session_id()
        {
            DataTable dt = FillDatastatic("select top 1 session_id from session_details where Old_Use_Mode='2' order by id desc  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["session_id"].ToString();
            }
        }

        internal string get_current_month_id_position()
        {
            try
            {
                var monthid = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
                SqlCommand cmd = new SqlCommand("select Position from Month_Index where Month_Id='" + monthid + "' ");
                DataTable dt = GetData(cmd);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    return "0";
                }
                else
                {
                    return dt.Rows[0]["Position"].ToString();
                }
            }
            catch
            {
                return "0";
            }

        }

        internal static string get_top_one_test_id(string session_id)
        {
            DataTable dt = FillDatastatic("select top 1 Test_id  from Online_reg_exam_test_master where Session_id='" + session_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Test_id"].ToString();
            }
        }
        internal void bind_all_ddl_with_id_cap_NA(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("NA", "0"));

        }
        public static string lasttattime()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMddhhmmss");
        }
        public static Firm_Details getFirminfo(string firm_id = null)
        {
            var data = My.dataTable($"select * from Firm_Details").toJsonString();
            var firm = JsonConvert.DeserializeObject<List<Firm_Details>>(data);
            return firm[0];
        }
        public string onedayago()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-1).ToString("yyyMMdd");
        }
        public string twodayago()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-2).ToString("yyyMMdd");
        }
        public string threedayago()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-2).ToString("yyyMMdd");
        }
        public void bind_all_ddl_with_id_cap_All_NA(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("ALL", "00"));
            ddl.Items.Insert(1, new ListItem("NA", "0"));
        }

        public void bind_ddlall_subject(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "All Subject";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("All Subject", "0"));
        }
        internal static string Sport_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_Sport_format();
            string session = get_session();
            return pre_fix + "/" + session + "/" + bill;
        }

        internal static string GetFinancialSessionFromString(string dateString)
        {
            // Parse date from "dd/MM/yyyy" format
            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None, out date))
            {
                int year = date.Year;
                if (date.Month < 4)
                {
                    return (year - 1) + "-" + year;
                }
                else
                {
                    return year + "-" + (year + 1);
                }
            }
            else
            {
                return "0";
            }
        }

        private static string get_Sport_format()
        {
            try
            {
                DataTable dt = FillDatastatic("Select Sport_certificate_prefix from Firm_Details  ");
                if (dt.Rows.Count == 0)
                {
                    return "SPORT";
                }
                else
                {
                    return dt.Rows[0]["Sport_certificate_prefix"].ToString();
                }
            }
            catch
            {
                return "SPORT";
            }
        }

        internal void bind_all_ddl_with_id_list(ListBox ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Please tag the user first";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Please tag the user first", "0"));
        }

        internal string get_usergcmid(string user_id)
        {
            DataTable dt = FillData("Select gcm_id from user_details where user_id='" + user_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["gcm_id"].ToString();
            }
        }
        public static Dictionary<string, object> get_user_menu_permission_Onlinetest(string UserID, string menupagename)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string user_type = get_user_type(UserID);
            if (user_type == "Admin")
            {
                dc["Is_Edit"] = "1";
                dc["Is_delete"] = "1";
                dc["Is_Download"] = "1";
                dc["Is_Print"] = "1";
                dc["Is_add"] = "1";
            }
            else
            {
                string query = "select mp.*,mgl.Group_name as main_menu,mm.Menu_name as submenu, mm.Menu_page from MenuPermissionForUser_OnlineTest mp join MenuMaster_web_OnlineTest mm on mp.MenuID=mm.MenuID and mp.MainMenuId=mm.Group_id join Menu_Group_List_web_OnlineTest mgl on mm.Group_id=mgl.Group_id where mp.UserID='" + UserID + "' and mm.Menu_page='" + menupagename + "' and mm.Type=1 order by mgl.Position";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    dc["Is_Edit"] = "1";
                    dc["Is_delete"] = "1";
                    dc["Is_Download"] = "1";
                    dc["Is_Print"] = "1";
                    dc["Is_add"] = "1";
                    //dc["Is_Edit"] = "0";
                    //dc["Is_delete"] = "0";
                    //dc["Is_Download"] = "0";
                    //dc["Is_Print"] = "0";
                    //dc["Is_add"] = "0";
                }
                else
                {
                    dc["Is_Edit"] = "1";
                    dc["Is_delete"] = "1";
                    dc["Is_Download"] = "1";
                    dc["Is_Print"] = "1";
                    dc["Is_add"] = "1";
                    //dc["Is_Edit"] = dt.Rows[0]["Is_Edit"].ToString();
                    //dc["Is_delete"] = dt.Rows[0]["Is_delete"].ToString();
                    //dc["Is_Download"] = dt.Rows[0]["Is_Download"].ToString();
                    //dc["Is_Print"] = dt.Rows[0]["Is_Print"].ToString();
                    //dc["Is_add"] = dt.Rows[0]["Is_add"].ToString();
                }
            }
            return dc;
        }

        internal static bool InsertUpdateData_sp_offline(SqlCommand cmd)
        {
            using (SqlConnection scon = new SqlConnection(My.conn))
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
        public Dictionary<string, object> gettestinformation(string Test_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = FillData("Select  *,(select top 1 Subject_name from Subject_Master where Subject_id=OlineTest_Exam_name.subjectname) as subject_name from OlineTest_Exam_name   where Exam_id='" + Test_id + "' ");
            if (dt.Rows.Count == 0)
            {
                dc["Session_Id"] = "0";
                dc["Exam_Id"] = "0";
                dc["Class_Id"] = "0";
                dc["Sub_id"] = "0";
                dc["Section"] = "0";
                dc["Exam_Activity_Id"] = "0";
                dc["Subjectname"] = "";

            }
            else
            {
                dc["Session_Id"] = dt.Rows[0]["Session_id"].ToString();
                dc["Exam_Id"] = dt.Rows[0]["Exam_id"].ToString();
                dc["Class_Id"] = dt.Rows[0]["Class_id"].ToString();
                dc["Sub_id"] = dt.Rows[0]["subjectname"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                if (dt.Rows[0]["subject_name"].ToString() == "0")
                {
                    dc["Subjectname"] = "All Subject";
                }
                else if (dt.Rows[0]["subject_name"].ToString() == "")
                {
                    dc["Subjectname"] = "All Subject";
                }
                else
                {
                    dc["Subjectname"] = dt.Rows[0]["subject_name"].ToString();
                }
                dc["Exam_Activity_Id"] = "0";

            }

            return dc;

        }

        internal static bool check_question_explanation(string testid)
        {
            string query = "Select top 1 qi.test_id,Question_no,qi.questionid,Question_name,Question_name_HN,qi.Section,Explanation_en,Explanation_hn from question_info qi  join Question_Explanation qe on qi.questionid=qe.questionid where qi.test_id=" + testid + "     order by  cast (Question_no as int) ASC";//and  
            DataTable dt = new DataTable();
            dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        internal void executeQuery(string query1)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(query1);
            SqlConnection con = new SqlConnection(My.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();

        }
        public DataTable featch_data(string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            return dt;

        }
        internal string find_ip()
        {
            string ip = null;
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            Console.WriteLine(hostName);
            // Get the IP  
            ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return ip;
        }
        internal string exam_categoty_id_via_examcode(string value)
        {
            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select  Category_code from Add_Exam_Category ", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Add_Exam_Category");
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result = dr["Category_code"].ToString();

                }
            }
            else
            {
                result = "1";
            }

            return result;
        }
        public static bool get_date_valid(string test_code)
        {
            return true;
        }
        public Dictionary<string, object> view_result_parameter2(string testid, string studentcode)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            SqlDataAdapter ad = new SqlDataAdapter("Select Ip_address,icreated_date,Attempt_id from user_test_total_marks_details where Studentid='" + studentcode + "' and Test_code='" + testid + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "user_test_total_marks_details");
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                dc["Ip_address"] = "0";
                dc["icreated_date"] = "0";
                dc["Attempt_id"] = "0";
            }
            else
            {
                dc["Ip_address"] = dt.Rows[0]["Ip_address"].ToString();
                dc["icreated_date"] = dt.Rows[0]["icreated_date"].ToString();
                dc["Attempt_id"] = dt.Rows[0]["Attempt_id"].ToString();
            }
            return dc;
        }
        internal static bool find_status_takstatus(string testid, string userid)
        {
            string query1 = "select * from Tack_Test where Student_id='" + userid + "' and Test_id='" + testid + "'   ";
            DataTable dt = My.dataTable(query1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }





        }

        internal static bool get_question_sinl_submit(string testid)
        {
            string query1 = "select test_id,Objective_Entry_id from question_info  where test_id='" + testid + "'  and Uploding_status='Uploaded'  ";
            DataTable dt = My.dataTable(query1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        internal static bool get_questionstatus_upload(string testid)
        {
            string query1 = "select test_id,Objective_Entry_id from question_info  where test_id='" + testid + "'  and Uploding_status='Administrator'  ";
            DataTable dt = My.dataTable(query1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
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
        public static Boolean InsertUpdateDatatest(SqlCommand cmd, string questionname)
        {
            //try
            //{
            String strConnString = My.conn;
            SqlConnection con = new SqlConnection(strConnString);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();

            //}
            //catch (Exception ex)
            //{


            //}

            return true;


        }
        internal static DataTable find_phrases(string phrases_id)
        {
            DataTable dt = My.dataTable("select * from Phrase_details where phrases_id ='" + phrases_id + "' ");
            return dt;



        }
        internal static string find_direction_hn(string declaration_id)
        {
            DataTable dt = My.dataTable("select declaration_hn from Declaration where declaration_id='" + declaration_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }


        }
        internal static string find_direction_en(string declaration_id)
        {
            DataTable dt = My.dataTable("select declaration from Declaration where declaration_id='" + declaration_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }


        }

        internal string get_marks(string testid)
        {
            DataTable dt = My.dataTable("Select Marks from OlineTest_Exam_name where Exam_id='" + testid + "'");
            if (dt.Rows.Count == 0)
            {
                return "1";
            }
            else
            {
                return dt.Rows[0]["Marks"].ToString();
            }
        }

        internal static string find_examtype(string examtype_code1)
        {

            string result = "";
            SqlDataAdapter ad = new SqlDataAdapter("select Examtype from Add_Exam_Type where examtype_code='" + examtype_code1 + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result = dr["Examtype"].ToString();
                }
            }
            return result;

        }


        public string Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return Convert.ToBase64String(mso.ToArray());
            }
        }
        public string Unzip(string str)
        {

            byte[] bytes = Convert.FromBase64String(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {                     //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }



        internal static string Is_annul_admission_editbile()
        {
            try
            {
                DataTable dt = My.dataTable("Select Is_annul_admission_editbile from Firm_Details");
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    if (dt.Rows[0]["Is_annul_admission_editbile"].ToString().ToUpper() == "TRUE")
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch
            {
                return "0";
            }
        }

        public static DataTable MydataTable(string query, string conStr = null)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = new DataTable();
            String strConnString = My.con;
            SqlConnection con1 = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con1;
            try
            {
                con1.Open();
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
                con1.Close();
                sda.Dispose();
                con1.Dispose();
            }
        }

        public void send_data_Create_employee(string party_id, string p_name, string gender, string dob, string city, string district, string pin, string mob, string state, string fathername, string dateofadmission)
        {
            string statename = getstatename(state);
            string getstatecode = getstatename(state);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = p_name;
                dr[2] = city;
                dr[3] = district;
                dr[4] = mob;
                dr["gstin"] = "UnRegistered";
                dr[6] = party_id;
                dr[7] = date();
                dr["firm"] = My.firm_id();
                dr["Registration_Type"] = "Customer";
                dr["State_Code"] = getstatecode;
                dr["State"] = statename;

                dr["type"] = "Customer";
                dr["Care_of"] = fathername;
                dr["pan_no"] = "";
                dr["Account_No"] = "";
                dr["Bank_Name"] = "";
                dr["IFSC_Code"] = "";
                dr[12] = "0";
                dr[13] = "NA";
                dt.Rows.Add(dr);
                //My.firm_wise_auto_serial("party_id");
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.save_Account_Ledger_Details(p_name, party_id, "3");
                My.update_Ledger_Opening_Balance(p_name, party_id, "3", "Dr", "0.00", dateofadmission, My.get_session());
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = p_name;
                    dr[2] = city;
                    dr[3] = district;
                    dr[4] = mob;

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }
        public static void send_to_cash_payment_Voucher_Details(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string VoucherType, string firm, string Financial_Year, string userid, string Bill_from, string ref_name, string VoucherNo_Manual, string ImagePath)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_cash_payment_Voucher_Details");
                cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
                cmd.Parameters.AddWithValue("@Main_account_id", Main_account_name);
                cmd.Parameters.AddWithValue("@Alternet_account_id", Alternet_Account);
                cmd.Parameters.AddWithValue("@Description ", Description);
                cmd.Parameters.AddWithValue("@amount ", Amount);
                cmd.Parameters.AddWithValue("@Date ", Date);
                cmd.Parameters.AddWithValue("@IDate ", IDate);
                cmd.Parameters.AddWithValue("@unique_entry_id ", unique_entry_id);
                cmd.Parameters.AddWithValue("@VoucherNo ", VoucherNo);
                cmd.Parameters.AddWithValue("@VoucherType ", VoucherType);
                cmd.Parameters.AddWithValue("@firm ", firm);
                cmd.Parameters.AddWithValue("@Session ", Financial_Year);
                cmd.Parameters.AddWithValue("@Created_by ", userid);
                cmd.Parameters.AddWithValue("@ref_name ", ref_name);
                cmd.Parameters.AddWithValue("@Bill_from ", Bill_from);
                cmd.Parameters.AddWithValue("@VoucherNo_Manual ", VoucherNo_Manual);
                cmd.Parameters.AddWithValue("@ImagePath ", ImagePath);

                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }

        }

        internal static string get_single_column_data(string qry)
        {
            DataTable dt = dataTable(qry);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Column_Name"].ToString();
            }
        }
        internal static string get_single_columndata(string qry)
        {
            DataTable dt = dataTable(qry);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }



        internal static void create_student_ledger(string party_id, string studentname, string gender, string dob, string city, string district, string pin, string father_mob, string state, string fathername, string dateofadmission)
        {
            string getstatecode = get_state_codes(state);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = studentname;
                dr[2] = city;
                dr[3] = district;
                dr[4] = father_mob;
                dr["gstin"] = "UnRegistered";
                dr[6] = party_id;
                dr[7] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                dr["firm"] = My.firm_id();
                dr["Registration_Type"] = "Customer";
                dr["State_Code"] = getstatecode;
                dr["State"] = state;

                dr["type"] = "Customer";
                dr["Care_of"] = fathername;
                dr["pan_no"] = "";
                dr["Account_No"] = "";
                dr["Bank_Name"] = "";
                dr["IFSC_Code"] = "";
                dr[12] = "0";
                dr[13] = "NA";

                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.save_Account_Ledger_Details(studentname, party_id, "22");
                My.update_Ledger_Opening_Balance(studentname, party_id, "22", "Dr", "0.00", dateofadmission, My.get_session());
            }
        }

        public static Dictionary<string, object> get_push_credantial()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            try
            {

                string query = "Select * from Push_Credential";
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    dc["type"] = "0";
                    dc["project_id"] = "0";
                    dc["private_key_id"] = "0";
                    dc["client_email"] = "0";
                    dc["client_id"] = "0";
                    dc["private_key"] = "0";
                }
                else
                {
                    dc["type"] = dt.Rows[0]["type"].ToString();
                    dc["project_id"] = dt.Rows[0]["project_id"].ToString();
                    dc["private_key_id"] = dt.Rows[0]["private_key_id"].ToString();
                    dc["client_email"] = dt.Rows[0]["client_email"].ToString();
                    dc["client_id"] = dt.Rows[0]["client_id"].ToString();
                    dc["private_key"] = dt.Rows[0]["private_key"].ToString();
                }
            }
            catch
            {
                dc["type"] = "0";
                dc["project_id"] = "0";
                dc["private_key_id"] = "0";
                dc["client_email"] = "0";
                dc["client_id"] = "0";
                dc["private_key"] = "0";
            }

            return dc;
        }
        internal static string getprint_slip_type()
        {
            try
            {
                string query = " Select Monthly_bill_type from  Firm_Details  ";
                DataTable dt = dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    return "A4";
                }
                else
                {
                    return dt.Rows[0]["Monthly_bill_type"].ToString();
                }
            }
            catch
            {
                return "A4";
            }
        }


        internal static string get_app_purchase_onlinetran(string appbillno, string party_id)
        {
            string query = " Select razorpay_payment_id,format (Date,'dd/MM/yyyy') as date1 from  Online_Sell_billwise where Bill_No='" + appbillno + "' and user_id='" + party_id + "' ";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["razorpay_payment_id"].ToString();
            }
        }

        internal static string get_app_purchase_date(string appbillno, string party_id)
        {
            string query = " Select format (Date,'dd/MM/yyyy') as date1 from  Online_Sell_billwise where Bill_No='" + appbillno + "' and user_id='" + party_id + "' ";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["date1"].ToString();
            }
        }
        public void bind_all_ddl_with_id_cap_All_name(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("ALL", "0"));
            ddl.Items.Insert(1, new ListItem("NA", "99"));
        }
        public static void send_to_bank_payment_Voucher_Details(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string VoucherType, string firm, string Financial_Year, string userid, string Bill_from, string ref_name, string VoucherNo_Manual, string ImagePath)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("sp_bank_payment_Voucher_Details");
                cmd.Parameters.AddWithValue("@sp_status ", "INSERT");
                cmd.Parameters.AddWithValue("@Main_account_id", Main_account_name);
                cmd.Parameters.AddWithValue("@Alternet_account_id", Alternet_Account);
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
                cmd.Parameters.AddWithValue("@ref_name ", ref_name);
                cmd.Parameters.AddWithValue("@VoucherNo_Manual ", VoucherNo_Manual);
                cmd.Parameters.AddWithValue("@ImagePath ", ImagePath);

                int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                if (rowsAffected > 0)
                {
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());

            }
        }

        internal static string dob_to_i_dob(string dob)
        {
            string rvalue = "0";
            if (dob == "")
            {
            }
            else
            {
                try
                {
                    DateTime d11 = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string idate = d11.ToString("yyyyMMdd");
                    rvalue = idate;
                }
                catch
                {
                    try
                    {
                        DateTime d11 = DateTime.ParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string idate = d11.ToString("yyyyMMdd");
                        rvalue = idate;
                    }
                    catch (Exception exx)
                    {
                    }
                }
            }
            return rvalue;
        }

        internal static string get_bank_idtop1()
        {
            string query = "select top 1 bank_id from bank_details order by id asc";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["bank_id"].ToString();
            }
        }

        internal static object get_end_idate()
        {
            string year = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
            string ab = year + "0401";
            return ab;
        }
        internal static string get_account_name_Payment(string uniqueid)
        {
            string query = "   Select top 1 Account_id,(Select top 1 Account_Name from Account_Ledger_Details where Account_id=Account_Voucher_Details.Account_id order by id asc) as accountname from Account_Voucher_Details where unique_entry_id='" + uniqueid + "' and VoucherType='Payment'  order by id asc ";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "Cash";
            }
            else
            {
                return dt.Rows[0]["accountname"].ToString();
            }
        }
        internal static string get_account_name_Receipt(string uniqueid)
        {
            string query = "   Select top 1 Account_id,(Select top 1 Account_Name from Account_Ledger_Details where Account_id=Account_Voucher_Details.Account_id order by id desc) as accountname from Account_Voucher_Details where unique_entry_id='" + uniqueid + "' and VoucherType='Receipt'  order by id desc ";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "Cash";
            }
            else
            {
                return dt.Rows[0]["accountname"].ToString();
            }
        }

        internal static void send_to_account_ledger(string Main_account_name, string Alternet_Account, string Amount, string VoucherNo, string unique_entry_id, string Description, string Date, string IDate, string cash_payment, string bank_payment, string bank_account, string VoucherType, string firm, string Financial_Year, string userid, string Bill_from, string VoucherNo_Manual)
        {
            try
            {
                if (Convert.ToDouble(Amount) > 0)
                {
                    SqlCommand cmd = new SqlCommand("sp_Account_Voucher_Details");
                    cmd.Parameters.AddWithValue("@sp_status", "INSERT");
                    cmd.Parameters.AddWithValue("@Main_account_id", Main_account_name);
                    cmd.Parameters.AddWithValue("@Alternet_account_id", Alternet_Account);
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
                    cmd.Parameters.AddWithValue("@VoucherNo_Manual ", VoucherNo_Manual);
                    int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
                    if (rowsAffected > 0)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        internal static string get_bank_id(string bank_name)
        {
            string query = "select Account_id from Account_Ledger_Details where Account_Name='" + bank_name + "'";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return get_bank_idtop1();
            }
            else
            {
                return dt.Rows[0]["Account_id"].ToString();
            }
        }

        internal static bool check_vochar_no_edit(string vochar_no, string uniqueid)
        {
            string query = "Select *   from Account_Voucher_Details where VoucherNo_Manual='" + vochar_no + "' and unique_entry_id!='" + uniqueid + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;

            }
        }
        internal static bool check_vochar_no(string vochar_no)
        {
            string query = "Select *   from Account_Voucher_Details where VoucherNo_Manual='" + vochar_no + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;

            }

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

        public static string session_wise_auto_serial(string column, string Financial_Year, string firm)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
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
                    My.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_auto_serial(column, Financial_Year, firm);
                }
                else if (e.Message.ToLower().Contains("concurrency violation"))
                {
                    result = session_wise_auto_serial(column, Financial_Year, firm);
                }
                else
                {

                }
            }
            return result;
        }

        public static string session_wise_view_auto_serial(string column, string Financial_Year, string firm)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
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

                    My.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_view_auto_serial(column, Financial_Year, firm);
                }
                else
                {

                }
            }
            return result;
        }

        internal static string find_account_id_for_student(string Account_id)
        {
            string username = get_std_name(Account_id);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where   Account_id='" + Account_id + "' and firm='" + My.firm_id() + "' ", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Account_id"].ToString();
            }
            else
            {

                string qry = "INSERT INTO Account_Ledger_Details (Account_Name,Account_id,Group_id,firm) values ('" + username + "','" + Account_id + "','26','" + My.firm_id() + "');";
                My.exeSql(qry);
                return Account_id;
            }
        }
        private static string get_std_name(string user_id)
        {
            string query = "select studentname from admission_registor where admissionserialnumber='" + user_id + "'";
            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["studentname"].ToString();
            }
        }
        internal static string find_account_id_new(string accountname, string groupname, string Account_id = null)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where   Account_Name='" + accountname + "' and firm='" + My.firm_id() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Account_id"].ToString();
            }
            else
            {
                if (Account_id == null)
                    Account_id = My.firm_id() + "AC" + My.auto_serialS("Account_Id");

                string qry = "INSERT INTO Account_Ledger_Details (Account_Name,Account_id,Group_id,firm) values ('" + accountname + "','" + Account_id + "','" + groupname + "','" + My.firm_id() + "');";
                My.exeSql(qry);
                return Account_id;
            }
        }

        internal static bool check_dup_bill_no_entry(string voucherNo, string session_name)
        {
            string query = "Select *   from Account_Voucher_Details where VoucherNo='" + voucherNo + "' and Session='" + session_name + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool check_dup_bill_no_entrysql(string voucherNo, string session_name, SqlConnection con)
        {
            string query = "Select *   from Account_Voucher_Details where VoucherNo='" + voucherNo + "' and Session='" + session_name + "'";
            DataTable dt = payments.dataTable(query, con);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static string find_account_id(string accountname)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where   Account_Name='" + accountname + "' and firm='" + My.firm_id() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Account_id"].ToString();
            }
            else
            {
                string Account_id = My.firm_id() + "AC" + My.auto_serialS("Account_Id");
                string qry = "INSERT INTO Account_Ledger_Details (Account_Name,Account_id,Group_id,firm) values ('" + accountname + "','" + Account_id + "','15','" + My.firm_id() + "');";
                My.exeSql(qry);
                return Account_id;
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

        internal static object getprint_slip_permission()
        {
            try
            {
                DataTable dt = My.dataTable("Select Is_perint_admission_slip from Firm_Details");
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    if (dt.Rows[0][0].ToString().ToUpper() == "TRUE")
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch
            {
                return "0";
            }

        }

        internal string get_teachername(string teacherid)
        {
            SqlCommand cmd5 = new SqlCommand("Select  name from user_details where user_id='" + teacherid + "' ");
            DataTable dt5 = GetData(cmd5);
            if (dt5.Rows.Count == 0)
            {
                return "";

            }
            else
            {
                return dt5.Rows[0]["name"].ToString();
            }
        }
        internal static string get_subjectname_via_subjectid(string subject_id, string Class_id)
        {
            string query = "Select Subject_name from Subject_Master where Subject_id='" + subject_id + "' and course_id=" + Class_id + "";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Subject_name"].ToString();
            }
        }
        public static string getdate3()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static string getstudent_subjectname(string Admission_no, string Section, string Session_id, string Class_id)
        {
            string CategoryID = "0";
            string qury = "Select  Sub_id from Subject_Mapping_New where Admission_no='" + Admission_no + "' and Section='" + Section + "' and Session_id='" + Session + "' and Class_id='" + Class_id + "'";
            DataTable dt = FillDatastatic(qury);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CategoryID1 = dt.Rows[i]["Sub_id"].ToString();
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


        public static string get_state_code()
        {
            try
            {
                DataTable dt = My.dataTable("Select State_code from Firm_Details");
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    if (dt.Rows[0]["State_code"].ToString() == "")
                    {
                        return "0";
                    }
                    else
                    {
                        return dt.Rows[0]["State_code"].ToString();
                    }
                }
            }
            catch
            {
                return "0";
            }
        }
        public static Dictionary<string, object> get_pushsenderid()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Comapny_Profile  ";
            DataTable dt = FillDatastatic(query);
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

        internal static void onlypush(string deviceId, Dictionary<String, String> data)
        {
            string sResponseFromServer = "";
            try
            {
                string ttype = data["type"];
                string project_id = data["project_id"];
                string private_key_id = data["private_key_id"];
                string client_email = data["client_email"];
                string client_id = data["client_id"];
                string private_key = data["private_key"];
                string notification_id = data["notification_id"];
                string message = data["message"];
                string title = data["title"];
                string messagetype = data["messagetype"];
                string UserId = data["UserId"];
                var jsonn = new
                {
                    type = ttype,
                    project_id = project_id,
                    private_key_id = private_key_id,
                    private_key = private_key,
                    client_email = client_email,
                    client_id = client_id,
                    token_uri = "https://oauth2.googleapis.com/token"
                };

                var ddata2 = new
                {
                    message = new
                    {
                        token = deviceId,
                        notification = new
                        {
                            body = message,
                            title = title,
                            image = "" // Optional, remove if not required
                        },
                        data = new
                        {
                            notification_id = notification_id,
                            message = message,
                            title = title,
                            url = "",
                            link_url = "",
                            messagetype = messagetype,
                            User_Id = UserId
                        }
                    }
                };
                var accessToken = GoogleCredential.FromJson(JsonConvert.SerializeObject(jsonn))
               .CreateScoped("https://www.googleapis.com/auth/firebase.messaging").UnderlyingCredential.GetAccessTokenForRequestAsync().GetAwaiter().GetResult();
                WebRequest tRequest = WebRequest.Create($"https://fcm.googleapis.com/v1/projects/{project_id}/messages:send");
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: Bearer {accessToken}");
                var json2 = new JavaScriptSerializer().Serialize(ddata2);
                byte[] byteArray = Encoding.UTF8.GetBytes(json2);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                sResponseFromServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch (Exception ex)
            {
                sResponseFromServer = ex.ToString()
;
            }
            String date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd-MM-yyyy");
            String time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
            String IDate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            SqlCommand cmd = new SqlCommand(" INSERT INTO PushNotification_Details (notification_id,message,title,messagetype,User_Id,Sender_Id,Idate,Date,Time,ResponseFromServer,Session_id) values (@notification_id,@message,@title,@messagetype,@User_Id,@Sender_Id,@Idate,@Date,@Time,@ResponseFromServer,@Session_id)");
            cmd.Parameters.AddWithValue("@notification_id", data["notification_id"]);
            cmd.Parameters.AddWithValue("@message", data["message"]);
            cmd.Parameters.AddWithValue("@title", data["title"]);
            cmd.Parameters.AddWithValue("@messagetype", data["messagetype"]);
            cmd.Parameters.AddWithValue("@User_Id", data["UserId"]);
            cmd.Parameters.AddWithValue("@Sender_Id", "");
            cmd.Parameters.AddWithValue("@Idate", IDate);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@ResponseFromServer", sResponseFromServer);
            cmd.Parameters.AddWithValue("@Session_id", My.get_session_id());
            if (My.InsertUpdateData(cmd))
            {
            }
        }

        public static string DataTableToCsv(DataTable dataTable)
        {
            var csvBuilder = new StringBuilder();

            // Add the header line
            foreach (DataColumn column in dataTable.Columns)
            {
                csvBuilder.Append(EscapeCsvField(column.ColumnName) + ",");
            }
            csvBuilder.Length--; // Remove the trailing comma
            csvBuilder.AppendLine();

            // Add the data rows
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    csvBuilder.Append(EscapeCsvField(item.ToString()) + ",");
                }
                csvBuilder.Length--; // Remove the trailing comma
                csvBuilder.AppendLine();
            }

            return csvBuilder.ToString();
        }

        private static string EscapeCsvField(string field)
        {
            if (field.Contains("\""))
            {
                field = field.Replace("\"", "\"\"");
            }
            if (field.Contains(",") || field.Contains("\n") || field.Contains("\r"))
            {
                field = "\"" + field + "\"";
            }
            return field;
        }
        public static void student_subject_mapping(string sessionid, string session, string class_id, string admission_no, string section, string Branch_id)
        {
            string qury = "select * from dbo.[Subject_Master] where course_id='" + class_id + "' and Is_mandatory=1 and Branch_id='" + Branch_id + "'";
            DataTable dt = FillDatastatic(qury);
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
                        DataTable dt1 = FillDatastatic(query);
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

        public Dictionary<string, object> App_Setting_get_paisa()
        {

            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from App_Setting    ";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["Sabpaisa_Client_Code"] = "";
                dc["Sabpaisa_Username"] = "0";
                dc["Sabpaisa_Password"] = "0";
                dc["Sabpaisa_Authentication"] = "0";
                dc["Sabpaisa_AuthenticationIV"] = "0";
            }
            else
            {
                dc["Sabpaisa_Client_Code"] = dt.Rows[0]["Sabpaisa_Client_Code"].ToString();
                dc["Sabpaisa_Username"] = dt.Rows[0]["Sabpaisa_Username"].ToString();
                dc["Sabpaisa_Password"] = dt.Rows[0]["Sabpaisa_Password"].ToString();
                dc["Sabpaisa_Authentication"] = dt.Rows[0]["Sabpaisa_Authentication"].ToString();
                dc["Sabpaisa_AuthenticationIV"] = dt.Rows[0]["Sabpaisa_AuthenticationIV"].ToString();
            }
            return dc;

        }
        internal static object get_general_expenses()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select  Is_general_expenses_verified from dbo.[Firm_Details] ");
                DataTable dt = GetDataq(cmd);
                if (dt.Rows.Count == 0)
                {
                    return 0;
                }
                else
                {
                    if (dt.Rows[0]["Is_general_expenses_verified"].ToString() == "True")
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
        public string dayback(int noofday)
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-noofday).ToString("dd/MM/yyyy");
        }

        public static Dictionary<string, object> get_student_info(string admission_No, string Session)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select   gcm_id,Session_id,studentname,session,Section,rollnumber,city,(select top 1 Course_Name from Add_course_table  where course_id=admission_registor.Class_id) as classname,email_id,mobilenumber,careof,Father_whatsApp_no,father_mob from admission_registor   where admissionserialnumber='" + admission_No + "' and session='" + Session + "'";
            SqlDataAdapter ad = new SqlDataAdapter(quiry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "UserRegistrationMaster");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                // go_to_Incubators_user(mobileno); 
                dc["studentname"] = "0";
                dc["Admission_no"] = "0";
                dc["Session"] = "0";
                dc["classname"] = "0";
                dc["rollnumber"] = "0";
                dc["Section"] = "0";
                dc["Address"] = "0";
                dc["email_id"] = "0";
                dc["Mobileno"] = "0";
                dc["Session_id"] = "0";
                dc["careof"] = "0";
                dc["Father_whatsApp_no"] = "0";
                dc["father_mob"] = "0";
                dc["gcm_id"] = "0";
            }
            else
            {
                dc["studentname"] = dt.Rows[0]["studentname"].ToString();
                dc["Admission_no"] = admission_No;
                dc["Session"] = dt.Rows[0]["session"].ToString();
                dc["classname"] = dt.Rows[0]["classname"].ToString();
                dc["rollnumber"] = dt.Rows[0]["rollnumber"].ToString();
                dc["Section"] = dt.Rows[0]["Section"].ToString();
                dc["Address"] = dt.Rows[0]["city"].ToString();
                dc["email_id"] = dt.Rows[0]["email_id"].ToString();
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                dc["Mobileno"] = dt.Rows[0]["mobilenumber"].ToString();
                dc["careof"] = dt.Rows[0]["careof"].ToString();
                dc["Father_whatsApp_no"] = dt.Rows[0]["Father_whatsApp_no"].ToString();
                dc["father_mob"] = dt.Rows[0]["father_mob"].ToString();
                dc["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
            }
            return dc;
        }


        public static Dictionary<string, object> get_student_info_by_adm(string admission_No)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select top 1 Session_id,Class_id from admission_registor where admissionserialnumber='" + admission_No + "' and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad = new SqlDataAdapter(quiry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "UserRegistrationMaster");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                // go_to_Incubators_user(mobileno); 
                dc["Session_id"] = "0";
                dc["Class_id"] = "0"; 
            }
            else
            {
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString(); 
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString(); 
            }
            return dc;
        }

        public Dictionary<string, object> get_auto_message_template(string messagetype)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select *,(select top 1 tny_app_link_android from Update_details) tny_app_link_android,(select top 1 tny_app_link_ios from Update_details) tny_app_link_ios,(select top 1 firm_name from Firm_Details) firm_name from SMS_Template_Setting where Send_From='" + messagetype + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
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
                dc["Wid"] = "0";
            }
            else
            {
                dc["Wid"] = dt.Rows[0]["Wid"].ToString();
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

        internal static string get_whatsapp(string Session_id, string admission_no)
        {
            string query = "select Father_whatsApp_no from dbo.[admission_registor] where admissionserialnumber='" + admission_no + "' and Session_id='" + Session_id + "' ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Father_whatsApp_no"].ToString();
            }
        }

        public static Dictionary<string, object> get_user_info(string user_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "select   * from dbo.[user_details]     where  user_id='" + user_id + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetDataq(cmd);
            if (dt.Rows.Count == 0)
            {
                dc["name"] = "0";
                dc["mobile"] = "0";
                dc["gcm_id"] = "0";
            }
            else
            {
                dc["name"] = dt.Rows[0]["name"].ToString();
                dc["mobile"] = dt.Rows[0]["mobile"].ToString();
                dc["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                dc["user_id"] = dt.Rows[0]["user_id"].ToString();
            }
            return dc;
        }


        public Dictionary<string, object> get_assined_hostel_info_adm(string admissionserialnumber)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "select *,(select top 1 Room_name from Hostel_room_master where Room_id=Hostel_assign_master.Room_id  and Hostel_id=Hostel_assign_master.Hostel_id  ) as Room_name,(select  top 1 Bed_name from Hostel_room_bed_master where Room_id=Hostel_assign_master.Room_id and Bed_id= Hostel_assign_master.Bed_id and Hostel_id=Hostel_assign_master.Hostel_id) as Bed_name from Hostel_assign_master where Admission_no='" + admissionserialnumber + "' and Session_id='" + get_session_id() + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["Hostel_id"] = "0";
                dc["Category_id"] = "0";
                dc["Room_id"] = "0";
                dc["Bed_id"] = "0";
                dc["Hostel_assign_id"] = "0";
                dc["Room_name"] = "0";
                dc["Bed_name"] = "0";
            }
            else
            {
                dc["Hostel_id"] = dt.Rows[0]["Hostel_id"].ToString();
                dc["Category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["Room_id"] = dt.Rows[0]["Room_id"].ToString();
                dc["Bed_id"] = dt.Rows[0]["Bed_id"].ToString();
                dc["Hostel_assign_id"] = dt.Rows[0]["Hostel_assign_id"].ToString();
                dc["Room_name"] = dt.Rows[0]["Room_name"].ToString();
                dc["Bed_name"] = dt.Rows[0]["Bed_name"].ToString();
            }
            return dc;
        }

        public Dictionary<string, object> getassinedhostel_info_adm(string admissionserialnumber, string session_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "select *,(Select top 1 Hostel_name from HOSTELS_MASTER where Hostel_id=Hostel_assign_master.Hostel_id) as hostelname,(select top 1 Room_name from Hostel_room_master where Room_id=Hostel_assign_master.Room_id  and Hostel_id=Hostel_assign_master.Hostel_id  ) as Room_name,(select  top 1 Bed_name from Hostel_room_bed_master where Room_id=Hostel_assign_master.Room_id and Bed_id= Hostel_assign_master.Bed_id and Hostel_id=Hostel_assign_master.Hostel_id) as Bed_name from Hostel_assign_master where Admission_no='" + admissionserialnumber + "' and Session_id='" + session_id + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["Hostel_id"] = "0";
                dc["Category_id"] = "0";
                dc["Room_id"] = "0";
                dc["Bed_id"] = "0";
                dc["Hostel_assign_id"] = "0";
                dc["Room_name"] = "0";
                dc["Bed_name"] = "0";
            }
            else
            {
                dc["Hostel_id"] = dt.Rows[0]["Hostel_id"].ToString();
                dc["Category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["Room_id"] = dt.Rows[0]["Room_id"].ToString();
                dc["Bed_id"] = dt.Rows[0]["Bed_id"].ToString();
                dc["Hostel_assign_id"] = dt.Rows[0]["Hostel_assign_id"].ToString();
                dc["Room_name"] = dt.Rows[0]["Room_name"].ToString();
                dc["Bed_name"] = dt.Rows[0]["Bed_name"].ToString();

                dc["hostelname"] = dt.Rows[0]["hostelname"].ToString();

            }
            return dc;
        }

        internal static void send_data_Create_ledger_for_student(string party_id, string studentname, string gender, string dob, string city, string district, string pin, string father_mob, string state, string fathername, string dateofadmission)
        {
            My mycode = new My();
            string statename = mycode.getstatename(state);
            string getstatecode = mycode.getstatename(state);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = studentname;
                dr[2] = city;
                dr[3] = district;
                dr[4] = father_mob;
                dr["gstin"] = "UnRegistered";
                dr[6] = party_id;
                dr[7] = mycode.date();
                dr["firm"] = My.firm_id();
                dr["Registration_Type"] = "Customer";
                dr["State_Code"] = getstatecode;
                dr["State"] = statename;
                dr["type"] = "Customer";
                dr["Care_of"] = fathername;
                dr["pan_no"] = "";
                dr["Account_No"] = "";
                dr["Bank_Name"] = "";
                dr["IFSC_Code"] = "";
                dr[12] = "0";
                dr[13] = "NA";
                dt.Rows.Add(dr);
                //My.firm_wise_auto_serial("party_id");
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.save_Account_Ledger_Details(studentname, party_id, "26");
                My.update_Ledger_Opening_Balance(studentname, party_id, "26", "Dr", "0.00", dateofadmission, My.get_session());
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = studentname;
                    dr[2] = city;
                    dr[3] = district;
                    dr[4] = father_mob;

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }

        public static Dictionary<string, object> get_parent_info(string Student_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "select   pd.* from dbo.[Parent_Login_Details] pd join Parent_Student_Mapping psm on pd.User_id=psm.Parent_id where psm.Student_id='" + Student_id + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            DataTable dt = GetDataq(cmd);
            if (dt.Rows.Count == 0)
            {
                dc["Name"] = "0";
                dc["User_id"] = "0";
                dc["Mobile"] = "0";
                dc["gcm_id"] = "0";
            }
            else
            {
                dc["Name"] = dt.Rows[0]["Name"].ToString();
                dc["User_id"] = dt.Rows[0]["User_id"].ToString();
                dc["Mobile"] = dt.Rows[0]["Mobile"].ToString();
                dc["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
            }
            return dc;
        }


        internal static string get_hostel_outpass_request(string Hosteloutpass)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int tempo = random.Next(10000, 99999);
            string bill = My.auto_serial1(Hosteloutpass);
            string pre_fix = "OutPass";
            return pre_fix + "/" + tempo + bill;
        }


        internal static string get_emp_code_from_Employee_id(string emp_id)
        {

            SqlCommand cmd = new SqlCommand("select Emp_Code from HR_Employee_Master where  Employee_id='" + emp_id + "'");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Emp_Code"].ToString();
            }
        }
        internal static object get_session_from_student(string admission_no)
        {
            string query = " select top 1  Session_id from admission_registor where admissionserialnumber='" + admission_no + "' order by id desc  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static string get_state_name()
        {
            string state = "0";
            try
            {
                DataTable dt = FillDatastatic("Select School_state from Firm_Details");
                if (dt.Rows.Count > 0)
                {
                    state = dt.Rows[0]["School_state"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return state;
        }
        internal static object getstock_item_unique_entry_id(string item_Code, string unit_id, string rate)
        {
            string query = "Select stock_item_unique_entry_id from HMS_Inventory_stock_details where Item_Code='" + item_Code + "' and unit_id='" + unit_id + "' and cast(Sale_rate as float)=" + rate + " ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["stock_item_unique_entry_id"].ToString();
            }
        }

        internal static string with_excel_name(string fine_name)
        {
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd-MM-yyyy");
            string time = dt.ToString("hhmmss");
            String filerename = fine_name + "-" + date + "-" + time;
            return filerename;
        }

        internal static string get_class_id_from_student_id(string regid, string sessionid)
        {
            string query = "Select Class_id from admission_registor where Session_id='" + sessionid + "' and admissionserialnumber='" + regid + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Class_id"].ToString();
            }

        }
        internal static object get_transport_id(string admissionnumber, string sessionid)
        {
            SqlDataAdapter ad = new SqlDataAdapter("Select transport_id from Student_mapping_with_TransportPath where Admission_no='" + admissionnumber + "' and Session_id='" + sessionid + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Setting_Data");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["transport_id"].ToString();
            }
        }

        internal static string top_one_hostel_id()
        {
            string returnValue = "0";
            DataTable dt = My.dataTable("select DISTINCT Hostel_name,Hostel_id from (select t1.Hostel_name,t1.Hostel_id,t2.Category_id,t2.Room_id,t2.Bed_id from Hostels_master t1 join HOSTEL_ROOM_BED_MASTER t2 on t1.Hostel_id=t2.Hostel_id where t2.Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id=t2.Room_id and Status='1' and Hostel_id=t2.Hostel_id and Category_id=t2.Category_id)) t order by Hostel_name asc");
            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["Hostel_id"].ToString();
            }
            return returnValue;
        }
        internal static string top_one_hostel_room(string hostel_id, string category_id)
        {
            string returnValue = "0";
            DataTable dt = My.dataTable("select top 1   Room_id from Hostel_room_master where Hostel_id='" + hostel_id + "' and Category_id='" + category_id + "' order by Room_name asc");
            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["Room_id"].ToString();
            }
            return returnValue;
        }
        internal static string get_top_on_bed(string hostel_id, string room_category, string room_id, string session_id)
        {
            string returnValue = "0";
            DataTable dt = My.dataTable("select top 1  Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + room_id + "' and Status='1' and Hostel_id=" + hostel_id + " and Category_id=" + room_category + " and Session_id='" + session_id + "' ) and Room_id='" + room_id + "' and Hostel_id='" + hostel_id + "'and Category_id='" + room_category + "' order by Id asc");
            if (dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["Bed_id"].ToString();
            }
            return returnValue;
        }

        internal static string get_class_name_to_from_class_id(string class_id)
        {
            string query = "Select Course_Name from Add_course_table where course_id='" + class_id + "' ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Course_Name"].ToString();
            }

        }
        internal static bool get_fee_added(string parameter, string sessionid)
        {
            string query1 = " select top 1 * from dbo.[Fee_master_content_wise]  where session_id='" + sessionid + "' and  Parameter ='" + parameter + "' ";
            DataTable dt = FillDatastatic(query1);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        internal static string get_student_old_new(string admission_no, string session_id, string session)
        {
            DataTable cdt = My.dataTable(" select top 1 Transfer_Status from admission_registor  where admissionserialnumber='" + admission_no + "' and Session_id='" + session_id + "' order by id desc ");

            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return "New";
            }
            else
            {
                return cdt.Rows[0]["Transfer_Status"].ToString();
            }

        }
        public void bind_all_ddl_with_id_na(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "NA";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("NA", "0"));
        }
        internal static string get_Employee_id_from_Employe_code(string empcode)
        {
            DataTable dt = FillDatastatic("Select Employee_id from HR_Employee_Master where Emp_Code='" + empcode + "'");
            if (dt.Rows.Count == 0)
            {
                return empcode;
            }
            else
            {
                return dt.Rows[0]["Employee_id"].ToString();
            }
        }
        internal static string get_Section_via_class(string classid)
        {
            SqlCommand cmd = new SqlCommand("select  Section from admission_registor where Class_id='" + classid + "' ");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "A";
            }
            else
            {
                return dt.Rows[0]["Section"].ToString();
            }
        }
        public DateTime getdate2(string date)
        {
            string mergeStartTime = date + " " + time();
            DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            return startTime;
        }


        internal static string get_top_one_Scholarship_name(string selectedValue)
        {
            DataTable dt = FillDatastatic("Select top 1 Test_id from Scholarship_Program where  Session_id='" + selectedValue + "' and Is_active=1 ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Test_id"].ToString();
            }
        }
        internal static int get_no_of_fill_from_seat_Scholarship(string Session_id, string Class_id, string Test_id)
        {
            DataTable dt = FillDatastatic("Select no_application from Scholarship_Parameter_fees where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Test_id='" + Test_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }

        internal static int get_no_seat_current_session_Scholarship(string sessionid, string classid, string Test_id)
        {
            int startingdate = getstarting_data_Scholarship(sessionid, classid, Test_id);
            DataTable dt = FillDatastatic("Select count(Id) from Scholarship_Parameter_fees where Session_id=" + sessionid + " and Class_id=" + classid + " and Payment_Status='Paid'  and idate>=" + startingdate + " and Test_id='" + Test_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }
        private static int getstarting_data_Scholarship(string sessionid, string classid, string test_id)
        {
            DataTable dt = FillDatastatic("Select start_Idate from Scholarship_Parameter_fees where Session_id=" + sessionid + " and Class_id=" + classid + " and Test_id='" + test_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["start_Idate"].ToString());
            }
        }
        internal static bool find_fee_collected_hostel(string hostelfeetype, string session, string class_id)
        {
            DataTable dt = FillDatastatic("Select top 1 * from Monthly_Fee_Collection_Slip where session='" + session + "' and parameter='" + hostelfeetype + "' and class='" + class_id + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        internal static bool check_row_no_avilavel_seat(string Transportation_Id, string TransportationPath_id, string sheet_position, string seat_modle)
        {
            DataTable cdt = My.dataTable(" select count(Id) from dbo.[Transport_Path_Mapping_With_Sheet] where TransportationPath_id=" + TransportationPath_id + " and Transportation_Id=" + Transportation_Id + " ");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {
                int count = My.toInt(cdt.Rows[0][0].ToString());
                if (count == Convert.ToInt32(seat_modle))
                {
                    return false;
                }
                else if (count >= Convert.ToInt32(seat_modle))
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        internal static int get_last_seat_no(string transportid, string transporpath)
        {
            string query1 = " select   top 1 Sheet_No from dbo.[Transport_Path_Mapping_With_Sheet]  where Transportation_Id='" + transportid + "' and  TransportationPath_id ='" + transporpath + "' order by id desc ";
            DataTable dt = FillDatastatic(query1);
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
        }

        //internal static string get_state_name()
        //{
        //    string state = "0";
        //    try
        //    {
        //        DataTable dt = FillDatastatic("Select School_state from Firm_Details");
        //        if (dt.Rows.Count > 0)
        //        {
        //            state = dt.Rows[0]["School_state"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return state;
        //}

        public bool checkvaliddate(string DateInString) //Format :: dd/MM/yyyy
        {
            try
            {
                DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void bind_ddl_all_Cap(DropDownList ddl, string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            ArrayList ar = new ArrayList();
            ar.Add("ALL");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
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


        internal static string getgcmid(string admissionid)
        {
            string query = "Select top 1 gcm_id from admission_registor where admissionserialnumber='" + admissionid + "' order by id desc ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["gcm_id"].ToString();
            }
        }
        internal static string get_house_name(string housname)
        {
            DataTable dt = dataTable("Select * from house_master where house_name='" + housname + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["house_id"].ToString();
            }
        }

        internal static string get_top_one_sms_data(string type)
        {
            DataTable dt = FillDatastatic("Select * from SMS_Template_Setting where  Send_From='" + type + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Id"].ToString();
            }
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

        public static string Whatsapp_api_url = "";
        public static string whatsapp_mobile_no = "";
        internal static double Get_tranport_fine_amount()
        {
            try
            {
                DataTable dt = FillDatastatic("Select * from Transport_Fine_Apply_Setting where  Session_id='" + My.get_session_id() + "' and Is_Transport_fine_apply='YES' ");
                if (dt.Rows.Count == 0)
                {
                    return 1;
                }
                else
                {
                    return My.toDouble(dt.Rows[0]["Multiply"].ToString());
                }
            }
            catch
            {
                return 1;
            }

        }
        internal static bool check_fine_add()
        {
            DataTable dt = FillDatastatic("Select * from Fine_master where  Session_id='" + My.get_session_id() + "' ");
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string Bind_data_if_add(string Admission_no, string Session_id, string Month)
        {
            DataTable dt = FillDatastatic("Select * from Split_Month_Fee_Student where Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "' and Month='" + Month + "'");
            if (dt.Rows.Count == 0)
            {
                return "0.00";
            }
            else
            {
                return dt.Rows[0]["Amount"].ToString();

            }
        }

        internal static string tempOTP()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("mmhh");
        }

        internal static void insert_data_split_month(string Month, string
              fee, string feetype, string slip_no, string Admission_no, string Session_id)
        {
            string strQuery = "";
            double fee_amt = My.toDouble(fee);
            string Month_id = My.get_month_id_from_month_name(Month);
            DataTable dt = FillDatastatic("Select * from Split_Month_Fee_Student where Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "' and Month='" + Month + "'");
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
                if (My.InsertUpdateData(cmd))
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
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        internal static bool check_assigned_boarding_point(string session_id, string Transportation_Id, string TransportationPath_id, string Boarding_Point_id)
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Student_mapping_with_TransportPath] where Session_id='" + session_id + "' and transport_id=" + Transportation_Id + " and TransportPath_id=" + TransportationPath_id + " and Boarding_Point_id=" + Boarding_Point_id + " ");

            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        internal static bool get_fee_added_boarding(string selectedValue)
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Transportation_Boarding_Point] where  Session_Id='" + selectedValue + "' ");

            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        public static string get_session_id_form_sale()
        {
            string session_id = "0";
            try
            {
                DataTable dt = FillDatastatic("Select Session from session_details where Form_sale_active='1'");
                if (dt.Rows.Count > 0)
                {
                    session_id = dt.Rows[0]["Session"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return session_id;
        }
        internal string get_current_month_year_id()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMM");
        }
        internal string get_fee_heading_admission_annual_month_bus(string Class_Id, string session_id, string branchid, string type, string schooltype)
        {
            string studenttype = "0";
            string hosteltaken = "";
            if (schooltype == "Days")
            {
                hosteltaken = "No";
                if (type == "Admission")
                {
                    type = "AdmissionFee";
                    studenttype = "New";

                }
                else if (type == "Annual")
                {
                    type = "AnnualFee";
                    studenttype = "NT";
                }
                else if (type == "Monthly")
                {
                    type = "MonthlyFee";
                }

            }
            else
            {
                hosteltaken = "Yes";
                if (type == "Admission")
                {
                    type = "HostelAdmissionFee";
                    studenttype = "New";
                }
                else if (type == "Annual")
                {
                    type = "HostelAnnualFee";
                    studenttype = "NT";
                }
                else if (type == "Monthly")
                {
                    type = "HostelMonthlyFee";
                }
            }
            string querymain = "";
            string query = "";
            if (type != "Bus Fees")
            {
                if (Class_Id == "0")
                {
                    querymain = "Select  studentname as Student_Name,admissionserialnumber as Admission_No,rollnumber as Roll_No  ";
                    query = " Select  distinct  fmc.content as parameter_name,fmc.content_id as parameter_id   from   Fee_master_content_wise fmc  where  fmc.session_id=" + session_id + " and fmc.parameter='" + type + "'     ORDER BY fmc.content";
                }
                else
                {
                    querymain = "Select  studentname as Student_Name,admissionserialnumber as Admission_No,rollnumber as Roll_No  ";
                    query = " Select  distinct fmc.content as parameter_name,fmc.content_id as parameter_id   from   Fee_master_content_wise fmc  where  fmc.class_id=" + Class_Id + " and fmc.session_id=" + session_id + " and fmc.parameter='" + type + "'     ORDER BY fmc.content";
                }
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = GetData(cmd);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        querymain += ", ' ' as " + dr["parameter_name"].ToString().Replace(' ', '_');
                    }
                }
                if (Class_Id == "0")
                {
                    if (studenttype == "0")
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "'   and Status='1' and  hosteltaken='" + hosteltaken + "'   order by rollnumber asc";
                    }
                    else
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "'   and Status='1' and  hosteltaken='" + hosteltaken + "' and Transfer_Status='" + studenttype + "'  order by rollnumber asc";
                    }

                }
                else
                {
                    if (studenttype == "0")
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "' and Class_id=" + Class_Id + " and Status='1' and  hosteltaken='" + hosteltaken + "'   order by rollnumber asc";
                    }
                    else
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "' and Class_id=" + Class_Id + " and Status='1' and  hosteltaken='" + hosteltaken + "' and Transfer_Status='" + studenttype + "'   order by rollnumber asc";
                    }
                }
            }
            else
            {
            }
            return querymain;
        }

        internal static string isChequePending(string session_id, string admission_no, string checque_date)
        {
            string returN = "0"; string Is_check_payment_verify = "0";
            try
            {
                DataTable dtFirm = My.dataTable("select Is_check_payment_verify from Firm_Details");
                if (dtFirm.Rows.Count > 0)
                {
                    if (dtFirm.Rows[0]["Is_check_payment_verify"].ToString() == "True")
                    {
                        Is_check_payment_verify = "1";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (Is_check_payment_verify == "1")
                {
                    DataTable dt = My.dataTable("select top 1 * from Fee_payment_by_cheque_status where  Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Status='PENDING' order by id desc");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (My.DateConvertToIdate(dr["Cheque_date"].ToString()) != My.DateConvertToIdate(checque_date))
                            {
                                returN = "1";
                            }
                        }
                        return returN;
                    }
                    else
                    {
                        return returN;
                    }
                }
                else
                {
                    return returN;
                }
            }
            catch (Exception ex)
            {
                return returN;
            }
        }

        internal string get_fee_heading_admission_annual_month_top1_blanck(string Class_Id, string session_id, string branchid, string type, string schooltype)
        {
            string hosteltaken = "";
            if (schooltype == "Days")
            {
                hosteltaken = "No";
                if (type == "Admission")
                {
                    type = "AdmissionFee";

                }
                else if (type == "Annual")
                {
                    type = "AnnualFee";
                }
                else if (type == "Monthly")
                {
                    type = "MonthlyFee";
                }

            }
            else
            {
                hosteltaken = "Yes";
                if (type == "Admission")
                {
                    type = "HostelAdmissionFee";

                }
                else if (type == "Annual")
                {
                    type = "HostelAnnualFee";
                }
                else if (type == "Monthly")
                {
                    type = "HostelMonthlyFee";
                }

            }
            string query = "";
            string querymain = "";
            if (type != "Bus Fees")
            {
                if (Class_Id == "0")
                {
                    querymain = "Select  top 1 'Student_Name' as Student_Name,'Admission_No' as Admission_No,'Roll_No' as Roll_No  ";
                    query = " Select  distinct fmc.content as parameter_name,fmc.content_id as parameter_id   from   Fee_master_content_wise fmc  where    fmc.session_id=" + session_id + " and fmc.parameter='" + type + "'   ORDER BY fmc.content";
                }
                else
                {
                    querymain = "Select  top 1 'Student_Name' as Student_Name,'Admission_No' as Admission_No,'Roll_No' as Roll_No  ";
                    query = " Select  distinct fmc.content as parameter_name,fmc.content_id as parameter_id   from   Fee_master_content_wise fmc  where  fmc.class_id=" + Class_Id + " and fmc.session_id=" + session_id + " and fmc.parameter='" + type + "'   ORDER BY fmc.content";
                }
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = GetData(cmd);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        querymain += ", '" + dr["parameter_name"].ToString().Replace(' ', '_') + "' as " + dr["parameter_name"].ToString().Replace(' ', '_');
                    }
                }
                if (Class_Id == "0")
                {
                    querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "'   and Status='1' and  hosteltaken='" + hosteltaken + "'  order by rollnumber asc";
                }
                else
                {
                    querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "' and Class_id=" + Class_Id + " and Status='1' and  hosteltaken='" + hosteltaken + "'  order by rollnumber asc";
                }
            }
            else
            {
            }
            return querymain;
        }




        public static string get_parameterid(string schooltype, string type)
        {

            if (schooltype == "Days")
            {

                if (type == "Admission")
                {
                    type = "1";

                }
                else if (type == "Annual")
                {
                    type = "2";
                }
                else if (type == "Monthly")
                {
                    type = "4";
                }

            }
            else
            {

                if (type == "Admission")
                {
                    type = "5";

                }
                else if (type == "Annual")
                {
                    type = "6";
                }
                else if (type == "Monthly")
                {
                    type = "3";
                }

            }
            return type;
        }
        public static string get_parametername(string schooltype, string type)
        {

            if (schooltype == "Days")
            {

                if (type == "Admission")
                {
                    type = "AdmissionFee";

                }
                else if (type == "Annual")
                {
                    type = "AnnualFee";
                }
                else if (type == "Monthly")
                {
                    type = "MonthlyFee";
                }

            }
            else
            {

                if (type == "Admission")
                {
                    type = "HostelAdmissionFee";

                }
                else if (type == "Annual")
                {
                    type = "HostelAnnualFee";
                }
                else if (type == "Monthly")
                {
                    type = "HostelMonthlyFee";
                }

            }

            return type;
        }

        internal static string get_feeid(string headname, string type, string sessionid, string class_id, string schooltype)
        {
            if (schooltype == "Days")
            {

                if (type == "Admission")
                {
                    type = "AdmissionFee";

                }
                else if (type == "Annual")
                {
                    type = "AnnualFee";
                }
                else if (type == "Monthly")
                {
                    type = "MonthlyFee";
                }

            }
            else
            {

                if (type == "Admission")
                {
                    type = "HostelAdmissionFee";

                }
                else if (type == "Annual")
                {
                    type = "HostelAnnualFee";
                }
                else if (type == "Monthly")
                {
                    type = "HostelMonthlyFee";
                }

            }
            string headname_final = headname.Replace('_', ' ');

            string query = "select   content_id,amount from dbo.[Fee_master_content_wise] where content='" + headname_final + "' and session_id='" + sessionid + "' and class_id='" + class_id + "' and parameter='" + type + "'";

            DataTable dt = dataTable(query);
            if (dt.Rows.Count == 0)
            {
                headname_final = headname.Replace(' ', '_');
                string query2 = "select   content_id,amount from dbo.[Fee_master_content_wise] where content='" + headname_final + "' and session_id='" + sessionid + "' and class_id='" + class_id + "' and parameter='" + type + "'";
                DataTable dt2 = dataTable(query2);
                if (dt2.Rows.Count == 0)
                {
                    return "0/0";
                }
                else
                {
                    return dt2.Rows[0]["content_id"].ToString() + "/" + dt2.Rows[0]["amount"].ToString();
                }
            }
            else
            {
                return dt.Rows[0]["content_id"].ToString() + "/" + dt.Rows[0]["amount"].ToString();
            }
        }

        // transport
        internal static string get_boarding_point(string session_id, string TransportPath_id, string admission_no)
        {
            string query = "Select Boarding_Point_id   from Student_mapping_with_TransportPath where Session_id='" + session_id + "'  and TransportPath_id='" + TransportPath_id + "' and Admission_no='" + admission_no + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Boarding_Point_id"].ToString();
            }
        }

        // hostel code

        public string oneyear_back_session(string session)
        {
            string session_frst_year = session.Substring(0, 4);

            int first = My.toint(session_frst_year) - 1;

            return first.ToString() + "-" + session_frst_year;


        }
        internal static string get_month_id_from_month_name(string Monthname)
        {
            string query = "Select top 1 Month_Id from Month_Index where Month='" + Monthname + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        public Dictionary<string, object> Bind_hostel_data_for_assined_student(string Session_id, string Class_id, string Admission_no)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = FillData("select * from Hostel_assign_master where Session_id='" + Session_id + "' and Admission_no='" + Admission_no + "' and Class_id='" + Class_id + "' and Status=1");
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
        internal static bool find_fee_collected_hostel(string session, string classid, string parameter_New, string Hostel_id)
        {
            SqlCommand cmd;
            string strQuery = "Select * from Student_Payment_History where parameter_New=@parameter_New and Session=@Session  and Class_id=@Class_id and Hostel_id=@Hostel_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            cmd.Parameters.AddWithValue("@Session", session);
            cmd.Parameters.AddWithValue("@Class_id", classid);
            cmd.Parameters.AddWithValue("@Hostel_id", Hostel_id);
            DataTable dt = My.GetDatas(cmd);
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
        internal static bool find_disc_fee_collected_hostel(string cat, string sub_cat, string session, string class_id, string content_id, string para_id, string hostelid)
        {
            SqlCommand cmd;
            string strQuery = "select t1.id from dbo.[Monthly_Fee_Collection_Slip] t1 join Hostel_Discount_master t2 on t1.session=t2.session and t1.class=t2.Class_id and t1.content_id=t2.content_id and t1.Hostel_id=t2.Hostel_id where t2.category_id=@category_id and t2.sub_category_id=@sub_category_id and t1.session=@session and t1.class=@class_id and    t1.content_id=@content_id and t2.parameter_id=@parameter_id and t2.Hostel_id=@Hostel_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@parameter_id", para_id);
            cmd.Parameters.AddWithValue("@Hostel_id", hostelid);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_disc_fee_collected_hostel_delete(string session, string head_id, string Hostel_id, string admissionserialnumber, string parameter)
        {
            SqlCommand cmd;
            string strQuery = " select  * from dbo.[Monthly_Fee_Collection_Slip] where adno='" + admissionserialnumber + "' and content_id=" + head_id + " and session='" + session + "' and Hostel_id='" + Hostel_id + "' and parameter='" + parameter + "'";
            cmd = new SqlCommand(strQuery);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_disc_fee_collected_hostel_admission(string cat, string sub_cat, string session, string class_id, string content_id, string para_id, string hostelid, string admission_no)
        {
            SqlCommand cmd;
            string strQuery = "select t1.id from dbo.[Monthly_Fee_Collection_Slip] t1 join Hostel_Discount_master t2 on t1.session=t2.session and t1.class=t2.Class_id and t1.content_id=t2.content_id and t1.Hostel_id=t2.Hostel_id and t1.adno=t2.admission_no where t2.category_id=@category_id and t2.sub_category_id=@sub_category_id and t1.session=@session and t1.class=@class_id and    t1.content_id=@content_id and t2.parameter_id=@parameter_id and t2.Hostel_id=@Hostel_id and t2.admission_no=@admission_no";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);

            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@parameter_id", para_id);
            cmd.Parameters.AddWithValue("@Hostel_id", hostelid);
            cmd.Parameters.AddWithValue("@admission_no", admission_no);

            DataTable dt = My.GetDatas(cmd);
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
        internal static bool find_disc_fee_collected_hostel_month_fee(string cat, string sub_cat, string session, string content_id, string para_id, string hostelid, string month, string Room_Category_id)
        {
            SqlCommand cmd;
            string strQuery = "select t1.id from dbo.[Monthly_Fee_Collection_Slip] t1 join Hostel_Discount_master t2 on t1.session=t2.session and t1.class=t2.Class_id and t1.content_id=t2.content_id and t1.Hostel_id=t2.Hostel_id and t1.Room_category=t2.Room_Category_id where t2.category_id=@category_id and t2.sub_category_id=@sub_category_id and t1.session=@session  and    t1.content_id=@content_id and t2.parameter_id=@parameter_id and t2.Hostel_id=@Hostel_id and t2.Month=@month and t2.Room_Category_id=@Room_Category_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);


            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@parameter_id", para_id);
            cmd.Parameters.AddWithValue("@Hostel_id", hostelid);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Room_Category_id", Room_Category_id);
            DataTable dt = My.GetDatas(cmd);
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



        private static bool getmonth_status_hostel(string monthname, string admission_no, string session)
        {
            string query = " select   * from dbo.[Typewise_fee_collection] where session='" + session + "' and admission_no='" + admission_no + "' and parameter='HostelMonthlyFee' and month='" + monthname + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static string get_month_name_no_fee_taken_hostel(string admission_no, string session)
        {
            string toreturnmonthname = "0";
            DataTable dt = FillDatastatic(" select Month   from dbo.[Month_Index] order by Position");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Monthname = dt.Rows[i]["Month"].ToString();
                    bool chk_month = getmonth_status_hostel(Monthname, admission_no, session);
                    if (chk_month == true)
                    {
                        toreturnmonthname = Monthname;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return toreturnmonthname;
        }



        internal string get_monthid(int monthid)
        {
            if (monthid == 1)
            {
                return "01";
            }
            else if (monthid == 2)
            {
                return "02";
            }

            else if (monthid == 3)
            {
                return "03";
            }
            else if (monthid == 4)
            {
                return "04";
            }
            else if (monthid == 5)
            {
                return "05";
            }
            else if (monthid == 6)
            {
                return "06";
            }
            else if (monthid == 7)
            {
                return "07";
            }
            else if (monthid == 8)
            {
                return "08";
            }
            else if (monthid == 9)
            {
                return "09";
            }
            else if (monthid == 10)
            {
                return "10";
            }
            else if (monthid == 11)
            {
                return "11";
            }
            else if (monthid == 12)
            {
                return "12";
            }
            else
            {

                return "0";
            }
        }
        internal static string get_top_one_hostel_name()
        {
            string query = "  Select top 1 Hostel_name from Hostels_master order by id desc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static string get_top_one_hostel_id()
        {
            string query = "  Select top 1 Hostel_id from Hostels_master order by id desc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static string get_top_one_hostel_catogery(string hostel_id, string sessionid)
        {
            string query = "  Select top 1 Room_Category_id from Hostel_Fee_master_content_wise where session_id=" + sessionid + " and Hostel_id='" + hostel_id + "' and parameter_id='3' order by id desc";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }
        public static DataTable Getdata_sp(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (SqlConnection scon = new SqlConnection(My.conn))
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














        //_______________________________Start___Sale Purcahse Code_____________________________
        public string GenerateRandomNumber(int start, int end)
        {
            Random random = new Random();
            int temp = random.Next(start, end);
            return temp.ToString();
        }
        internal static bool convert_amount(string qty)
        {
            try
            {
                double amt = Convert.ToDouble(qty);
                return false;
            }
            catch
            {
                return true;
            }
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
        internal static void update_Ledger_Opening_Balance(string account_name, string account_id, string grop_id, string type, string opening_balance, string Date, string session)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Ledger_Opening_Balance where  Account_id ='" + account_id + "' and firm='" + My.firm_id() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Ledger_Details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();

                dr["Account_id"] = account_id;//partyid
                dr["Group_id"] = grop_id;
                dr["firm"] = My.firm_id();
                dr["Session"] = session;
                dr["Date"] = Date;
                dr["idate"] = Convert.ToDateTime(Date).ToString("yyyyMMdd");
                dr["Debit_Credit"] = type;

                if (type.ToUpper() == "DR")
                {
                    dr["Debit"] = My.toDouble(opening_balance).ToString("0.00");
                    dr["Credit"] = "0.00";
                }
                else
                {
                    dr["Credit"] = My.toDouble(opening_balance).ToString("0.00");
                    dr["Debit"] = "0.00";
                }
                dt.Rows.Add(dr);
            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    dr["Account_id"] = account_id;//partyid
                    dr["Group_id"] = grop_id;
                    dr["firm"] = My.firm_id();
                    dr["Session"] = session;
                    dr["Date"] = Date;
                    dr["idate"] = Convert.ToDateTime(Date).ToString("yyyyMMdd");
                    dr["Debit_Credit"] = type;

                    if (type.ToUpper() == "DR")
                    {
                        dr["Debit"] = My.toDouble(opening_balance).ToString("0.00");
                        dr["Credit"] = "0.00";
                    }
                    else
                    {
                        dr["Credit"] = My.toDouble(opening_balance).ToString("0.00");
                        dr["Debit"] = "0.00";
                    }
                    break;
                }

            }

            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        internal static void save_Account_Ledger_Details(string account_name, string account_id, string grop_id)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Account_Ledger_Details where  Account_id ='" + account_id + "' and firm='" + My.firm_id() + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Account_Ledger_Details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Account_Name"] = account_name;
                dr["Account_id"] = account_id;//partyid
                dr["Group_id"] = grop_id;
                dr["firm"] = My.firm_id();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Account_Name"] = account_name;
                    dr["Account_id"] = account_id;
                    dr["Group_id"] = grop_id;
                    dr["firm"] = My.firm_id();
                    break;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

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

        internal static void send_data_to_user_log_history(string description, string userid)
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
            if (My.InsertUpdateData(cmd))
            {
            }

            //var list = new SqlParameter[] {
            //new SqlParameter("Description", description),
            //new SqlParameter("firm", My.firm_id),
            //new SqlParameter("Date", My.datetime()),
            //new SqlParameter("idate", My.idate()),
            //new SqlParameter("user_id", userid),
            //new SqlParameter("sp_status", "INSERT") };
            //My.exeSP("sp_HMS_User_Log_History", list);

        }

        internal static void send_data_to_user_log_history(string description, string userid, string entry_id, string Billing_for)
        {
            string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate,Branch_id) values (@Date,@Description,@user_id,@firm,@idate,@Branch_id)";
            string message = description + "Billing for-" + Billing_for + " Entry Id-" + entry_id;

            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Date", My.getdate1());
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@user_id", userid);
            cmd.Parameters.AddWithValue("@firm", 1);
            cmd.Parameters.AddWithValue("@idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Branch_id", "1");

            if (My.InsertUpdateData(cmd))
            {

            }

        }


        public static Dictionary<String, String> group = new Dictionary<String, String>();
        internal static bool landscape;

        public static void fetch_all_account()
        {
            group = new Dictionary<String, String>();
            DataTable dt = My.dataTable("select  * from dbo.[Account_Ledger_Details]  where  firm = '" + My.firm_id() + "'");
            foreach (DataRow dr in dt.Rows)
            {
                group[dr["Account_id"].ToString()] = dr["Group_id"].ToString();
            }
        }
        public static string session_wise_auto_serial_New(string column, string Financial_Year, string firm)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
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
                    My.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_auto_serial_New(column, Financial_Year, firm);
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
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data_session_wise where  firm='" + My.firm_id() + "' and session='" + My.get_session() + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["session"] = My.get_session();
                    dr["firm"] = My.firm_id();
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
                    My.exeSql("Alter table globle_data_session_wise add " + column + " varchar(50)");
                    result = session_wise_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }
        internal static double Round(double value)
        {
            return Roundnew(value, 2);
        }

        private static double Roundnew(double value, int f_point)
        {

            return Math.Round(value, f_point, MidpointRounding.AwayFromZero);
        }
        public static void update_annual_fee_amission_fee(string session)
        {
            DataTable cdt = My.dataTable(" select ac.*,(select top 1 Transfer_Status from admission_registor where admissionserialnumber=ac.Addmission_no and session=ac.session) as Transfer_Status,(select top 1 hosteltaken from admission_registor where admissionserialnumber=ac.Addmission_no and session=ac.session) as hosteltaken from Student_Payment_History ac  where  ac.Session='" + session + "' and ac.Type in('Admission','Annual') order by ac.id desc "); //and ac.Addmission_no='TEMP/23-24/00212333'
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                for (int i = 0; i < cdt.Rows.Count; i++)
                {
                    string Transfer_Status = cdt.Rows[i]["Transfer_Status"].ToString();
                    string Date = cdt.Rows[i]["Date"].ToString();
                    string regid = cdt.Rows[i]["Addmission_no"].ToString();
                    string payment_Amount = cdt.Rows[i]["Amount"].ToString();
                    string payment_Mode = cdt.Rows[i]["mode"].ToString();
                    string admission_No = cdt.Rows[i]["Addmission_no"].ToString();
                    string hostaltaken = cdt.Rows[i]["hosteltaken"].ToString();
                    string session_id = get_sess_prm(session);
                    Dictionary<string, object> dc1 = My.getstudentinfo(admission_No, session_id);
                    string Name = (String)dc1["Name"];
                    string Class_id = (String)dc1["Class_id"];
                    string Session_id = (String)dc1["Session_id"];
                    string Session = (String)dc1["Session"];
                    string Total_pay = payment_Amount;
                    string Payment_type = payment_Mode;
                    string category_id = (String)dc1["category_id"];
                    string sub_category_id = (String)dc1["sub_category_id"];

                    string Admission_no = (String)dc1["Admission_no"];
                    string classname = (String)dc1["classname"];
                    string Section = (String)dc1["Section"];

                    string parameter = "";
                    string parameter_id = "";

                    string Discount_on = "";
                    string type = "";
                    if (Transfer_Status.ToUpper() == "NEW")
                    {
                        parameter = hostaltaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                        parameter_id = hostaltaken.ToUpper() == "NO" ? "1" : "5";
                        type = "Admission";
                        Discount_on = "Admission";
                    }
                    else
                    {
                        parameter = hostaltaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                        parameter_id = hostaltaken.ToUpper().ToUpper() == "NO" ? "2" : "6";
                        type = "Annual";
                        Discount_on = "Annual";
                    }
                    Dictionary<string, object> dc12 = Bindhostel_dataforassined_student(Session_id, Class_id, admission_No);

                    string Hostel_id = (String)dc12["Hostel_id"];

                    string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + regid + "'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + regid + "' and parameter='" + parameter + "' and session='" + Session + "')t";
                    DataTable fee_dt = My.dataTable(qry);
                    if (fee_dt.Rows.Count == 0)
                    {
                        if (Hostel_id == "0")
                        {
                            qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + regid + "'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + sub_category_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + Session + "' and class_id='" + Class_id + "' )t";
                        }
                        else
                        {
                            qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + Hostel_id + " and admission_no='" + regid + "'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and Class_id='" + Class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + Session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + sub_category_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + Session + "' and class_id='" + Class_id + "' and fmc.Hostel_Id='" + Hostel_id + "')t";
                        }
                        fee_dt = My.dataTable(qry);
                    }
                    DataTable dt = find_data_static(qry);
                    if (dt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                            payable += My.toDouble(dr["payable"]);
                            paid += My.toDouble(dr["paid"]);
                            dues += My.toDouble(dr["dues"]);
                            disc += My.toDouble(dr["disc_amount"]);
                            payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                        }
                        string previous_dues = get_previous_amount_static(regid, Session_id, Class_id);
                        double totalpay = payble_after_disc + My.toDouble(previous_dues);
                        string paybaleamount = totalpay.ToString("0.00");
                        string adjustamount = payble_after_disc.ToString("0.00");
                        string totalamount = payable.ToString("0.00");
                        string total_discount = disc.ToString("0.00");
                        senddatatoannual_fee_collection(Payment_type, Session, Session_id, Class_id, regid, category_id, sub_category_id, Total_pay, Date, type, Name, total_discount, Section, previous_dues, totalamount, paybaleamount, Total_pay, adjustamount, parameter);
                    }
                }
            }
        }

        private static void senddatatoannual_fee_collection(string payment_type, string session, string session_id, string class_id, string regid, string category_id, string sub_category_id, string total_pay, string date, string type, string name, string total_discount, string section, string previous_dues, string totalamount, string paybaleamount, string Total_pay, string payble_after_disc, string parameter)
        {
            if (parameter == "AnnualFee" || parameter == "HostelAnnualFee")
            {
                SqlConnection conn2 = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + regid + "' and session='" + session + "'", conn2);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = regid;
                    dr[2] = My.toDouble(totalamount).ToString("0.00");
                    dr[3] = total_discount;
                    dr[4] = My.toDouble(totalamount) - My.toDouble(total_discount);
                    dr[5] = My.toDouble(Total_pay).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                    dr[7] = date;
                    dr[8] = "Online";

                    dr["session"] = session;
                    dr["idate"] = Convert.ToDateTime(date).ToString("yyyyMMdd");
                    dr["remark"] = "Online Paymnet";

                    dr["user_id"] = regid;
                    dr["Slip_no"] = "";
                    dr["Acamedic_Semester_Id"] = "0";
                    dr["branchid"] = "1";
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[5] = My.toDouble(My.toDouble(dr[5]) + My.toDouble(Total_pay)).ToString("0.00"); //My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                        dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
            else
            {
                SqlConnection conn3 = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + regid + "' and session='" + session + "'", conn3);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = regid;
                    dr[2] = My.toDouble(totalamount).ToString("0.00");
                    dr[3] = My.toDouble(total_discount).ToString("0.00");
                    dr[4] = My.toDouble(totalamount) - My.toDouble(total_discount);
                    dr[5] = My.toDouble(Total_pay);
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                    dr[7] = date;
                    dr[8] = "Online";

                    dr["session"] = session;

                    dr["idate"] = Convert.ToDateTime(date).ToString("yyyyMMdd");
                    dr["remark"] = "Online Paymnet";
                    dr["entry_id"] = "0";

                    dr["user_id"] = regid;
                    dr["Slip_no"] = "";
                    dr["Acamedic_Semester_Id"] = "0";
                    dr["branchid"] = "0";

                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[5] = My.toDouble(My.toDouble(dr[5]) + My.toDouble(Total_pay)).ToString("0.00"); //My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                        dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }

        private static string get_previous_amount_static(string regid, string session_id, string class_id)
        {
            DataTable dt = find_data_static("select Dues_Amount from Previous_Year_Dues  where Session_id='" + session_id + "' and AdmissionNumber='" + regid + "'  and Class_id='" + class_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "0.00";
            }
            else
            {

                return dt.Rows[0][0].ToString();
            }
        }

        private static DataTable find_data_static(string qry)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(qry, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }

        private static Dictionary<string, object> Bindhostel_dataforassined_student(string Session_id, string Class_id, string Admission_no)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = find_data_static("select * from Hostel_assign_master where Session_id='" + Session_id + "' and Admission_no='" + Admission_no + "' and Class_id='" + Class_id + "' and Status=1");
            if (dt.Rows.Count == 0)
            {
                dc["Hostel_id"] = "0";
                dc["Room_Category_id"] = "0";
                dc["From_month_name"] = "0";
                dc["From_month_id"] = "0";
                dc["Assined_Year_Month"] = "0";
                dc["Hostel_assign_id"] = "0";
            }
            else
            {
                dc["Hostel_id"] = dt.Rows[0]["Hostel_id"].ToString();
                dc["Room_Category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["From_month_name"] = dt.Rows[0]["From_month_name"].ToString();
                dc["From_month_id"] = dt.Rows[0]["From_month_id"].ToString();
                dc["Assined_Year_Month"] = dt.Rows[0]["Assined_Year_Month"].ToString();
                dc["Hostel_assign_id"] = dt.Rows[0]["Hostel_assign_id"].ToString();

            }
            return dc;

        }
        public static string global_id_creation(string columnname)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data", My.conn);
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
                    My.exeSql("Alter table globle_data add " + columnname + " varchar(500)");
                    result = global_id_creation(columnname);
                }
                else
                {

                }
            }
            return result;
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
        public static string datetime_new()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM/dd/yyyy hh:mm:ss tt");
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

                string idate = date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2);
                return idate;
            }
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
        public Dictionary<string, object> Firm_details()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "Select * from Firm_Details   ";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                dc["firm_name"] = "";
                dc["address1"] = "0";
                dc["logo"] = "0";
                dc["contact_no"] = "0";
                dc["email"] = "0";
                dc["State"] = "0";
                dc["State_code"] = "0";
            }
            else
            {
                dc["firm_name"] = dt.Rows[0]["firm_name"].ToString();
                dc["address"] = dt.Rows[0]["address1"].ToString();
                dc["logo"] = dt.Rows[0]["logo"].ToString();
                dc["contact_no"] = dt.Rows[0]["contact_no"].ToString();
                dc["email"] = dt.Rows[0]["email"].ToString();
                dc["State"] = dt.Rows[0]["State"].ToString();
                dc["State_code"] = dt.Rows[0]["State_code"].ToString();
            }
            return dc;

        }

        
        internal static void submitexception(string ex)
        {
            SqlCommand cmd;
            string strQuery = @"INSERT INTO Exception_table (message,error_name,date) values (@message,@error_name,@date)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@error_name", ex);
            cmd.Parameters.AddWithValue("@message", "Sale And Purchase");
            cmd.Parameters.AddWithValue("@date", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            if (InsertUpdateData(cmd))
            {

            }
        }


        internal static string firm_id()
        {
            try
            {
                DataTable dt = My.dataTable("Select firm_id_Sale_Purchase from Firm_Details");
                if (dt.Rows.Count == 0)
                {
                    return "1";
                }
                else
                {
                    return dt.Rows[0]["firm_id_Sale_Purchase"].ToString();
                }
            }
            catch
            {
                return "1";
            }
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
                My.submitexception(ex.ToString() + "==" + date + "==" + format);
                return Convert.ToDateTime(date);
            }
        }


        //_______________________________End___Sale Purcahse Code_____________________________
        internal static string month_position(string month_name)
        {
            string mnth_position = "0";
            DataTable dt = My.dataTable("select Position from dbo.[Month_Index] where Month='" + month_name + "'");
            if (dt.Rows.Count > 0)
            {
                mnth_position = dt.Rows[0]["Position"].ToString();
            }
            return mnth_position;
        }
        internal static string get_months_group(string group_id)
        {
            string month_name = "";
            DataTable dt = My.dataTable("Select Month_name from Custome_month_selection_setting where Pair_group_id='" + group_id + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    month_name = month_name + "'" + dr["Month_name"].ToString() + "'" + ",";
                }
                month_name = month_name.Remove(month_name.Length - 1);
            }
            return month_name;
        }
        public Dictionary<string, object> Bind_Transport_data_for_assined_student(string Session_id, string Class_id, string Admission_no)
        {
            string path = "Select top 1 Pathname from TransportationPath where TransportationPath_id=t1.TransportPath_id";
            string transportname = "Select top 1 transport_name from Transport_Master where transport_id=t1.transport_id";
            string Bus_no = "Select top 1 Bus_no from Transport_Master where transport_id=t1.transport_id";
            string seatname = "Select top 1 Sheet_No from Transport_Path_Mapping_With_Sheet where Transportation_Id=t1.transport_id and TransportationPath_id=t1.TransportPath_id  and Sheet_Id=t1.Sheet_Id";

            Dictionary<string, object> dc = new Dictionary<string, object>();
            DataTable dt = FillData("select top 1 *,(" + path + ") as path,(" + transportname + ") as transportname,(" + seatname + ") as seatname,(" + Bus_no + ") as Bus_no,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id = t1.TransportPath_id and Transportation_Id = t1.transport_id and Boarding_Point_id = t1.Boarding_Point_id and Session_Id = " + Session_id + " order by Id desc) as Boarding_Point  from Student_mapping_with_TransportPath t1 where t1.Session_id='" + Session_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Class_id='" + Class_id + "' order by t1.id desc");
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



        internal static bool get_selected_monthpaid_busss(string admSionNo, string session, string month, string class_id)
        {
            string query = "  select  * from dbo.[Typewise_fee_collection] where parameter='MonthlyFee' and  admission_no='" + admSionNo + "' and month='" + month + "' and session='" + session + "' and status='Paid'  and class_id=" + class_id + "   ";

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {

                string query2 = " select  * from dbo.[Typewise_fee_collection] where parameter='MonthlyFee' and  admission_no='" + admSionNo + "' and month='" + month + "' and session='" + session + "' and status='Dues' and Year=" + class_id + "  ";

                DataTable dt2 = FillDatastatic(query2);
                if (dt2.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;

                }


            }
        }

        public static void no_any_paymnet_then_delete_type_wise_admission_annul_row(string Addmission_no, string Session, string Class_id, string parameter)
        {

            DataTable dt = FillDatastatic("Select *  from Student_Payment_History where parameter_New='" + parameter + "' and Addmission_no='" + Addmission_no + "' and Session='" + Session + "'");
            if (dt.Rows.Count == 0)
            {
                My.exeSql("delete from Typewise_fee_collection where admission_no='" + Addmission_no + "' and session='" + Session + "' and class_id=" + Class_id + " and parameter='" + parameter + "'");

            }
            else
            {

            }
        }
        internal static string get_topone_boarding_point(string sessionid, string vechis_id, string vechile_path_id)
        {
            string query = " select top 1 Boarding_Point_id  from dbo.[Transportation_Boarding_Point] where Session_Id=" + sessionid + "  and Transportation_Id=" + vechis_id + " and TransportationPath_id=" + vechile_path_id + "    ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }
        public static string conn = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        internal static string get_top_one_vechile()
        {
            string query = " select top 1 Transportation_Id  from dbo.[TransportationPath] ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }
        internal static string gettop_one_trans_path(string Transportation_Id)
        {
            string query = " select top 1 TransportationPath_id  from dbo.[TransportationPath] where  Transportation_Id=" + Transportation_Id + "     ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }

        //------------------------22/08/2023---------------------

        internal static bool find_mnthly_transp_fee_collected_N(string session_id, string Transportation_Id, string rout_path_id, string Boarding_point)
        {
            string get_sessionname = get_session_static(session_id);
            SqlCommand cmd;
            string strQuery = "Select  * from Monthly_Fee_Collection_Slip  where Transport_Boarding_Point_id=@Transport_Boarding_Point_id and Transportation_Id=@Transportation_Id and TransportationPath_id=@TransportationPath_id and session=@session  ";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Transport_Boarding_Point_id", Boarding_point);
            cmd.Parameters.AddWithValue("@Transportation_Id", Transportation_Id);
            cmd.Parameters.AddWithValue("@TransportationPath_id", rout_path_id);
            cmd.Parameters.AddWithValue("@session", get_sessionname);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_month_transp_fee_collected_N_discunt_chek(string session_id, string Transportation_Id, string rout_path_id, string Boarding_point, string Month)
        {
            string get_sessionname = get_session_static(session_id);
            SqlCommand cmd;
            string strQuery = "Select  * from Monthly_Fee_Collection_Slip  where Transport_Boarding_Point_id=@Transport_Boarding_Point_id and Transportation_Id=@Transportation_Id and TransportationPath_id=@TransportationPath_id and session=@session and Month=@Month ";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Transport_Boarding_Point_id", Boarding_point);
            cmd.Parameters.AddWithValue("@Transportation_Id", Transportation_Id);
            cmd.Parameters.AddWithValue("@TransportationPath_id", rout_path_id);
            cmd.Parameters.AddWithValue("@session", get_sessionname);
            cmd.Parameters.AddWithValue("@Month", Month);
            DataTable dt = My.GetDatas(cmd);
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

        public static string get_session_static(string session_id)
        {
            DataTable dt = FillDatastatic("Select Session from session_details where session_id='" + session_id + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }



        //--------------------18/08/2023


        public bool bind_not_paymnet_datat(string admission_no, string session_id)
        {
            string query = " select  top  1 * from dbo.[Payment_transaction_process] where Admission_no='" + admission_no + "' and Session_id='" + session_id + "' and status='Pending' order by Id desc ";

            DataTable dt = FillData(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                //a1.Visible = true;
                return true;
            }
        }










        internal string get_catogery(string Category_id)
        {
            string query = " Select Category_Name  from Category_Details  where Category_Id='" + Category_id + "'   ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {

                return dt.Rows[0][0].ToString();

            }
        }

        internal string get_subcatogery(string Category_id, string subcatogery)
        {
            string query = " Select Sub_CategoryName  from Sub_Category_Details  where Category_Id='" + Category_id + "'  and Sub_CategoryId=" + subcatogery + "  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {

                return dt.Rows[0][0].ToString();

            }
        }


        public int ConvertStringToiDate(string DateInString) //Format :: dd/MM/yyyy
        {
            try
            {
                DateInString = DateInString.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);
                return Convert.ToInt32(DateInString);
            }
            catch
            {
                return Convert.ToInt32(0);
            }
        }



        public string convertdattodate(string DateInString) //Format :: dd/MM/yyyy
        {
            try
            {
                DateInString = DateInString.Substring(0, 2) + "/" + DateInString.Substring(3, 2) + "/" + DateInString.Substring(6, 4);
                return DateInString;
            }
            catch
            {
                return "";
            }
        }



        public string day_month_new(string date)
        {
            //string DateInString = date.Substring(6, 4) + DateInString.Substring(3, 2) + DateInString.Substring(0, 2);

            // return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM");

            try
            {
                return date.Substring(0, 2) + "/" + date.Substring(3, 2);
            }
            catch
            {
                return "0";
            }
        }

        internal static bool check_row_no_avilavel_seat(string Transportation_Id, string TransportationPath_id, string sheet_position, string seat_modle, string Row)
        {

            DataTable cdt = My.dataTable(" select count(Id) from dbo.[Transport_Path_Mapping_With_Sheet] where TransportationPath_id=" + TransportationPath_id + " and Transportation_Id=" + Transportation_Id + " and Sheet_position='" + sheet_position + "'  and Seat_Model='" + seat_modle + "' and Row='" + Row + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {
                int count = My.toInt(cdt.Rows[0][0].ToString());
                if (count == Convert.ToInt32(seat_modle))
                {
                    return false;
                }
                else if (count >= Convert.ToInt32(seat_modle))
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public string day_month_2_new(string date)
        {
            try
            {
                return date.Substring(0, 2) + "-" + date.Substring(3, 2);
            }
            catch
            {
                return "0";
            }
        }

        public string day_month()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM");
        }



        public string day_month_2()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd-MM");
        }

        public string todaymonthday()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MMdd");
        }

        public string get_one_monthback(string monthid)
        {
            int finalmonth = Convert.ToInt32(monthid) - 1;

            string counter = "0";
            switch (finalmonth)
            {
                case 01:
                    counter = "January";
                    break;
                case 02:
                    counter = "February";
                    break;

                case 03:
                    counter = "March";
                    break;

                case 04:
                    counter = "April";
                    break;
                case 05:
                    counter = "May";
                    break;
                case 06:
                    counter = "June";
                    break;

                case 07:
                    counter = "July";
                    break;
                case 08:
                    counter = "August";
                    break;

                case 09:
                    counter = "September";
                    break;

                case 10:
                    counter = "October";
                    break;
                case 11:
                    counter = "November";
                    break;

                case 12:
                    counter = "December";
                    break;

            }
            return counter;



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

        public string time()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
        }
        public string sevendaysback()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-1).ToString("dd/MM/yyyy");
        }
        public string sevendaysbackseven()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(-7).ToString("dd/MM/yyyy");
        }




        private static string get_state_codes(string state)
        {
            SqlCommand cmd = new SqlCommand("Select Code from  StateList where State='" + state + "'");
            DataTable dt = GetDataq(cmd);
            if (dt.Rows.Count == 0)
            {
                return state;
            }
            else
            {
                return dt.Rows[0]["Code"].ToString();
            }
        }

        public string fiftendaysnext()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).AddDays(15).ToString("dd/MM/yyyy");
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

        public string date_with_slash()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd-MM-yyyy");
        }
        public string cmonth()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
        }
        public string idate()
        {

            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
        }
        public string day()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dddd");
        }
        public string daysingle()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd");
        }
        internal string get_current_month_id()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
        }
        internal string get_current_monthname()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MMM");
        }
        internal string get_current_fullmonthname()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MMMM");
        }

        public static Boolean InsertUpdateData(SqlCommand cmd)
        {
            bool status = false;
            SqlConnection con = new SqlConnection(My.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            status = true;
            con.Close();
            con.Dispose();
            return status;
        }

        public static string getdate1()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MMM/yyyy hh:mm:ss tt");
        }


        public void executequery(string query)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(My.conn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
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

        public DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = My.conn;
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

        public DataTable FillData(string query)
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



        public string auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", My.conn);
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

                    exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }
        public static void exeSql(string query)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
        }


        public void bind_all_ddl_with_id(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
        public void bind_all_ddl_with_id_All(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("All", "0"));
        }



        public void bind_all_ddl_with_id_no_select(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
                ddl.DataValueField = dt.Columns[1].ToString();
            }

            ddl.DataSource = dt;
            ddl.DataBind();
        }

        public void PopulateDropDown(DropDownList DropDown, DataTable DatatableName, string text, string value)
        {
            try
            {

                if (DatatableName.Rows.Count > 0)
                {
                    DropDown.DataTextField = "";
                    DropDown.DataValueField = "";
                    DropDown.DataSource = DatatableName;
                    DropDown.DataBind();
                    DropDown.DataTextField = text;
                    DropDown.DataValueField = value;
                    DropDown.DataBind();
                    if (DropDown.Items[0].Text != "0")
                    {
                        DropDown.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {

                    DropDown.Items.Insert(0, new ListItem("No Data", "0"));
                    DropDown.DataSource = null;
                    DropDown.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void insert_data_logfile(string userid, string firm, string msg, string Branchid)
        {
            string strQuery = " INSERT INTO User_Log_History (Date,Description,user_id,firm,idate,Branch_id) values (@Date,@Description,@user_id,@firm,@idate,@Branch_id)";
            string message = msg;
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Date", My.getdate1());
            cmd.Parameters.AddWithValue("@Description", message);
            cmd.Parameters.AddWithValue("@user_id", userid);
            cmd.Parameters.AddWithValue("@firm", firm);
            cmd.Parameters.AddWithValue("@idate", idate());
            cmd.Parameters.AddWithValue("@Branch_id", Branchid);
            if (My.InsertUpdateData(cmd))
            {
            }
        }
        public void bind_ddl_neotselect(DropDownList ddl, string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();

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
        public void bind_ddlall_sm(DropDownList ddl, string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
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
        //--------------------------------------wahab----------------------------------------------


        internal static int toint(object p)
        {
            try
            {
                return Convert.ToInt32(p);
            }
            catch
            {
                return 0;
            }
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool check_valid_mobile(string mob)
        {
            var r = new Regex("[0-9]");
            bool status = false;
            if (mob == "")
            {
                status = false;
            }
            if (r.IsMatch(mob) && mob.Length == 10)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        internal static string view_admission_no_format(string column_name)
        {
            string adno = My.view_auto_serial(column_name);
            DateTime dtm = DateTime.Now;
            if (My.adno_formate.Length > 0)
            {
                adno = My.toDouble(adno).ToString(My.adno_formate);
            }
            string sm_sess = My.get_session().Substring(2, 2) + '-' + My.get_session().Substring(7, 2);
            string m_sess = My.get_session().Substring(0, 4) + '-' + My.get_session().Substring(7, 2);
            string pre_fix = My.adno_prefix.Replace("{dd}", dtm.ToString("dd")).Replace("{mm}", dtm.ToString("MM")).Replace("{ss}", sm_sess).Replace("{sss}", m_sess).Replace("{ssss}", My.get_session());
            string post_fix = My.adno_postfix.Replace("{dd}", dtm.ToString("dd")).Replace("{mm}", dtm.ToString("MM")).Replace("{ss}", sm_sess).Replace("{sss}", m_sess).Replace("{ssss}", My.get_session());
            return pre_fix + adno + post_fix;
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
        internal static string tomonth_name(int month)
        {
            string counter = "0";
            switch (month)
            {
                case 01:
                    counter = "January";
                    break;
                case 02:
                    counter = "February";
                    break;

                case 03:
                    counter = "March";
                    break;

                case 04:
                    counter = "April";
                    break;
                case 05:
                    counter = "May";
                    break;
                case 06:
                    counter = "June";
                    break;

                case 07:
                    counter = "July";
                    break;
                case 08:
                    counter = "August";
                    break;

                case 09:
                    counter = "September";
                    break;

                case 10:
                    counter = "October";
                    break;
                case 11:
                    counter = "November";
                    break;

                case 12:
                    counter = "December";
                    break;

            }
            return counter;
        }

        internal static int tomonth_number(string prev_month)
        {
            int counter = 0;
            switch (prev_month)
            {
                case "January":
                    counter = 01;
                    break;
                case "February":
                    counter = 02;
                    break;

                case "March":
                    counter = 03;
                    break;

                case "April":
                    counter = 04;
                    break;
                case "May":
                    counter = 05;
                    break;
                case "June":
                    counter = 06;
                    break;

                case "July":
                    counter = 07;
                    break;
                case "August":
                    counter = 08;
                    break;

                case "September":
                    counter = 09;
                    break;

                case "October":
                    counter = 10;
                    break;
                case "November":
                    counter = 11;
                    break;

                case "December":
                    counter = 12;
                    break;

            }
            return counter;
        }

        internal static string tomonth_numberstring(string prev_month)
        {
            string counter = "0";
            switch (prev_month)
            {
                case "January":
                    counter = "01";
                    break;
                case "February":
                    counter = "02";
                    break;

                case "March":
                    counter = "03";
                    break;

                case "April":
                    counter = "04";
                    break;
                case "May":
                    counter = "05";
                    break;
                case "June":
                    counter = "06";
                    break;

                case "July":
                    counter = "07";
                    break;
                case "August":
                    counter = "08";
                    break;

                case "September":
                    counter = "09";
                    break;

                case "October":
                    counter = "10";
                    break;
                case "November":
                    counter = "11";
                    break;

                case "December":
                    counter = "12";
                    break;

            }
            return counter;
        }




        internal static string dir_log;
        public static void Save_Exception(string exception)
        {
            try
            {
                if (!System.IO.Directory.Exists(My.dir_log + "\\Error\\"))
                {
                    Directory.CreateDirectory(My.dir_log + "\\Error\\");
                }
                System.IO.File.WriteAllText(My.dir_log + "\\Error\\" + DateTime.Now.ToString("dd-MM-yyyy hhmmss tt") + ".txt", exception);
            }
            catch
            {
            }
        }

        public static ArrayList bindList(string query)
        {
            ArrayList lst = new ArrayList();
            DataTable dt = dataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(dr[0].ToString());
            }
            return lst;
        }
        public static string data(string query)
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
        internal static void Bind_year(DropDownList dl)
        {
            string Startyear = "2022";
            string End_year = "2030";
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            for (int i = Convert.ToInt32(Startyear); i <= Convert.ToInt32(End_year); i++)
            {
                ar.Add(i);
            }
            dl.DataSource = ar;
            dl.DataBind();
        }

        public static DataTable dataTable(string query)
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds.Tables[0];

        }
        public static DataTable dataTableHDB(string query, string con)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            return ds.Tables[0];
        }
        internal string get_current_year()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
        }

        #region veriable

        internal static void init_payroll()
        {
            DataTable dt = My.dataTable("select * from Payroll_Setting");
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    My.emp_code_prefix = dr["Emp_Code_Prefix"].ToString();
                    My.emp_code = dr["Emp_Code"].ToString();
                    My.emp_code_postfix = dr["Emp_Code_Postfix"].ToString();
                }
            }
        }



        public static string emp_code = "";
        public static string emp_code_postfix = "";
        public static string emp_code_prefix = "";

        public static string FormSale_allow = "";

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
        public static string con = My.conn;
        public static string client_id = "";

        public static string er = "false";
        public static Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
        public static string user_status = "";


        public static string ddd;
        public static bool negativesale = false;
        public static string address2;
        public static bool is_inclusive = true;
        public static double page_width = 8.5;
        public static double page_height = 11.7;
        public static string table;
        public static string sts;
        public static string printer_name;
        //public static string current_session = My.get_session();
        //public static string current_session_id = My.get_session_id();
        public static string address1 = " ";

        public static string bill_type;
        public static bool show_discount;
        public static bool diff_barcode;
        public static string terms_and_condition;
        public static string state;
        public static bool show_return_item_on_bill;
        public static string state_code;
        public static bool print_header;

        public static string gstin;
        public static string opening_paid_amount = "";
        #endregion


        public static void bind_ddl_all(DropDownList ddl, string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            ArrayList ar = new ArrayList();
            ar.Add("All");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }



        public static string auto_serialS(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
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
                    My.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = auto_serialS(column);
                }
                else
                {

                }
            }
            return result;
        }

        public static string pre_updated_auto_serial(string column)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(My.conn);
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
            return result;
        }


        public static void bind_ddl_select(DropDownList ddl, string query)
        {
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
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

        internal static object get_session_id_from_Scholarship_id(string selectedValue)
        {
            DataTable dt = FillDatastatic("Select * from Scholarship_Program where Test_id='" + selectedValue + "' ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Session_id"].ToString();
            }
        }


        public static string view_auto_serial(string column)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
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
                    My.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = view_auto_serial(column);
                }
                else
                {

                }
            }
            return result;
        }

        internal bool chkenum(string p)
        {
            try
            {
                double a = Convert.ToDouble(p);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string get_session(string sessionid)
        {
            DataTable dt = FillData("Select Session from session_details where session_id='" + sessionid + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        public static string invoice_no = "";
        public static string invoice_post = "";
        public static string invoice_prefix = "";
        internal static string invoice_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_slip_prifix();
            string session = get_session();


            return pre_fix + "/" + session + "/" + bill;
        }

        public static string get_session()
        {
            DataTable dt = FillDatastatic("Select Session from session_details where Use_mode='1'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Session"].ToString();
            }
        }
        public static string get_session_id()
        {
            DataTable dt = FillDatastatic("Select session_id from session_details where Use_mode='1'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["session_id"].ToString();
            }
        }

        public static string get_session_id_for_admission()
        {
            DataTable dt = FillDatastatic("Select session_id from session_details where Admission_form_active='1'");
            if (dt.Rows.Count == 0)
            {
                DataTable dts = FillDatastatic("Select session_id from session_details where Use_mode='1'");
                if (dts.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    return dts.Rows[0]["session_id"].ToString();
                }
            }
            else
            {
                return dt.Rows[0]["session_id"].ToString();
            }
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

        internal static string check_manual_dup_bill_no(string slip_no)
        {
            DataTable dt = FillDatastatic("select Id from Student_Payment_History where Slip_no='" + slip_no + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        public static string get_slip_prifix()
        {
            DataTable dt = FillDatastatic("Select Slip_Prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Slip_Prefix"].ToString();
            }
        }

        private static string auto_serial1(string column)
        {
            string result = "";
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from globle_data ", My.conn);
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
                    My.exeSql("Alter table globle_data add " + column + " varchar(50)");
                    result = auto_serial1(column);
                }
                else
                {

                }
            }
            return result;
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

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words;
        }

        internal string get_type(string courseid)
        {
            DataTable dt = FillData("Select Type from Add_course_table where course_id='" + courseid + "'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Type"].ToString();
            }
        }

        internal static DataTable dataTableSP(string sp, string class_id, string session, string admission_no)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@admission_no", admission_no);

            cmd.CommandText = sp;

            DataTable dt = new DataTable();
            using (SqlConnection scon = new SqlConnection(My.con))
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
        public static DataTable dataTableSP(string sp_name, Dictionary<string, object> param)
        {

            SqlConnection conn = new SqlConnection(con);
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

        public static int DateConvertToIdate(string date)
        {
            try
            {
                DateTime d11 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string idate = d11.ToString("yyyyMMdd");
                return Convert.ToInt32(idate);
            }
            catch
            {
                return Convert.ToInt32("0");
            }
        }


        public static string get_month_name_by_date(string date)
        {
            try
            {
                DateTime d11 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string month = d11.ToString("MMMM");
                return month;
            }
            catch
            {
                return "-";
            }
        }

        public static string get_is_billl_no_modify()
        {
            try
            {
                DataTable dt = find_data_static("select Is_student_bill_no_modify from Firm_Details ");
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch
            {
                return "0";
            }
        }

        internal static bool verify_bill_no(string billno, string id)
        {
            string query = "select  * from Student_Payment_History where Slip_no='" + billno + "' and Id!='" + id + "' ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static string idateConvertToDate(int idate)
        {
            string day = idate.ToString().Substring(6, 2);
            string month = idate.ToString().Substring(4, 2);
            string year = idate.ToString().Substring(0, 4);
            return day + "/" + month + "/" + year;
        }

        public static string DateConvertTomonth(string date)
        {
            DateTime d11 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string month = d11.ToString("MM");
            return month;
        }
        public static int DateConvertToIyearMonth(string date)
        {
            DateTime d11 = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string idate = d11.ToString("yyyyMM");
            return Convert.ToInt32(idate);
        }


        public static DataSet executeReaderDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            using (SqlConnection scon = new SqlConnection(My.conn))
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

        public DataSet Fill_Data_set(string query)
        {
            DataSet dtc = new DataSet();
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
        public double cheknumer(string val)
        {
            double toretun = 0;
            try
            {
                toretun = Convert.ToDouble(val);
            }
            catch
            {
            }
            return toretun;
        }

        public bool cheknumer_Double(string val)
        {
            bool toretun = false;
            try
            {
                if (val.ToUpper() == "NAN")
                {
                    toretun = false;
                }
                else
                {
                    double abcd = Convert.ToDouble(val);
                    toretun = true;
                }
            }
            catch
            {
            }
            return toretun;
        }


        internal static double Round(double value, int f_point)
        {
            return Math.Round(value, f_point);
        }


        internal string get_semester_yearname(string Acamedic_Semester_Id, string sessionid)
        {
            DataTable dt = FillData("Select * from Session_Academic where Session_Id=" + sessionid + " and Academic_Year_Id=" + Acamedic_Semester_Id + "");
            if (dt.Rows.Count == 0)
            {
                return semstername(Acamedic_Semester_Id, sessionid);
            }
            else
            {
                return dt.Rows[0]["Academic_Year"].ToString();
            }
        }

        private string semstername(string Acamedic_Semester_Id, string sessionid)
        {
            DataTable dt = FillData("Select * from Session_Academic where Session_Id=" + sessionid + " and Acamedic_Semester_Id=" + Acamedic_Semester_Id + "");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Acamedic_Semester"].ToString();
            }
        }

        internal string get_user(string user_id)
        {
            DataTable dt = FillData("Select * from user_details where user_id='" + user_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["name"].ToString();
            }
        }
        public static string create_random_no_otp()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random();
            int tempo = random.Next(100000, 999999);
            return tempo.ToString();
        }



        internal void BindRepeater(string sql, Repeater RPDetails)
        {
            DataTable dt = FillData(sql);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }
        internal static object get_Transportationpathid(string admissionserialnumber, string sessionid, string Class_id)
        {
            string query = " select top 1 TransportPath_id  from dbo.[Student_mapping_with_TransportPath] where Admission_no='" + admissionserialnumber + "' and Class_id='" + Class_id + "' and Session_id='" + sessionid + "'  order by id desc     ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal static string get_TransportationPath(string TransportationPath_id)
        {
            string query = " select Rootname  from dbo.[TransportationPath] where TransportationPath_id='" + TransportationPath_id + "'      ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }
        public Dictionary<string, object> get_start_month_and_month_id()
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string query = "select   * from Month_Index where Position='1'";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
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

        internal string get_branch_id(string AdminCode)
        {
            DataTable dt = FillData("Select Branch_id from user_details where user_id='" + AdminCode + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "1";
            }
            else
            {
                return dt.Rows[0]["Branch_id"].ToString();
            }
        }





        internal static void bind_ddl_noselect(DropDownList ddl, string strQuery)
        {
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetDataq(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                ddl.DataTextField = "Select";
                ddl.DataValueField = "0";
            }
            else
            {
                ddl.DataTextField = dt.Columns[0].ToString();
            }
            ddl.DataSource = dt;
            ddl.DataBind();
        }

        private static DataTable GetDataq(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = My.conn;
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



        internal string getstatename(string statecode)
        {
            SqlCommand cmd = new SqlCommand("Select State from  StateList where Code='" + statecode + "'");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return statecode;
            }
            else
            {
                return dt.Rows[0]["State"].ToString();
            }
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
        public static ArrayList bindMonthNameAll()
        {
            ArrayList lst = new ArrayList();
            lst.Add("All");
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


        public static string session_file_name = "settings.dat";
        internal static string session(string session_name)
        {
            if (My.Session.Keys.Contains(session_name))
            {
                return My.Session[session_name];
            }
            SqlDataAdapter ad = new SqlDataAdapter("select * from globle_session_data where  session_name='" + session_name + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Setting_Data");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                My.session(session_name, dr["session_value"].ToString());
                return dr["session_value"].ToString();
            }
            if (File.Exists(My.dir_log + session_file_name))
            {
                System.Xml.Linq.XElement xdoc = System.Xml.Linq.XElement.Load(My.dir_log + session_file_name);
                Dictionary<string, string> config = (from element in xdoc.Elements() select new KeyValuePair<string, string>(element.Name.ToString(), element.Value)).ToDictionary(x => x.Key, x => x.Value);
                if (config.ContainsKey(session_name))
                {
                    My.session(session_name, config[session_name]);
                    return config[session_name];
                }
            }
            return "";
        }

        internal static void session(string session_name, string session_value)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from globle_session_data where  session_name='" + session_name + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Setting_Data");
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
            My.Session[session_name] = session_value;
        }

        public static Dictionary<string, string> Session = new Dictionary<string, string>();
        public static bool is_combine = false;

        internal static string invoice_format_admssion(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_slip_prifix_admission();
            string session = get_session();


            return pre_fix + "/" + session + "/" + bill;
        }





        private static string get_slip_prifix_admission()
        {
            DataTable dt = FillDatastatic("Select Slip_Prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Slip_Prefix"].ToString();
            }
        }

        internal static string invoice_readmission(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_slip_prifix_readmission();
            string session = get_session();


            return pre_fix + "/" + session + "/" + bill;
        }

        private static string get_slip_prifix_readmission()
        {
            DataTable dt = FillDatastatic("Select Readmison_Prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Readmison_Prefix"].ToString();
            }
        }


        internal static string invoice_monthly(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_slip_prifix_monthly();
            string session = get_session();
            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                return pre_fix + "/" + session + "/" + bill;
            }
        }

        private static string get_slip_prifix_chnageclass()
        {
            DataTable dt = FillDatastatic("Select Change_class_Perifix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "CC";
            }
            else
            {
                return dt.Rows[0]["Change_class_Perifix"].ToString();
            }

        }
        private static string get_slip_prifix_monthly()
        {
            DataTable dt = FillDatastatic("Select Monthly_Slip_Prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Monthly_Slip_Prefix"].ToString();
            }
        }
        internal static string get_student_gcm_id(string regid)
        {

            DataTable dt = FillDatastatic("Select top 1 gcm_id from admission_registor where admissionserialnumber='" + regid + "' order by id desc");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["gcm_id"].ToString();
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
        public static Dictionary<string, object> getstudentinfo(string admission_No, string session_id)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string quiry = "Select   *,(select top 1 Course_Name from Add_course_table where course_id=admission_registor.Class_id) as classname from admission_registor   where admissionserialnumber='" + admission_No + "' and Session_id='" + session_id + "'";


            SqlDataAdapter ad = new SqlDataAdapter(quiry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "UserRegistrationMaster");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {
                // go_to_Incubators_user(mobileno);

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
                dc["Section"] = "0";
                dc["Transfer_Status"] = "0";
                dc["classname"] = "0";
                dc["Hostel_id"] = "0";
                dc["is_applied_dayboarding"] = "0";
            }
            else
            {
                dc["Name"] = dt.Rows[0]["studentname"].ToString();
                dc["Admission_no"] = dt.Rows[0]["admissionserialnumber"].ToString();
                dc["Session"] = dt.Rows[0]["session"].ToString();
                dc["Class_id"] = dt.Rows[0]["Class_id"].ToString();
                dc["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                dc["category_id"] = dt.Rows[0]["Category_id"].ToString();
                dc["sub_category_id"] = dt.Rows[0]["SubCategory_id"].ToString();
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
                dc["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();
                dc["classname"] = dt.Rows[0]["classname"].ToString();
                dc["Hostel_id"] = dt.Rows[0]["Hostel_id"].ToString();
                dc["is_applied_dayboarding"] = dt.Rows[0]["is_applied_dayboarding"].ToString();
                dc["day_boarding_with_lunch"] = dt.Rows[0]["day_boarding_with_lunch"].ToString();
                dc["transportationtaken"] = dt.Rows[0]["transportationtaken"].ToString();
            }
            return dc;
        }


        //--------------------------------------------Payroll--------------------------------------

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



        internal static string getMonthS_full_name(string text)
        {
            try
            {
                if (text == "1" || text == "01")
                {
                    return "January";
                }
                else if (text == "2" || text == "02")
                {
                    return "February";
                }
                else if (text == "3" || text == "03")
                {
                    return "March";
                }
                else if (text == "4" || text == "04")
                {
                    return "April";
                }
                else if (text == "5" || text == "05")
                {
                    return "May";
                }
                else if (text == "6" || text == "06")
                {
                    return "June";
                }

                else if (text == "7" || text == "07")
                {
                    return "July";
                }
                else if (text == "8" || text == "08")
                {
                    return "August";
                }
                else if (text == "9" || text == "09")
                {
                    return "September";
                }
                else if (text == "10")
                {
                    return "October";
                }
                else if (text == "11")
                {
                    return "November";
                }
                else
                {
                    return "December";
                }
            }
            catch
            {
                return "0";
            }
        }
        internal static string getMonthS_sort_name(string text)
        {
            try
            {
                if (text == "January")
                {
                    return "Jan";
                }
                else if (text == "February")
                {
                    return "Feb";
                }
                else if (text == "March")
                {
                    return "Mar";
                }
                else if (text == "April")
                {
                    return "Apr";
                }
                else if (text == "May")
                {
                    return "May";
                }
                else if (text == "June")
                {
                    return "Jun";
                }

                else if (text == "July")
                {
                    return "Jul";
                }
                else if (text == "August")
                {
                    return "Aug";
                }
                else if (text == "September")
                {
                    return "Sep";
                }
                else if (text == "October")
                {
                    return "Oct";
                }
                else if (text == "November")
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

        internal static string getMonthS_twoDigit(string text)
        {
            try
            {
                if (text == "1" || text == "01")
                {
                    return "01";
                }
                else if (text == "2" || text == "02")
                {
                    return "02";
                }
                else if (text == "3" || text == "03")
                {
                    return "03";
                }
                else if (text == "4" || text == "04")
                {
                    return "04";
                }
                else if (text == "5" || text == "05")
                {
                    return "05";
                }
                else if (text == "6" || text == "06")
                {
                    return "06";
                }

                else if (text == "7" || text == "07")
                {
                    return "07";
                }
                else if (text == "8" || text == "08")
                {
                    return "08";
                }
                else if (text == "9" || text == "09")
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
        internal static string getMonthS_in(string text)
        {
            try
            {
                if (text == "1" || text == "01")
                {
                    return "01";
                }
                else if (text == "2" || text == "02")
                {
                    return "02";
                }
                else if (text == "3" || text == "03")
                {
                    return "03";
                }
                else if (text == "4" || text == "04")
                {
                    return "04";
                }
                else if (text == "5" || text == "05")
                {
                    return "05";
                }
                else if (text == "6" || text == "06")
                {
                    return "06";
                }

                else if (text == "7" || text == "07")
                {
                    return "07";
                }
                else if (text == "8" || text == "08")
                {
                    return "08";
                }
                else if (text == "9" || text == "09")
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


        public static IEnumerable bindMonth()
        {
            List<string> lst = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                lst.Add(i.ToString("00"));
            }
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


        //public static DataTable dv_dt; 
        public static String Format_Sample { get { try { return "dd/MM/yyyy"; } catch { return "dd/MM/yyyy"; } } }
        public static String Working_shift { get { try { return "Single"; } catch { return "Single"; } } }
        public static String set_minimun_hour_to_calculate_present { get { try { return "4"; } catch { return "4"; } } }









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



        internal static bool find_fee_collected(string fee_type, string session, string class_id)
        {
            SqlCommand cmd;
            string strQuery = "Select * from Student_Payment_History where Type=@Type and Session=@Session and Class_id=@Class_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Type", fee_type);
            cmd.Parameters.AddWithValue("@Session", session);
            cmd.Parameters.AddWithValue("@Class_id", class_id);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_mnthly_fee_collected(string fee_type, string session, string class_id, string month)
        {
            SqlCommand cmd;
            string strQuery = "Select * from Monthly_Fee_Collection_Slip where parameter=@parameter and session=@session and class=@class and Month=@Month";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class", class_id);
            cmd.Parameters.AddWithValue("@Month", month);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_mnthly_fee_collected_hostel(string fee_type, string session, string class_id, string month)
        {
            SqlCommand cmd;
            string strQuery = "Select * from Monthly_Fee_Collection_Slip where parameter=@parameter and session=@session and class=@class";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class", class_id);
            cmd.Parameters.AddWithValue("@Month", month);
            DataTable dt = My.GetDatas(cmd);
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


        internal static bool find_mnthly_transp_fee_collected(string fee_type, string session, string transportation_path_id, string month)
        {
            SqlCommand cmd;
            string strQuery = "Select t1.* from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.adno=t2.admissionserialnumber and t1.session=t2.session where t1.parameter=@parameter and t1.session=@session and t2.Transportation_Id=@Transportation_Id and t1.Month=@Month and t1.Content='TransportationFee'";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@Transportation_Id", transportation_path_id);
            cmd.Parameters.AddWithValue("@Month", month);
            DataTable dt = My.GetDatas(cmd);
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


        internal static bool find_disc_fee_collected(string fee_type, string cat, string sub_cat, string session, string class_id, string content_id, string para_id)
        {
            SqlCommand cmd;
            string strQuery = "select t1.id from dbo.[Typewise_fee_collection] t1 join Discount_Master t2 on t1.session=t2.session and t1.class_id=t2.Class_id and t1.content_id=t2.fee_head_id where t2.category_id=@category_id and t2.sub_category_id=@sub_category_id and t1.session=@session and t1.class_id=@class_id and t1.parameter=@parameter and t1.content_id=@content_id and t2.parameter_id=@parameter_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@parameter_id", para_id);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_mnth_disc_fee_collected(string fee_type, string cat, string sub_cat, string session, string class_id, string content_id, string para_id, string month_name)
        {
            SqlCommand cmd;
            string strQuery = "select * from Typewise_fee_collection t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber and t1.session=t2.session and t1.class_id=t2.Class_id where t2.Category_id=@category_id and t2.SubCategory_id=@sub_category_id and t1.session=@session and t1.class_id=@class_id and t1.parameter=@parameter and t1.content_id=@content_id and t1.month=@month";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@content_id", content_id);
            //cmd.Parameters.AddWithValue("@parameter_id", para_id);
            cmd.Parameters.AddWithValue("@month", month_name);
            DataTable dt = My.GetDatas(cmd);
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


        internal static bool find_mnth_disc_fee_collected_of_student(string fee_type, string cat, string sub_cat, string session, string class_id, string content_id, string para_id, string month_name, string adm_no)
        {
            SqlCommand cmd;
            string strQuery = "select t1.id from dbo.[Typewise_fee_collection] t1 join Discount_Master t2 on t1.session=t2.session and t1.class_id=t2.Class_id and t1.content_id=t2.fee_head_id where t2.category_id=@category_id and t2.sub_category_id=@sub_category_id and t1.session=@session and t1.class_id=@class_id and t1.parameter=@parameter and t1.content_id=@content_id and t2.parameter_id=@parameter_id and t1.month=@month and t1.admission_no=@admission_no";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@parameter_id", para_id);
            cmd.Parameters.AddWithValue("@month", month_name);
            cmd.Parameters.AddWithValue("@admission_no", adm_no);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_disc_fee_collected_stdntwise(string fee_type, string cat, string sub_cat, string session, string class_id, string content_id, string para_id, string admNo)
        {
            SqlCommand cmd;
            string strQuery = "select t1.id from dbo.[Typewise_fee_collection] t1 join Discount_Master t2 on t1.session=t2.session and t1.class_id=t2.Class_id and t1.content_id=t2.fee_head_id where t2.category_id=@category_id and t2.sub_category_id=@sub_category_id and t1.session=@session and t1.class_id=@class_id and t1.parameter=@parameter and t1.content_id=@content_id and t2.parameter_id=@parameter_id and t1.admission_no=@admission_no";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@category_id", cat);
            cmd.Parameters.AddWithValue("@sub_category_id", sub_cat);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@parameter_id", para_id);
            cmd.Parameters.AddWithValue("@admission_no", admNo);
            DataTable dt = My.GetDatas(cmd);
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
        internal static bool find_disc_fee_collected_adm_ann(string session, string class_id, string admNo, string content_id)
        {
            SqlCommand cmd;
            string strQuery = "select id from dbo.[Typewise_fee_collection] where  session=@session and t1.class_id=@class_id and content_id=@content_id  and admission_no=@admission_no and (parameter='AdmissionFee' or parameter='AnnualFee')";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@class_id", class_id);
            cmd.Parameters.AddWithValue("@content_id", content_id);
            cmd.Parameters.AddWithValue("@admission_no", admNo);
            DataTable dt = My.GetDatas(cmd);
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
        public static string get_studentname_prm(string admissionserialnumber)
        {

            DataTable dt = FillDatastatic("select top 1 studentname=REPLACE(studentname, '&nbsp;', '') from admission_registor where admissionserialnumber='" + admissionserialnumber + "' order by id desc ");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        private static DataTable GetDatas(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = My.conn;
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

        //-------------------------22/02/2022---------------

        internal string get_classid(string class1)
        {
            DataTable dt = FillData("Select course_id from Add_course_table where Course_Name='" + class1 + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["course_id"].ToString();
            }
        }
        internal string get_classname(string classid)
        {
            DataTable dt = FillData("Select Course_Name from Add_course_table where course_id='" + classid + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Course_Name"].ToString();
            }
        }
        internal static bool check_valid_dob(string std_class, string dob)
        {
            int dobyear = My.get_Year_by_date(dob);
            int crnt_year = My.getcyear();
            if (crnt_year > dobyear)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private static int getcyear()
        {
            string years = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
            return Convert.ToInt32(years);
        }

        private static int get_Year_by_date(string dob)
        {
            dob = dob.Substring(6, 4);
            return Convert.ToInt32(dob);
        }

        internal static bool find_fee_taken_date(string table, int pay_idate, string adm_no, string session)
        {
            SqlCommand cmd;
            string strQuery = "select * from " + table + " where Admission_no=@Admission_no and session=@session and idate>" + pay_idate + "";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Admission_no", adm_no);
            cmd.Parameters.AddWithValue("@session", session);
            DataTable dt = My.GetDatas(cmd);
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

        internal static bool find_mnth_fee_taken_date(string table, int pay_idate, string adm_no, string classid, string session)
        {
            SqlCommand cmd;
            string strQuery = "select * from " + table + " where Addmission_no=@Addmission_no and Session=@Session and Class_id=@Class_id and Idate>" + pay_idate + "";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Addmission_no", adm_no);
            cmd.Parameters.AddWithValue("@Session", session);
            cmd.Parameters.AddWithValue("@Class_id", classid);
            DataTable dt = My.GetDatas(cmd);
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

        public static string get_start_month()
        {
            DataTable dt = FillDatastatic("Select Month from Month_Index where Position='1'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Month"].ToString();
            }
        }
        public static string get_end_month()
        {
            DataTable dt = FillDatastatic("Select Month from Month_Index where Position='12'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Month"].ToString();
            }
        }
        public static string get_start_month(SqlConnection con)
        {
            DataTable dt = FillDatastatic("Select Month from Month_Index where Position='1'");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Month"].ToString();
            }
        }

        internal static void send_success_notice_fee(string Student_name, string class_name, string section, string roll_no, string admission_no, string amount, string notice_type, string notice_type_name, string session_id)
        {
            DataTable dtTemplate = My.dataTable("select * from Auto_notice_template where Status='1' and Notice_type='" + notice_type + "'");
            if (dtTemplate.Rows.Count > 0)
            {
                string messageTemplate = dtTemplate.Rows[0]["Notice_message"].ToString();
                string cdate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                string msgs = messageTemplate.Replace("#student_name#", Student_name).Replace("#class_name#", class_name).Replace("#section#", section).Replace("#roll_no#", roll_no).Replace("#amount#", amount).Replace("#admission_no#", admission_no).Replace("#date#", cdate);
                save_auto_send_notice_fee(admission_no, session_id, msgs, notice_type, notice_type_name, cdate);
            }
        }

        private static void save_auto_send_notice_fee(string admission_no, string session_id, string msgs, string notice_type_id, string notice_type_name, string cdate)
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
            if (My.InsertUpdateData(cmd))
            {
                sendpushNotice(admission_no, session_id, notice_type_id, notice_type_name, msgs);
            }
        }

        private static void sendpushNotice(string admission_no, string session_id, string notice_type_id, string notice_type_name, string msgs)
        {
            SqlCommand cmd = new SqlCommand(" select studentname,admissionserialnumber,gcm_id from admission_registor where admissionserialnumber='" + admission_no + "' and Session_id='" + session_id + "'");
            DataTable dt = InsertUpdate.GetData(cmd);
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
                        UsesCode.SendNotification(dr["gcm_id"].ToString(), ss);
                    }
                }
            }
        }


        internal static bool find_bus_fee_tkn(string fee_type, string session, string month_name)
        {
            SqlCommand cmd;
            string strQuery = "select id from dbo.[Typewise_fee_collection] where parameter=@parameter and session=@session and month=@month";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@month", month_name);
            DataTable dt = My.GetDatas(cmd);
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
        internal static bool find_bus_fee_tkn_std(string fee_type, string session, string month_name, string adm_no, string class_id)
        {
            SqlCommand cmd;
            string strQuery = "select id from dbo.[Typewise_fee_collection] where parameter=@parameter and session=@session and month=@month and Class_id=@Class_id and admission_no=@admission_no";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@parameter", fee_type);
            cmd.Parameters.AddWithValue("@session", session);
            cmd.Parameters.AddWithValue("@month", month_name);
            cmd.Parameters.AddWithValue("@Class_id", class_id);
            cmd.Parameters.AddWithValue("@admission_no", adm_no);
            DataTable dt = My.GetDatas(cmd);
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

        internal bool get_empid(string empcode)
        {
            SqlCommand cmd;
            string strQuery = "select Emp_Code from dbo.[PRL_Employee_Master] where Emp_Code=@Emp_Code  ";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Emp_Code", empcode);
            DataTable dt = My.GetDatas(cmd);
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

        internal static string character_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_character_prifix();
            string session = get_session();
            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                return pre_fix + "/" + session + "/" + bill;
            }
        }

        public static string get_character_prifix()
        {
            DataTable dt = FillDatastatic("Select Character_certificate_prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Character_certificate_prefix"].ToString();
            }
        }

        internal static string tc_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_tc_prifix();
            string session = get_session();
            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                return pre_fix + "/" + session + "/" + bill;
            }

        }

        public static string get_tc_prifix()
        {
            DataTable dt = FillDatastatic("Select Transfer_certificate_prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Transfer_certificate_prefix"].ToString();
            }
        }

        internal static string slc_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_slc_prifix();
            string session = get_session();
            return pre_fix + "/" + session + "/" + bill;
        }

        public static string get_slc_prifix()
        {
            DataTable dt = FillDatastatic("Select Leaving_certificate_prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Leaving_certificate_prefix"].ToString();
            }
        }

        internal static object get_college_name()
        {
            SqlCommand cmd = new SqlCommand("select Shortname from Firm_Details");
            DataTable dt = GetDataq(cmd);
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Shortname"].ToString();
            }
        }

        internal static string get_logo()
        {
            SqlCommand cmd = new SqlCommand("select logo from Firm_Details");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["logo"].ToString();
            }
        }

        internal static string MERCHANT_KEY()//rozor
        {
            SqlCommand cmd = new SqlCommand("select Murchnatkey_rozeror from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Murchnatkey_rozeror"].ToString();
            }
        }

        internal static string autkeyrozorkey()//rozor
        {
            SqlCommand cmd = new SqlCommand("select Rozror_Auth_Key from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Rozror_Auth_Key"].ToString();
            }
        }

        //=============================================CCAVENUE
        internal static string MERCHANT_KEY_CCAV()//CCAVENUE
        {
            SqlCommand cmd = new SqlCommand("select Murchnatkey_ccav from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Murchnatkey_ccav"].ToString();
            }
        }
        internal static string Working_KEY_CCAV()//CCAVENUE
        {
            SqlCommand cmd = new SqlCommand("select WorkingKey_ccav from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["WorkingKey_ccav"].ToString();
            }
        }
        internal static string strAccessCode()//CCAVENUE
        {
            SqlCommand cmd = new SqlCommand("select strAccessCode_ccav from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["strAccessCode_ccav"].ToString();
            }
        }
        //===================== // =============================================EZ Pay----------------=========*****************************
        internal static string Encryptionkey_EZPAY()
        {
            SqlCommand cmd = new SqlCommand("select Encryptionkey_EZPAY from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Encryptionkey_EZPAY"].ToString();
            }
        }
        internal static string MERCHANT_KEY_EZ_PAY()
        {

            SqlCommand cmd = new SqlCommand("select Murchnatkey_EZPAY from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Murchnatkey_EZPAY"].ToString();
            }
        }
        //--------------------------------Get E Pay -------------------------------

        internal static string Merchant_Key_Get_E_PAY()
        {
            SqlCommand cmd = new SqlCommand("select Murchnatkey_Getepay from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Murchnatkey_Getepay"].ToString();
            }
        }
        internal static string Encryptionkey_Get_E_PAY()
        {
            SqlCommand cmd = new SqlCommand("select Encryptionkey_Getepay from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Encryptionkey_Getepay"].ToString();
            }
        }

        internal static string IV_Get_E_PAY()
        {
            SqlCommand cmd = new SqlCommand("select iv_Getepay from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["iv_Getepay"].ToString();
            }

        }

        internal static string TerminalId_Get_E_PAY()
        {
            SqlCommand cmd = new SqlCommand("select TerminalId_Getepay from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["TerminalId_Getepay"].ToString();
            }
        }


        //---------------------------------Worldline -------------------------------------------
        internal static string Encryptionkey_Worldline_Get_Worldline()
        {

            SqlCommand cmd = new SqlCommand("select Encryptionkey_Worldline from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Encryptionkey_Worldline"].ToString();
            }
        }
        internal static string Encryption_IV_Get_Worldline()
        {

            SqlCommand cmd = new SqlCommand("select Worldline_Encryption_IV from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Worldline_Encryption_IV"].ToString();
            }
        }
        internal static string Merchant_Key_Get_Worldline()
        {
            SqlCommand cmd = new SqlCommand("select Murchnatkey_Worldline from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Murchnatkey_Worldline"].ToString();
            }
        }
        internal static string Autkeyrozorkey_Get_Worldline()
        {

            SqlCommand cmd = new SqlCommand("select Encryptionkey_Worldline from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Encryptionkey_Worldline"].ToString();
            }
        }
        //---------------------Billdesk ----------------------------
        internal static string Billdesk_ClientID()
        {
            SqlCommand cmd = new SqlCommand("select ClientID_Billdesk from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["ClientID_Billdesk"].ToString();
            }
        }

        internal static string Billdesk_SecretKey()
        {
            SqlCommand cmd = new SqlCommand("select MerchantID_Billdesk from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["MerchantID_Billdesk"].ToString();
            }
        }


        internal static string Billdesk_MerchantID()
        {
            SqlCommand cmd = new SqlCommand("select MerchantID_Billdesk from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["MerchantID_Billdesk"].ToString();
            }

        }
        internal static string Billdesk_ProxyUrl()
        {
            SqlCommand cmd = new SqlCommand("select ProxyUrl_Billdesk from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["ProxyUrl_Billdesk"].ToString();
            }
        }


        internal static string get_reg_id_from_order_id(string referenceNo)
        {
            SqlCommand cmd = new SqlCommand("select Admission_no  from  Payment_transaction_process where ordertrackingid='" + referenceNo + "'");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";

            }
            else
            {

                return dt.Rows[0][0].ToString();
            }
        }



        //==============================*****************************
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
                                        SqlCommand cmd8 = new SqlCommand("Select  top 1 Id from Attendance_Notification where Send_status=1 ");
                                        DataTable dt8 = InsertUpdate.GetData(cmd8);
                                        if (dt8.Rows.Count == 0)
                                        {
                                            toreturn = false;
                                        }
                                        else
                                        {
                                            toreturn = true;
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
                                SqlCommand cmd8 = new SqlCommand("Select  top 1 Id from Attendance_Notification where Send_status=1 ");
                                DataTable dt8 = InsertUpdate.GetData(cmd8);
                                if (dt8.Rows.Count == 0)
                                {
                                    toreturn = false;
                                }
                                else
                                {
                                    toreturn = true;
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

        internal static string get_top_one_class()
        {
            SqlCommand cmd = new SqlCommand("select top 1 course_id from Add_course_table order by Position asc ");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["course_id"].ToString();
            }
        }
        internal static string get_top_one_class_teacher(string teacherid)
        {
            SqlCommand cmd = new SqlCommand("Select top 1  course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + teacherid + "')  order by Position asc ");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["course_id"].ToString();
            }
        }
        internal bool get_sub_subject_add_or_not(string classid, string ddl_subjectid)
        {
            SqlCommand cmd = new SqlCommand("select Sub_Subject_id from Sub_Subject_Master  where course_id='" + classid + "'  and  Subject_id=" + ddl_subjectid + "");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //-------------------------------------------------------------------


        internal DataTable get_class_routine(string session, string classid, string section)
        {
            string querymain = "Select  Day  ";
            DataTable temp_dt = new DataTable();
            string query = " Select  distinct Class_period,(Select top 1 Period_Name from Class_Routine_period where Period=Class_Routine_Master.Class_period and Session_id=Class_Routine_Master.Session_id) as Period_Name   from  Class_Routine_Master   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "' ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                temp_dt.Columns.Add("Day");
                foreach (DataRow dr in dt.Rows)
                {
                    //temp_dt.Columns.Add("P_" + dr["Period_Name"].ToString());
                    // temp_dt.Columns.Add("P_" + dr["Period_Name"].ToString());
                    string gettimeperiod = get_time_span(dr["Period_Name"].ToString(), session, classid);
                    temp_dt.Columns.Add(dr["Period_Name"].ToString() + " " + gettimeperiod);
                }
                DataTable day_dt = My.dataTable("select * from    Day_Master  ");
                foreach (DataRow ddr in day_dt.Rows)
                {
                    string day = ddr["day"].ToString();


                    DataRow dr = temp_dt.NewRow();
                    dr["Day"] = day;
                    foreach (DataColumn column in temp_dt.Columns)
                    {

                        string column_name = column.ColumnName;

                        if (column_name != "Day")
                        {
                            //  string Class_period = column_name.Replace("P_", "");
                            string Class_period = column_name;
                            // string subjectid = getsubjectid(Class_period, classid, section, day, session);
                            dr[column_name] = find_routine_subject(Class_period, classid, section, day, session);

                        }
                    }

                    temp_dt.Rows.Add(dr);

                }

            }

            return temp_dt;
        }

        private object find_routine_subject(string Class_period, string classid, string section, string day, string session)
        {
            string subj_name = "";
            string[] stringSeparators = new string[] { " " };
            string[] arr = Class_period.Split(stringSeparators, StringSplitOptions.None);
            string Class_period22 = arr[0];
            string classname = arr[1];

            string periodid = My.get_period_id(Class_period22, session);
            SqlCommand cmd = new SqlCommand("Select  *,(select top 1 Subject_name from Subject_Master where course_id=Class_routine_period_subject_mapping.Class_id  and Subject_id=Class_routine_period_subject_mapping.Subject_id) as subjectname from Class_routine_period_subject_mapping where Session_id='" + session + "' and Class_id='" + classid + "' and Section='" + section + "' and Class_period='" + periodid + "' and Day='" + day + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    subj_name = subj_name + dr["subjectname"].ToString() + ",";
                }
                subj_name = subj_name.Remove(subj_name.Length - 1);
            }
            return subj_name;
        }


        private string get_time_span(string timeperiodname, string session, string classid)
        {
            string query = "Select   format(Start_Time, 'hh-mm-tt') as Start_Time_show,format(End_time, 'hh-mm-tt') as End_time_show  from Class_Routine_period where Session_id='" + session + "' and Period_Name='" + timeperiodname + "' and course_id='" + classid + "'   ";
            DataTable dt = FillData(query);
            if (dt.Rows.Count == 0)
            {

                return "";

            }
            else
            {

                return dt.Rows[0]["Start_Time_show"].ToString() + "--" + dt.Rows[0]["End_time_show"].ToString();
            }

        }
        private string getsubjectid(string Class_period, string classid, string section, string day, string session)
        {
            string[] stringSeparators = new string[] { " " };
            string[] arr = Class_period.Split(stringSeparators, StringSplitOptions.None);
            string Class_period22 = arr[0];
            string classname = arr[1];



            string periodid = My.get_period_id(Class_period22, session);
            SqlCommand cmd = new SqlCommand("Select  Subject_id from Class_Routine_Master where Session_id='" + session + "' and Class_id='" + classid + "' and Section='" + section + "' and Class_period='" + periodid + "' and Day='" + day + "'");
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
        private object fins_subjectnae(string subjectid)
        {
            SqlCommand cmd = new SqlCommand("Select  Subject_name    from Subject_Master where Subject_id='" + subjectid + "'   ");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "N/A";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }






        private static string get_period_id(string Class_period, string session)
        {
            SqlCommand cmd = new SqlCommand("select Period from Class_Routine_period where Period_Name='" + Class_period + "' and Session_id='" + session + "'");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Period"].ToString();
            }
        }
        internal static string get_empname(string userid)
        {
            SqlCommand cmd = new SqlCommand("select name from user_details where user_id='" + userid + "'");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["name"].ToString();
            }
        }

        internal static object get_period(string Session_id)
        {
            SqlCommand cmd = new SqlCommand(" select  count(*) from dbo.[Class_Routine_period] where Session_id='" + Session_id + "'");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }


        //----------------------------Teacher_class_section wise class chart-------------------------
        internal DataTable get_class_routine_teacher_wise(string session, string classid, string section, string tecaherid)
        {

            DataTable temp_dt = new DataTable();
            string query = " Select  distinct Class_period,(Select top 1 Period_Name from Class_Routine_period where Period=Class_Routine_Master_Teacher.Class_period and Session_id=Class_Routine_Master_Teacher.Session_id) as Period_Name   from  Class_Routine_Master_Teacher   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "' and Teacher_id='" + tecaherid + "'  ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                temp_dt.Columns.Add("Day");
                foreach (DataRow dr in dt.Rows)
                {
                    string gettimeperiod = get_time_span(dr["Period_Name"].ToString(), session, classid);
                    temp_dt.Columns.Add(dr["Period_Name"].ToString() + " " + gettimeperiod);
                    //temp_dt.Columns.Add("P_" + dr["Period_Name"].ToString());
                }
                DataTable day_dt = My.dataTable("select * from    Day_Master  ");
                foreach (DataRow ddr in day_dt.Rows)
                {
                    string day = ddr["day"].ToString();


                    DataRow dr = temp_dt.NewRow();
                    dr["Day"] = day;
                    foreach (DataColumn column in temp_dt.Columns)
                    {
                        string column_name = column.ColumnName;
                        if (column_name != "Day")
                        {
                            string Class_period = column_name;
                            string subjectid = getsubjectid_Teacher(Class_period, classid, section, day, session, tecaherid);
                            dr[column_name] = fins_subjectnae(subjectid);

                        }
                    }

                    temp_dt.Rows.Add(dr);

                }

            }

            return temp_dt;
        }

        private string getsubjectid_Teacher(string Class_period, string classid, string section, string day, string session, string teacherid)
        {
            string[] stringSeparators = new string[] { " " };
            string[] arr = Class_period.Split(stringSeparators, StringSplitOptions.None);
            string Class_period22 = arr[0];
            string classname = arr[1];
            string periodid = My.get_period_id(Class_period22, session);

            SqlCommand cmd = new SqlCommand("Select  Subject_id from Class_Routine_Master_Teacher where Session_id='" + session + "' and Class_id='" + classid + "' and Section='" + section + "' and Class_period='" + periodid + "' and Day='" + day + "' and Teacher_id='" + teacherid + "'");
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




        internal DataTable get_class_routine_subjectteacher_wise(string session, string classid, string section)
        {
            string subjectid = "";
            DataTable temp_dt = new DataTable();
            string query = " Select  distinct Class_period,(Select top 1 Period_Name from Class_Routine_period where Period=Class_Routine_Master_Teacher.Class_period and Session_id=Class_Routine_Master_Teacher.Session_id) as Period_Name   from  Class_Routine_Master_Teacher   where   Session_id=" + session + " and  Class_id=" + classid + " and  Section='" + section + "'    ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                temp_dt.Columns.Add("Day");
                foreach (DataRow dr in dt.Rows)
                {
                    string gettimeperiod = get_time_span(dr["Period_Name"].ToString(), session, classid);
                    temp_dt.Columns.Add(dr["Period_Name"].ToString() + " " + gettimeperiod);
                }
                DataTable day_dt = My.dataTable("select * from    Day_Master  ");
                foreach (DataRow ddr in day_dt.Rows)
                {
                    string day = ddr["day"].ToString();


                    DataRow dr = temp_dt.NewRow();
                    dr["Day"] = day;
                    foreach (DataColumn column in temp_dt.Columns)
                    {
                        string column_name = column.ColumnName;
                        if (column_name != "Day")
                        {
                            string Class_period = column_name;
                            subjectid = getsubjectid_Teacher(Class_period, classid, section, day, session);



                            string[] stringSeparators = new string[] { "_" };
                            string[] arr = subjectid.Split(stringSeparators, StringSplitOptions.None);
                            string first = arr[0];

                            if (first == "0")
                            {
                                dr[column_name] = "N/A";
                            }
                            else
                            {
                                string second = arr[1];


                                dr[column_name] = fins_subjectnae(first) + " {" + second + "}";


                            }


                        }
                    }

                    temp_dt.Rows.Add(dr);

                }

            }

            return temp_dt;
        }

        private string getsubjectid_Teacher(string Class_period, string classid, string section, string day, string session)
        {
            string[] stringSeparators = new string[] { " " };
            string[] arr = Class_period.Split(stringSeparators, StringSplitOptions.None);
            string Class_period22 = arr[0];
            string classname = arr[1];
            string periodid = My.get_period_id(Class_period22, session);

            SqlCommand cmd = new SqlCommand("Select  Subject_id,(Select top 1 name from user_details where user_id=Class_Routine_Master_Teacher.Teacher_id) as teachername from Class_Routine_Master_Teacher where Session_id='" + session + "' and Class_id='" + classid + "' and Section='" + section + "' and Class_period='" + periodid + "' and Day='" + day + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Subject_id"].ToString() + "_" + dt.Rows[0]["teachername"].ToString();
            }
        }

        //------------------------Day_Wise_Teacher Chart--------------------------------



        internal DataTable get_class_routine_day_wise(string session, string Day1, string classid)
        {
            string subjectid = "";
            DataTable temp_dt = new DataTable();
            string query = " Select  distinct Class_period,(Select top 1 Period_Name from Class_Routine_period where Period=Class_Routine_Master_Teacher.Class_period and Session_id=Class_Routine_Master_Teacher.Session_id) as Period_Name   from  Class_Routine_Master_Teacher   where   Session_id=" + session + "  and Class_id='" + classid + "'  ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                temp_dt.Columns.Add("Teacher");
                foreach (DataRow dr in dt.Rows)
                {
                    string gettimeperiod = get_time_span(dr["Period_Name"].ToString(), session, classid);
                    temp_dt.Columns.Add(dr["Period_Name"].ToString() + " " + gettimeperiod);
                }
                DataTable day_dt = My.dataTable("select name,user_id from    user_details where User_Type='Teacher' and Istatus='1'  ");
                foreach (DataRow ddr in day_dt.Rows)
                {
                    string day = ddr["name"].ToString();


                    DataRow dr = temp_dt.NewRow();
                    dr["Teacher"] = day;
                    foreach (DataColumn column in temp_dt.Columns)
                    {
                        string column_name = column.ColumnName;
                        if (column_name != "Teacher")
                        {
                            string teacherid = ddr["user_id"].ToString();
                            string Class_period = column_name;
                            subjectid = getsubjectid_daywise(teacherid, Day1, session, Class_period);

                            if (subjectid == "0")
                            {
                                dr[column_name] = "N/A";

                            }
                            else
                            {
                                string[] stringSeparators = new string[] { "_" };
                                string[] arr = subjectid.Split(stringSeparators, StringSplitOptions.None);
                                string subjectidnew = arr[0];
                                string classname = arr[1];
                                string section = arr[2];
                                // string teachername = arr[3];


                                dr[column_name] = fins_subjectnae(subjectidnew) + " {" + classname + "-" + section + "}";

                            }

                        }
                    }

                    temp_dt.Rows.Add(dr);

                }

            }

            return temp_dt;
        }

        private string getsubjectid_daywise(string teacherid, string day, string session, string Class_period)
        {
            string[] stringSeparators = new string[] { " " };
            string[] arr = Class_period.Split(stringSeparators, StringSplitOptions.None);
            string Class_period22 = arr[0];
            string classname = arr[1];
            string periodid = My.get_period_id(Class_period22, session);

            SqlCommand cmd = new SqlCommand("Select  Subject_id,Section,(Select top 1 name from user_details where user_id=Class_Routine_Master_Teacher.Teacher_id) as teachername,(Select top 1 Course_Name from Add_course_table where course_id=Class_Routine_Master_Teacher.Class_id) as Course_Name from Class_Routine_Master_Teacher where Session_id='" + session + "' and Class_period='" + periodid + "' and Day='" + day + "' and Teacher_id='" + teacherid + "'");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Subject_id"].ToString() + "_" + dt.Rows[0]["Course_Name"].ToString() + "_" + dt.Rows[0]["Section"].ToString();
            }
        }
        internal DataTable get_class_routine_day_wise_selectedteacher(string session, string Day1, string teacherid1, string classid)
        {
            string subjectid = "";
            DataTable temp_dt = new DataTable();
            string query = " Select  distinct Class_period,(Select top 1 Period_Name from Class_Routine_period where Period=Class_Routine_Master_Teacher.Class_period and Session_id=Class_Routine_Master_Teacher.Session_id) as Period_Name   from  Class_Routine_Master_Teacher   where   Session_id=" + session + "   and Class_id='" + classid + "'   ORDER BY  Class_period";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                // temp_dt.Columns.Add("Teacher");
                foreach (DataRow dr in dt.Rows)
                {

                    string gettimeperiod = get_time_span(dr["Period_Name"].ToString(), session, classid);
                    temp_dt.Columns.Add(dr["Period_Name"].ToString() + " " + gettimeperiod);
                }
                DataTable day_dt = My.dataTable("select name,user_id from    user_details where User_Type='Teacher' and Istatus='1' and  user_id='" + teacherid1 + "' ");
                foreach (DataRow ddr in day_dt.Rows)
                {
                    // string day = ddr["name"].ToString();


                    DataRow dr = temp_dt.NewRow();
                    // dr["Teacher"] = day;
                    foreach (DataColumn column in temp_dt.Columns)
                    {
                        string column_name = column.ColumnName;
                        if (column_name != "Teacher")
                        {
                            string teacherid = ddr["user_id"].ToString();
                            string Class_period = column_name;
                            subjectid = getsubjectid_daywise(teacherid, Day1, session, Class_period);

                            if (subjectid == "0")
                            {
                                dr[column_name] = "N/A";

                            }
                            else
                            {
                                string[] stringSeparators = new string[] { "_" };
                                string[] arr = subjectid.Split(stringSeparators, StringSplitOptions.None);
                                string subjectidnew = arr[0];
                                string classname = arr[1];
                                string section = arr[2];
                                // string teachername = arr[3];


                                dr[column_name] = fins_subjectnae(subjectidnew) + " {" + classname + "-" + section + "}";

                            }

                        }
                    }

                    temp_dt.Rows.Add(dr);

                }

            }

            return temp_dt;
        }


        internal string get_firm_name()
        {
            SqlCommand cmd = new SqlCommand("Select firm_name from Firm_Details  ");
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["firm_name"].ToString();
            }
        }

        internal static string chnage_classuniqno()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random();
            int tempo = random.Next(1, 999);

            string bill = tempo.ToString() + date;
            string pre_fix = get_slip_prifix_chnageclass();
            string session = get_session();


            return pre_fix + "/" + session + "/" + bill;
        }


        //---------------------------------------------Update  Table---------------
        internal static string getdata(string session_name)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from School_System_setting where session_name='" + session_name + "' ", conn);
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
                My.exeSql(@"Create Table dbo.[School_System_setting] ( 
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
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from School_System_setting where session_name='" + session_name + "' ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["session_name"] = session_name;
                    dr["session_value"] = "1.0.0.3";
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
                        My.exeSql(@"Create Table dbo.[HMS_System_setting] ( 
    [id] int NOT NULL  Identity (1,1)  PRIMARY KEY ,
    [session_name] varchar(500)  NULL ,
    [session_value] varchar(500)  NULL  
    );");
                        setdata(session_name, session_value);
                    }
                    catch (Exception e1)
                    {

                        My.submitException(e1, "setdata");

                    }
                }
                else
                {

                }
            }

        }


        public static List<String> version_list = new List<string>();
        public static List<String> version_code_list = new List<string>();
        public static Dictionary<String, List<String>> version_query_list = new Dictionary<String, List<String>>();





        internal static string get_school_name()
        {

            DataTable dt = FillDatastatic("Select firm_name from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["firm_name"].ToString();
            }
        }
        public void addparentslogin(string admissionserialnumber, string mobilenumber, string fathername)
        {
            string userid;
            string querychk = "SELECT * FROM Parent_Student_Mapping WHERE Student_id = '" + admissionserialnumber + "'";
            DataTable dt = FillData(querychk);

            if (dt.Rows.Count == 0)
            {
                // Check if parent already exists with this mobile number
                string querychk1 = "SELECT * FROM Parent_Login_Details WHERE Mobile = '" + mobilenumber + "'";
                DataTable dt1 = FillData(querychk1);

                if (dt1.Rows.Count == 0)
                {
                    userid = get_parents_user_id();

                    string query = @"INSERT INTO Parent_Login_Details 
                         (Name, User_id, Password, Mobile, Status, Created_by, Created_date) 
                         VALUES (@Name, @User_id, @Password, @Mobile, @Status, @Created_by, 
                                 DATEADD(HOUR, 5, DATEADD(MINUTE, 30, GETUTCDATE())))";

                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@Name", fathername ?? "");
                        cmd.Parameters.AddWithValue("@User_id", userid);
                        cmd.Parameters.AddWithValue("@Password", "12345");  // 🔒 Better: hash this later
                        cmd.Parameters.AddWithValue("@Mobile", mobilenumber);
                        cmd.Parameters.AddWithValue("@Status", "Active");
                        cmd.Parameters.AddWithValue("@Created_by", "Dev");

                        if (My.InsertUpdateData(cmd))
                        {
                            send_data_Parent_Student_Mapping(admissionserialnumber, userid);
                        }
                    }
                }
                else
                {
                    // Parent already exists → map student to this user id
                    userid = dt1.Rows[0]["User_id"].ToString();
                    send_data_Parent_Student_Mapping(admissionserialnumber, userid);
                }
            }
            else
            {
                // Student already mapped, nothing to do
            }

        }
        private string get_parents_user_id()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("hhmmss");
            string pwd = "";
            bool duplicateid = false;
            Random random = new Random(DateTime.Now.Millisecond);
            int i = 1;
            int j = 9999;
            do
            {
                int k = random.Next(i, j);

                pwd = date + k.ToString();
                duplicateid = check_dauplicate_id(pwd);

                if (duplicateid == true)
                {

                }
            } while (duplicateid == false);

            return pwd;

        }
        private bool check_dauplicate_id(string User_id)
        {
            DataTable dt = FillData("Select  Password  from Parent_Login_Details where User_id='" + User_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void send_data_Parent_Student_Mapping(string admissionserialnumber, string userid)
        {
            string querychk = "Select *   from Parent_Student_Mapping where Student_id='" + admissionserialnumber + "'";
            DataTable dt = FillData(querychk);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Parent_Student_Mapping (Parent_id,Student_id) values (@Parent_id,@Student_id)";
                SqlCommand cmd;
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Parent_id", userid);
                cmd.Parameters.AddWithValue("@Student_id", admissionserialnumber);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        internal static string get_birthday_message()
        {
            DataTable dt = FillDatastatic("Select Message from Birthday_Message_Template where Status='Active' ");
            if (dt.Rows.Count == 0)
            {
                return "Hapyy Birthday";
            }
            else
            {
                return dt.Rows[0]["Message"].ToString();
            }

        }

        internal static string get_chek_message_send_studentid()
        {
            string Idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string CategoryID = "";

            DataTable dt = FillDatastatic("Select  User_Id from PushNotification_Details where title='Birthday' and Session_id=" + My.get_session_id() + " and Idate=" + Idate + "");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CategoryID1 = dt.Rows[i]["User_Id"].ToString();
                    if (CategoryID == "")
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

        internal static string get_old_bill_no(string slip_no, string Branch_id)
        {
            DataTable dt = FillDatastatic("Select  Slip_no from admission_registor_Change_admission_no_history where Slip_no='" + slip_no + "' and Branch_id=" + Branch_id + "  ");
            if (dt.Rows.Count == 0)
            {
                return " Sorry you entered slip is not exist in delete bill history";
            }
            else
            {
                DataTable dt1 = FillDatastatic("Select  User_Id from Student_Payment_History where Slip_no='" + slip_no + "' and Branch=" + Branch_id + "  ");
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



        internal static string get_top_one_subject(string class_id, string Section, string regid)
        {
            string query = " Select   top 1 Sub_id from Subject_Mapping_New     where  Class_id='" + class_id + "' and  Section='" + Section + "' and Admission_no='" + regid + "'   ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }


        public static string get_sess_prm(string session)
        {
            string query = " select  session_id  from dbo.[session_details] where Session='" + session + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static string get_top_one_path()
        {
            string query = " select top 1 TransportationPath_id  from dbo.[TransportationPath]    ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }
        private static int get_startingmonth()
        {
            string query = " select top 1  Month_Id  from dbo.[Month_Index] order by Position asc ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }






        internal object get_session_date_dat_time(string date)
        {
            string day = date.Substring(0, 2);
            string month = date.Substring(3, 2);
            string year = date.Substring(6, 4);
            string session = year + "-" + (Convert.ToInt32(year) + 1);
            return get_sess_prm(session);
        }

        internal string get_date_uploading_Homework(string Home_Work_id)
        {
            string query = " Select    Upload_Date from Homework_Details  where Home_Work_id='" + Home_Work_id + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal string get_date_uploading_askdout(string Id)
        {
            string query = " Select Date from Student_doubt_list  where Id='" + Id + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal static object get_admission_condition()
        {
            try
            {
                DataTable dt = FillDatastatic("Select Is_Annual_Admission_fee from Firm_Details   ");
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    if (dt.Rows[0][0].ToString() == "True")
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch
            {
                return "0";
            }
        }

        internal string hmac_sha256(string order_id, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(order_id);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);

            byte[] bytes = cryptographer.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        internal static string MERCHANT_KEY_rozeror_secret()
        {
            SqlCommand cmd = new SqlCommand("select rozeror_secret from App_Setting");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["rozeror_secret"].ToString();
            }
        }

        internal static string url()
        {
            SqlCommand cmd = new SqlCommand("select URL from Global");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["URL"].ToString();
            }
        }
        public void bind_all_ddl_with_id_cap_All(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("ALL", "0"));
        }

        public void bind_all_ddl_with_id_notselect(DropDownList ddl, string strQuery)
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
        }
        internal static bool cheknum_fine(string val)
        {
            double toretun = 0;
            bool toretun2 = false;
            try
            {
                toretun = Convert.ToDouble(val);
                toretun2 = true;
            }
            catch
            {
            }
            return toretun2;
        }

        internal static bool get_status_ispaymnet_on(string class_id)
        {
            string query = " Select is_payment_gatway_enabled from App_Setting     ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "True")
                {
                    string queryx = "Select * from Payment_gateway_active_class where Status=1";
                    DataTable dtx = FillDatastatic(queryx);
                    if (dtx.Rows.Count > 0)
                    {
                        string status = "0";
                        foreach (DataRow dr in dtx.Rows)
                        {
                            if (dr["Class_id"].ToString() == class_id)
                            {
                                status = "1";
                            }
                        }
                        if (status == "1")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        internal static bool get_status_ispaymnet_on_other()
        {
            string query = " Select is_payment_gatway_enabled from App_Setting     ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        internal static int get_paymnet_chekseeting()
        {
            try
            {
                string query = " Select Month_selection from Firm_Details     ";
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch
            {
                return 1;
            }

        }

        public bool IsDivisible(int x, int n)
        {
            return (x % n) == 0;
        }

        internal void bind_ddl_year(DropDownList ddlyear)
        {
            int starte = 2022;
            int endyaer = My.getcyear();
            ArrayList ar = new ArrayList();
            ar.Add("Select");
            for (int i = starte; i <= endyaer; i++)
            {
                ar.Add(i);
            }
            ddlyear.DataSource = ar;
            ddlyear.DataBind();
        }
        internal object get_payment_Getwey_Type()
        {
            string returN = "";
            string query = " Select Getwey_Type from App_Setting  where is_payment_gatway_enabled=1  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                returN = dt.Rows[0][0].ToString();
            }
            return returN;
        }


        internal object get_payment_Getwey_Type_monthly(string class_id)
        {
            string returN = "";
            string query = " Select Getwey_Type from App_Setting  where is_payment_gatway_enabled=1  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                string queryx = "Select Gateway_type from Payment_gateway_active_class where Status=1 and Class_id='" + class_id + "'";
                DataTable dtx = FillDatastatic(queryx);
                if (dtx.Rows.Count > 0)
                {
                    returN = dtx.Rows[0][0].ToString();
                }
                else
                {
                    returN = dt.Rows[0][0].ToString();
                }
            }
            return returN;
        }
        internal static string URL()
        {
            string query = " Select URL from Global    ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal object get_slip_no(string orderid, string Admission_no)
        {
            string query = "  Select Slip_no,Addmission_no from  Student_Payment_History  where Pay_mode_transaction_no='" + orderid + "'  and Addmission_no='" + Admission_no + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }

        }



        internal static bool get_selected_monthpaid(string admSionNo, string session, string month)
        {
            string query = "  select  * from dbo.[Typewise_fee_collection] where parameter='MonthlyFee' and  admission_no='" + admSionNo + "' and month='" + month + "' and session='" + session + "' and status='Paid'   ";

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {

                string query2 = "  select  * from dbo.[Typewise_fee_collection] where parameter='MonthlyFee' and  admission_no='" + admSionNo + "' and month='" + month + "' and session='" + session + "' and status='Dues'   ";

                DataTable dt2 = FillDatastatic(query2);
                if (dt2.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;

                }


            }
        }

        internal string get_sttratingmonth()
        {
            string query = "  select top 1 Month from dbo.[Month_Index]  order by Position asc     ";

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal string get_sttratingmonthid(string Month)
        {
            string query = "  select   Month_Id from dbo.[Month_Index]   where Month='" + Month + "'    ";

            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        internal static string get_transport_assigned_id()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("yyyyMMddhhmmss");
            Random random = new Random(DateTime.Now.Millisecond);
            int tempo = random.Next(1, 999);
            string session = get_session();
            string bill = tempo.ToString() + date + session;
            return bill;

        }

        internal void bind_all_list_with_id(ListBox lst_class, string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                lst_class.DataTextField = "Select";
                lst_class.DataValueField = "0";
            }
            else
            {
                lst_class.DataTextField = dt.Columns[0].ToString();
                lst_class.DataValueField = dt.Columns[1].ToString();
            }

            lst_class.DataSource = dt;
            lst_class.DataBind();
            lst_class.Items.Insert(0, new ListItem("Select", "0"));
        }

        internal void bind_all_checkboxlist(Saplin.Controls.DropDownCheckBoxes DropDownCheckBoxes11, string query)
        {
            SqlConnection conn = new SqlConnection(My.conn);


            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "tests");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                DropDownCheckBoxes11.DataTextField = "Select";
                DropDownCheckBoxes11.DataValueField = "Select";

            }

            else
            {
                DropDownCheckBoxes11.DataTextField = ds.Tables[0].Columns[0].ToString();
                DropDownCheckBoxes11.DataValueField = ds.Tables[0].Columns[1].ToString();
            }
            DropDownCheckBoxes11.DataSource = ds.Tables[0];
            DropDownCheckBoxes11.DataBind();
        }

        internal object getdayname(string Examination_Date)
        {
            try
            {
                DateTime d1 = DateTime.ParseExact(Examination_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                return d1.ToString("dddd");
            }
            catch
            {
                return "0";
            }
        }




        public string monthYear()
        {
            return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM/yyyy");
        }

        internal static Dictionary<string, object> get_selected_studentinfo(string admission_no, string sessionid, string branchid)
        {
            My mycode = new My();
            Dictionary<string, object> dc = new Dictionary<string, object>();
            // string query = "Select * from admission_registor   where admissionserialnumber='" + admission_no + "' and Session_Id=" + sessionid + " and Branch_id='" + branchid + "'";
            string query = "Select * from admission_registor   where admissionserialnumber='" + admission_no + "' and Session_Id=" + sessionid + " and Branch_id='" + branchid + "' and Transfer_Status in('NT','New') and Status='1' ";
            SqlCommand cmd;

            cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
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
                dc["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();
                dc["transportationtaken"] = dt.Rows[0]["transportationtaken"].ToString();
            }
            return dc;
        }
        internal static Dictionary<string, object> get_selected_studentinfo_SubjMap(string admission_no, string sessionid, string branchid)
        {
            My mycode = new My();
            Dictionary<string, object> dc = new Dictionary<string, object>(); 
            string query = "Select * from admission_registor   where admissionserialnumber='" + admission_no + "' and Session_Id=" + sessionid + " and Branch_id='" + branchid + "' and Status='1' ";
            SqlCommand cmd; 
            cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
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
                dc["Transfer_Status"] = dt.Rows[0]["Transfer_Status"].ToString();
                dc["transportationtaken"] = dt.Rows[0]["transportationtaken"].ToString();
            }
            return dc;
        }

        internal string format_otherfee(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_other_fee_prifix();
            string session = get_session();


            return pre_fix + "/" + session + "/" + bill;
        }

        private string get_other_fee_prifix()
        {
            try
            {
                string query = "  select   other_fee_prefix from dbo.[Firm_Details]      ";

                DataTable dt = FillDatastatic(query);
                if (dt.Rows.Count == 0)
                {
                    return "0";
                }
                else
                {
                    return dt.Rows[0]["other_fee_prefix"].ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public int get_no_day_towdateselection(string startdate, string enddate)
        {
            DateTime startmont = DateTime.ParseExact(startdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime etartmont = DateTime.ParseExact(enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // TimeSpan day = (etartmont - startmont);

            System.TimeSpan diff = etartmont.Subtract(startmont);
            int totaldays = Convert.ToInt32(diff.Days) + 1;

            if (totaldays == 0)
            {
                return 1;
            }

            else
            {
                return totaldays;
            }
        }

        internal static string get_top_one_section()
        {
            SqlCommand cmd = new SqlCommand("select top 1 Section from admission_registor order by Section asc ");
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "A";
            }
            else
            {
                return dt.Rows[0]["Section"].ToString();
            }
        }

        internal static object get_Is_roll_no_class_attendance()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select Is_roll_no_class_attendance   from Firm_Details ");
                DataTable dt = GetDataq(cmd);
                if (dt.Rows.Count == 0)
                {
                    return "1";
                }
                else
                {
                    return dt.Rows[0]["Is_roll_no_class_attendance"].ToString();
                }
            }
            catch
            {
                return "1";
            }

        }


        public string getbranchid(string userid)
        {
            SqlCommand cmd = new SqlCommand("Select Branch_id from user_details where user_id='" + userid + "'   ");
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

        internal static string get_next_month(string month_name)
        {
            string counter = "0";
            switch (month_name)
            {
                case "January":
                    counter = "February";
                    break;
                case "February":
                    counter = "March";
                    break;

                case "March":
                    counter = "April";
                    break;

                case "April":
                    counter = "May";
                    break;
                case "May":
                    counter = "June";
                    break;
                case "June":
                    counter = "July";
                    break;

                case "July":
                    counter = "August";
                    break;
                case "August":
                    counter = "September";
                    break;

                case "September":
                    counter = "October";
                    break;

                case "October":
                    counter = "November";
                    break;
                case "November":
                    counter = "December";
                    break;

                case "December":
                    counter = "January";
                    break;
            }
            return counter;
        }
        internal static string get_top_one_class_teacher(string classid, string section, string Sessionid)
        {
            string query = "Select top 1 ud.user_id from user_details ud join Class_Routine_Master_Teacher crmt on crmt.Teacher_id=ud.user_id where crmt.Class_id='" + classid + "' and crmt.Section='" + section + "' and crmt.Session_id=" + Sessionid + " order by ud.name ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetDataq(cmd);

            if (dt.Rows.Count == 0)
            {
                return "0";

            }
            else
            {

                return dt.Rows[0][0].ToString();
            }
        }

        public void bind_ddl_no_select(DropDownList ddl, string strQuery)
        {
            ddl.DataTextField = "";
            ddl.DataValueField = "";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = InsertUpdate.GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        public static string get_firm_id()
        {
            DataTable dt = FillDatastatic("Select firm_id from Firm_Details");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["firm_id"].ToString();
            }
        }


        internal static object get_fine_repat_no()
        {
            DataTable dt = FillDatastatic("Select Is_fine_repeat from session_details  ");
            if (dt.Rows.Count == 0)
            {
                return "No";
            }
            else
            {
                if (dt.Rows[0]["firm_id"].ToString() == "true")
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }

            }

        }
        internal static string get_short_school_name()
        {
            DataTable dt = FillDatastatic("Select Shortname from Firm_Details    ");
            if (dt.Rows.Count == 0)
            {
                return "N/A";

            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        public void bind_all_ddl_with_id_cap_NA_All(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("NA", "0"));
            ddl.Items.Insert(1, new ListItem("ALL", "555"));
        }


        internal static string get_month_name_no_fee_taken(string admission_no, string session)
        {
            string toreturnmonthname = "0";
            DataTable dt = FillDatastatic(" select Month   from dbo.[Month_Index] order by Position");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Monthname = dt.Rows[i]["Month"].ToString();
                    bool chk_month = getmonth_status(Monthname, admission_no, session);
                    if (chk_month == true)
                    {
                        toreturnmonthname = Monthname;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return toreturnmonthname;
        }
        private static bool getmonth_status(string monthname, string admission_no, string session)
        {
            string query = " select   * from dbo.[Typewise_fee_collection] where session='" + session + "' and admission_no='" + admission_no + "' and parameter='MonthlyFee' and month='" + monthname + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static string get_monthename_paymnet_slip(string admissionno, string slipno)
        {
            string CategoryID = "";
            DataTable dt = FillDatastatic("Select  distinct Month from Monthly_Fee_Collection_Slip where adno='" + admissionno + "' and slipno='" + slipno + "' ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CategoryID1 = dt.Rows[i]["Month"].ToString();
                    if (CategoryID == "")
                    {
                        CategoryID = CategoryID1;
                    }
                    else
                    {
                        CategoryID = CategoryID + "," + CategoryID1;

                    }
                }
            }

            return CategoryID;
        }

        public static string get_session_id_onlinereg()
        {
            DataTable dt = FillDatastatic("Select session_id from session_details where Old_Use_Mode='2'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["session_id"].ToString();
            }
        }

        internal static string get_reg_apply_session()
        {
            DataTable dt = FillDatastatic("Select Session from session_details where Old_Use_Mode='2'  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Session"].ToString();
            }
        }

        internal static int get_no_seat_current_session(string sessionid, string classid, string Test_id)
        {
            int startingdate = get_starting_data(sessionid, classid, Test_id);
            DataTable dt = FillDatastatic("Select count(Id) from Online_Admission where Session_id=" + sessionid + " and Class_id=" + classid + " and Payment_Status='Paid'  and idate>=" + startingdate + " and Test_id='" + Test_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }

        private static int get_starting_data(string sessionid, string classid, string Test_id)
        {
            DataTable dt = FillDatastatic("Select start_Idate from Online_reg_fees where Session_id=" + sessionid + " and Class_id=" + classid + " and Test_id='" + Test_id + "' ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["start_Idate"].ToString());
            }
        }

        internal static int get_no_of_fill_from_seat(string Session_id, string Class_id, string Test_id)
        {
            DataTable dt = FillDatastatic("Select no_application from Online_reg_fees where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Test_id='" + Test_id + "'  ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }


        internal static bool print_bill()
        {
            DataTable dt = FillDatastatic("Select Is_print_Slip from Firm_Details    ");
            if (dt.Rows.Count == 0)
            {
                return false;

            }
            else
            {
                if (dt.Rows[0][0].ToString() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }


        internal static string dob_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_dob_prifix();
            string session = get_session();
            return pre_fix + "/" + session + "/" + bill;
        }
        public static string get_dob_prifix()
        {
            DataTable dt = FillDatastatic("Select Dob_certificate_prefix from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Dob_certificate_prefix"].ToString();
            }
        }


        internal static string bonafide_format(string column_name)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_bonafide_prifix();
            string session = get_session();


            string[] stringSeparatorss = new string[] { "-" };
            string[] arr = session.Split(stringSeparatorss, StringSplitOptions.None);
            string s_session = arr[0];
            string e_session = arr[1];
            s_session = s_session.Substring(2, 2);
            e_session = e_session.Substring(2, 2);

            return pre_fix + "/" + s_session + "-" + e_session + "/" + bill;
        }

        public static string get_bonafide_prifix()
        {
            DataTable dt = FillDatastatic("Select Bonafied_prefix from Firm_Details");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Bonafied_prefix"].ToString();
            }
        }

        internal static string income_format(string column_name, string firmId)
        {
            string bill = My.auto_serial1(column_name);
            string pre_fix = get_income_prifix();
            string session = get_session();
            if (firmId == "GPSKTR")
            {
                session = "2026";
            }
            if (pre_fix == "")
            {
                return bill;
            }
            else
            {
                return pre_fix + "/" + session + "/" + bill;
            }
        }
        public static string get_income_prifix()
        {
            DataTable dt = FillDatastatic("Select Income_certificate_prefix from Firm_Details");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Income_certificate_prefix"].ToString();
            }
        }
        internal static string get_top_one_test_name(string selectedValue)
        {
            DataTable dt = FillDatastatic("Select top 1 Test_id from Online_reg_exam_test_master where Session_id=" + selectedValue + "   order by id desc ");
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Test_id"].ToString();
            }
        }

        internal static int check_start_months(int pay_month, int s_year)
        {
            string pay_month_two_digit = My.getMonthS_twoDigit(pay_month.ToString());
            string sessionMonth = get_starting_month();
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

        private static string get_starting_month()
        {
            string query = "select top 1 Month_Id from dbo.[Month_Index]  order by Position asc";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        internal static int get_no_seat_current_session_employee_hiring(string sessionid, string Apply_for, string Hiring_id)
        {
            DataTable dt = FillDatastatic("Select count(Id) from Employee_Online_Apply where Session_id=" + sessionid + " and Apply_for='" + Apply_for + "' and Hiring_id=" + Hiring_id + " and Payment_Status='Paid'  ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }

        }
        public void bind_ddlall_na_subject_type(DropDownList ddl, string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("SELECT TYPE OF THE POST");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();

        }

        public void bind_ddlall_na_subject(DropDownList ddl, string strQuery)
        {

            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = GetData(cmd);
            int rowcount = dt.Rows.Count;
            ArrayList ar = new ArrayList();
            ar.Add("SELECT SUBJECT");
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr[0].ToString());
            }
            ddl.DataSource = ar;
            ddl.DataBind();
        }

        internal static string get_top_onle_subject(string regid, string Session_id)
        {
            string query = "select top 1 Sub_id from dbo.[Subject_Mapping_New] where Admission_no='" + regid + "' and Session_id=" + Session_id + "  order by id asc ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["Sub_id"].ToString();
            }

        }

        internal static object get_state_code(string state_name)
        {
            string query = "select State from dbo.[state_and_district] where State like '%" + state_name + "%'  ";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["State"].ToString();
            }
        }


        internal string get_subject_heading_subjective(string Class_Id, string section, string session_id, string branchid, string type, string admission_no, string religion)
        {
            if (type == "admission_no")
            {
                Dictionary<string, object> dc1 = My.get_selected_studentinfo(admission_no, session_id, branchid);
                Class_Id = (String)dc1["Class_id"];
                section = (String)dc1["Section"];
            }

            string querymain = "Select  studentname as Student_Name,admissionserialnumber as Admission_No,Section,rollnumber as Roll_No  ";
            string query = " Select su.subject2,su.Subject_id,su.course_id ,su.Subject_Short_Name from Subject_Master su where su.Branch_id='" + branchid + "' and su.course_id=" + Class_Id + "   ORDER BY su.Subject_position";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = GetData(cmd);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    querymain += ", isnull(" + dr["Subject_id"].ToString() + ",0) [" + dr["subject2"].ToString() + "]";
                }
            }

            if (type == "admission_no")
            {
                querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "'  and ar.Section='" + section + "' and ar.Class_id=" + Class_Id + " and ar.Status='1' and ar.admissionserialnumber='" + admission_no + "'   order by rollnumber asc";
            }
            else
            {
                if (section == "ALL")
                {
                    if (religion == "ALL")
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "' and ar.Class_id=" + Class_Id + " and ar.Status='1' order by ar.Section,rollnumber asc";
                    }
                    else
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "' and ar.Class_id=" + Class_Id + " and ar.Status='1'  and ar.religion='" + religion + "' order by ar.Section,rollnumber asc";
                    }
                }
                else
                {
                    if (religion == "ALL")
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "'  and ar.Section='" + section + "' and Class_id=" + Class_Id + " and Status='1'  order by rollnumber asc";
                    }
                    else
                    {
                        querymain += " from    admission_registor ar   where   ar.Session_id=" + session_id + " and ar.Branch_id='" + branchid + "'  and ar.Section='" + section + "' and Class_id=" + Class_Id + " and Status='1'  and ar.religion='" + religion + "' order by rollnumber asc";
                    }
                }
            }
            return querymain;
        }


        internal static string get_top_one_user_for_fee_colection()
        {
            string query = " select top 1 t1.user_id from Student_Payment_History t1 join user_details t2 on t1.user_id = t2.user_id";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0][0].ToString();

            }

        }

        internal string get_spareat_month(string months)
        {
            string Something = "";
            string mainstring = months;
            string result2 = "";

            string[] multiArray = mainstring.Split(new Char[] { ',' });

            foreach (string author in multiArray)
            {
                if (author.Trim() != "")
                {
                    result2 += "'" + author + "',";
                }

            }


            Something = result2.TrimEnd(',');

            return Something;
        }

        internal static string get_restricted_message()
        {
            return "SORRY! You have not permission for this work.";
        }


        public void bind_all_ddl_with_id_All_New(DropDownList ddl, string strQuery)
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
            ddl.Items.Insert(0, new ListItem("ALL", "0"));
        }

        internal static string get_user_type(string userid)
        {

            string query = "Select User_Type from user_details where user_id='" + userid + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["User_Type"].ToString();
            }
        }


        public static Dictionary<string, object> get_user_menu_permission_data(string UserID, string menupagename)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string user_type = get_user_type(UserID);
            if (user_type == "Admin")
            {
                dc["Is_Edit"] = "1";
                dc["Is_delete"] = "1";
                dc["Is_Download"] = "1";
                dc["Is_Print"] = "1";
                dc["Is_add"] = "1";
            }
            else
            {
                string query = "select mp.*,mgl.Group_name as main_menu,mm.Menu_name as submenu, mm.Menu_page from MenuPermissionForUser_web mp join MenuMaster_web mm on mp.MenuID=mm.MenuID and mp.MainMenuId=mm.Group_id join Menu_Group_List_web mgl on mm.Group_id=mgl.Group_id where mp.UserID='" + UserID + "' and mm.Menu_page='" + menupagename + "' and mm.Type=1 order by mgl.Position";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    dc["Is_Edit"] = "0";
                    dc["Is_delete"] = "0";
                    dc["Is_Download"] = "0";
                    dc["Is_Print"] = "0";
                    dc["Is_add"] = "0";
                }
                else
                {
                    dc["Is_Edit"] = dt.Rows[0]["Is_Edit"].ToString();
                    dc["Is_delete"] = dt.Rows[0]["Is_delete"].ToString();
                    dc["Is_Download"] = dt.Rows[0]["Is_Download"].ToString();
                    dc["Is_Print"] = dt.Rows[0]["Is_Print"].ToString();
                    dc["Is_add"] = dt.Rows[0]["Is_add"].ToString();
                }
            }
            return dc;
        }


        //FOR FEE COLLECTION PAGE
        public static Dictionary<string, object> get_user_menu_permission_data_fee_collection_page(string UserID, string menupagename)
        {
            Dictionary<string, object> dc = new Dictionary<string, object>();
            string user_type = get_user_type(UserID);
            if (user_type.ToUpper() == "ADMIN")
            {
                dc["Is_Edit"] = "1";
                dc["Is_delete"] = "1";
                dc["Is_Download"] = "1";
                dc["Is_Print"] = "1";
                dc["Is_add"] = "1";

                dc["Is_Edit_Bus"] = "1";
                dc["Is_delete_Bus"] = "1";
                dc["Is_add_Bus"] = "1";

                dc["Is_Edit_Hostel"] = "1";
                dc["Is_delete_Hostel"] = "1";
                dc["Is_add_Hostel"] = "1";

                dc["Is_Edit_Discount"] = "1";
                dc["Is_delete_Discount"] = "1";
                dc["Is_add_Discount"] = "1";

                dc["Is_Edit_Bill"] = "1";
                dc["Is_delete_Bill"] = "1";
                dc["Is_add_Bill"] = "1";
            }
            else
            {
                string query = "select mp.*,mgl.Group_name as main_menu,mm.Menu_name as submenu, mm.Menu_page from MenuPermissionForUser_web mp join MenuMaster_web mm on mp.MenuID=mm.MenuID and mp.MainMenuId=mm.Group_id join Menu_Group_List_web mgl on mm.Group_id=mgl.Group_id where mp.UserID='" + UserID + "' and (mm.Menu_page='fee-collection-monthly-wise.aspx' or mm.Menu_page='fee-collection.aspx' or mm.Menu_page='fee-collections.aspx' or mm.Menu_page='fees-collection.aspx' or mm.Menu_page='fee-collection-test.aspx' or mm.Menu_page='set-discount-on-admission-fee.aspx'  or mm.Menu_page='set-discount-on-admission-fee.aspx' or mm.Menu_page='Mapping_Transportation_with_Student_N.aspx' or mm.Menu_page='Hostel_Assign_to_student.aspx' or mm.Menu_page='Delete_Bill.aspx' or mm.Menu_page='bill-delete.aspx' or mm.Menu_page='fees-collections.aspx' or mm.Menu_page='fees-collection-1.aspx' or mm.Menu_page='fees-collection-2.aspx' or mm.Menu_page='fees-collection-3.aspx') and mm.Type=1 order by mgl.Position;";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    dc["Is_Edit"] = "0";
                    dc["Is_delete"] = "0";
                    dc["Is_Download"] = "0";
                    dc["Is_Print"] = "0";
                    dc["Is_add"] = "0";


                    dc["Is_Edit_Bus"] = "0";
                    dc["Is_delete_Bus"] = "0";
                    dc["Is_add_Bus"] = "0";

                    dc["Is_Edit_Hostel"] = "0";
                    dc["Is_delete_Hostel"] = "0";
                    dc["Is_add_Hostel"] = "0";

                    dc["Is_Edit_Discount"] = "0";
                    dc["Is_delete_Discount"] = "0";
                    dc["Is_add_Discount"] = "0";

                    dc["Is_Edit_Bill"] = "0";
                    dc["Is_delete_Bill"] = "0";
                    dc["Is_add_Bill"] = "0";
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Menu_page"].ToString() == "fee-collection-monthly-wise.aspx" || dr["Menu_page"].ToString() == "fee-collection.aspx" || dr["Menu_page"].ToString() == "fee-collections.aspx" || dr["Menu_page"].ToString() == "fees-collection.aspx" || dr["Menu_page"].ToString() == "fees-collections.aspx" || dr["Menu_page"].ToString() == "fees-collection-1.aspx" || dr["Menu_page"].ToString() == "fees-collection-2.aspx" || dr["Menu_page"].ToString() == "fees-collection-3.aspx")
                        {
                            dc["Is_Edit"] = dr["Is_Edit"].ToString();
                            dc["Is_delete"] = dr["Is_delete"].ToString();
                            dc["Is_Download"] = dr["Is_Download"].ToString();
                            dc["Is_Print"] = dr["Is_Print"].ToString();
                            dc["Is_add"] = dr["Is_add"].ToString();
                        }
                        if (dr["Menu_page"].ToString() == "Mapping_Transportation_with_Student_N.aspx")
                        {
                            dc["Is_Edit_Bus"] = dr["Is_Edit"].ToString();
                            dc["Is_delete_Bus"] = dr["Is_delete"].ToString();
                            dc["Is_add_Bus"] = dr["Is_add"].ToString();
                        }

                        if (dr["Menu_page"].ToString() == "Hostel_Assign_to_student.aspx")
                        {
                            dc["Is_Edit_Hostel"] = dr["Is_Edit"].ToString();
                            dc["Is_delete_Hostel"] = dr["Is_delete"].ToString();
                            dc["Is_add_Hostel"] = dr["Is_add"].ToString();
                        }

                        if (dr["Menu_page"].ToString() == "set-discount-on-admission-fee.aspx")
                        {
                            dc["Is_Edit_Discount"] = dr["Is_Edit"].ToString();
                            dc["Is_delete_Discount"] = dr["Is_delete"].ToString();
                            dc["Is_add_Discount"] = dr["Is_add"].ToString();
                        }

                        if (dr["Menu_page"].ToString() == "Delete_Bill.aspx" || dr["Menu_page"].ToString() == "bill-delete.aspx")
                        {
                            dc["Is_Edit_Bill"] = dr["Is_Edit"].ToString();
                            dc["Is_delete_Bill"] = dr["Is_delete"].ToString();
                            dc["Is_add_Bill"] = dr["Is_add"].ToString();
                        }
                    }
                }
            }
            return dc;
        }

        public static void Insert(string tableName, object jsonData, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            Insert(tableName, table, conStr, sqlCon, ignoreColumn);
        }




        internal static void Insert(string tableName, Dictionary<string, object> table, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var cols = ignoreColumn.Split(',');
            foreach (var c in cols)
            {
                if (table.ContainsKey(c))
                {
                    table.Remove(c);
                }
                else if (table.ContainsKey(c.ToUpper()))
                {
                    table.Remove(c.ToUpper());
                }
                else if (table.ContainsKey(c.ToLower()))
                {
                    table.Remove(c.ToLower());
                }
            }
            var qry = $"insert into {tableName} ({string.Join(",", table.Keys)}) values (@{string.Join(",@", table.Keys)})";
            var cmd = new SqlCommand(qry);
            foreach (var key in table.Keys)
            {
                cmd.Parameters.AddWithValue($"@{key}", table[key]);
            }
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteCommand(conStr, sqlCon);
        }

        internal class smsdata
        {
            public string responseCode { get; set; }
            public string response { get; set; }

        }


        public static void Update(string tableName, object jsonData, string where = null, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var js = new JavaScriptSerializer();
            var json = js.Serialize(jsonData);
            var table = js.Deserialize<Dictionary<string, object>>(json);
            Update(tableName, table, where, conStr, sqlCon, ignoreColumn);
        }
        public static void Update(string tableName, Dictionary<string, object> data, string where = null, string conStr = null, SqlConnection sqlCon = null, string ignoreColumn = "Id")
        {
            var cols = ignoreColumn.Split(',');
            foreach (var c in cols)
            {
                if (data.ContainsKey(c))
                {
                    data.Remove(c);
                }
                else if (data.ContainsKey(c.ToUpper()))
                {
                    data.Remove(c.ToUpper());
                }
                else if (data.ContainsKey(c.ToLower()))
                {
                    data.Remove(c.ToLower());
                }
            }
            var condition = where == null ? "" : "where " + where;
            var cmd = new SqlCommand();
            var qryprm = new List<String>();
            foreach (var key in data.Keys)
            {
                qryprm.Add($"{key}=@{key}");
                cmd.Parameters.AddWithValue($"@{key}", data[key] ?? DBNull.Value);
            }
            cmd.CommandText = $"update {tableName} set {string.Join(",", qryprm)} {condition}";
            InsertUpdate.InsertUpdateData(cmd);
        }


    }




    public static class mywraper
    {
        public static bool ExecuteCommand(this SqlCommand cmd, string conStr = null, SqlConnection sqlCon = null)
        {
            var result = false;
            var scon = sqlCon ?? new SqlConnection(conStr ?? My.con);
            var isConOpenHere = false;
            try
            {
                if (scon.State != System.Data.ConnectionState.Open)
                {
                    cmd.Connection = scon;
                    scon.Open();
                    isConOpenHere = true;
                }
                cmd.ExecuteNonQuery();
                result = true;
            }
            finally
            {
                if (isConOpenHere)
                {
                    scon.Close();
                    scon.Dispose();
                }
            }
            return result;
        }
    }





}