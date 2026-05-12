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
    /// Summary description for studentQR
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class studentQR : System.Web.Services.WebService
    {
        public class Fatch_all_vertical_id_card
        {
            public string SchoolName { get; set; }
            public string SchoolAddress { get; set; }
            public string Contact_no { get; set; }
            public string Logo { get; set; }

            public string Session { get; set; }
            public string name { get; set; }
            public string Admission_no { get; set; }
            public string sclass { get; set; }
            public string sec { get; set; }
            public string roll_no { get; set; }
            public string father_name { get; set; }
            public string Mothername { get; set; }
            public string mobile { get; set; }
            public string dob { get; set; }
            public string Blood_group { get; set; }
            public string address { get; set; }
            public string QrCode { get; set; }
        }

        List<Fatch_all_vertical_id_card> Show_all_vertical_id_card = new List<Fatch_all_vertical_id_card>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_QR_student(string Session_id, string Class_id, string Sections, string Branch_id, string IdType, string Admission_no)
        {
            string query = "";
            if (IdType == "CHECK")
            {
                query = "select *,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1' and Id in(" + Admission_no + ") order by Section,rollnumber asc";
            }
            else
            {
                if (IdType == "BULK")
                {
                    if (Sections == "ALL")
                    {
                        query = "select *,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1'  order by Section,rollnumber asc";
                    }
                    else
                    {
                        query = "select *,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Section='" + Sections + "' and Status='1' order by Section,rollnumber asc";
                    }
                }
                else
                {
                    query = "select *,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where admissionserialnumber='" + Admission_no + "' and Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1' order by Section,rollnumber asc";
                }
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                DataTable dtF = My.dataTable("select * from Firm_Details");
                foreach (DataRow dr in dt.Rows)
                {
                    string QrCode = "https://api.qrserver.com/v1/create-qr-code/?data=Adm. No. :" + dr["admissionserialnumber"].ToString() + ", Name : " + dr["studentname"].ToString() + ",  Class: " + dr["class"].ToString() + ", Section: " + dr["Section"].ToString() + ", Roll No.: " + dr["rollnumber"].ToString() + ", Father's Name: " + dr["fathername"].ToString() + ", Mother's Name: " + dr["mothername"].ToString() + ", Mobile No.: " + dr["mobilenumber"].ToString() + ", Session: " + dr["session"].ToString() + "&amp;size=110x110";
                    Show_all_vertical_id_card.Add(new Fatch_all_vertical_id_card
                    {
                        SchoolName = dtF.Rows[0]["firm_name"].ToString(),
                        SchoolAddress = dtF.Rows[0]["address1"].ToString(),
                        Contact_no = dtF.Rows[0]["contact_no"].ToString(),
                        Logo = dtF.Rows[0]["logo"].ToString(),

                        Session = "Session : " + dr["session"].ToString(),
                        name = dr["studentname"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        sclass = dr["class"].ToString(),
                        sec = dr["Section"].ToString(),
                        roll_no = dr["rollnumber"].ToString(),
                        father_name = dr["fathername"].ToString(),
                        Mothername = dr["mothername"].ToString(),
                        mobile = dr["mobilenumber"].ToString(),

                        dob = dr["dob"].ToString(),
                        Blood_group = dr["blood_group"].ToString(),
                        address = dr["careof"].ToString(),
                        QrCode = QrCode,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_all_vertical_id_card));
            }
        }
    }
}
