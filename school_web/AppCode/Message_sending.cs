using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace school_web.AppCode
{
    public class Message_sending
    {
        UsesCode my = new UsesCode();

        public void send_sms_singl(string mobileno, string message, string Userid)
        {

            string sql = "select * from Message_config ";
            DataTable dt = my.FillTable(sql);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "STOP")
                {
                    return;
                }
                string str1 = dt.Rows[0]["uid"].ToString();       // imp1.Key;                              //Key
                string str2 = dt.Rows[0]["sender"].ToString();       // imp1.SenderID;                         //Sender ID
                string str3 = "1";
                string text = mobileno;
                string str4 = Uri.EscapeDataString(message);
                string url = "http://" + "mysms.msgclub.net" + "/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + str1 + "&message=" + str4 + "&senderId=" + str2 +
                    "&routeId=" + str3 + "&mobileNos=" + text + "&smsContentType=English";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                try
                {
                    HttpWebResponse httpres = (HttpWebResponse)httpWebRequest.GetResponse();
                    StreamReader sr = new StreamReader(httpres.GetResponseStream());
                    string result = sr.ReadToEnd();
                    sr.Close();

                    send_message_details_in_Message_send_details(mobileno, message, "SEND", url, "SELF", Userid, result, "English");
                }
                catch (Exception ex)
                {
                }

            }
            else { }

        }
        private void send_message_details_in_Message_send_details(string mobile, string message, string status, string url, string Groupcode, string Userid, string result,string msgetype)
        {
            SqlCommand cmd;
            string strQuery = "INSERT INTO Message_Details (Mobile_No,Message,Date,Idate,Time,Result,Groupcode,Status,Url,User_id,Mesage_Type) values (@Mobile_No,@Message,@Date,@Idate,@Time,@Result,@Groupcode,@Status,@Url,@User_id,@Mesage_Type)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Mobile_No", mobile);
            cmd.Parameters.AddWithValue("@Message", message);
            cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Groupcode", Groupcode);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Url", url);
            cmd.Parameters.AddWithValue("@User_id", Userid);
            cmd.Parameters.AddWithValue("@Result", result);
            cmd.Parameters.AddWithValue("@Mesage_Type", msgetype);
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }

             
        }
        public void send_sms_group(string mobileno, string message, string Groupcode, string Userid)
        {

            string sql = "select * from Message_config ";
            DataTable dt = my.FillTable(sql);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "STOP")
                {
                    return;
                }
                string str1 = dt.Rows[0]["uid"].ToString();       // imp1.Key;                              //Key
                string str2 = dt.Rows[0]["sender"].ToString();       // imp1.SenderID;                         //Sender ID
                string str3 = "1";
                string text = mobileno;
                string str4 = Uri.EscapeDataString(message);
                string url = "http://" + "mysms.msgclub.net" + "/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + str1 + "&message=" + str4 + "&senderId=" + str2 +
                    "&routeId=" + str3 + "&mobileNos=" + text + "&smsContentType=English";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                try
                {
                    HttpWebResponse httpres = (HttpWebResponse)httpWebRequest.GetResponse();
                    StreamReader sr = new StreamReader(httpres.GetResponseStream());
                    string result = sr.ReadToEnd();
                    sr.Close();
                    send_message_details_in_Message_send_details(mobileno, message, "SEND", url, Groupcode, Userid, result, "English");
                }
                catch (Exception ex)
                {
                }

            }
            else { }

        }

        public void SendMsg_hindi(string mobileno, string message, string Groupcode, string Userid)
        {

            string sql = "select * from Message_config ";
            DataTable dt = my.FillTable(sql);
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "STOP")
                {
                    return;
                }
                string str1 = dt.Rows[0]["uid"].ToString();       // imp1.Key;                              //Key
                string str2 = dt.Rows[0]["sender"].ToString();       // imp1.SenderID;                         //Sender ID
                string str3 = "1";
                string text = mobileno;
                string str4 = Uri.EscapeDataString(message);
                string url = "http://" + "mysms.msgclub.net" + "/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + str1 + "&message=" + str4 + "&senderId=" + str2 +
                    "&routeId=" + str3 + "&mobileNos=" + text + "&smsContentType=unicode";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                try
                {
                    HttpWebResponse httpres = (HttpWebResponse)httpWebRequest.GetResponse();
                    StreamReader sr = new StreamReader(httpres.GetResponseStream());
                    string result = sr.ReadToEnd();
                    sr.Close();
                    send_message_details_in_Message_send_details(mobileno, message, "SEND", url, Groupcode, Userid, result, "Hindi");
                }
                catch (Exception ex)
                {
                }

            }
            else { }

        }
    }
}