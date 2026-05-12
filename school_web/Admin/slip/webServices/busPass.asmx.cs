using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.slip.webServices
{
    /// <summary>
    /// Summary description for busPass
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class busPass : System.Web.Services.WebService
    {
        public class MyAdmitCardStudent
        {
            public string Student_name { get; set; }
            public string Mothername { get; set; }
            public string Father_name { get; set; }
            public string Student_mob_no { get; set; }
            public string Registration_id { get; set; }
            public string Course_Name { get; set; }
            public string Session_name { get; set; }
            public string Rollnumber { get; set; }
            public string Mobile_no { get; set; }
            public string Vehicle_no { get; set; }
            public string Section { get; set; }
            public string Boarding_point { get; set; }
            public string School_name { get; set; }
            public string Address { get; set; }
            public string Mobile_no_email { get; set; }
            public string LogoSchool { get; set; }
            public string WhatDiv { get; set; }
            public string WhatDivTB { get; set; }
            public string Print_date { get; set; }
            public string IsHeaderImageShow { get; set; }
            public string IsHeaderContentShow { get; set; }
            public string Header_images { get; set; }
        }



        List<MyAdmitCardStudent> EMySubMark = new List<MyAdmitCardStudent>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_demand_bill(string Session_id, string Class_id, string Admission_no, string Section, string Checked)
        {
            My mycode = new My();
            string query = "";
            if (Checked == "1")
            {
                query = "select t1.admissionserialnumber,t1.studentname,t1.fathername,t1.mothername,t1.class,t1.Section,t1.rollnumber,t1.father_mob,(select top 1 Boarding_Point from TRANSPORTATION_BOARDING_POINT where Session_Id=t1.Session_Id and Transportation_Id=t2.transport_id and Boarding_Point_id=t2.Boarding_Point_id) as Boarding_point,(select top 1 Bus_no from Transport_Master where transport_id=t2.transport_id) as Vehicle_no from admission_registor t1 join Student_mapping_with_TransportPath t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.transportationtaken='Yes' and t1.Session_id=" + Session_id + " and t1.Id in (" + Admission_no + ") and t1.Status='1' order by t1.Section,t1.rollnumber asc";
            }
            else
            {
                if (Section == "0")
                {
                    query = "select t1.admissionserialnumber,t1.studentname,t1.fathername,t1.mothername,t1.class,t1.Section,t1.rollnumber,t1.father_mob,(select top 1 Boarding_Point from TRANSPORTATION_BOARDING_POINT where Session_Id=t1.Session_Id and Transportation_Id=t2.transport_id and Boarding_Point_id=t2.Boarding_Point_id) as Boarding_point,(select top 1 Bus_no from Transport_Master where transport_id=t2.transport_id) as Vehicle_no from admission_registor t1 join Student_mapping_with_TransportPath t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.transportationtaken='Yes' and t1.Session_id=" + Session_id + " and t1.Class_id='" + Class_id + "' and t1.Status='1' order by t1.Section,t1.rollnumber asc";
                }
                else
                {
                    query = "select t1.admissionserialnumber,t1.studentname,t1.fathername,t1.mothername,t1.class,t1.Section,t1.rollnumber,t1.father_mob,(select top 1 Boarding_Point from TRANSPORTATION_BOARDING_POINT where Session_Id=t1.Session_Id and Transportation_Id=t2.transport_id and Boarding_Point_id=t2.Boarding_Point_id) as Boarding_point,(select top 1 Bus_no from Transport_Master where transport_id=t2.transport_id) as Vehicle_no from admission_registor t1 join Student_mapping_with_TransportPath t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.transportationtaken='Yes' and t1.Session_id=" + Session_id + " and t1.Class_id='" + Class_id + "' and Section='" + Section + "' and t1.Status='1' order by t1.Section,t1.rollnumber asc";
                }
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                string queryF = "select * from Firm_Details";
                DataTable dtfrm = My.dataTable(queryF);
                string isHeaderImageShow = "hidden"; string isHeaderContentShow = "showed";
                if (dtfrm.Rows[0]["Is_slip_header"].ToString() == "True")
                {
                    isHeaderImageShow = "showed";
                    isHeaderContentShow = "hidden";
                }
                bool loops = false; int topM = 1; bool loopsTB = false;
                foreach (DataRow dr in dt.Rows)
                {
                    string sectionS = dr["Section"].ToString();
                    if (dr["Section"].ToString().ToUpper() == "NA")
                    {
                        sectionS = "";
                    }
                    string roll_no = dr["rollnumber"].ToString();
                    if (dr["rollnumber"].ToString() == "0")
                    {
                        roll_no = "";
                    }


                    string whatDiv = "";
                    if (loops == false)
                    {
                        whatDiv = "lftdb";
                        loops = true;
                    }
                    else
                    {
                        loops = false;
                        whatDiv = "rghtdb";
                    }

                    string headerImg = dtfrm.Rows[0]["Header_images"].ToString();
                    if (dtfrm.Rows[0]["firm_id"].ToString() == "PLSN-01") { headerImg = "https://plsn.purnank.co.in/Master_Img/buspass-header001.png"; }
                    EMySubMark.Add(new MyAdmitCardStudent
                    {
                        Student_name = dr["studentname"].ToString(),
                        Mothername = dr["mothername"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Registration_id = dr["admissionserialnumber"].ToString(),
                        Course_Name = dr["class"].ToString(),
                        Section = sectionS,
                        Rollnumber = roll_no,
                        Mobile_no = dr["father_mob"].ToString(),
                        Vehicle_no = dr["Vehicle_no"].ToString(),
                        Boarding_point = dr["Boarding_point"].ToString(),
                        WhatDiv = whatDiv,
                        School_name = dtfrm.Rows[0]["firm_name"].ToString(),
                        Address = dtfrm.Rows[0]["address1"].ToString(),
                        Mobile_no_email = "Telephone No : " + dtfrm.Rows[0]["contact_no"].ToString() + " , E-mail Address : " + dtfrm.Rows[0]["email"].ToString(),
                        LogoSchool = dtfrm.Rows[0]["logo"].ToString(),
                        IsHeaderImageShow = isHeaderImageShow,
                        IsHeaderContentShow = isHeaderContentShow,
                        Header_images = headerImg,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }
    }
}
