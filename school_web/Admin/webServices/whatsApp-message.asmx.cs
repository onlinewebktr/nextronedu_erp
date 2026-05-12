using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace school_web.Admin.webServices
{
    /// <summary>
    /// Summary description for whatsApp_message
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class whatsApp_message : System.Web.Services.WebService
    { 
        [WebMethod]
        public void send_whatsappmessage()  
        {
            try
            {
                My mycode = new My();
                SqlDataAdapter ad6 = new SqlDataAdapter("Select * from WhatsApp_send where Status='Pending'", My.con);
                DataSet ds6 = new DataSet();
                ad6.Fill(ds6, "WhatsApp_send");
                DataTable dt6 = ds6.Tables[0];
                if (dt6.Rows.Count > 0)
                {
                    string messageurl = "", Message_type = "";
                    foreach (DataRow dr in dt6.Rows)
                    {
                        messageurl = dr["Message_url"].ToString();
                        Message_type = dr["Mesage_Type"].ToString();

                        try
                        {
                            string sendStatus = My.get_single_column_data("Select Status as Column_Name from WhatsApp_send where Id='" + dr["Id"].ToString() + "'");
                            if (sendStatus == "Pending")
                            {
                                My.exeSql("update WhatsApp_send set Status='SEND' where Id='" + dr["Id"].ToString() + "'");
                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(messageurl);
                                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                string results = sr.ReadToEnd();
                                sr.Close();
                                My.Insert("Message_Details", new
                                {
                                    Mobile_No = dr["Mobile_no"].ToString(),
                                    Message = dr["Message"].ToString(),
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Result = results,
                                    User_id = dr["Send_by"].ToString(),
                                    Mesage_Type = dr["Mesage_Type"].ToString(),
                                    Groupcode = "Wahataap",
                                    Status = "SEND",
                                    Url = dr["Message_url"].ToString(),
                                    Message_to_Type = "Student",
                                    admin_user_id = dr["Admission_no"].ToString(),
                                });
                            }
                        }
                        catch
                        {
                            My.exeSql("update WhatsApp_send set Status='Network Error' where Id='" + dr["Id"].ToString() + "'");
                        }
                    }
                }
            }
            catch
            {

            }
    
        }
    }
}
