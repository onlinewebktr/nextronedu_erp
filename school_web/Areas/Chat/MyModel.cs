using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.Areas.Chat
{
    public class MyModel
    {
        public class Fetch_Details_of_Message
        {
            public string Message { get; set; }
            public string new_date { get; set; }
            public string Time { get; set; }
            public string Message_by { get; set; }
            public string profilephoto { get; set; }
            public string Message_by1 { get; set; }
            public List<Mydoc> MydocItem { get; set; }
        }
        public class Mydoc
        {
            public string Documents { get; set; }
        }
        public static void Internal_chat_group_list(string creatby, string groupname, string filename, string groupid, SqlConnection con)
        {
            string query = "INSERT INTO Internal_chat_group_list (Group_Id,Group_name,Group_ProfilePhoto,Group_Create_date_time,Istatus,Created_by) values (@Group_Id,@Group_name,@Group_ProfilePhoto,@Group_Create_date_time,@Istatus,@Created_by)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Group_Id", groupid);
            cmd.Parameters.AddWithValue("@Group_name", groupname);
            cmd.Parameters.AddWithValue("@Group_ProfilePhoto", filename);
            cmd.Parameters.AddWithValue("@Group_Create_date_time", My.getdate1());
            cmd.Parameters.AddWithValue("@Istatus", 1);
            cmd.Parameters.AddWithValue("@Created_by", creatby);
            if (payments.InsertUpdateData(cmd, con))
            {

            }
        }
        public static void Internal_Chat_group_user(string groupid, string userid_selected, SqlConnection con)
        {

            string query = "INSERT INTO Internal_Chat_group_users_list(Group_Id, User_Id, Added_on_date, Status) values(@Group_Id, @User_Id, @Added_on_date, @Status)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Group_Id", groupid);
            cmd.Parameters.AddWithValue("@User_Id", userid_selected);
            cmd.Parameters.AddWithValue("@Added_on_date", My.getdate1());
            cmd.Parameters.AddWithValue("@Status", "Active");
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }
        public static void Internal_chat_group_list_update(string creatby, string groupname, string filename, string groupid, SqlConnection con)
        {
            string query = "Update Internal_chat_group_list set Group_name=@Group_name,Group_ProfilePhoto=@Group_ProfilePhoto,Group_updated_date_time=@Group_updated_date_time,Updated_By=@Updated_By where Group_Id = @Group_Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Group_name", groupname);
            cmd.Parameters.AddWithValue("@Group_ProfilePhoto", filename);
            cmd.Parameters.AddWithValue("@Group_updated_date_time", My.getdate1());
            cmd.Parameters.AddWithValue("@Updated_By", creatby);
            cmd.Parameters.AddWithValue("@Group_Id", groupid);
            if (payments.InsertUpdateData(cmd, con))
            {

            }
        }
        public static void Internal_Chat_group_user_update(string creatby, string groupid, string userid_selected, SqlConnection con)
        {


            var dt2 = payments.dataTable($"select *  from Internal_Chat_group_users_list where  Group_Id = '" + groupid + "' and User_Id='" + userid_selected + "' ", con);
            if (dt2.Rows.Count == 0)
            {
                string query = "INSERT INTO Internal_Chat_group_users_list(Group_Id, User_Id, Added_on_date, Status) values(@Group_Id, @User_Id, @Added_on_date, @Status)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Group_Id", groupid);
                cmd.Parameters.AddWithValue("@User_Id", userid_selected);
                cmd.Parameters.AddWithValue("@Added_on_date", My.getdate1());
                cmd.Parameters.AddWithValue("@Status", "Active");
                if (payments.InsertUpdateData(cmd, con))
                {
                } 
            }
            else
            {

            }
        }
        internal static void insert_data_Internal_chat_history(SqlConnection con, string Receiver_id, string messageid, string regid, string chatmessage, string chattype)
        {
            string query = "INSERT INTO Internal_chat_history (Message_id,Sender_id,Receiver_id,Content,Chat_type,Status,Time_stamp) values (@Message_id,@Sender_id,@Receiver_id,@Content,@Chat_type,@Status,@Time_stamp)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Message_id", messageid);
            cmd.Parameters.AddWithValue("@Sender_id", regid);
            cmd.Parameters.AddWithValue("@Receiver_id", Receiver_id);
            cmd.Parameters.AddWithValue("@Content", chatmessage);
            cmd.Parameters.AddWithValue("@Chat_type", chattype);
            cmd.Parameters.AddWithValue("@Status", "");
            cmd.Parameters.AddWithValue("@Time_stamp", My.getdate1()); 
            if (payments.InsertUpdateData(cmd, con))
            {
                DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dtm.ToString("yyyyMMddhhmmss");
                Random random = new Random();
                int tempo = random.Next(1, 9999);
                string NotificationID = tempo.ToString() + date;
                // send data Internal_chat_Notification
                if (chattype == "Group")
                {
                    string query1 = "Select * from Internal_Chat_group_users_list where Group_Id='" + Receiver_id + "' and User_Id!='" + regid + "' and Status='Active'";
                    var dt3 = payments.dataTable(query1, con);
                    if (dt3.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dt3.Rows)
                        {
                            string userid = dr["User_Id"].ToString(); 
                            string query2 = "INSERT INTO Internal_chat_Notification (NotificationID,Message_id,Receiver_id,Time_stamp,Is_read,chattype) values (@NotificationID,@Message_id,@Receiver_id,@Time_stamp,@Is_read,@chattype)";
                            SqlCommand cmd1 = new SqlCommand(query2);
                            cmd1.Parameters.AddWithValue("@NotificationID", NotificationID);
                            cmd1.Parameters.AddWithValue("@Message_id", messageid);
                            cmd1.Parameters.AddWithValue("@Receiver_id", userid);
                            cmd1.Parameters.AddWithValue("@Time_stamp", My.getdate1());
                            cmd1.Parameters.AddWithValue("@Is_read", 0);
                            cmd1.Parameters.AddWithValue("@chattype", "Group");
                            if (payments.InsertUpdateData(cmd1, con))
                            {
                            }
                        }
                    }
                }
                else
                {
                    string query2 = "INSERT INTO Internal_chat_Notification (NotificationID,Message_id,Receiver_id,Time_stamp,Is_read,chattype) values (@NotificationID,@Message_id,@Receiver_id,@Time_stamp,@Is_read,@chattype)";
                    SqlCommand cmd1 = new SqlCommand(query2);
                    cmd1.Parameters.AddWithValue("@NotificationID", NotificationID);
                    cmd1.Parameters.AddWithValue("@Message_id", messageid);
                    cmd1.Parameters.AddWithValue("@Receiver_id", Receiver_id);
                    cmd1.Parameters.AddWithValue("@Time_stamp", My.getdate1());
                    cmd1.Parameters.AddWithValue("@Is_read", 0);
          
                    cmd1.Parameters.AddWithValue("@chattype", "User");
                    if (payments.InsertUpdateData(cmd1, con))
                    {

                    }
                }
            }
        }
        internal static void update_data_Internal_chat_history(SqlConnection con, string groupid, string regid, string chatmessage, string Message_id, string chattype)
        {
            string Remarks = "Message has been updated by " + MyModel.get_user_name(regid) + " Message id " + Message_id;
            string query1 = "INSERT INTO Internal_chat_history_Edit_Delete_backup (Message_id, Sender_id, Receiver_id, Content, Chat_type, Document, Status, Time_stamp, Edited_Deleted_By, Edited_Deleted_timestamp, Remarks)select Message_id,Sender_id,Receiver_id,Content,Chat_type,Document,Status,Time_stamp,'" + regid + "','" + My.getdate1() + "','"+ Remarks + "' from Internal_chat_history where Message_id='" + Message_id + "'";
            SqlCommand cmd1 = new SqlCommand(query1);
            if (payments.InsertUpdateData(cmd1, con))
            {
                string query = "Update Internal_chat_history set Status=@Status, Content=@Content,Time_stamp=@Time_stamp where Message_id = @Message_id ";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Content", chatmessage);
                cmd.Parameters.AddWithValue("@Updated_by", regid);
                cmd.Parameters.AddWithValue("@Status", "Edited");
                cmd.Parameters.AddWithValue("@Message_id", Message_id);
                cmd.Parameters.AddWithValue("@Time_stamp", My.getdate1());
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }

        internal static string get_user_name(string regid)
        {
            DataTable dt = My.dataTable("Select name from user_details where user_id='"+ regid + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["name"].ToString();
            }
        }
    }
}