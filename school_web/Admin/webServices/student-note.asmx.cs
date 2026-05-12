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

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for student_note
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class student_note : System.Web.Services.WebService
    {
        My mycode = new My();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void send_student_note_data(string Admission_no, string Session_id, string NoteStd, string User_id)
        {
            SqlCommand cmd;
            string strQuery = "INSERT INTO Student_note (Session_id,Admision_no,Note,Created_by,Created_date,Created_time,Created_idate) values (@Session_id,@Admision_no,@Note,@Created_by,@Created_date,@Created_time,@Created_idate);";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Session_id", Session_id);
            cmd.Parameters.AddWithValue("@Admision_no", Admission_no);
            cmd.Parameters.AddWithValue("@Note", NoteStd);
            cmd.Parameters.AddWithValue("@Created_by", User_id);
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
            if (My.InsertUpdateData(cmd))
            {
            }
        }



        public class Fetch_Details_of_student_note
        {
            public string RowId { get; set; }
            public string Dates { get; set; }
            public string Times { get; set; }
            public string Content { get; set; }
        }

        List<Fetch_Details_of_student_note> Show_of_student_note = new List<Fetch_Details_of_student_note>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_student_note_data(string Session_id, string Admission_no)
        {
            string query = "select * from Student_note where Session_id = '" + Session_id + "' and Admision_no = '" + Admission_no + "'";
            DataTable dtD = My.dataTable(query);
            if (dtD.Rows.Count > 0)
            {
                foreach (DataRow dr in dtD.Rows)
                {
                    Show_of_student_note.Add(new Fetch_Details_of_student_note
                    {
                        RowId = dr["Id"].ToString(),
                        Dates = dr["Created_date"].ToString(),
                        Times = dr["Created_time"].ToString(),
                        Content = dr["Note"].ToString(),
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_student_note));
        }


        //===--====DeletevideO
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void delete_student_note(string RowId_del)
        {
            string qryd = "delete from Student_note where Id='" + RowId_del + "'";
            My.exeSql(qryd);
        }


        public class Fetch_Details_of_student
        {
            public string Admission_no { get; set; }
            public string Class_name { get; set; }
            public string Section { get; set; }
            public string Rollnumber { get; set; }
            public string Studentname { get; set; }
            public string Fathername { get; set; }
            public string Session_id { get; set; }
            public string Mobile_no { get; set; }
            public string MotherName { get; set; }
            public string Admission_no_date { get; set; }
            public string Is_RegShow { get; set; }
        }

        List<Fetch_Details_of_student> Show_of_student = new List<Fetch_Details_of_student>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_student_data(string Session_id, string Firm_id)
        {
            string query = "select admissionserialnumber,class,Section,rollnumber,studentname,fathername,Session_id,mobilenumber,mothername,Admission_no_date from admission_registor where Session_id='" + Session_id + "' and Status='1'";
            DataTable dtD = My.dataTable(query);
            if (dtD.Rows.Count > 0)
            {
                string is_RegShow = "hidden";
                if (Firm_id == "NESM")
                {
                    is_RegShow = "";
                }
                foreach (DataRow dr in dtD.Rows)
                {
                    Show_of_student.Add(new Fetch_Details_of_student
                    {
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                        Fathername = dr["fathername"].ToString(),
                        Session_id = dr["Session_id"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        MotherName = dr["mothername"].ToString(),
                        Admission_no_date = dr["Admission_no_date"].ToString(),
                        Is_RegShow= is_RegShow,
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_student));
        }


        public class Fetch_Details_of_student_paid_status
        {
            public string Month_name { get; set; }
            public string Fee_head { get; set; }
            public string Amount { get; set; }
            public string Disc_amount { get; set; }
            public string Prev_paid { get; set; }
            public string Dues_amt { get; set; }
            public string PaymentStatus { get; set; }
        }

        List<Fetch_Details_of_student_paid_status> Show_of_student_paid_status = new List<Fetch_Details_of_student_paid_status>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_student_payment_status(string Admission_no, string Session_id, string Class_id)
        {
            string query = "select * from STUDENT_WISE_DUES_AMOUNT where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + Admission_no + "' order by Month_position asc";
            DataTable dtD = My.dataTable(query);
            if (dtD.Rows.Count > 0)
            {
                string overdues = "0"; string ischecked = "0";
                string cMonth = mycode.get_current_fullmonthname();
                string month_position = get_month_position(cMonth);
                foreach (DataRow dr in dtD.Rows)
                {
                    string paymentStatus = "";
                    if (My.toDouble(dr["Dues_amt"].ToString()) == 0)
                    {
                        paymentStatus = "duesZero";

                        if (ischecked == "0")
                        {
                            if (My.toDouble(dr["Dues_amt"].ToString()) == 0)
                            {
                                ischecked = "0";
                                overdues = "1";
                            }
                            else
                            {
                                ischecked = "1";
                            }
                        }
                    }
                    else
                    {
                        if (My.toDouble(month_position) > My.toDouble(dr["Month_position"].ToString()))
                        {
                            paymentStatus = "OverDues";
                            overdues = "0";
                            if (ischecked == "0")
                            {
                                if (My.toDouble(dr["Dues_amt"].ToString()) == 0)
                                {
                                    ischecked = "0";
                                    overdues = "1";
                                }
                                else
                                {
                                    ischecked = "1";
                                }
                            }
                        }
                        if (My.toDouble(month_position) == My.toDouble(dr["Month_position"].ToString()))
                        {
                            if (overdues == "1")
                            {
                                paymentStatus = "CurrentPayable";
                            }
                            else
                            {
                                paymentStatus = "OverDues";
                            }
                        }
                        if (My.toDouble(month_position) < My.toDouble(dr["Month_position"].ToString()))
                        {
                            paymentStatus = "UpcomingPayment";
                        }
                    }
                    Show_of_student_paid_status.Add(new Fetch_Details_of_student_paid_status
                    {
                        Month_name = dr["months"].ToString(),
                        Fee_head = dr["content"].ToString(),
                        Amount = dr["amount"].ToString(),
                        Disc_amount = dr["disc_amount"].ToString(),
                        Prev_paid = dr["Prev_paid"].ToString(),
                        Dues_amt = dr["Dues_amt"].ToString(),
                        PaymentStatus = paymentStatus,
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_student_paid_status));
        }

        private string get_month_position(string cMonth)
        {
            string position = "1";
            DataTable dt = My.dataTable("select Position from Month_Index where Month='" + cMonth + "'");
            if (dt.Rows.Count > 0)
            {
                position = dt.Rows[0]["Position"].ToString();
            }
            return position;
        }
    }
}
