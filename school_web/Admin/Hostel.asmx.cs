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
    /// Summary description for Hostel
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Hostel : System.Web.Services.WebService
    {
        #region BEDAVAILABILITY
        My mycode = new My();
        public class MyBedAvailabilityShow
        {
            public string Hostel_id { get; set; }
            public string Room_id { get; set; }
            public string Room_name { get; set; }
            public string Hostel_name { get; set; }
            public string No_of_room { get; set; }
            public string No_of_bed { get; set; }
            public List<MyBedDetails> MyBookingBeDs { get; set; }

        }

        public class MyBedDetails
        {
            public string Bed_name { get; set; }
            public string Bed_id { get; set; }
            public string Room_id { get; set; }
            public string Hostel_id { get; set; }
            public string Admission_no { get; set; }
            public string studentname { get; set; }
            public string session { get; set; }
            public string Class_name { get; set; }
            public string Current_Semester_or_Year { get; set; }
            public string Assign_date { get; set; }
            public string Is_bed_assigned { get; set; }
            public string BackgrounDS { get; set; }
        }

        List<MyBedAvailabilityShow> EMyBedBooking = new List<MyBedAvailabilityShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Bed_available(string Hostel, string RoomCode, string BedCode)
        {
            string qry = "";
            if (Hostel != "0" && RoomCode == "0" && BedCode == "0")
            {
                qry = "select DISTINCT t1.Hostel_id,t1.Room_id,t2.Room_name,(select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name,(select count (id) from Hostel_room_master where Hostel_id='" + Hostel + "') as No_of_room,(select count (id) from Hostel_room_bed_master where Hostel_id=t1.Hostel_id) as No_of_bed from Hostel_room_bed_master t1 join Hostel_room_master t2 on t1.Room_id=t2.Room_id where t1.Hostel_id='" + Hostel + "' order by Room_name asc";
            }
            if (Hostel != "0" && RoomCode != "0" && BedCode == "0")
            {
                qry = "select DISTINCT t1.Hostel_id,t1.Room_id,t2.Room_name,(select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name,(select count (id) from Hostel_room_master where Hostel_id='" + Hostel + "') as No_of_room,(select count (id) from Hostel_room_bed_master where Hostel_id=t1.Hostel_id) as No_of_bed from Hostel_room_bed_master t1 join Hostel_room_master t2 on t1.Room_id=t2.Room_id where t1.Hostel_id='" + Hostel + "' and t1.Room_id='" + RoomCode + "' order by Room_name asc";
            }
            if (Hostel != "0" && RoomCode != "0" && BedCode != "0")
            {
                qry = "select DISTINCT t1.Hostel_id,t1.Room_id,t2.Room_name,(select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name,(select count (id) from Hostel_room_master where Hostel_id='" + Hostel + "') as No_of_room,(select count (id) from Hostel_room_bed_master where Hostel_id=t1.Hostel_id) as No_of_bed from Hostel_room_bed_master t1 join Hostel_room_master t2 on t1.Room_id=t2.Room_id where t1.Hostel_id='" + Hostel + "' and t1.Room_id='" + RoomCode + "' and t1.Bed_id='" + BedCode + "' order by Room_name asc";
            }
            SqlCommand cmd = new SqlCommand(qry);
            DataTable dt = mycode.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyBedDetails> MBdetails = findmyBedDetails(dr["Hostel_id"].ToString(), dr["Room_id"].ToString(), BedCode);
                    EMyBedBooking.Add(new MyBedAvailabilityShow
                    {
                        Hostel_id = dr["Hostel_id"].ToString(),
                        Room_id = dr["Room_id"].ToString(),
                        Room_name = dr["Room_name"].ToString(),
                        Hostel_name = dr["Hostel_name"].ToString(), 
                        No_of_room = dr["No_of_room"].ToString(),
                        No_of_bed = dr["No_of_bed"].ToString(), 
                        MyBookingBeDs = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBedBooking));
            }
        }

        private List<MyBedDetails> findmyBedDetails(string Hostel_id, string Room_id, string BedCode)
        {
            string sessionId = My.get_session_id();
            List<MyBedDetails> MyBookingBeDs = new List<MyBedDetails>();
            string qry = "";
            if (BedCode == "0")
            {
                qry = "select t3.Section,t3.rollnumber,t1.Hostel_id,t1.Room_id,t1.Bed_id,t1.Bed_name,t2.Session_id,t2.Class_id,t2.Admission_no,t2.From_month_name,t2.Hostel_assign_id,(select count (id) from Hostel_assign_master where Hostel_id=t1.Hostel_id and Room_id=t1.Room_id and Bed_id=t1.Bed_id and Status='1'  and Session_id='" + sessionId + "') as Is_bed_assigned,t2.Created_date as Assign_date,t3.session,t3.studentname,t3.class as Class_name from Hostel_room_bed_master t1 LEFT JOIN Hostel_assign_master t2 on t1.Bed_id=t2.Bed_id and t2.Status='1' and t2.Session_id='" + sessionId + "' LEFT JOIN admission_registor t3 on t2.Session_id=t3.Session_id and t2.Class_id=t3.Class_id and t2.Admission_no=t3.admissionserialnumber where t1.Hostel_id='" + Hostel_id + "' and t1.Room_id='" + Room_id + "' order by t1.Id asc";
            }
            else
            {
                qry = "select t3.Section,t3.rollnumber,t1.Hostel_id,t1.Room_id,t1.Bed_id,t1.Bed_name,t2.Session_id,t2.Class_id,t2.Admission_no,t2.From_month_name,t2.Hostel_assign_id,(select count (id) from Hostel_assign_master where Hostel_id=t1.Hostel_id and Room_id=t1.Room_id and Bed_id=t1.Bed_id and Status='1'  and Session_id='" + sessionId + "') as Is_bed_assigned,t2.Created_date as Assign_date,t3.session,t3.studentname,t3.class as Class_name  from Hostel_room_bed_master t1 LEFT JOIN Hostel_assign_master t2 on t1.Bed_id=t2.Bed_id and t2.Status='1' and t2.Session_id='" + sessionId + "' LEFT JOIN admission_registor t3 on t2.Session_id=t3.Session_id and t2.Class_id=t3.Class_id  and t2.Admission_no=t3.admissionserialnumber where t1.Hostel_id='" + Hostel_id + "' and t1.Room_id='" + Room_id + "' and t1.Bed_id='" + BedCode + "' order by t1.Id asc";
            }
            SqlCommand cmd = new SqlCommand(qry);
            DataTable dt = mycode.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string bed_asined_status = ""; string student_name = "NA"; string Student_class = "NA"; string session_name = "NA"; string current_sem_year = "NA"; string assign_date = "NA"; string backgrnds = "bedAvals";
                    string isBedAssigned = dr["Is_bed_assigned"].ToString();

                    student_name = dr["studentname"].ToString();
                    if (isBedAssigned != "0")
                    {
                        bed_asined_status = "Not Available";
                    }
                    else
                    {
                        bed_asined_status = "Available";
                    }
                    if (bed_asined_status == "Not Available")
                    {
                        student_name = dr["studentname"].ToString();
                        Student_class = dr["Class_name"].ToString();
                        session_name = dr["session"].ToString();
                        current_sem_year = dr["Section"].ToString() +" Roll No."+ dr["rollnumber"].ToString(); //dr["Current_Semester_or_Year"].ToString();
                        assign_date = dr["Assign_date"].ToString();
                        backgrnds = "bedNotAvals";
                    }


                    MyBookingBeDs.Add(new MyBedDetails
                    {
                        Bed_name = dr["Bed_name"].ToString(),
                        Bed_id = dr["Bed_id"].ToString(),
                        Room_id = dr["Room_id"].ToString(),
                        Hostel_id = dr["Hostel_id"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                        Is_bed_assigned = bed_asined_status,


                        studentname = student_name,
                        session = session_name,
                        Class_name = Student_class,
                        Current_Semester_or_Year = current_sem_year,
                        Assign_date = assign_date,
                        BackgrounDS = backgrnds
                    });
                }
            }
            return MyBookingBeDs;
        }

        #endregion

    }
}
