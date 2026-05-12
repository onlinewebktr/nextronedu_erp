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

namespace school_web.complain
{
    /// <summary>
    /// Summary description for chat
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class chat : System.Web.Services.WebService
    {

        //=======================================================
        public class MyMessagesShow
        {
            public string Message { get; set; }
            public string new_date { get; set; }
            public string Message_by { get; set; }
            public string School_name { get; set; }
            public string Request_id { get; set; }
            public string Complain_date { get; set; }
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
            string sgBy = "";
            string query = "select *, Format(convert(DateTime,Date,103), 'dd MMM yyyy-HH:MM tt') as new_date,(select top 1 School_name from Complain_request where Request_id=Complain_chat.Request_id) as School_name,(select top 1 Format(convert(DateTime,Request_date,103), 'dd MMM yyyy-HH:MM tt') from Complain_request where Request_id=Complain_chat.Request_id) as Complain_date from Complain_chat where Request_id='" + Request_id + "' order by id asc";
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
                        sgBy = "School";
                    }
                    else
                    {
                        sgBy = "You";
                    }

                    List<MyMessagesDetails> MBdetails = findmyBookingProduct(dr["Request_id"].ToString(), dr["Docs_From"].ToString(), dr["Docs_time"].ToString());
                    EMyBooking.Add(new MyMessagesShow
                    {
                        new_date = dr["new_date"].ToString(),
                        Message = dr["Message"].ToString(),
                        Message_by = sgBy,
                        School_name = dr["School_name"].ToString(),
                        Request_id = dr["Request_id"].ToString(),
                        Complain_date = dr["Complain_date"].ToString(),
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
    }
}
