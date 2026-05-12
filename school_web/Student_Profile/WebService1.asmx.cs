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

namespace school_web.Student_Profile
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
        My mycode = new My();
        public class Fetch_Details_of_menu
        {
            public string Name { get; set; }
            public string Icon { get; set; }
            public string Page { get; set; }
            public string Banner { get; set; }
        }

        List<Fetch_Details_of_menu> Show_menu_details = new List<Fetch_Details_of_menu>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_menus(string Group_id)
        {
            string query = "select * from Iphone_menu where Group_id=" + Group_id + " and Status=1 order by Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
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
                    Show_menu_details.Add(new Fetch_Details_of_menu
                    {
                        Name = dr["Name"].ToString(),
                        Icon = dr["Icon"].ToString(),
                        Page = dr["Page"].ToString(),
                        Banner = dr["Banner"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_menu_details));
            }
        }



        //=======================================================
        public class MyMessagesShow
        {
            public string Message { get; set; }
            public string new_date { get; set; }
            public string Message_by { get; set; }
            public string Request_id { get; set; }
            public string is_Closed { get; set; }
        }


        List<MyMessagesShow> EMyBooking = new List<MyMessagesShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_chat_messages(string Request_id)
        {
            string sgBy = ""; string is_closed = "";
            string query = "select *, (Date+' '+Time) as new_date,(select top 1 Status from Complain_feedback where Request_id=Complain_feedback_chat.Request_id) as Comp_Status from Complain_feedback_chat where Request_id='" + Request_id + "' order by id asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
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
                    is_closed = dr["Comp_Status"].ToString();
                    if (sgBy == "1")
                    {
                        sgBy = "You";
                    }
                    else
                    {
                        sgBy = "Admin";
                    }

                    if (is_closed == "1")
                    {
                        is_closed = "hidden";
                    }
                    else
                    {
                        is_closed = "show";
                    }


                    EMyBooking.Add(new MyMessagesShow
                    {
                        new_date = dr["new_date"].ToString(),
                        Message = dr["Message"].ToString(),
                        Message_by = sgBy,
                        Request_id = dr["Request_id"].ToString(),
                        is_Closed = is_closed,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBooking));
            }
        }



        #region ContactuS
        //----------------------------------------------------------------------------
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void send_complain_data(string Message, string Request_id)
        {
            SqlCommand cmd;
            string strQuery = "INSERT INTO Complain_feedback_chat (Request_id,Message,Message_by,Date,Time,idate,UserId,ShowStatus) values (@Request_id,@Message,@Message_by,@Date,@Time,@idate,@UserId,@ShowStatus);";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Request_id", Request_id);
            cmd.Parameters.AddWithValue("@Message", Message);
            cmd.Parameters.AddWithValue("@Message_by", "1");
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Time", mycode.time());
            cmd.Parameters.AddWithValue("@idate", mycode.idate());
            cmd.Parameters.AddWithValue("@UserId", Session["User"].ToString());
            cmd.Parameters.AddWithValue("@ShowStatus", "1");
            if (My.InsertUpdateData(cmd))
            {
            }

        }
        #endregion



        public class Fetch_Details_of_messsage
        {
            public string Subject { get; set; }
            public string Details { get; set; }
            public string Attachments { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
        }

        List<Fetch_Details_of_messsage> Show_of_messsage = new List<Fetch_Details_of_messsage>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_message_details(string Request_id)
        {
            string query = "select  * from Private_Messages where Id='" + Request_id + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
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
                    string Attachments = dr["Attachments"].ToString();
                    if (dr["Attachments"].ToString() == "")
                    {
                        Attachments = "hidden";
                    }
                    Show_of_messsage.Add(new Fetch_Details_of_messsage
                    {
                        Subject = dr["Subject"].ToString(),
                        Details = dr["Details"].ToString(),
                        Attachments = Attachments,
                        Date = dr["Date"].ToString(),
                        Time = dr["Time"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_messsage));
            }
        }

        //======================================PAYMENY HISTORY

        public class MyPaymentHistoryShow
        {
            public string Slip_no { get; set; }
            public string Total_amt { get; set; }
            public string Total_disc { get; set; }
            public string Yotal_amt_after_disc { get; set; }
            public string Total_paid_amt { get; set; }
            public string Total_dues { get; set; }
            public string Date { get; set; }
            public string payment_link { get; set; }


            public List<MyPaymentDetails> MyPatmentItem { get; set; }
        }

        public class MyPaymentDetails
        {
            public string Content { get; set; }
            public string payable { get; set; }
            public string disc_amt { get; set; }
            public string paid { get; set; }

        }


        List<MyPaymentHistoryShow> EMySubMark = new List<MyPaymentHistoryShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_payment_history(string Session, string Branch_id, string Admission_no)
        {

            string sliptype = My.getprint_slip_type();


            string paymentlink = "";
            string query = "select Slip_no,Date,Addmission_no,Class_id,Session from Student_Payment_History where Session='" + Session + "' and Branch='" + Branch_id + "' and Addmission_no='" + Admission_no + "' group by Slip_no,Date,Addmission_no,Class_id,Session";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyPaymentDetails> MBdetails = findPaymentItems(Session, Branch_id, dr["Slip_no"].ToString());

                    if (paymentlink.ToUpper() == "A5")
                    {
                        paymentlink = "../Admin/slip/monthly-slip-a5.aspx?admissionno=" + dr["Addmission_no"].ToString() + "&sessionid=" + My.get_sess_prm(dr["Session"].ToString()) + "&classid=" + dr["Class_id"].ToString() + "&Slip_no=" + dr["Slip_no"].ToString();

                    }
                    else
                    {
                        paymentlink = "../Admin/slip/monthly-slip.aspx?admissionno=" + dr["Addmission_no"].ToString() + "&sessionid=" + My.get_sess_prm(dr["Session"].ToString()) + "&classid=" + dr["Class_id"].ToString() + "&Slip_no=" + dr["Slip_no"].ToString();
                    }


                    DataTable dtt = mycode.FillData("select *,(Total_after_disc-Total_paid) as Total_dues from (select isnull(sum(convert(float, payable)),0) as Total_payable,isnull(sum(convert(float, disc_amt)),0) as Total_disc_amt,(isnull(sum(convert(float, payable)),0)-isnull(sum(convert(float, disc_amt)),0)) as Total_after_disc,isnull(sum(convert(float, paid)),0) as Total_paid from dbo.[Monthly_Fee_Collection_Slip] where session='" + Session + "' and branchid='" + Branch_id + "' and slipno='" + dr["Slip_no"].ToString() + "')t");

                    EMySubMark.Add(new MyPaymentHistoryShow
                    {
                        Slip_no = dr["Slip_no"].ToString(),
                        Date = dr["Date"].ToString(),
                        payment_link = paymentlink,
                        Total_amt = dtt.Rows[0]["Total_payable"].ToString(),
                        Total_disc = dtt.Rows[0]["Total_disc_amt"].ToString(),
                        Yotal_amt_after_disc = dtt.Rows[0]["Total_after_disc"].ToString(),
                        Total_paid_amt = dtt.Rows[0]["Total_paid"].ToString(),
                        Total_dues = dtt.Rows[0]["Total_dues"].ToString(),

                        MyPatmentItem = MBdetails
                    }); ;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }


        private List<MyPaymentDetails> findPaymentItems(string Session, string Branch_id, string Slip_no)
        {
            List<MyPaymentDetails> MyPatmentItem = new List<MyPaymentDetails>();
            string query = "select * from Monthly_Fee_Collection_Slip where session='" + Session + "' and branchid='" + Branch_id + "' and slipno='" + Slip_no + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {


                    MyPatmentItem.Add(new MyPaymentDetails
                    {
                        Content = dr["Content"].ToString(),
                        payable = dr["payable"].ToString(),
                        disc_amt = dr["disc_amt"].ToString(),
                        paid = dr["paid"].ToString(),
                    });
                }
            }
            return MyPatmentItem;
        }
    }
}
