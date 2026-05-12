using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class log_hostory
    {
        
        internal static void delete_log(string session_id, string class_id, string admission_no, string type, string desc, string page, string created_by)
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
            if (My.InsertUpdateData(cmd))
            {
            }
        }


        internal static void edit_log(string session_id, string class_id, string admission_no, string type, string desc, string page, string created_by)
        {
            My mycode = new My();
            SqlCommand cmd;
            string query = "INSERT INTO Edit_log_history (Session_id,Admission_no,Class_id,Type,Description,Page,Created_by,Created_date,Created_time,Created_idate) values (@Session_id,@Admission_no,@Class_id,@Type,@Description,@Page,@Created_by,@Created_date,@Created_time,@Created_idate)";
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
            if (My.InsertUpdateData(cmd))
            {
            }
        }

        internal static void save_student_activity_log(string session_id, string class_id, string admission_no, string type, string desc, string page, string created_by)
        {
            My mycode = new My();
            SqlCommand cmd;
            string query = "INSERT INTO Edit_log_history (Session_id,Admission_no,Class_id,Type,Description,Page,Created_by,Created_date,Created_time,Created_idate) values (@Session_id,@Admission_no,@Class_id,@Type,@Description,@Page,@Created_by,@Created_date,@Created_time,@Created_idate)";
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
            if (My.InsertUpdateData(cmd))
            {
            }
        }
    }
}