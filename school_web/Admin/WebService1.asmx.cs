using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public class Fetch_Details_of_Message
        {
            public string Message { get; set; }
            public string new_date { get; set; }
            public string Time { get; set; }
            public string Message_by { get; set; }
        }
        UsesCode mycode = new UsesCode();
        List<Fetch_Details_of_Message> Show_of_Message = new List<Fetch_Details_of_Message>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_chat_message(string Request_id)
        {
            SqlConnection conn = new SqlConnection(connection.conn);
            string sgBy = "";
            string query = "select *, Format(convert(DateTime,Date,103), 'dd MMM yyyy') as new_date from Complain_feedback_chat where Request_id='" + Request_id + "'   order by id asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_feedback_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    sgBy = dr["Message_by"].ToString();
                    if (sgBy == "1")
                    {
                        sgBy = "User";
                    }
                    else
                    {
                        sgBy = "Admin";
                    }
                    Show_of_Message.Add(new Fetch_Details_of_Message
                    {
                        new_date = dr["new_date"].ToString(),
                        Message = dr["Message"].ToString(),
                        Time = dr["Time"].ToString(),
                        Message_by = sgBy,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_Message));
            }

        }
        public class Fetch_req_status
        {
            public string ShowStatus { get; set; }
            public string PnlClosed { get; set; }
        }
        List<Fetch_req_status> Show_req_status = new List<Fetch_req_status>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_reqest_status(string Request_id)
        {
            SqlConnection conn = new SqlConnection(connection.conn);
            string Status = "";
            string ClosedStatus = "";
            string query = "select * from Complain_feedback where Request_id ='" + Request_id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_feedback");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Status = dr["Status"].ToString();
                    if (Status == "1")
                    {
                        Status = "hidden";
                        ClosedStatus = "show";
                    }
                    else
                    {
                        Status = "show";
                        ClosedStatus = "hidden";
                    }

                    Show_req_status.Add(new Fetch_req_status
                    {
                        ShowStatus = Status,
                        PnlClosed = ClosedStatus,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_req_status));
            }

        }

        #region send message

        //----------------------------------------------------------------------------
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void send_message_to_user(string Request_id, string Message, string Status, string studentid)
        {
            SqlCommand cmd;
            string strQuery = "INSERT INTO Complain_feedback_chat (Request_id,Message,Message_by,Date,Time,idate,ShowStatus) values (@Request_id,@Message,@Message_by,@Date,@Time,@idate,@ShowStatus);";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Request_id", Request_id);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@Message_by", 2);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Time", mycode.time());
            cmd.Parameters.AddWithValue("@idate", mycode.idate());
            cmd.Parameters.AddWithValue("@ShowStatus", 1);
            cmd.Parameters.AddWithValue("@UserId", studentid);
            if (InsertUpdate.InsertUpdateData(cmd))
            {
                send_push_to_user(Request_id, Message);
                string qryss = "update Complain_feedback_chat set ShowStatus=1 where Request_id='" + Request_id + "'";
                mycode.executequery(qryss);
                if (Status == "1")
                {
                    string qry = "update Complain_feedback set Status='1',Closed_date='" + mycode.date() + "',Closed_time='" + mycode.time() + "',Closed_idate='" + mycode.idate() + "',Last_reply_date='" + mycode.date() + "',Last_reply_time='" + mycode.time() + "',Last_reply_idate='" + mycode.idate() + "' where Request_id='" + Request_id + "' and User_id='" + studentid + "'";
                    mycode.executequery(qry);
                }
                else
                {
                    string qry = "update Complain_feedback set Last_reply_date='" + mycode.date() + "',Last_reply_time='" + mycode.time() + "',Last_reply_idate='" + mycode.idate() + "' where Request_id='" + Request_id + "' and User_id='" + studentid + "'";
                    mycode.executequery(qry);
                }
            }
        }

        private void send_push_to_user(string Request_id, string Message)
        {
            string sql = @"select t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.gcm_id  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Request_id='" + Request_id + "' and   t2.Transfer_Status in('New','NT')";
            DataTable dt = mycode.FillTable(sql);
            if (dt.Rows[0]["gcm_id"].ToString() != null)
            {
                mycode.pushnotification(Message, "You have one message by " + get_schoolname() + " ", dt.Rows[0]["gcm_id"].ToString(), Request_id, dt.Rows[0]["User_id"].ToString(), "RequestReplyByAdmin");
            }
        }

        private string get_schoolname()
        {
            DataTable dt = mycode.FillTable("Select firm_name from Firm_Details");
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        #endregion

    }
}
