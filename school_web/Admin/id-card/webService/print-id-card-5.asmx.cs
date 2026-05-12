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

namespace school_web.Admin.id_card.webService
{
    /// <summary>
    /// Summary description for print_id_card_5
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class print_id_card_5 : System.Web.Services.WebService
    {
        public class Fatch_all_vertical_id_card
        {
            public string Admission_no { get; set; }
            public string photo { get; set; }
            public string name { get; set; }
            public string father_name { get; set; }
            public string Mothername { get; set; }
            public string sclass { get; set; }
            public string roll_no { get; set; }
            public string dob { get; set; }
            public string Blood_group { get; set; }
            public string address { get; set; }
            public string mobile { get; set; }
            public string idcard_template { get; set; }
            public string sec { get; set; }
            public string gender { get; set; }
            public string So_or_Do { get; set; }
            public string Transports { get; set; }
            public string Session { get; set; }
            public string Session_only { get; set; }
        }

        List<Fatch_all_vertical_id_card> Show_all_vertical_id_card = new List<Fatch_all_vertical_id_card>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_all_verticle_id_card_details(string Session_id, string Class_id, string Sections, string Branch_id, string IdType, string Admission_no)
        {
            string query = "";
            if (IdType == "CHECK")
            {
                query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1' and Id in(" + Admission_no + ")";
            }
            else
            {
                if (IdType == "BULK")
                {
                    if (Sections == "ALL")
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1'";
                    }
                    else
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Section='" + Sections + "' and Status='1'";
                    }
                }
                else
                {
                    query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template from admission_registor where admissionserialnumber='" + Admission_no + "' and Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1'";
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Registration_db");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string transportationtaken = "SELF";
                    string gender = dr["gender"].ToString();

                    string so_do;
                    if (gender.ToUpper() == "FEMALE")
                    {
                        so_do = "D/O";
                    }
                    else
                    {
                        so_do = "S/O";
                    }
                    if (dr["transportationtaken"].ToString().ToUpper() == "YES")
                    {
                        transportationtaken = "VAN";
                    }

                    DataTable dtUpdated = My.dataTable("select * from Id_card_updated_student where Session_id='" + dr["Session_Id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' order by Id desc");
                    if (dtUpdated.Rows.Count > 0)
                    {
                        if (dtUpdated.Rows[0]["Transport_taken"].ToString().ToUpper() == "YES")
                        {
                            try
                            {
                                transportationtaken = dtUpdated.Rows[0]["Transport_type"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }

                            if (transportationtaken == "")
                            {
                                transportationtaken = "VAN";
                            }
                        }

                        Show_all_vertical_id_card.Add(new Fatch_all_vertical_id_card
                        {
                            Admission_no = dr["admissionserialnumber"].ToString(),
                            photo = dr["studentimagepath"].ToString(),
                            name = dtUpdated.Rows[0]["Name"].ToString(),
                            father_name = dtUpdated.Rows[0]["Father_name"].ToString(),
                            Mothername = dtUpdated.Rows[0]["Mother_full_name"].ToString(),
                            sclass = dtUpdated.Rows[0]["Class_name"].ToString(),
                            sec = dtUpdated.Rows[0]["Section"].ToString(),
                            roll_no = dtUpdated.Rows[0]["Roll_no"].ToString(),
                            dob = dtUpdated.Rows[0]["Date_of_birth"].ToString(),
                            Blood_group = dr["blood_group"].ToString(),
                            address = dtUpdated.Rows[0]["Address"].ToString(),
                            mobile = dtUpdated.Rows[0]["Mobile_no"].ToString(),
                            idcard_template = dr["idcard_template"].ToString(),
                            So_or_Do = so_do,
                            Transports = transportationtaken,
                            Session = "Session : " + dr["session"].ToString(),
                            Session_only= dr["session"].ToString(),
                        });
                    }
                    else
                    {
                        Show_all_vertical_id_card.Add(new Fatch_all_vertical_id_card
                        {
                            Admission_no = dr["admissionserialnumber"].ToString(),
                            photo = dr["studentimagepath"].ToString(),
                            name = dr["studentname"].ToString(),
                            father_name = dr["fathername"].ToString(),
                            Mothername = dr["mothername"].ToString(),
                            sclass = dr["class"].ToString(),
                            sec = dr["Section"].ToString(),
                            roll_no = dr["rollnumber"].ToString(),
                            dob = dr["dob"].ToString(),
                            Blood_group = dr["blood_group"].ToString(),
                            address = dr["careof"].ToString(), // + "," + dr["postoffice"].ToString() + ", " + dr["district"].ToString() + ", " + dr["pin"].ToString(),
                            mobile = dr["mobilenumber"].ToString(),
                            idcard_template = dr["idcard_template"].ToString(),
                            So_or_Do = so_do,
                            Transports = transportationtaken,
                            Session = "Session : " + dr["session"].ToString(),
                            Session_only = dr["session"].ToString(),
                        });
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_all_vertical_id_card));
            }
        }
    }
}
