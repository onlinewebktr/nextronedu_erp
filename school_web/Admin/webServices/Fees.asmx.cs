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
    /// Summary description for Fees
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Fees : System.Web.Services.WebService
    {
        My mycode = new My();
        public class Fetch_Dt_of_students
        {
            public string parameterDisc { get; set; }
            public string Class_id { get; set; }
            public string Session_id { get; set; }
            public string Section { get; set; }
            public string Rollnumber { get; set; }
            public string Studentname { get; set; }
            public string Fathername { get; set; }
            public string Class_name { get; set; }
            public string Admission_no { get; set; }
            public string Hosteltaken { get; set; }
            public string Transport_ntaken { get; set; }
            public string StudentImgPath { get; set; }
            public string Transfer_Status { get; set; }
            public string Contact_no { get; set; }
            public string Category_mame { get; set; }
            public string Sub_category_name { get; set; }
            public string Transport_name { get; set; }
            public string Transportpathpath { get; set; }
            public string Boarding_Point { get; set; }
            public string Month_name { get; set; }
            public string seatname { get; set; }
            public string TransPortDv { get; set; }

            public List<MyDuesMonth> MyDuesMonthItem { get; set; }
        }
        public class MyDuesMonth
        {
            public string Month { get; set; }
            public string Session_id { get; set; }
            public string Session_name { get; set; }
            public string Class_id { get; set; }
            public string Admission_no { get; set; }
            public string Parameter { get; set; }
            public string Parameter_id { get; set; }
            public string Is_quarterwise_payment { get; set; }
            public string ParameteridS { get; set; }
            public string Class_name { get; set; }
        }

        List<Fetch_Dt_of_students> Show_dt_of_student = new List<Fetch_Dt_of_students>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_student_details(string Session_id, string Admission_no, string Branch_id, string Is_quarterwise_payment)
        {
            string query = "select top 1 *,(Select top 1 Category_Name from Category_Details  where Category_Id=admission_registor.Category_id) as Category_Name,(Select top 1 Sub_CategoryName from Sub_Category_Details  where Category_Id=admission_registor.Category_id  and Sub_CategoryId=admission_registor.SubCategory_id) as Sub_category_name from admission_registor where admissionserialnumber='" + Admission_no + "' and Session_id='" + Session_id + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id desc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
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
                    string parameter = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    string parameter_id = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";

                    string parameteridS = "4";


                    List<MyDuesMonth> Monthdetails = findDuesMonth(dr["Session_id"].ToString(), dr["session"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), parameter, parameter_id, Is_quarterwise_payment, parameteridS, dr["class"].ToString());

                    string studentImgPath = dr["studentimagepath"].ToString();
                    if (studentImgPath == "")
                    {
                        studentImgPath = "hidden";
                    }

                    string Transfer_Status = dr["Transfer_Status"].ToString();
                    if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                    {
                        Transfer_Status = dr["Transfer_Status_Old"].ToString();
                    }

                    if (Transfer_Status == "New")
                    {
                    }
                    else
                    {
                    }

                    Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString());


                    string transPortDv = "showd"; string Transportname = ""; string Transportpathpath = ""; string Boarding_Point = ""; string Month_name = ""; string seatname = "";
                    if ((String)dc2["Transport_Assigned_Id"].ToString() == "0")
                    {
                        transPortDv = "hidden";
                    }
                    else
                    {
                        Transportname = (String)dc2["Transportname"];
                        Transportpathpath = (String)dc2["Transportpathpath"];
                        Boarding_Point = (String)dc2["Boarding_Point"];
                        Month_name = (String)dc2["Month_name"];
                        seatname = (String)dc2["seatname"];
                    }


                    Show_dt_of_student.Add(new Fetch_Dt_of_students
                    {
                        parameterDisc = "3",
                        Class_id = dr["Class_id"].ToString(),
                        Session_id = dr["Session_id"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                        Fathername = dr["fathername"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Hosteltaken = dr["hosteltaken"].ToString(),
                        Transport_ntaken = dr["transportationtaken"].ToString(),
                        StudentImgPath = studentImgPath,
                        Transfer_Status = Transfer_Status,
                        Contact_no = dr["mobilenumber"].ToString(),
                        Category_mame = dr["Category_Name"].ToString(),
                        Sub_category_name = dr["Sub_category_name"].ToString(),
                        Transport_name = Transportname,
                        Transportpathpath = Transportpathpath,
                        Boarding_Point = Boarding_Point,
                        Month_name = Month_name,
                        seatname = seatname,
                        TransPortDv = transPortDv,
                        MyDuesMonthItem = Monthdetails,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_dt_of_student));
            }
        }

        private List<MyDuesMonth> findDuesMonth(string Session_id, string session_name, string Class_id, string admission_no, string parameter, string parameter_id, string Is_quarterwise_payment, string parameteridS, string class_name)
        {
            List<MyDuesMonth> MyDuesMonthItem = new List<MyDuesMonth>();
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("value");
            dtDatas.Columns.Add("fee_amount");
            dtDatas.Columns.Add("discount_per");
            dtDatas.Columns.Add("paid_peev");
            dtDatas.Columns.Add("paid_status");
            dtDatas.Columns.Add("bac_colour");
            int temp;
            List<string> lst = new List<string>();
            string temp_month = My.get_start_month();

            for (temp = 1; temp <= 12; temp++)
            {
                DataTable dtstnG = My.dataTable("select Pair_group_id from Custome_month_selection_setting where Month_name='" + temp_month + "'");
                if (dtstnG.Rows.Count > 0)
                {
                    string months = My.get_months_group(dtstnG.Rows[0]["Pair_group_id"].ToString());
                    DataTable paid_dt = My.dataTable("select month,status from dbo.[Typewise_fee_collection] where session='" + session_name + "' and admission_no='" + admission_no + "' and parameter like '%" + parameter + "%' and month in (" + months + ")");
                    if (paid_dt.Rows.Count > 0)
                    {
                        string remove_month = "";
                        foreach (DataRow pdr in paid_dt.Rows)
                        {
                            if (pdr["status"].ToString() == "Dues")
                            {
                                lst.Add(temp_month);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (Is_quarterwise_payment == "1")
                        {
                            DataTable dtFee = My.dataTable("select isnull(sum(convert(float, amount)),0) as Total_fee from Fee_master_content_wise where parameter_id='" + parameteridS + "' and session_id='" + Session_id + "' and class_id='" + Class_id + "' and Month='" + temp_month + "'");
                            {
                                if (My.toDouble(dtFee.Rows[0]["Total_fee"].ToString()) == 0)
                                {
                                }
                                else
                                {
                                    lst.Add(temp_month);
                                }
                            }
                        }
                        else
                        {
                            lst.Add(temp_month);
                        }
                    }
                }
                else
                {
                    DataTable paid_dt = My.dataTable("select month,status from dbo.[Typewise_fee_collection] where   session='" + Session + "' and admission_no='" + admission_no + "' and parameter like '%" + parameter + "%' and month='" + temp_month + "'");
                    if (paid_dt.Rows.Count > 0)
                    {
                        string remove_month = "";
                        foreach (DataRow pdr in paid_dt.Rows)
                        {
                            if (pdr["status"].ToString() == "Dues")
                            {
                                lst.Add(temp_month);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (Is_quarterwise_payment == "1")
                        {
                            DataTable dtFee = My.dataTable("select isnull(sum(convert(float, amount)),0) as Total_fee from Fee_master_content_wise where parameter_id='" + parameteridS + "' and session_id='" + Session_id + "' and class_id='" + Class_id + "' and Month='" + temp_month + "'");
                            {
                                if (My.toDouble(dtFee.Rows[0]["Total_fee"].ToString()) == 0)
                                {
                                }
                                else
                                {
                                    lst.Add(temp_month);
                                }
                            }
                        }
                        else
                        {
                            lst.Add(temp_month);
                        }
                    }
                }

                temp_month = find_month(temp_month);
            }

            int i = 0;
            foreach (string str in lst)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Month"] = lst[i];
                drNewRow["value"] = false;
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            find_prev_dues(dtDatas, Session_id, session_name, Class_id, admission_no, parameter, parameter_id, Is_quarterwise_payment, parameteridS, class_name);

            foreach (DataRow dr in dtDatas.Rows)
            {
                MyDuesMonthItem.Add(new MyDuesMonth
                {
                    Month = dr["Month"].ToString(),
                    Session_id = Session_id,
                    Session_name = session_name,
                    Class_id = Class_id,
                    Admission_no = admission_no,
                    Parameter = parameter,
                    Parameter_id = parameter_id,
                    Is_quarterwise_payment = Is_quarterwise_payment,
                    ParameteridS = parameteridS,
                    Class_name = class_name,
                });
            }
            return MyDuesMonthItem;
        }


        private void find_prev_dues(DataTable dtDatas, string Session_id, string session_name, string Class_id, string admission_no, string parameter, string parameter_id, string Is_quarterwise_payment, string parameteridS, string class_name)
        {
            DataTable prevdues_dt = new DataTable();
            prevdues_dt = My.dataTable(" select sum(isnull(cast(dues as float),'0')) dues,month from dbo.[Typewise_fee_collection] where session='" + Session + "' and status='Dues' and Class='" + class_name + "' and admission_no='" + admission_no + "' and parameter='" + parameter + "' group by month");
            foreach (DataRow mr in dtDatas.Rows)
            {
                var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                if (row.Length > 0)
                {
                    mr["paid_status"] = "Created";
                    mr["bac_colour"] = "Yellow";
                }
            }
            show_dues(dtDatas, prevdues_dt);
        }

        private void show_dues(DataTable dtDatas, DataTable prevdues_dt)
        {
            double anula_dues = 0; double prev_session_dues = 0;
            double admission_dues = 0; string adm_transection = "";
            double month_dues = 0;
            foreach (DataRow mr in dtDatas.Rows)
            {
                if (My.toBool(mr["Value"]))
                {
                    var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                    if (row.Length > 0)
                    {
                        DataRow dr = row[0];
                        month_dues += My.toDouble(dr["dues"]);
                    }
                }
            }
            if (month_dues + admission_dues + anula_dues + prev_session_dues == 0)
            {
            }
            else
            {
            }
        }


        private string find_month(string month)
        {
            string next = "nn";
            switch (month)
            {
                case "January":
                    next = "February";
                    return next;

                case "February":
                    next = "March";
                    return next;

                case "March":
                    next = "April";
                    return next;

                case "April":
                    next = "May";
                    return next;

                case "May":
                    next = "June";
                    return next;

                case "June":
                    next = "July";
                    return next;

                case "July":
                    next = "August";
                    return next;

                case "August":
                    next = "September";
                    return next;

                case "September":
                    next = "October";
                    return next;

                case "October":
                    next = "November";
                    return next;

                case "November":
                    next = "December";
                    return next;

                case "December":
                    next = "January";
                    return next;

            }
            return next;
        }

    }
}
