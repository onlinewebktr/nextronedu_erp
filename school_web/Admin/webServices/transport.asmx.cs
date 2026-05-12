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
    /// Summary description for transport
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class transport : System.Web.Services.WebService
    {

        #region SEATAVAILABILITY
        My mycode = new My();
        public class MySeatAvailabilityShow
        {
            public string Transport_id { get; set; }
            public string Transport_name { get; set; }
            public string Bus_owner_name { get; set; }
            public string Vehicle_Registration_No { get; set; }
            public string Transportation_route { get; set; }
            public string No_of_seat { get; set; }

            public string Occupied { get; set; }
            public string Vaccant { get; set; }

            public List<MySeatDetails> MyVechilesSeats { get; set; }
        }

        public class MySeatDetails
        {
            public string Sheet_No { get; set; }
            public string Sheet_Id { get; set; }
            public string TransportationPath_id { get; set; }
            public string Transportation_Id { get; set; }
            public string Admission_no { get; set; }
            public string studentname { get; set; }
            public string session { get; set; }
            public string Class_name { get; set; }
            public string Current_Semester_or_Year { get; set; }
            public string Assign_date { get; set; }
            public string Is_seat_assigned { get; set; }
            public string BackgrounDS { get; set; }
            public string AvalHidden { get; set; }
            public string Avaltop { get; set; }
        }

        List<MySeatAvailabilityShow> EMyBedBooking = new List<MySeatAvailabilityShow>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void seat_available_ststua(string Session_id, string Vehicle_id, string Route_id)
        {
            string qry = "";
            if (Vehicle_id == "0" && Route_id == "0")
            {
                qry = "select DISTINCT t1.transport_name,t1.transport_id,t1.Bus_owner_name,t1.Vehicle_Registration_No,t2.TransportationPath_id,(select top 1 Rootname from TransportationPath where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as Transportation_route,(select count (id) from TRANSPORT_PATH_MAPPING_WITH_SHEET where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as No_of_seat from Transport_Master t1 join TRANSPORT_PATH_MAPPING_WITH_SHEET t2 on t1.transport_id=t2.Transportation_Id order by t1.transport_name asc";
            }
            else if (Vehicle_id != "0" && Route_id == "0")
            {
                qry = "select DISTINCT t1.transport_name,t1.transport_id,t1.Bus_owner_name,t1.Vehicle_Registration_No,t2.TransportationPath_id,(select top 1 Rootname from TransportationPath where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as Transportation_route,(select count (id) from TRANSPORT_PATH_MAPPING_WITH_SHEET where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as No_of_seat from Transport_Master t1 join TRANSPORT_PATH_MAPPING_WITH_SHEET t2 on t1.transport_id=t2.Transportation_Id where t1.transport_id='" + Vehicle_id + "' order by t1.transport_name asc";
            }
            else if (Vehicle_id != "0" && Route_id != "0")
            {
                qry = "select DISTINCT t1.transport_name,t1.transport_id,t1.Bus_owner_name,t1.Vehicle_Registration_No,t2.TransportationPath_id,(select top 1 Rootname from TransportationPath where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as Transportation_route,(select count (id) from TRANSPORT_PATH_MAPPING_WITH_SHEET where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as No_of_seat from Transport_Master t1 join TRANSPORT_PATH_MAPPING_WITH_SHEET t2 on t1.transport_id=t2.Transportation_Id  where t1.transport_id='" + Vehicle_id + "' and t2.TransportationPath_id='" + Route_id + "' order by t1.transport_name asc";
            }
            else
            {
                qry = "select DISTINCT t1.transport_name,t1.transport_id,t1.Bus_owner_name,t1.Vehicle_Registration_No,t2.TransportationPath_id,(select top 1 Rootname from TransportationPath where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as Transportation_route,(select count (id) from TRANSPORT_PATH_MAPPING_WITH_SHEET where Transportation_Id=t1.transport_id and TransportationPath_id=t2.TransportationPath_id) as No_of_seat from Transport_Master t1 join TRANSPORT_PATH_MAPPING_WITH_SHEET t2 on t1.transport_id=t2.Transportation_Id order by t1.transport_name asc";
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
                    List<MySeatDetails> MBdetails = findmyBedDetails(Session_id, dr["transport_id"].ToString(), dr["TransportationPath_id"].ToString());

                    string seat_details = get_seat_details(Session_id, dr["transport_id"].ToString(), dr["TransportationPath_id"].ToString());
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = seat_details.Split(stringSeparatorss, StringSplitOptions.None);
                    string occupied = arrs[0];
                    string vaccant = arrs[1];

                    EMyBedBooking.Add(new MySeatAvailabilityShow
                    {
                        Transport_id = dr["transport_id"].ToString(),
                        Transport_name = dr["transport_name"].ToString(),
                        Bus_owner_name = dr["Bus_owner_name"].ToString(),
                        Vehicle_Registration_No = dr["Vehicle_Registration_No"].ToString(),
                        Transportation_route = dr["Transportation_route"].ToString(),
                        No_of_seat = dr["No_of_seat"].ToString(),
                        Occupied = occupied,
                        Vaccant = vaccant,
                        MyVechilesSeats = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBedBooking));
            }
        }

        private string get_seat_details(string Session_id, string Transport_id, string TransportationPath_id)
        {
            string vacant = "0"; string occupied = "0";
            DataTable dt = My.dataTable("select count(Is_seat_assigned) as Total,Status from (select *,CASE WHEN Is_seat_assigned = '0' THEN 'Vacant' WHEN Is_seat_assigned= '1' THEN 'Occupied' END AS Status from (select (select count (id) from Student_mapping_with_TransportPath where transport_id=t1.Transportation_Id and TransportPath_id=t1.TransportationPath_id and Sheet_Id=t1.Sheet_No and Session_id='" + Session_id + "') as Is_seat_assigned from TRANSPORT_PATH_MAPPING_WITH_SHEET t1 LEFT JOIN Student_mapping_with_TransportPath t2 on t1.Transportation_Id=t2.transport_id and t1.TransportationPath_id=t2.TransportPath_id and t1.Sheet_No=t2.Sheet_Id and t2.Session_id='" + Session_id + "' LEFT JOIN admission_registor t3 on t2.Session_id=t3.Session_id and t2.Class_id=t3.Class_id and t2.Admission_no=t3.admissionserialnumber where  t1.Transportation_Id='" + Transport_id + "' and t1.TransportationPath_id='" + TransportationPath_id + "') t) y GROUP by Status order by Status asc");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 2)
                {
                    occupied = dt.Rows[0][0].ToString();
                    vacant = dt.Rows[1][0].ToString();
                }
                else
                {
                    if (dt.Rows[0][1].ToString() == "Occupied")
                    {
                        occupied = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        vacant = dt.Rows[0][0].ToString();
                    }
                }

            }
            return occupied + "/" + vacant;
        }

        private List<MySeatDetails> findmyBedDetails(string Session_id, string Transport_id, string TransportationPath_id)
        {
            List<MySeatDetails> MyVechilesSeats = new List<MySeatDetails>();
            string qry = "select t3.Section,t3.rollnumber,t1.Sheet_Id,t1.Transportation_Id,t1.TransportationPath_id,t1.Sheet_No,t2.Session_id,t2.Class_id,t2.Admission_no,t2.Month_name,t2.Transport_Assigned_Id,(select count (id) from Student_mapping_with_TransportPath where transport_id=t1.Transportation_Id and TransportPath_id=t1.TransportationPath_id and Sheet_Id=t1.Sheet_No and Session_id='" + Session_id + "') as Is_seat_assigned,t2.Created_date as Assign_date,t3.session,t3.studentname,t3.class as Class_name from TRANSPORT_PATH_MAPPING_WITH_SHEET t1 LEFT JOIN Student_mapping_with_TransportPath t2 on t1.Transportation_Id=t2.transport_id and t1.TransportationPath_id=t2.TransportPath_id and t1.Sheet_No=t2.Sheet_Id and t2.Session_id='" + Session_id + "' LEFT JOIN admission_registor t3 on t2.Session_id=t3.Session_id and t2.Class_id=t3.Class_id and t2.Admission_no=t3.admissionserialnumber where t1.Transportation_Id='" + Transport_id + "' and t1.TransportationPath_id='" + TransportationPath_id + "' order by t1.Sheet_Id asc";
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
                    string avaltop = ""; string avalHidden = ""; string seat_asined_status = ""; string student_name = "NA"; string Student_class = "NA"; string session_name = "NA"; string current_sem_year = "NA"; string assign_date = "NA"; string backgrnds = "bedAvals";
                    string isSeatAssigned = dr["Is_seat_assigned"].ToString();

                    student_name = dr["studentname"].ToString();
                    if (isSeatAssigned != "0")
                    {
                        avalHidden = "";
                        seat_asined_status = "Occupied";
                        avaltop = "";
                    }
                    else
                    {
                        avalHidden = "hidden";
                        seat_asined_status = "Vacant";
                        avaltop = "topminus50";
                    }
                    if (seat_asined_status == "Occupied")
                    {
                        student_name = dr["studentname"].ToString();
                        Student_class = dr["Class_name"].ToString();
                        session_name = dr["session"].ToString();
                        current_sem_year = dr["Section"].ToString() + " Roll No." + dr["rollnumber"].ToString();
                        assign_date = dr["Assign_date"].ToString();
                        backgrnds = "bedNotAvals";
                    }


                    MyVechilesSeats.Add(new MySeatDetails
                    {
                        Sheet_No = dr["Sheet_No"].ToString(),
                        Sheet_Id = dr["Sheet_Id"].ToString(),
                        TransportationPath_id = dr["TransportationPath_id"].ToString(),
                        Transportation_Id = dr["Transportation_Id"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                        Is_seat_assigned = seat_asined_status,
                        studentname = student_name,
                        session = session_name,
                        Class_name = Student_class,
                        Current_Semester_or_Year = current_sem_year,
                        Assign_date = assign_date,
                        BackgrounDS = backgrnds,
                        AvalHidden = avalHidden,
                        Avaltop = avaltop,
                    });
                }
            }
            return MyVechilesSeats;
        }

        #endregion
    }
}
