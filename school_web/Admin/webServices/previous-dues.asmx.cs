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
    /// Summary description for previous_dues
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class previous_dues : System.Web.Services.WebService
    {
        public class Fetch_Details_of_back_year_dues
        {
            public string Session_id { get; set; }
            public string Dateofadmission { get; set; }
            public string Admission_no { get; set; }
            public string Session { get; set; }
            public string Class_name { get; set; }
            public string Class_id { get; set; }
            public string Rollnumber { get; set; }
            public string Studentname { get; set; }
            public string Fathername { get; set; }
            public string Mobilenumber { get; set; }
            public string Section { get; set; }
            public string Amount { get; set; }
            public string IsEditOption { get; set; }
            public string IsDeleteOption { get; set; }
            public string AlreadyPaid { get; set; }
            public string IsMonthAnuual { get; set; }
            public string Perticular { get; set; }
            public string RowiD { get; set; }
        }

        List<Fetch_Details_of_back_year_dues> Show_of_back_year_dues = new List<Fetch_Details_of_back_year_dues>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_of_back_year_dues(string Session_id, string Class_id)
        {
            string query = "";
            if (Class_id == "0")
            {
                query = "select t1.*,t2.studentname,t2.class,t2.Section,t2.Class_id,t2.rollnumber,t2.fathername,t2.mobilenumber from Misc_Fee_Master_Studentwise t1 join  admission_registor t2 on t1.Admission_No=t2.admissionserialnumber and t1.Session_id=t2.Session_id where t1.Session_id='" + Session_id + "' and Old_year_Dues_Type='Yes' order by t2.class,t2.rollnumber asc";
            }
            else
            {
                query = "select t1.*,t2.studentname,t2.class,t2.Section,t2.Class_id,t2.rollnumber,t2.fathername,t2.mobilenumber from Misc_Fee_Master_Studentwise t1 join  admission_registor t2 on t1.Admission_No=t2.admissionserialnumber and t1.Session_id=t2.Session_id where t1.Session_id='" + Session_id + "' and t2.Class_id='" + Class_id + "'  and Old_year_Dues_Type='Yes'  order by t2.class,t2.rollnumber asc";
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
                    string isMonthAnuual = "1001";
                    if (dr["Type_Mode"].ToString() == "AnnualFee")
                    {
                        isMonthAnuual = "ADM01";
                    }
                    string isFeeTaken = check_fee_taken(dr["Session"].ToString(), dr["Admission_No"].ToString(), dr["Perticular"].ToString(), isMonthAnuual);
                    string[] stringSeparatorss = new string[] { "月" };
                    string[] arrs = isFeeTaken.Split(stringSeparatorss, StringSplitOptions.None);
                    string isEdt = arrs[0];
                    string isDelte = arrs[1];
                    string alreadyPaid = arrs[2];

                    string isEditOption = "showd"; string isDeleteOption = "showd";
                    if (isEdt == "1")
                    {
                        isEditOption = "hidden";
                    }

                    if (isDelte == "1")
                    {
                        isDeleteOption = "hidden";
                    }

                    Show_of_back_year_dues.Add(new Fetch_Details_of_back_year_dues
                    {
                        Session_id = dr["Session_id"].ToString(),
                        Admission_no = dr["Admission_No"].ToString(),
                        Session = dr["Session"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Class_id = dr["Class_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                        Fathername = dr["fathername"].ToString(),
                        Mobilenumber = dr["mobilenumber"].ToString(),
                        Amount = dr["Amount"].ToString(),
                        IsEditOption = isEditOption,
                        IsDeleteOption = isDeleteOption,
                        AlreadyPaid = alreadyPaid,
                        IsMonthAnuual = isMonthAnuual,
                        Perticular = dr["Perticular"].ToString(),
                        RowiD = dr["Id"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_back_year_dues));
            }
        }

        private string check_fee_taken(string Session, string admission_no, string Perticular, string isMonthAnuual)
        {
            string status = "0月0月0";
            DataTable dt = My.dataTable("select * from Typewise_fee_collection where admission_no='" + admission_no + "' and Session='" + Session + "' and feetype='" + Perticular + "' and content_id='" + isMonthAnuual + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["status"].ToString() == "Paid")
                {
                    status = "1月1月" + dt.Rows[0]["paid"].ToString();
                }
                else if (My.toDouble(dt.Rows[0]["paid"].ToString()) == 0)
                {
                    status = "0月0月" + dt.Rows[0]["paid"].ToString();
                }
                else if (My.toDouble(dt.Rows[0]["paid"].ToString()) > 0)
                {
                    status = "0月1月" + dt.Rows[0]["paid"].ToString();
                }
            }
            return status;
        }



        //===--====Edit
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void update_fee(string Session_edt, string Session_id, string Admission_no, string Amount_Old, string Amount_New, string Updated_by, string Back_year_dues, string Paid_amt, string IsMonthAnuual, string Perticular,string RowiDEdt)
        {
            double acyual_dues_after_paid = (My.toDouble(Back_year_dues) - My.toDouble(Paid_amt));
            double paybleAmt = My.toDouble(Paid_amt) + My.toDouble(Amount_New);
            double cduesTypewise = (paybleAmt - My.toDouble(Paid_amt));
            string qry = "update Misc_Fee_Master_Studentwise set Amount='" + paybleAmt + "' where Id='"+ RowiDEdt + "'; update Typewise_fee_collection set payable='"+ paybleAmt + "',Payable_after_disc='" + paybleAmt + "',dues='"+ cduesTypewise + "' where admission_no='" + Admission_no + "' and Session='" + Session_edt + "' and feetype='" + Perticular + "' and content_id='" + IsMonthAnuual + "'";
            My.exeSql(qry);

            string desc = "Previuos year dues updated old dues :" + Amount_Old + " to " + Amount_New + " by " + Updated_by;
            log_hostory.edit_log(My.get_session_id(), "0", Admission_no, "Previuosyeardues", desc, "previous-year-dues-list.aspx", Updated_by);
        }


        //===--====DeletevideO
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void delete_dues(string Session_id, string Admission_no, string User_by)
        {
            string qryd = "delete from Misc_Fee_Master_Studentwise where Session_id='" + Session_id + "' and Admission_No='" + Admission_no + "' and Old_year_Dues_Type='Yes'";
            My.exeSql(qryd);

            string desc = "Previuos year dues deleted of admission no :" + Admission_no + " by " + User_by;
            log_hostory.delete_log(My.get_session_id(), "0", Admission_no, "DeletePreviuosyeardues", desc, "previous-year-dues-list.aspx", User_by);

        }
    }
}
