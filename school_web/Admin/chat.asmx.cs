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
    /// Summary description for chat1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class chat1 : System.Web.Services.WebService
    {

        My mycode = new My();
        public class Fetch_Details_of_Message
        {
            public string Message { get; set; }
            public string new_date { get; set; }
            public string Message_by { get; set; }
        }

        List<Fetch_Details_of_Message> Show_of_Message = new List<Fetch_Details_of_Message>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_chat_message(string Request_id)
        {
            string sgBy = "";
            string query = "select *, Format(convert(DateTime,Date,103), 'dd MMM yyyy-HH:MM tt') as new_date from Complain_chat where Request_id='" + Request_id + "' order by id asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, compLN.comp);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
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
            string Status = "";
            string ClosedStatus = "";
            string query = "select * from Complain_chat where Request_id ='" + Request_id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, compLN.comp);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
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

        compLN mycode_c = new compLN();
        //----------------------------------------------------------------------------
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void send_message_to_user(string Request_id, string Message, string Status)
        {
            SqlCommand cmd;
            string strQuery = "INSERT INTO COMPLAIN (Request_id,Message,Message_by,Date,idate,UserId,ShowStatus) values (@Request_id,@Message,@Message_by,@Date,@idate,@UserId,@ShowStatus);";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Request_id", Request_id);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@Message_by", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Date", mycode.datetime());
            cmd.Parameters.AddWithValue("@idate", mycode.idate());
            cmd.Parameters.AddWithValue("@ShowStatus", 1);
            cmd.Parameters.AddWithValue("@UserId", Session["Admin"].ToString());
            if (compLN.InsertUpdateDataComp(cmd))
            {
                string qryss = "update Complain_request set ShowStatus=1 where Request_id='" + Request_id + "'";
                mycode_c.executequery_comp(qryss);
                if (Status == "1")
                {
                    string qry = "update Complain_request set Status='1',Solve_date='" + mycode.datetime() + "',Closed_idate='" + mycode.idate() + "',Last_reply_date='" + mycode.datetime() + "',Last_reply_idate='" + mycode.idate() + "' where Request_id='" + Request_id + "'";
                    mycode_c.executequery_comp(qry);
                }
                else
                {
                    string qry = "update Complain_request set Last_reply_date='" + mycode.datetime() + "',Last_reply_idate='" + mycode.idate() + "' where Request_id='" + Request_id + "'";
                    mycode_c.executequery_comp(qry);
                }
            }
        }




        //=======================================================
        public class MyMessagesShow
        {
            public string Message { get; set; }
            public string new_date { get; set; }
            public string Message_by { get; set; }
            public string Message_by_Name { get; set; }
            public List<MyMessagesDetails> MyMessagesItem { get; set; }

        }

        public class MyMessagesDetails
        {
            public string Documents { get; set; }
        }

        List<MyMessagesShow> EMyBooking = new List<MyMessagesShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_chat_messages(string Request_id)
        {
            string qrys = "update Complain_chat set ShowStatus='0' where Request_id='" + Request_id + "'";
            compLN.exeSql_comp(qrys);
            string sgBy = ""; string sgByName = "";
            string query = "select *, Format(convert(DateTime,Date,103), 'dd MMM yyyy-HH:MM tt') as new_date from Complain_chat where Request_id='" + Request_id + "' and (Note_Type='Additional Notes' or Note_Type is null)  order by id asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, compLN.comp);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
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
                        sgBy = "You";
                        sgByName = "You";
                    }
                    else
                    {
                        sgBy = "Admin";
                        sgByName = dr["update_User_Type"].ToString();
                    }

                    List<MyMessagesDetails> MBdetails = findmyBookingProduct(dr["Request_id"].ToString(), dr["Docs_From"].ToString(), dr["Docs_time"].ToString());
                    EMyBooking.Add(new MyMessagesShow
                    {
                        new_date = dr["new_date"].ToString(),
                        Message = dr["Message"].ToString(),
                        Message_by = sgBy,
                        Message_by_Name = sgByName,
                        MyMessagesItem = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBooking));
            }
        }


        private List<MyMessagesDetails> findmyBookingProduct(string req_id, string doc_From, string Docs_time)
        {
            List<MyMessagesDetails> MyMessagesItem = new List<MyMessagesDetails>();
            string query = "Select * from Complain_docs where Request_id='" + req_id + "' and Docs_From='" + doc_From + "' and Docs_time='" + Docs_time + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, compLN.comp);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MyMessagesItem.Add(new MyMessagesDetails
                    {
                        Documents = dr["Documents"].ToString(),
                    });
                }
            }
            return MyMessagesItem;
        }


        //=================================Meeting Join History

        public class Fetch_meet_join_history
        {
            public string Session { get; set; }
            public string Admission_no { get; set; }
            public string Class_name { get; set; }
            public string Section { get; set; }
            public string Rollnumber { get; set; }
            public string Studentname { get; set; }
        }

        List<Fetch_meet_join_history> Show_meet_join_history = new List<Fetch_meet_join_history>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_meeting_history(string Request_id)
        {
            string query = "select * from zoom_ptm_allowed_students t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber and t1.class=t2.Class_id and t1.session_id=t2.Session_id where zoom_id='" + Request_id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_meet_join_history.Add(new Fetch_meet_join_history
                    {
                        Session = dr["session"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_meet_join_history));
            }

        }





        //FetchRequestProgress
        public class Fetch_request_progress
        {


            //StatuS
            public string Complain_id { get; set; }
            public string Status { get; set; }
            public string first_pending { get; set; }
            public string first_pendingshowStatus { get; set; }
            public string dates { get; set; }
            public string MoreInfo { get; set; }
            public string ClosedLast { get; set; }
            public string dvWidth { get; set; }
            public string process_status { get; set; }
        }


        List<Fetch_request_progress> Show_request_progress = new List<Fetch_request_progress>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_request_progress(string Paramiter1)
        {
            string[] stringSeparatorss = new string[] { "~" };
            string[] arrs = Paramiter1.Split(stringSeparatorss, StringSplitOptions.None);
            string complain_id = arrs[0];
            string complain_status = arrs[1];
            string qry = "";
            if (complain_status.ToUpper() == "CLOSED")
            {
                qry = "select Complain_id,Status,Format(convert(DateTime,Created_date,103), 'dd MMM yyyy - hh:mm tt') as Date from Complain_request_progress where Complain_id='" + complain_id + "'";
            }
            else
            {
                qry = "select Complain_id,Status,Format(convert(DateTime,Created_date,103), 'dd MMM yyyy - hh:mm tt') as Date from Complain_request_progress where Complain_id='" + complain_id + "'  UNION all  select top 1 Complain_id,'Closed' as Status,Format(convert(DateTime,Created_date,103), 'dd MMM yyyy - hh:mm tt') as Date from Complain_request_progress where Complain_id='" + complain_id + "'";
            }

            SqlDataAdapter ad = new SqlDataAdapter(qry, compLN.comp);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_request_progress");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                double width = 100 / rowcount1;
                int count = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    string process_status = "processDone";
                    string moreinfoS = "shows"; string closedLast = "";
                    if (complain_status.ToUpper() != "CLOSED")
                    {
                        if (rowcount1 == count)
                        {
                            moreinfoS = "hidden";
                            closedLast = "closedLast";
                            process_status = "processPending";
                        }
                    }

                    if (complain_status.ToUpper() == "CLOSED")
                    {
                        if (rowcount1 == count)
                        {
                            closedLast = "closedLast";
                        }
                    }
                    count++;
                    Show_request_progress.Add(new Fetch_request_progress
                    {
                        Complain_id = dr["Complain_id"].ToString(),
                        first_pending = "0",
                        first_pendingshowStatus = "",
                        Status = dr["Status"].ToString(),
                        dates = dr["Date"].ToString(),
                        MoreInfo = moreinfoS,
                        ClosedLast = closedLast,
                        dvWidth = width.ToString() + "%",
                        process_status = process_status,
                    });

                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_request_progress));
            }
        }
    }
}
