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

namespace school_web.Admin.slip.webServices
{
    /// <summary>
    /// Summary description for labels
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class labels : System.Web.Services.WebService
    {

        public class Fetch_Details_of_labels
        {
            public string Session { get; set; }
            public string Student_name { get; set; }
            public string Class_name { get; set; }
            public string Roll_number { get; set; }
            public string Admission_serial_number { get; set; }
            public string Father_name { get; set; }
            public string Address { get; set; }
            public string Mobile_number { get; set; }
        }

        List<Fetch_Details_of_labels> Show_of_labels = new List<Fetch_Details_of_labels>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_details_of_labels(string Session_id, string Class_id, string Admission_no, string Section, string Type)
        {
            string query = "";
            if (Type == "1")
            {
                query = "select * from admission_registor where Session_id=" + Session_id + " and Class_id=" + Class_id + " and admissionserialnumber='" + Admission_no + "' order by rollnumber";
            }
            else
            {
                query = "select * from admission_registor where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' order by rollnumber";
            }


            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_labels.Add(new Fetch_Details_of_labels
                    {
                        Session = dr["session"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Roll_number = dr["rollnumber"].ToString(),
                        Admission_serial_number = dr["admissionserialnumber"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Address = dr["careof"].ToString(),
                        Mobile_number = dr["mobilenumber"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_labels));
            }
        }


        ////===================================
        ///
        public class Fetch_Details_of_labels_check
        {
            public string Session { get; set; }
            public string Student_name { get; set; }
            public string Class_name { get; set; }
            public string Roll_number { get; set; }
            public string Admission_serial_number { get; set; }
            public string Father_name { get; set; }
            public string Address { get; set; }
            public string Mobile_number { get; set; }
        }

        List<Fetch_Details_of_labels_check> Show_of_labels_check = new List<Fetch_Details_of_labels_check>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_details_of_labels_check(string Session, string Admission_no)
        {
            string query = "select * from admission_registor where session='" + Session + "'   and admissionserialnumber in(" + Admission_no + ") order by rollnumber";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_labels_check.Add(new Fetch_Details_of_labels_check
                    {
                        Session = dr["session"].ToString(),
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Roll_number = dr["rollnumber"].ToString(),
                        Admission_serial_number = dr["admissionserialnumber"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Address = dr["careof"].ToString(),
                        Mobile_number = dr["mobilenumber"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_labels_check));
            }
        }
    }
}
