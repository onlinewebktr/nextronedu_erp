using Newtonsoft.Json;
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
    /// Summary description for graph
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class graph : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string weekly_revenue_section(string From)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_Graph_Report");
            if (From == "Home")
            {
                cmd.Parameters.AddWithValue("@sp_status", "8");
            }
            else
            {
                cmd.Parameters.AddWithValue("@sp_status", "5");
            }

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["class"].ToString());
                    yaxis.Add(dr["total_amt"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        public class inpsection_Area
        {
            public List<string> xaxis { get; set; }
            public List<string> yaxis { get; set; }

        }


        //OrderSummarY
        [WebMethod(EnableSession = true)]
        public string find_order_summary_report_report(string SessioN)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_Graph_Report");
            if (SessioN == "0")
            {
                cmd.Parameters.AddWithValue("@sp_status", "1");
            }
            else
            {
                cmd.Parameters.AddWithValue("@sp_status", "11");
                cmd.Parameters.AddWithValue("@Session_id", SessioN);
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["class"].ToString());
                    yaxis.Add(dr["total_adm"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        //PaymentHistorY=======================
        [WebMethod(EnableSession = true)]
        public string find_payment_history_report(string From, string SessioN)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_Graph_Report");
            if (From == "Home")
            {
                cmd.Parameters.AddWithValue("@sp_status", "2");
            }
            else
            {
                cmd.Parameters.AddWithValue("@sp_status", "5");
            }
            //if (SessioN == "0")
            //{
            //    if (From == "Home")
            //    {
            //        cmd.Parameters.AddWithValue("@sp_status", "2");
            //    }
            //    else
            //    {
            //        cmd.Parameters.AddWithValue("@sp_status", "5");
            //    }
            //}
            //else
            //{
            //    if (From == "Home")
            //    {
            //        cmd.Parameters.AddWithValue("@sp_status", "9");
            //        cmd.Parameters.AddWithValue("@Session_id", SessioN);
            //    }
            //    else
            //    {
            //        cmd.Parameters.AddWithValue("@Session_id", SessioN);
            //        cmd.Parameters.AddWithValue("@sp_status", "10");
            //    }
            //}

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["class"].ToString());
                    yaxis.Add(dr["total_amt"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        //PaymentHistorYDUES=======================
        [WebMethod(EnableSession = true)]
        public string find_payment_history_report_dues(string From)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_Graph_Report");
            if (From == "Home")
            {
                cmd.Parameters.AddWithValue("@sp_status", "3");
            }
            else
            {
                cmd.Parameters.AddWithValue("@sp_status", "6");
            }

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["class"].ToString());
                    yaxis.Add(dr["total_amt"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        public class namevalue
        {
            public string name { get; set; }
            public string value { get; set; }

        }
        public class piedata
        {
            public List<namevalue> nmv { get; set; }

        }




        //Order StatuS SummarY===================
        [WebMethod(EnableSession = true)]
        public string Get_order_status_summary(string From)
        {
            List<namevalue> nm_list1 = new List<namevalue>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                if (From == "Home")
                {
                    cmd.Parameters.AddWithValue("@sp_status", "4");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sp_status", "7");
                }
                DataSet ds = My.executeReaderDataSet(cmd);
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm21 = new namevalue();
                        nm21.name = dr["Status"].ToString();
                        nm21.value = dr["total_amt"].ToString();
                        nm_list1.Add(nm21);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            piedata piedata1 = new piedata();
            piedata1.nmv = nm_list1;
            return JsonConvert.SerializeObject(piedata1, Newtonsoft.Json.Formatting.Indented);
        }




        //================================
        public class Fetch_student_hostel_details
        {
            public string Session { get; set; }
            public string Studentname { get; set; }
            public string Class_name { get; set; }
            public string Current_Semester_or_Year { get; set; }
            public string Hostel_name { get; set; }
            public string Room_category_name { get; set; }

            public string Room_name { get; set; }
            public string Bed_name { get; set; }
            public string From_month_name { get; set; }
            public string Admission_no { get; set; }
            public string Section { get; set; }
            public string rollnumber { get; set; }
        }
        List<Fetch_student_hostel_details> Show_student_hostel_details = new List<Fetch_student_hostel_details>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_transfer_details(string Paramiter1)
        {
            string qry = "select t2.session,t2.studentname,t2.class as Class_name,t2.Current_Semester_or_Year,(select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name,(select top 1 Category_name from Hostel_room_category_master where Category_id=t1.Category_id) as Room_category_name,(select top 1 Room_name from Hostel_room_master where Room_id=t1.Room_id) as Room_name,(select top 1 Bed_name from Hostel_room_bed_master where Bed_id=t1.Bed_id) as Bed_name,t1.Admission_no,t1.From_month_name from Hostel_assign_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Academic_sem_id=t2.Current_Semester_or_Year_id and t1.Admission_no=t2.admissionserialnumber where Hostel_assign_id='" + Paramiter1 + "'";

            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Hostel_assign_master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_student_hostel_details.Add(new Fetch_student_hostel_details
                    {
                        Session = dr["session"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                        Class_name = dr["Class_name"].ToString(),
                        Current_Semester_or_Year = dr["Current_Semester_or_Year"].ToString(),
                        Hostel_name = dr["Hostel_name"].ToString(),
                        Room_category_name = dr["Room_category_name"].ToString(),
                        Room_name = dr["Room_name"].ToString(),
                        Bed_name = dr["Bed_name"].ToString(),
                        From_month_name = dr["From_month_name"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_student_hostel_details));
            }
        }



        //==================================================Fee CollecTION
        //PAIDSummarY
        [WebMethod(EnableSession = true)]
        public string find_paid_summary_report(string FromDate, string ToDate, string TYPE)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", FromDate);
            cmd.Parameters.AddWithValue("@todate ", ToDate);
            cmd.Parameters.AddWithValue("@Type ", TYPE);
            cmd.Parameters.AddWithValue("@sp_status ", "1");
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["mode"].ToString());
                    yaxis.Add(dr["Paid_amt"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        //PAIDSummarY
        [WebMethod(EnableSession = true)]
        public string find_paid_summary_reportS(string FromDate, string ToDate, string ClasSID, string SecTION, string TYPE)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", FromDate);
            cmd.Parameters.AddWithValue("@todate ", ToDate);
            cmd.Parameters.AddWithValue("@Session ", My.get_session());
            cmd.Parameters.AddWithValue("@parameter ", TYPE);
            cmd.Parameters.AddWithValue("@parameter2 ", "HostelMonthlyFee");
            
            if (ClasSID == "0")
            {
                cmd.Parameters.AddWithValue("@sp_status ", "4");
            }

            if (ClasSID != "0" && SecTION == "ALL")
            {
                cmd.Parameters.AddWithValue("@class_id ", ClasSID);
                cmd.Parameters.AddWithValue("@sp_status ", "7");
            }

            if (ClasSID != "0" && SecTION != "ALL")
            {
                cmd.Parameters.AddWithValue("@class_id ", ClasSID);
                cmd.Parameters.AddWithValue("@section ", SecTION);
                cmd.Parameters.AddWithValue("@sp_status ", "8");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["feetype"].ToString());
                    yaxis.Add(dr["Paid_amt"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        ///========================================
        ///
        [WebMethod(EnableSession = true)]
        public string Get_student_status_summary(string Session, string Class_id, string Section, string Statuss)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (Class_id == "0")
                {
                    if (Statuss == "0")
                    {
                        query = "select CASE WHEN Status = '1' THEN 'Active' WHEN Status = '0' THEN 'Inactive' END AS Status ,count(Id) as Total from admission_registor where Session_id='" + Session + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by Status";
                    }
                    else if (Statuss == "1")
                    {
                        query = "select CASE WHEN Status = '1' THEN 'Active' WHEN Status = '0' THEN 'Inactive' END AS Status ,count(Id) as Total from admission_registor where Session_id='" + Session + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by Status";
                    }
                    else
                    {
                        query = "select CASE WHEN Status = '1' THEN 'Active' WHEN Status = '0' THEN 'Inactive' END AS Status ,count(Id) as Total from admission_registor where Session_id='" + Session + "' and Status='0' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by Status";
                    }
                }
                else
                {
                    if (Statuss == "0")
                    {
                        query = "select CASE WHEN Status = '1' THEN 'Active' WHEN Status = '0' THEN 'Inactive' END AS Status ,count(Id) as Total from admission_registor where Session_id='" + Session + "' and Class_id='" + Class_id + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by Status";
                    }
                    else if (Statuss == "1")
                    {
                        query = "select CASE WHEN Status = '1' THEN 'Active' WHEN Status = '0' THEN 'Inactive' END AS Status ,count(Id) as Total from admission_registor where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by Status";
                    }
                    else
                    {
                        query = "select CASE WHEN Status = '1' THEN 'Active' WHEN Status = '0' THEN 'Inactive' END AS Status ,count(Id) as Total from admission_registor where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='0' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by Status";
                    }
                }

                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }



        ///========================================
        ///
        [WebMethod(EnableSession = true)]
        public string Get_student_status_summary_counts(string Session, string Class_id, string Section, string Student_type)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (Class_id == "0")
                {
                    if (Student_type == "ALL")
                    {
                        query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";
                    }
                    else
                    {
                        query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Transfer_Status ='" + Student_type + "'  and Status='1'  group by Transfer_Status";
                    }
                }
                else
                {
                    if (Section == "0")
                    {
                        if (Student_type == "ALL")
                        {
                            query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";
                        }
                        else
                        {
                            query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and Transfer_Status ='" + Student_type + "'  group by Transfer_Status";
                        }
                    }
                    else
                    {
                        if (Student_type == "ALL")
                        {
                            query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";
                        }
                        else
                        {
                            query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old' END AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and Transfer_Status ='" + Student_type + "' group by Transfer_Status";
                        }
                    }
                }

                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }




        ///========================================CASTEWISE STUDENT
        ///
        [WebMethod(EnableSession = true)]
        public string Get_student_castewise(string Session, string Class_id, string Section, string Caste)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (Class_id == "0")
                 {
                    if (Caste == "ALL")
                    {
                        query = "select cast AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and cast!='Select' group by cast";
                    }
                    else
                    {
                        query = "select cast AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and cast='" + Caste + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and cast!='Select' group by cast";
                    }
                }
                else
                {
                    if (Section == "0")
                    {
                        if (Caste == "ALL")
                        {
                            query = "select cast AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and cast!='Select' group by cast";
                        }
                        else
                        {
                            query = "select cast AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and cast='" + Caste + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and cast!='Select' group by cast";
                        }
                    }
                    else
                    {
                        if (Caste == "ALL")
                        {
                            query = "select cast AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and cast!='Select' group by cast";
                        }
                        else
                        {
                            query = "select cast AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "'  and cast='" + Caste + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and cast!='Select' group by cast";
                        }
                    }
                }

                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }


        ///========================================RELIGION STUDENT
        ///
        [WebMethod(EnableSession = true)]
        public string Get_student_religionwise(string Session, string Class_id, string Section, string Religion)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (Class_id == "0")
                {
                    if (Religion == "ALL")
                    {
                        query = "select religion AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by religion";
                    }
                    else
                    {
                        query = "select religion AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and religion='" + Religion + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by religion";
                    }
                }
                else
                {
                    if (Section == "0")
                    {
                        if (Religion == "ALL")
                        {
                            query = "select religion AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by religion";
                        }
                        else
                        {
                            query = "select religion AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and religion='" + Religion + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by religion";
                        }
                    }
                    else
                    {
                        if (Religion == "ALL")
                        {
                            query = "select religion AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by religion";
                        }
                        else
                        {
                            query = "select religion AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "'  and religion='" + Religion + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by religion";
                        }
                    }
                }

                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }



        ///========================================DATEWISE STUDENTS
        ///
        [WebMethod(EnableSession = true)]
        public string Get_student_by_datewise(string FromDate, string ToDate)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "select CASE WHEN Transfer_Status = 'AV' THEN 'Transferred To Next Session' WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Readmission' END AS Status,count(Id) as total  from admission_registor  where admission_idate>=" + FromDate + " and admission_idate<=" + ToDate + " and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by Transfer_Status";

                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }


        ///========================================ADMISSIONFEE COLLECTION
        ///
        [WebMethod(EnableSession = true)]
        public string Get_admission_fee_mode(string Session, string From_date, string To_date)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (From_date == "0")
                {
                    query = "select sum(isnull(convert(float, Amount),0)) as Total,mode as Status from Student_Payment_History where Type='Admission' and Session='" + Session + "' group by mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, Amount),0)) as Total,mode as Status from Student_Payment_History where Type='Admission' and Session='" + Session + "' and Idate>='" + From_date + "' and Idate<='" + To_date + "' group by mode";
                }


                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }


        ///========================================ANNUAL FEE COLLECTION
        ///
        [WebMethod(EnableSession = true)]
        public string Get_annual_fee_mode(string Session, string From_date, string To_date)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (From_date == "0")
                {
                    query = "select sum(isnull(convert(float, Amount),0)) as Total,mode as Status from Student_Payment_History where Type='Annual' and Session='" + Session + "' group by mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, Amount),0)) as Total,mode as Status from Student_Payment_History where Type='Annual' and Session='" + Session + "' and Idate>='" + From_date + "' and Idate<='" + To_date + "' group by mode";
                }
                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }


        ///========================================MONTHLY COLLECTION
        ///
        [WebMethod(EnableSession = true)]
        public string Get_monthly_fee_mode(string Session, string From_date, string To_date)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (From_date == "0")
                {
                    query = "select sum(isnull(convert(float, Amount),0)) as Total,mode as Status from Student_Payment_History where Type='Monthly' and Session='" + Session + "' group by mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, Amount),0)) as Total,mode as Status from Student_Payment_History where Type='Monthly' and Idate>='" + From_date + "' and Idate<='" + To_date + "' group by mode";
                }


                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }



        ///========================================TODAYS COLLECTION
        ///
        [WebMethod(EnableSession = true)]
        public string Get_todays_fee_collection(string Session, string From_date, string To_date)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (Session.ToUpper() == "ALL")
                {
                    if (From_date == "0")
                    {
                        query = "select sum(isnull(convert(float, Amount),0)) as Total,Type as Status from Student_Payment_History group by Type";
                    }
                    else
                    {
                        query = "select sum(isnull(convert(float, Amount),0)) as Total,Type as Status from Student_Payment_History where Idate>='" + From_date + "' and Idate<='" + To_date + "' group by Type";
                    }
                }
                else
                {
                    if (From_date == "0")
                    {
                        query = "select sum(isnull(convert(float, Amount),0)) as Total,Type as Status from Student_Payment_History where Session='" + Session + "' group by Type";
                    }
                    else
                    {
                        query = "select sum(isnull(convert(float, Amount),0)) as Total,Type as Status from Student_Payment_History where Session='" + Session + "' and Idate>='" + From_date + "' and Idate<='" + To_date + "' group by Type";
                    }
                }
                
                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }


        //FEE COLLECTION SUMMARY
        [WebMethod(EnableSession = true)]
        public string find_fee_collection_summary(string Session, string Class_id)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            string query = "";
            if (Class_id == "0")
            {
                query = "select  sum(cast (paid as float )) as total_adm,Content from dbo.[Monthly_Fee_Collection_Slip] where Session='" + Session + "'  group by Content";
            }
            else
            {
                query = " select  sum(cast (paid as float )) as total_adm,Content from dbo.[Monthly_Fee_Collection_Slip] where  class='" + Class_id + "' and Session='" + Session + "'  group by Content";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Content"].ToString());
                    yaxis.Add(dr["total_adm"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }



        //FEE DAY END SUMMARY
        [WebMethod(EnableSession = true)]
        public string find_fee_day_end_summary(string Day_date)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            string query = "";
            query = "select  sum(cast (mfcs.paid as float )) as total_adm,mfcs.Content from dbo.[Monthly_Fee_Collection_Slip] mfcs join Student_Payment_History sph on mfcs.slipno=sph.Slip_no where sph.Idate='" + Day_date + "'  group by mfcs.Content";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];

            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Content"].ToString());
                    yaxis.Add(dr["total_adm"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }



        //TODAY FEE  SUMMARY
        [WebMethod(EnableSession = true)]
        public string find_today_fee_summary(string Day_date, string Class)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            string query = "";
            if (Class == "0")
            {
                query = "select  sum(cast (mfcs.paid as float )) as total_adm,mfcs.Content from dbo.[Monthly_Fee_Collection_Slip] mfcs join Student_Payment_History sph on mfcs.slipno=sph.Slip_no where  sph.Idate='" + Day_date + "'  group by mfcs.Content";
            }
            else
            {
                query = "select  sum(cast (mfcs.paid as float )) as total_adm,mfcs.Content from dbo.[Monthly_Fee_Collection_Slip] mfcs join Student_Payment_History sph on mfcs.slipno=sph.Slip_no where  mfcs.class='" + Class + "' and sph.Idate='" + Day_date + "' group by mfcs.Content";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];

            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Content"].ToString());
                    yaxis.Add(dr["total_adm"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }



        //===========================GenderWise
        ///========================================gender STUDENT
        ///
        [WebMethod(EnableSession = true)]
        public string Get_student_Genderwise(string Session, string Class_id, string Section, string Gender)
        {
            List<namevalue> nm_list = new List<namevalue>();
            try
            {
                string query = "";
                if (Class_id == "0")
                {
                    if (Gender == "ALL")
                    {
                        query = "select gender AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by gender";
                    }
                    else
                    {
                        query = "select gender AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and gender='" + Gender + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by gender";
                    }
                }
                else
                {
                    if (Section == "0")
                    {
                        if (Gender == "ALL")
                        {
                            query = "select gender AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by gender";
                        }
                        else
                        {
                            query = "select gender AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and gender='" + Gender + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by gender";
                        }
                    }
                    else
                    {
                        if (Gender == "ALL")
                        {
                            query = "select gender AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  group by gender";
                        }
                        else
                        {
                            query = "select gender AS Status,count(Id) as total  from admission_registor  where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "'  and gender='" + Gender + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') group by gender";
                        }
                    }
                }

                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Cart_Table");
                DataTable dt = ds.Tables[0];
                inpsection_Area ir = new inpsection_Area();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        namevalue nm2 = new namevalue();
                        nm2.name = dr["Status"].ToString();
                        nm2.value = dr["Total"].ToString();
                        nm_list.Add(nm2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            piedata piedata = new piedata();
            piedata.nmv = nm_list;
            return JsonConvert.SerializeObject(piedata, Newtonsoft.Json.Formatting.Indented);
        }


        ///=======================================================
        ///

        public class Fetch_Details_of_day_end_report_head
        {
            public string Content { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head> Show_of_day_end_report_head = new List<Fetch_Details_of_day_end_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end(string Dates, string Type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
            }
            else
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelAnnualFee') group by Content,content_id order by Content desc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_report_head.Add(new Fetch_Details_of_day_end_report_head
                    {
                        Content = dr["Content"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head));
            }
        }

        My mycode = new My();
        public class Fetch_Details_of_day_end_report_head_amts
        {
            public string Student_name { get; set; }
            public string Class_name { get; set; }
            public string Sections { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }
            public string Payment_mode { get; set; }
            //============-----------==============
            public string showFeeS1 { get; set; }
            public string showFeeS2 { get; set; }
            public string showFeeS3 { get; set; }
            public string showFeeS4 { get; set; }
            public string showFeeS5 { get; set; }
            public string showFeeS6 { get; set; }
            public string showFeeS7 { get; set; }
            public string showFeeS8 { get; set; }
            public string showFeeS9 { get; set; }
            public string showFeeS10 { get; set; }
            public string showFeeS11 { get; set; }
            public string showFeeS12 { get; set; }
            public string showFeeS13 { get; set; }
            public string showFeeS14 { get; set; }
            public string showFeeS15 { get; set; }
            public string TotaLFeeS { get; set; }

            //======================================
            public string DshowFeeS1 { get; set; }
            public string DshowFeeS2 { get; set; }
            public string DshowFeeS3 { get; set; }
            public string DshowFeeS4 { get; set; }
            public string DshowFeeS5 { get; set; }
            public string DshowFeeS6 { get; set; }
            public string DshowFeeS7 { get; set; }
            public string DshowFeeS8 { get; set; }
            public string DshowFeeS9 { get; set; }
            public string DshowFeeS10 { get; set; }
            public string DshowFeeS11 { get; set; }
            public string DshowFeeS12 { get; set; }
            public string DshowFeeS13 { get; set; }
            public string DshowFeeS14 { get; set; }
            public string DshowFeeS15 { get; set; }
            public string DTotaLFeeS { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_amts> Show_of_day_end_report_head_amts = new List<Fetch_Details_of_day_end_report_head_amts>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_amts(string Dates, string Type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee') order by rollnumber desc";
            }
            else
            {
                query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAnnualFee') order by rollnumber desc";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string fees1 = "hidden";
                string fees2 = "hidden";
                string fees3 = "hidden";
                string fees4 = "hidden";
                string fees5 = "hidden";
                string fees6 = "hidden";
                string fees7 = "hidden";
                string fees8 = "hidden";
                string fees9 = "hidden";
                string fees10 = "hidden";
                string fees11 = "hidden";
                string fees12 = "hidden";
                string fees13 = "hidden";
                string fees14 = "hidden";
                string fees15 = "hidden";

                //===============
                string dfees1 = "";
                string dfees2 = "";
                string dfees3 = "";
                string dfees4 = "";
                string dfees5 = "";
                string dfees6 = "";
                string dfees7 = "";
                string dfees8 = "";
                string dfees9 = "";
                string dfees10 = "";
                string dfees11 = "";
                string dfees12 = "";
                string dfees13 = "";
                string dfees14 = "";
                string dfees15 = "";

                foreach (DataRow dr in dt.Rows)
                {
                    string querys = "";
                    if (Type == "MonthlyFee")
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
                    }
                    else
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelAnnualFee') group by Content,content_id order by Content desc";
                    }
                    DataTable dts = mycode.FillData(querys);
                    if (dts.Rows.Count != 0)
                    {
                        int i = 0;
                        foreach (DataRow drr in dts.Rows)
                        {
                            string fees_amt = get_fees_amt(drr["content_id"].ToString(), dr["adno"].ToString(), Dates, Type, dr["slipno"].ToString());
                            if (i == 0)
                            {
                                fees1 = fees_amt;
                                dfees1 = fees_amt;
                            }
                            if (i == 1)
                            {
                                fees2 = fees_amt;
                                dfees2 = fees_amt;
                            }
                            if (i == 2)
                            {
                                fees3 = fees_amt;
                                dfees3 = fees_amt;
                            }
                            if (i == 3)
                            {
                                fees4 = fees_amt;
                                dfees4 = fees_amt;
                            }
                            if (i == 4)
                            {
                                fees5 = fees_amt;
                                dfees5 = fees_amt;
                            }
                            if (i == 5)
                            {
                                fees6 = fees_amt;
                                dfees6 = fees_amt;
                            }
                            if (i == 6)
                            {
                                fees7 = fees_amt;
                                dfees7 = fees_amt;
                            }
                            if (i == 7)
                            {
                                fees8 = fees_amt;
                                dfees8 = fees_amt;
                            }
                            if (i == 8)
                            {
                                fees9 = fees_amt;
                                dfees9 = fees_amt;
                            }
                            if (i == 9)
                            {
                                fees10 = fees_amt;
                                dfees10 = fees_amt;
                            }
                            if (i == 10)
                            {
                                fees11 = fees_amt;
                                dfees11 = fees_amt;
                            }
                            if (i == 11)
                            {
                                fees12 = fees_amt;
                                dfees12 = fees_amt;
                            }
                            if (i == 12)
                            {
                                fees13 = fees_amt;
                                dfees13 = fees_amt;
                            }
                            if (i == 13)
                            {
                                fees14 = fees_amt;
                                dfees14 = fees_amt;
                            }
                            if (i == 14)
                            {
                                fees15 = fees_amt;
                                dfees15 = fees_amt;
                            }

                            i++;
                        }
                    }

                    double ttlFinalFeeS = My.toDouble(fees1) + My.toDouble(fees2) + My.toDouble(fees3) + My.toDouble(fees4) + My.toDouble(fees5) + My.toDouble(fees6) + My.toDouble(fees7) + My.toDouble(fees8) + My.toDouble(fees9) + My.toDouble(fees10) + My.toDouble(fees11) + My.toDouble(fees12) + My.toDouble(fees13) + My.toDouble(fees14) + My.toDouble(fees15);
                    Show_of_day_end_report_head_amts.Add(new Fetch_Details_of_day_end_report_head_amts
                    {
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Sections = dr["Section"].ToString(),
                        Admission_no = dr["adno"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        Payment_mode = dr["Payment_mode"].ToString(),

                        //==========================
                        showFeeS1 = fees1,
                        showFeeS2 = fees2,
                        showFeeS3 = fees3,
                        showFeeS4 = fees4,
                        showFeeS5 = fees5,
                        showFeeS6 = fees6,
                        showFeeS7 = fees7,
                        showFeeS8 = fees8,
                        showFeeS9 = fees9,
                        showFeeS10 = fees10,
                        showFeeS11 = fees11,
                        showFeeS12 = fees12,
                        showFeeS13 = fees13,
                        showFeeS14 = fees14,
                        showFeeS15 = fees15,

                        //=======================
                        DshowFeeS1 = dfees1,
                        DshowFeeS2 = dfees2,
                        DshowFeeS3 = dfees3,
                        DshowFeeS4 = dfees4,
                        DshowFeeS5 = dfees5,
                        DshowFeeS6 = dfees6,
                        DshowFeeS7 = dfees7,
                        DshowFeeS8 = dfees8,
                        DshowFeeS9 = dfees9,
                        DshowFeeS10 = dfees10,
                        DshowFeeS11 = dfees11,
                        DshowFeeS12 = dfees12,
                        DshowFeeS13 = dfees13,
                        DshowFeeS14 = dfees14,
                        DshowFeeS15 = dfees15,

                        TotaLFeeS = ttlFinalFeeS.ToString(),

                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));
            }
        }

        private string get_fees_amt(string content_id, string admission_no, string Dates, string Type, string slip_no)
        {
            string querys = "";
            if (Type == "MonthlyFee")
            {
                querys = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "'  or parameter='HostelMonthlyFee') and Idate='" + My.DateConvertToIdate(Dates) + "' and slipno='" + slip_no + "'";
            }
            else
            {
                querys = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where adno='" + admission_no + "' and content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAnnualFee') and Idate='" + My.DateConvertToIdate(Dates) + "' and slipno='" + slip_no + "'";
            }
            string paids_amt = "0";
            DataTable dt = mycode.FillData(querys);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }




        public class Fetch_Details_of_day_end_report_head_amts_final
        {
            public string showFeeS1 { get; set; }
            public string showFeeS2 { get; set; }
            public string showFeeS3 { get; set; }
            public string showFeeS4 { get; set; }
            public string showFeeS5 { get; set; }
            public string showFeeS6 { get; set; }
            public string showFeeS7 { get; set; }
            public string showFeeS8 { get; set; }
            public string showFeeS9 { get; set; }
            public string showFeeS10 { get; set; }
            public string showFeeS11 { get; set; }
            public string showFeeS12 { get; set; }
            public string showFeeS13 { get; set; }
            public string showFeeS14 { get; set; }
            public string showFeeS15 { get; set; }

            //=================
            public string DshowFeeS1 { get; set; }
            public string DshowFeeS2 { get; set; }
            public string DshowFeeS3 { get; set; }
            public string DshowFeeS4 { get; set; }
            public string DshowFeeS5 { get; set; }
            public string DshowFeeS6 { get; set; }
            public string DshowFeeS7 { get; set; }
            public string DshowFeeS8 { get; set; }
            public string DshowFeeS9 { get; set; }
            public string DshowFeeS10 { get; set; }
            public string DshowFeeS11 { get; set; }
            public string DshowFeeS12 { get; set; }
            public string DshowFeeS13 { get; set; }
            public string DshowFeeS14 { get; set; }
            public string DshowFeeS15 { get; set; }


            public string TotaLFeeS { get; set; }
            public string TotaLFinaLFeeS { get; set; }
        }

        List<Fetch_Details_of_day_end_report_head_amts_final> Show_of_day_end_report_head_amts_final = new List<Fetch_Details_of_day_end_report_head_amts_final>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_final_amts(string Dates, string Type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            }
            else
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelAnnualFee')";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string fees1 = "hidden";
                string fees2 = "hidden";
                string fees3 = "hidden";
                string fees4 = "hidden";
                string fees5 = "hidden";
                string fees6 = "hidden";
                string fees7 = "hidden";
                string fees8 = "hidden";
                string fees9 = "hidden";
                string fees10 = "hidden";
                string fees11 = "hidden";
                string fees12 = "hidden";
                string fees13 = "hidden";
                string fees14 = "hidden";
                string fees15 = "hidden";

                //===============================
                string Dfees1 = "";
                string Dfees2 = "";
                string Dfees3 = "";
                string Dfees4 = "";
                string Dfees5 = "";
                string Dfees6 = "";
                string Dfees7 = "";
                string Dfees8 = "";
                string Dfees9 = "";
                string Dfees10 = "";
                string Dfees11 = "";
                string Dfees12 = "";
                string Dfees13 = "";
                string Dfees14 = "";
                string Dfees15 = "";

                foreach (DataRow dr in dt.Rows)
                {
                    string querys = "";
                    if (Type == "MonthlyFee")
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
                    }
                    else
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates) + "' and  (parameter='" + Type + "' or parameter='HostelAnnualFee') group by Content,content_id order by Content desc";
                    }
                    DataTable dts = mycode.FillData(querys);
                    if (dts.Rows.Count != 0)
                    {
                        int i = 0;
                        foreach (DataRow drr in dts.Rows)
                        {
                            string fees_amt = get_final_fees_amt(drr["content_id"].ToString(), Dates, Type);
                            if (i == 0)
                            {
                                fees1 = fees_amt;
                                Dfees1 = fees_amt;
                            }
                            if (i == 1)
                            {
                                fees2 = fees_amt;
                                Dfees2 = fees_amt;
                            }
                            if (i == 2)
                            {
                                fees3 = fees_amt;
                                Dfees3 = fees_amt;
                            }
                            if (i == 3)
                            {
                                fees4 = fees_amt;
                                Dfees4 = fees_amt;
                            }
                            if (i == 4)
                            {
                                fees5 = fees_amt;
                                Dfees5 = fees_amt;
                            }
                            if (i == 5)
                            {
                                fees6 = fees_amt;
                                Dfees6 = fees_amt;
                            }
                            if (i == 6)
                            {
                                fees7 = fees_amt;
                                Dfees7 = fees_amt;
                            }
                            if (i == 7)
                            {
                                fees8 = fees_amt;
                                Dfees8 = fees_amt;
                            }
                            if (i == 8)
                            {
                                fees9 = fees_amt;
                                Dfees9 = fees_amt;
                            }
                            if (i == 9)
                            {
                                fees10 = fees_amt;
                                Dfees10 = fees_amt;
                            }
                            if (i == 10)
                            {
                                fees11 = fees_amt;
                                Dfees11 = fees_amt;
                            }
                            if (i == 11)
                            {
                                fees12 = fees_amt;
                            }
                            if (i == 12)
                            {
                                fees13 = fees_amt;
                                Dfees13 = fees_amt;
                            }
                            if (i == 13)
                            {
                                fees14 = fees_amt;
                                Dfees14 = fees_amt;
                            }
                            if (i == 14)
                            {
                                fees15 = fees_amt;
                                Dfees15 = fees_amt;
                            }
                            i++;
                        }
                    }


                    double ttlFinalFeeS = My.toDouble(fees1) + My.toDouble(fees2) + My.toDouble(fees3) + My.toDouble(fees4) + My.toDouble(fees5) + My.toDouble(fees6) + My.toDouble(fees7) + My.toDouble(fees8) + My.toDouble(fees9) + My.toDouble(fees10) + My.toDouble(fees11) + My.toDouble(fees12) + My.toDouble(fees13) + My.toDouble(fees14) + My.toDouble(fees15);
                    Show_of_day_end_report_head_amts_final.Add(new Fetch_Details_of_day_end_report_head_amts_final
                    {
                        showFeeS1 = fees1,
                        showFeeS2 = fees2,
                        showFeeS3 = fees3,
                        showFeeS4 = fees4,
                        showFeeS5 = fees5,
                        showFeeS6 = fees6,
                        showFeeS7 = fees7,
                        showFeeS8 = fees8,
                        showFeeS9 = fees9,
                        showFeeS10 = fees10,
                        showFeeS11 = fees11,
                        showFeeS12 = fees12,
                        showFeeS13 = fees13,
                        showFeeS14 = fees14,
                        showFeeS15 = fees15,

                        //=========================
                        DshowFeeS1 = Dfees1,
                        DshowFeeS2 = Dfees2,
                        DshowFeeS3 = Dfees3,
                        DshowFeeS4 = Dfees4,
                        DshowFeeS5 = Dfees5,
                        DshowFeeS6 = Dfees6,
                        DshowFeeS7 = Dfees7,
                        DshowFeeS8 = Dfees8,
                        DshowFeeS9 = Dfees9,
                        DshowFeeS10 = Dfees10,
                        DshowFeeS11 = Dfees11,
                        DshowFeeS12 = Dfees12,
                        DshowFeeS13 = Dfees13,
                        DshowFeeS14 = Dfees14,
                        DshowFeeS15 = Dfees15,

                        TotaLFinaLFeeS = ttlFinalFeeS.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final));
            }
        }

        private string get_final_fees_amt(string content_id, string Dates, string Type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelMonthlyFee') and Idate='" + My.DateConvertToIdate(Dates) + "'";
            }
            else
            {
                query = "select isnull(sum(convert(float, paid)),0) as Paid_amt from Monthly_Fee_Collection_Slip where content_id='" + content_id + "' and (parameter='" + Type + "' or parameter='HostelAnnualFee') and Idate='" + My.DateConvertToIdate(Dates) + "'";
            }
            string paids_amt = "0";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                paids_amt = "0";
            }
            else
            {
                paids_amt = dt.Rows[0]["Paid_amt"].ToString();
            }
            return paids_amt;
        }


        //=========================Day End ModeWise Total Amount
        public class Fetch_Details_of_day_end_mode
        {
            public string Paid_amt { get; set; }
            public string Pay_mode { get; set; }
        }

        List<Fetch_Details_of_day_end_mode> Show_of_day_end_mode = new List<Fetch_Details_of_day_end_mode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_ttl_by_mode(string Dates, string Type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "'  or t1.parameter='HostelMonthlyFee') group by t2.mode";
            }
            else
            {
                if (Type == "AnnualFee")
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAnnualFee') group by t2.mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelAdmissionFee') group by t2.mode";
                }
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_mode.Add(new Fetch_Details_of_day_end_mode
                    {
                        Paid_amt = dr["Paid_amt"].ToString(),
                        Pay_mode = dr["mode"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_mode));
            }
        }



        //OrderSummarY
        [WebMethod(EnableSession = true)]
        public string find_hostel_report_chart(string SessioN)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Graph_Report");
            if (SessioN == "0")
            {
                cmd.Parameters.AddWithValue("@sp_status", "1");
            }
            else
            {
                cmd.Parameters.AddWithValue("@sp_status", "12");
                cmd.Parameters.AddWithValue("@Session_id", SessioN);
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["class"].ToString());
                    yaxis.Add(dr["total_adm"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        public class Fetch_details_of_form_sale
        {
            public string Form_no { get; set; }
            public string Student_name { get; set; }
            public string Gender { get; set; }
            public string Mobile { get; set; }
            public string Class_name { get; set; }
            public string Session { get; set; }
            public string Payment_Mode { get; set; }
            public string Amount { get; set; }
            public string Ttl_amount { get; set; }
        }

        List<Fetch_details_of_form_sale> Show_of_day_end_report_form_sale = new List<Fetch_details_of_form_sale>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_form_sale(string Dates)
        {
            string query = "select * from Form_sale_details where idate='" + My.DateConvertToIdate(Dates) + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                double ttl_amt = 0;
                foreach (DataRow drx in dt.Rows)
                {
                    ttl_amt = ttl_amt + My.toDouble(drx["Amount"].ToString());
                }

                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_report_form_sale.Add(new Fetch_details_of_form_sale
                    {
                        Form_no = dr["Form_no"].ToString(),
                        Student_name = dr["student_name"].ToString(),
                        Gender = dr["gender"].ToString(),
                        Mobile = dr["mobile"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Session = dr["session"].ToString(),
                        Payment_Mode = dr["Payment_Mode"].ToString(),
                        Amount = dr["Amount"].ToString(),
                        Ttl_amount = ttl_amt.ToString("0.00"),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_form_sale));
            }
        }


        public class Fetch_Details_of_form_sale_by_mode
        {
            public string Paid_amt { get; set; }
            public string Pay_mode { get; set; }
        }

        List<Fetch_Details_of_form_sale_by_mode> Show_of_form_sale_by_mode = new List<Fetch_Details_of_form_sale_by_mode>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_form_sale_ttl_by_mode(string Dates)
        {
            string query = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,Payment_Mode from Form_sale_details  where Idate='" + My.DateConvertToIdate(Dates) + "' group by Payment_Mode";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_form_sale_by_mode.Add(new Fetch_Details_of_form_sale_by_mode
                    {
                        Paid_amt = My.toDouble(dr["Paid_amt"].ToString()).ToString("0.00"),
                        Pay_mode = dr["Payment_Mode"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_form_sale_by_mode));
            }
        }



        //------------------------Annual Report data two date-------------------

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_tow_date(string Dates_start, string Dates_End, string Type, string Paymnet_type)
        {
            string session = My.get_session();
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates_start) + "' and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
            }
            else
            {

                query = "select distinct Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>= " + My.DateConvertToIdate(Dates_start) + "  and Idate<=" + My.DateConvertToIdate(Dates_start) + " and parameter='" + Type + "' session='" + session + "'  group by Content,content_id order by Content desc";



            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Monthly_Fee_Collection_Slip");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_report_head.Add(new Fetch_Details_of_day_end_report_head
                    {
                        Content = dr["Content"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_amts_twodate(string Dates_start, string Dates_End, string Type, string Paymnet_type)
        {
            if (Type == "AnnualFee")
            {
                Type = "Annual";
            }
            else
            {


            }

            string session = My.get_session();
            string query = "";
            if (Type == "MonthlyFee")
            {
                //query = "select DISTINCT t1.slipno,t2.studentname,t2.class,t1.Section,t1.adno,t2.rollnumber,(select top 1 mode from Student_Payment_History where Slip_no=t1.slipno) as Payment_mode from Monthly_Fee_Collection_Slip t1 join admission_registor t2 on t1.session=t2.session  and t1.adno=t2.admissionserialnumber where t1.Idate='" + My.DateConvertToIdate(Dates) + "' and  (t1.parameter='" + Type + "' or t1.parameter='HostelMonthlyFee') order by rollnumber desc";
            }
            else
            {
                if (Paymnet_type == "All")
                {
                    query = " select   t1.Slip_no as slipno,t2.studentname,t2.class,t2.Section,t1.Addmission_no as adno,t2.rollnumber,t1.mode as Payment_mode  from Student_Payment_History t1 join admission_registor t2 on t1.session=t2.session  and t1.Addmission_no=t2.admissionserialnumber where t1.Idate>=" + My.DateConvertToIdate(Dates_start) + " and t1.Idate<=" + My.DateConvertToIdate(Dates_End) + " and  t1.Type='" + Type + "' order by rollnumber desc";
                }
                else
                {
                    query = " select   t1.Slip_no as slipno,t2.studentname,t2.class,t2.Section,t1.Addmission_no as adno,t2.rollnumber,t1.mode as Payment_mode  from Student_Payment_History t1 join admission_registor t2 on t1.session=t2.session  and t1.Addmission_no=t2.admissionserialnumber where t2.Idate>=" + My.DateConvertToIdate(Dates_start) + " and t2.Idate<=" + My.DateConvertToIdate(Dates_End) + " and  t1.Type='" + Type + "' and t2.session='" + session + "' and t2.mode='" + Paymnet_type + "'  order by rollnumber desc";
                }

            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string fees1 = "hidden";
                string fees2 = "hidden";
                string fees3 = "hidden";
                string fees4 = "hidden";
                string fees5 = "hidden";
                string fees6 = "hidden";
                string fees7 = "hidden";
                string fees8 = "hidden";
                string fees9 = "hidden";
                string fees10 = "hidden";
                string fees11 = "hidden";
                string fees12 = "hidden";
                string fees13 = "hidden";
                string fees14 = "hidden";
                string fees15 = "hidden";

                //===============
                string dfees1 = "";
                string dfees2 = "";
                string dfees3 = "";
                string dfees4 = "";
                string dfees5 = "";
                string dfees6 = "";
                string dfees7 = "";
                string dfees8 = "";
                string dfees9 = "";
                string dfees10 = "";
                string dfees11 = "";
                string dfees12 = "";
                string dfees13 = "";
                string dfees14 = "";
                string dfees15 = "";

                foreach (DataRow dr in dt.Rows)
                {
                    string querys = "";
                    if (Type == "MonthlyFee")
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates_start) + "' and  (parameter='" + Type + "'  or parameter='HostelMonthlyFee')   group by Content,content_id order by Content desc";
                    }
                    else
                    {
                        querys = "select Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate='" + My.DateConvertToIdate(Dates_start) + "' and  parameter='" + Type + "' group by Content,content_id order by Content desc";
                    }
                    DataTable dts = mycode.FillData(querys);
                    if (dts.Rows.Count != 0)
                    {
                        int i = 0;
                        foreach (DataRow drr in dts.Rows)
                        {
                            string fees_amt = get_fees_amt(drr["content_id"].ToString(), dr["adno"].ToString(), Dates_start, Type, dr["slipno"].ToString());
                            if (i == 0)
                            {
                                fees1 = fees_amt;
                                dfees1 = fees_amt;
                            }
                            if (i == 1)
                            {
                                fees2 = fees_amt;
                                dfees2 = fees_amt;
                            }
                            if (i == 2)
                            {
                                fees3 = fees_amt;
                                dfees3 = fees_amt;
                            }
                            if (i == 3)
                            {
                                fees4 = fees_amt;
                                dfees4 = fees_amt;
                            }
                            if (i == 4)
                            {
                                fees5 = fees_amt;
                                dfees5 = fees_amt;
                            }
                            if (i == 5)
                            {
                                fees6 = fees_amt;
                                dfees6 = fees_amt;
                            }
                            if (i == 6)
                            {
                                fees7 = fees_amt;
                                dfees7 = fees_amt;
                            }
                            if (i == 7)
                            {
                                fees8 = fees_amt;
                                dfees8 = fees_amt;
                            }
                            if (i == 8)
                            {
                                fees9 = fees_amt;
                                dfees9 = fees_amt;
                            }
                            if (i == 9)
                            {
                                fees10 = fees_amt;
                                dfees10 = fees_amt;
                            }
                            if (i == 10)
                            {
                                fees11 = fees_amt;
                                dfees11 = fees_amt;
                            }
                            if (i == 11)
                            {
                                fees12 = fees_amt;
                                dfees12 = fees_amt;
                            }
                            if (i == 12)
                            {
                                fees13 = fees_amt;
                                dfees13 = fees_amt;
                            }
                            if (i == 13)
                            {
                                fees14 = fees_amt;
                                dfees14 = fees_amt;
                            }
                            if (i == 14)
                            {
                                fees15 = fees_amt;
                                dfees15 = fees_amt;
                            }

                            i++;
                        }
                    }

                    double ttlFinalFeeS = My.toDouble(fees1) + My.toDouble(fees2) + My.toDouble(fees3) + My.toDouble(fees4) + My.toDouble(fees5) + My.toDouble(fees6) + My.toDouble(fees7) + My.toDouble(fees8) + My.toDouble(fees9) + My.toDouble(fees10) + My.toDouble(fees11) + My.toDouble(fees12) + My.toDouble(fees13) + My.toDouble(fees14) + My.toDouble(fees15);
                    Show_of_day_end_report_head_amts.Add(new Fetch_Details_of_day_end_report_head_amts
                    {
                        Student_name = dr["studentname"].ToString(),
                        Class_name = dr["class"].ToString(),
                        Sections = dr["Section"].ToString(),
                        Admission_no = dr["adno"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),
                        Payment_mode = dr["Payment_mode"].ToString(),

                        //==========================
                        showFeeS1 = fees1,
                        showFeeS2 = fees2,
                        showFeeS3 = fees3,
                        showFeeS4 = fees4,
                        showFeeS5 = fees5,
                        showFeeS6 = fees6,
                        showFeeS7 = fees7,
                        showFeeS8 = fees8,
                        showFeeS9 = fees9,
                        showFeeS10 = fees10,
                        showFeeS11 = fees11,
                        showFeeS12 = fees12,
                        showFeeS13 = fees13,
                        showFeeS14 = fees14,
                        showFeeS15 = fees15,

                        //=======================
                        DshowFeeS1 = dfees1,
                        DshowFeeS2 = dfees2,
                        DshowFeeS3 = dfees3,
                        DshowFeeS4 = dfees4,
                        DshowFeeS5 = dfees5,
                        DshowFeeS6 = dfees6,
                        DshowFeeS7 = dfees7,
                        DshowFeeS8 = dfees8,
                        DshowFeeS9 = dfees9,
                        DshowFeeS10 = dfees10,
                        DshowFeeS11 = dfees11,
                        DshowFeeS12 = dfees12,
                        DshowFeeS13 = dfees13,
                        DshowFeeS14 = dfees14,
                        DshowFeeS15 = dfees15,

                        TotaLFeeS = ttlFinalFeeS.ToString(),

                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts));
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_heading_day_end_final_amts_twodate(string Dates_start, string Dates_End, string Type, string Paymnet_type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>= " + My.DateConvertToIdate(Dates_start) + "  and Idate<=" + My.DateConvertToIdate(Dates_start) + " and  (parameter='" + Type + "' or parameter='HostelMonthlyFee')";
            }
            else
            {
                query = "select top 1 content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>= " + My.DateConvertToIdate(Dates_start) + "  and Idate<=" + My.DateConvertToIdate(Dates_start) + " and  parameter='" + Type + "'";
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string fees1 = "hidden";
                string fees2 = "hidden";
                string fees3 = "hidden";
                string fees4 = "hidden";
                string fees5 = "hidden";
                string fees6 = "hidden";
                string fees7 = "hidden";
                string fees8 = "hidden";
                string fees9 = "hidden";
                string fees10 = "hidden";
                string fees11 = "hidden";
                string fees12 = "hidden";
                string fees13 = "hidden";
                string fees14 = "hidden";
                string fees15 = "hidden";

                //===============================
                string Dfees1 = "";
                string Dfees2 = "";
                string Dfees3 = "";
                string Dfees4 = "";
                string Dfees5 = "";
                string Dfees6 = "";
                string Dfees7 = "";
                string Dfees8 = "";
                string Dfees9 = "";
                string Dfees10 = "";
                string Dfees11 = "";
                string Dfees12 = "";
                string Dfees13 = "";
                string Dfees14 = "";
                string Dfees15 = "";

                foreach (DataRow dr in dt.Rows)
                {
                    string querys = "";
                    if (Type == "MonthlyFee")
                    {
                        querys = "select distinct Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>= " + My.DateConvertToIdate(Dates_start) + "  and Idate<=" + My.DateConvertToIdate(Dates_start) + " and  (parameter='" + Type + "' or parameter='HostelMonthlyFee') group by Content,content_id order by Content desc";
                    }
                    else
                    {
                        querys = "select distinct Content,content_id from dbo.[Monthly_Fee_Collection_Slip] where Idate>= " + My.DateConvertToIdate(Dates_start) + "  and Idate<=" + My.DateConvertToIdate(Dates_start) + " and  parameter='" + Type + "' group by Content,content_id order by Content desc";
                    }
                    DataTable dts = mycode.FillData(querys);
                    if (dts.Rows.Count != 0)
                    {
                        int i = 0;
                        foreach (DataRow drr in dts.Rows)
                        {
                            string fees_amt = get_final_fees_amt(drr["content_id"].ToString(), Dates_start, Type);
                            if (i == 0)
                            {
                                fees1 = fees_amt;
                                Dfees1 = fees_amt;
                            }
                            if (i == 1)
                            {
                                fees2 = fees_amt;
                                Dfees2 = fees_amt;
                            }
                            if (i == 2)
                            {
                                fees3 = fees_amt;
                                Dfees3 = fees_amt;
                            }
                            if (i == 3)
                            {
                                fees4 = fees_amt;
                                Dfees4 = fees_amt;
                            }
                            if (i == 4)
                            {
                                fees5 = fees_amt;
                                Dfees5 = fees_amt;
                            }
                            if (i == 5)
                            {
                                fees6 = fees_amt;
                                Dfees6 = fees_amt;
                            }
                            if (i == 6)
                            {
                                fees7 = fees_amt;
                                Dfees7 = fees_amt;
                            }
                            if (i == 7)
                            {
                                fees8 = fees_amt;
                                Dfees8 = fees_amt;
                            }
                            if (i == 8)
                            {
                                fees9 = fees_amt;
                                Dfees9 = fees_amt;
                            }
                            if (i == 9)
                            {
                                fees10 = fees_amt;
                                Dfees10 = fees_amt;
                            }
                            if (i == 10)
                            {
                                fees11 = fees_amt;
                                Dfees11 = fees_amt;
                            }
                            if (i == 11)
                            {
                                fees12 = fees_amt;
                            }
                            if (i == 12)
                            {
                                fees13 = fees_amt;
                                Dfees13 = fees_amt;
                            }
                            if (i == 13)
                            {
                                fees14 = fees_amt;
                                Dfees14 = fees_amt;
                            }
                            if (i == 14)
                            {
                                fees15 = fees_amt;
                                Dfees15 = fees_amt;
                            }
                            i++;
                        }
                    }


                    double ttlFinalFeeS = My.toDouble(fees1) + My.toDouble(fees2) + My.toDouble(fees3) + My.toDouble(fees4) + My.toDouble(fees5) + My.toDouble(fees6) + My.toDouble(fees7) + My.toDouble(fees8) + My.toDouble(fees9) + My.toDouble(fees10) + My.toDouble(fees11) + My.toDouble(fees12) + My.toDouble(fees13) + My.toDouble(fees14) + My.toDouble(fees15);
                    Show_of_day_end_report_head_amts_final.Add(new Fetch_Details_of_day_end_report_head_amts_final
                    {
                        showFeeS1 = fees1,
                        showFeeS2 = fees2,
                        showFeeS3 = fees3,
                        showFeeS4 = fees4,
                        showFeeS5 = fees5,
                        showFeeS6 = fees6,
                        showFeeS7 = fees7,
                        showFeeS8 = fees8,
                        showFeeS9 = fees9,
                        showFeeS10 = fees10,
                        showFeeS11 = fees11,
                        showFeeS12 = fees12,
                        showFeeS13 = fees13,
                        showFeeS14 = fees14,
                        showFeeS15 = fees15,

                        //=========================
                        DshowFeeS1 = Dfees1,
                        DshowFeeS2 = Dfees2,
                        DshowFeeS3 = Dfees3,
                        DshowFeeS4 = Dfees4,
                        DshowFeeS5 = Dfees5,
                        DshowFeeS6 = Dfees6,
                        DshowFeeS7 = Dfees7,
                        DshowFeeS8 = Dfees8,
                        DshowFeeS9 = Dfees9,
                        DshowFeeS10 = Dfees10,
                        DshowFeeS11 = Dfees11,
                        DshowFeeS12 = Dfees12,
                        DshowFeeS13 = Dfees13,
                        DshowFeeS14 = Dfees14,
                        DshowFeeS15 = Dfees15,

                        TotaLFinaLFeeS = ttlFinalFeeS.ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_report_head_amts_final));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_ttl_by_mode_two_date(string Dates_start, string Dates_End, string Type, string Paymnet_type)
        {
            string query = "";
            if (Type == "MonthlyFee")
            {
                if (Paymnet_type == "All")
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate>= " + My.DateConvertToIdate(Dates_start) + "  and t1.Idate<=" + My.DateConvertToIdate(Dates_start) + " and  (t1.parameter='" + Type + "'  or t1.parameter='HostelMonthlyFee') group by t2.mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate>= " + My.DateConvertToIdate(Dates_start) + "  and t1.Idate<=" + My.DateConvertToIdate(Dates_start) + " and  (t1.parameter='" + Type + "'  or t1.parameter='HostelMonthlyFee') and t2.mode='" + Paymnet_type + "' group by t2.mode";
                }

            }
            else
            {
                if (Paymnet_type == "All")
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate>= " + My.DateConvertToIdate(Dates_start) + "  and t1.Idate<=" + My.DateConvertToIdate(Dates_start) + " and  t1.parameter='" + Type + "' group by t2.mode";
                }
                else
                {
                    query = "select sum(isnull(convert(float, t1.paid),0)) as Paid_amt,t2.mode from Monthly_Fee_Collection_Slip t1 join Student_Payment_History t2 on t1.slipno=t2.Slip_no where t1.Idate>= " + My.DateConvertToIdate(Dates_start) + "  and t1.Idate<=" + My.DateConvertToIdate(Dates_start) + " and  t1.parameter='" + Type + "' group by t2.mode";
                }
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_of_day_end_mode.Add(new Fetch_Details_of_day_end_mode
                    {
                        Paid_amt = dr["Paid_amt"].ToString(),
                        Pay_mode = dr["mode"].ToString(),
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_day_end_mode));
            }
        }

        //==============================================CHART
        [WebMethod(EnableSession = true)]
        public string monthly_revenue_collection(string Session_id, string Session_name, string Branch_id, string class_id)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();



            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Total");


            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1; string prevs_month = dtm.Rows[0]["Month"].ToString();
                    //PreviouS
                    string prv_month_id_in_two_dgt = My.getMonthS_twoDigit(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_chart_month = My.toint(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_s_year = My.check_start_months(prv_chart_month, s_year_real);
                    int prv_month_s_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "01");
                    int prv_month_e_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "31");
                    //PreviouS

                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");

                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), "MonthlyFee");
                            if (is_dues_calculated == "0")
                            {
                                if (class_id == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "22");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "24");
                                }
                            }
                            else
                            {
                                if (class_id == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "30");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "31");
                                }
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), "MonthlyFee");
                            if (is_dues_calculated == "0")
                            {
                                if (class_id == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "23");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "25");
                                }
                            }
                            else
                            {
                                if (class_id == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "30");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "31");
                                }
                            }
                        }

                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);
                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", prevs_month);
                        cmd.Parameters.AddWithValue("@Class_id", class_id);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0];


                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dt.Rows[0]["Month"].ToString();
                        drNewRow["Total"] = dt.Rows[0]["Total"].ToString();
                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        prevs_month = dr["Month"].ToString();
                        xi++;
                        prv_month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        prv_month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    }
                }
            }

            inpsection_Area ir = new inpsection_Area();
            if (dtDatas.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDatas.Rows)
                {
                    xaxis.Add(dr["Month"].ToString());
                    yaxis.Add(dr["Total"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }




        //==============================================CHART
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report(string Session_id, string Session_name, string Type, string Branch_id, string Payment_estd_class)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Estimated Collection");
            dtDatas.Columns.Add("Total Collected");
            dtDatas.Columns.Add("Total Dues");

            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1;
                    string prevs_month = dtm.Rows[0]["Month"].ToString();
                    string prevs_month_position = dtm.Rows[0]["Position"].ToString();

                    //PreviouS
                    string prv_month_id_in_two_dgt = My.getMonthS_twoDigit(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_chart_month = My.toint(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_s_year = My.check_start_months(prv_chart_month, s_year_real);
                    int prv_month_s_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "01");
                    int prv_month_e_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "31");
                    //PreviouS

                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");

                        if (mycode.IsUserExist("select Id from Graph_amount_calculated where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + dr["Month"].ToString() + "'"))
                        {
                            CalculateExpectedCollection.calculateExpCollectionMonthwise(Session_id, Branch_id, dr["Month"].ToString());
                            save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                        }
                        else
                        {
                            string isnew_std = find_is_new_std(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (isnew_std == "0")
                            {
                                CalculateExpectedCollection.calculateExpCollectionMonthwise(Session_id, Branch_id, dr["Month"].ToString());
                                save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                            }
                        }

                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "16");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "19");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "17");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "21");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);

                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", prevs_month);
                        cmd.Parameters.AddWithValue("@Class_id", Payment_estd_class);
                        cmd.Parameters.AddWithValue("@prevs_month_position", prevs_month_position);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0];

                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dt.Rows[0]["Month_name"].ToString();
                        drNewRow["Estimated Collection"] = dt.Rows[0]["Estimated Collection"].ToString();
                        drNewRow["Total Collected"] = dt.Rows[0]["Total Collected"].ToString();
                        drNewRow["Total Dues"] = dt.Rows[0]["Total Dues"].ToString();
                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        prevs_month = dr["Month"].ToString();
                        xi++;
                        prv_month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        prv_month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    }
                }


                labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
                var colors = new String[] { "#3e8ea6", "#83a82f", "#c5272a" };
                for (int i = 1; i < dtDatas.Columns.Count; i++)
                {
                    datasets.Add(new
                    {
                        label = dtDatas.Columns[i].ColumnName,
                        backgroundColor = colors[i - 1],
                        data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                    });
                }
                chartData = new
                {
                    labels = labels,
                    datasets = datasets,
                    last_data = new
                    {
                        month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                        estd_amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                        ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][2],
                        dues_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][3],
                    }
                };
            }
            return JsonConvert.SerializeObject(chartData);
        }

        private string find_is_new_std(string Session_id, string Branch_id, string Month, string Type)
        {
            string status = "1";
            string query = "select Id from admission_registor where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and admissionserialnumber not in (select Admission_no from Graph_calculated_student_monthwise where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Month='" + Month + "' and Type='MonthlyFee')";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                status = "0";
            }
            return status;
        }
        private string find_is_new_std_adm(string Session_id, string Branch_id, string Month, string Type)
        {
            string status = "1";
            string query = "select Id from admission_registor where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and (payment_status!= 'Paid' or payment_status='Dues' or payment_status='Unpaid')  and  Transfer_Status='New' and Status='1' and admissionserialnumber not in (select Admission_no from Graph_calculated_student_monthwise where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Type='" + Type + "')";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                status = "0";
            }
            return status;
        }
        private string get_dues_calculated_status(string Session_id, string Branch_id, string Month, string Type)
        {
            string columnName = ""; string columnNameDues = "";
            if (Type == "MonthlyFee")
            {
                columnName = "Is_Month";
                columnNameDues = "Is_prev_dues_calculaed_month";
            }
            if (Type == "AdmissionFee")
            {
                columnName = "Is_Admission";
                columnNameDues = "Is_prev_dues_calculaed_admission";
            }
            if (Type == "AnnualFee")
            {
                columnName = "Is_Annual";
                columnNameDues = "Is_prev_dues_calculaed_annual";
            }
            string status = "0";
            string query = "select Id from Graph_amount_calculated where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Month='" + Month + "' and " + columnName + "=1 and " + columnNameDues + "=1";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                status = "1";
            }
            return status;
        }

        private void save_status(string Session_id, string Branch_id, string MonthName, string prevs_month, string Type, string month_position, int month_s_idate, int month_e_idate, string cunrt_session, int mnth_nmbr, int prv_month_s_idate, int prv_month_e_idate)
        {
            if (mycode.IsUserExist("select Id from Graph_amount_calculated where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + MonthName + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Graph_amount_calculated (Session_id,Branch_id,Month,Updated_date,Updated_time,Updated_idate) values (@Session_id,@Branch_id,@Month,@Updated_date,@Updated_time,@Updated_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", Session_id);
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                cmd.Parameters.AddWithValue("@Month", MonthName);
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    string columnName = "";
                    string columnNameDues = "";
                    if (Type == "MonthlyFee")
                    {
                        columnName = "Is_Month";
                        columnNameDues = "Is_prev_dues_calculaed_month";
                    }
                    if (Type == "AdmissionFee")
                    {
                        columnName = "Is_Admission";
                        columnNameDues = "Is_prev_dues_calculaed_admission";
                    }
                    if (Type == "AnnualFee")
                    {
                        columnName = "Is_Annual";
                        columnNameDues = "Is_prev_dues_calculaed_annual";
                    }
                    if (Type == "OtherFee")
                    {
                        columnName = "Is_other_fee";
                        columnNameDues = "Is_prev_dues_calculaed_other_fee";
                    }
                    My.exeSql("update Graph_amount_calculated set " + columnName + "=1 where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + MonthName + "'");
                }
            }
            else
            {
                string columnName = "";
                string columnNameDues = "";
                if (Type == "MonthlyFee")
                {
                    columnName = "Is_Month";
                    columnNameDues = "Is_prev_dues_calculaed_month";
                }
                if (Type == "AdmissionFee")
                {
                    columnName = "Is_Admission";
                    columnNameDues = "Is_prev_dues_calculaed_admission";
                }
                if (Type == "AnnualFee")
                {
                    columnName = "Is_Annual";
                    columnNameDues = "Is_prev_dues_calculaed_annual";
                }
                if (Type == "OtherFee")
                {
                    columnName = "Is_other_fee";
                    columnNameDues = "Is_prev_dues_calculaed_other_fee";
                }
                My.exeSql("update Graph_amount_calculated set " + columnName + "=1 where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + MonthName + "'");
            }




            if (Type == "OtherFee")
            {
                if (mnth_nmbr > 1)
                {
                    //=================================
                    string qryss = "";
                    if (mnth_nmbr == 2)
                    {
                        qryss = "select t1.Class_id,t1.Month_name,isnull(sum(convert(float, Payable_amounts)),0) as Estimated_collection,(select isnull(sum(convert(float, Content_Fee)),0) from Other_Fee_Taken_For_Student where Payment_Idate<=" + prv_month_e_idate + " and Session_id=" + Session_id + " and Class_id=t1.Class_id) as Total_collected,t2.Position,(select isnull(sum(convert(float, Content_Fee)),0) from Other_Fee_Taken_For_Student where Payment_Idate<=" + prv_month_e_idate + " and Session_id=" + Session_id + " and Class_id=t1.Class_id) as Adv_collected from Temp_typewise_estimated_fee t1 join Month_Index t2 on t1.Month_name=t2.Month where Session_id=" + Session_id + " and  Parameter='OtherFee' and t2.Month='" + prevs_month + "'  group by t1.Month_name,t2.Position,t1.Class_id";
                    }
                    else
                    {
                        qryss = "select t1.Class_id,t1.Month_name,isnull(sum(convert(float, Payable_amounts)),0) as Estimated_collection,(select isnull(sum(convert(float, Content_Fee)),0) from Other_Fee_Taken_For_Student where Payment_Idate<=" + prv_month_s_idate + " and Payment_Idate<=" + prv_month_e_idate + " and Session_id=" + Session_id + " and Class_id=t1.Class_id) as Total_collected,t2.Position,(select isnull(sum(convert(float, Content_Fee)),0) from Other_Fee_Taken_For_Student where Payment_Idate<=" + prv_month_s_idate + " and Payment_Idate<=" + prv_month_e_idate + " and Session_id=" + Session_id + " and Class_id=t1.Class_id) as Adv_collected from Temp_typewise_estimated_fee t1 join Month_Index t2 on t1.Month_name=t2.Month where Session_id=" + Session_id + " and  Parameter='OtherFee' and t2.Month='" + prevs_month + "'  group by t1.Month_name,t2.Position,t1.Class_id";
                    }
                    DataTable dsgdt = My.dataTable(qryss);

                    foreach (DataRow dr in dsgdt.Rows)
                    {
                        double estimatedCollection = (My.toDouble(dr["Estimated_collection"].ToString()));
                        double duesAmt = estimatedCollection - My.toDouble(dr["Total_collected"].ToString());
                        if (mycode.IsUserExist("select Id from Graph_amount_calculated_prev_month where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id=" + dr["Class_id"].ToString() + " and Month='" + dr["Month_name"].ToString() + "' and Type='" + Type + "'"))
                        {
                            SqlCommand cmds;
                            string querys = "INSERT INTO Graph_amount_calculated_prev_month (Session_id,Branch_id,Class_id,Month,Estimated_collection,Total_collected,Total_dues,Updated_date,Updated_idate,Updated_time,Type) values (@Session_id,@Branch_id,@Class_id,@Month,@Estimated_collection,@Total_collected,@Total_dues,@Updated_date,@Updated_idate,@Updated_time,@Type)";
                            cmds = new SqlCommand(querys);
                            cmds.Parameters.AddWithValue("@Session_id", Session_id);
                            cmds.Parameters.AddWithValue("@Branch_id", Branch_id);
                            cmds.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                            cmds.Parameters.AddWithValue("@Month", dr["Month_name"].ToString());
                            cmds.Parameters.AddWithValue("@Estimated_collection", estimatedCollection.ToString());
                            cmds.Parameters.AddWithValue("@Total_collected", dr["Total_collected"].ToString());
                            cmds.Parameters.AddWithValue("@Total_dues", duesAmt);
                            cmds.Parameters.AddWithValue("@Updated_date", mycode.date());
                            cmds.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                            cmds.Parameters.AddWithValue("@Updated_time", mycode.time());
                            cmds.Parameters.AddWithValue("@Type", Type);
                            if (My.InsertUpdateData(cmds))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmds;
                            string querys = "Update Graph_amount_calculated_prev_month set Estimated_collection=@Estimated_collection,Total_collected=@Total_collected,Total_dues=@Total_dues  where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id=" + dr["Class_id"].ToString() + " and Month='" + dr["Month_name"].ToString() + "'  and Type='" + Type + "'";
                            cmds = new SqlCommand(querys);
                            cmds.Parameters.AddWithValue("@Estimated_collection", estimatedCollection.ToString());
                            cmds.Parameters.AddWithValue("@Total_collected", dr["Total_collected"].ToString());
                            cmds.Parameters.AddWithValue("@Total_dues", duesAmt);
                            if (My.InsertUpdateData(cmds))
                            {
                            }
                        }
                    }


                    string columnName = "";
                    string columnNameDues = "";
                    if (Type == "MonthlyFee")
                    {
                        columnName = "Is_Month";
                        columnNameDues = "Is_prev_dues_calculaed_month";
                    }
                    if (Type == "AdmissionFee")
                    {
                        columnName = "Is_Admission";
                        columnNameDues = "Is_prev_dues_calculaed_admission";
                    }
                    if (Type == "AnnualFee")
                    {
                        columnName = "Is_Annual";
                        columnNameDues = "Is_prev_dues_calculaed_annual";
                    }
                    if (Type == "OtherFee")
                    {
                        columnName = "Is_other_fee";
                        columnNameDues = "Is_prev_dues_calculaed_other_fee";
                    }
                    mycode.executequery("Update Graph_amount_calculated set " + columnNameDues + "=1  where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + prevs_month + "'");
                }
            }
            else
            {
                if (mnth_nmbr > 1)
                {
                    //=================================
                    string qryss = "";
                    if (mnth_nmbr == 2)
                    {
                        qryss = "select t1.Class_id,t1.Month_name,isnull(sum(convert(float, Payable_amounts)),0) as Estimated_collection,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where Idate<=" + prv_month_e_idate + " and session='" + cunrt_session + "' and  parameter='" + Type + "' and class=t1.Class_id) as Total_collected,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where Month=t1.Month_name and session='" + cunrt_session + "' and  parameter='" + Type + "' and Content='Late Fine' and class=t1.Class_id) as Total_late_fine,t2.Position,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where Idate<=" + prv_month_e_idate + " and session='" + cunrt_session + "' and  parameter='" + Type + "' and class=t1.Class_id) as Adv_collected from Temp_typewise_estimated_fee t1 join Month_Index t2 on t1.Month_name=t2.Month where Session_id=" + Session_id + " and  Parameter='" + Type + "' and Fee_head_type!='Late Fine' and t2.Month='" + prevs_month + "'  group by t1.Month_name,t2.Position,t1.Class_id";
                    }
                    else
                    {
                        qryss = "select t1.Class_id,t1.Month_name,isnull(sum(convert(float, Payable_amounts)),0) as Estimated_collection,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where Idate>=" + prv_month_s_idate + " and  Idate<=" + prv_month_e_idate + " and session='" + cunrt_session + "' and  parameter='" + Type + "' and class=t1.Class_id) as Total_collected,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where Month=t1.Month_name and session='" + cunrt_session + "' and  parameter='" + Type + "' and Content='Late Fine' and class=t1.Class_id) as Total_late_fine,t2.Position,(select isnull(sum(convert(float, paid)),0) from Monthly_Fee_Collection_Slip where Idate>=" + prv_month_s_idate + " and  Idate<=" + prv_month_e_idate + " and session='" + cunrt_session + "' and  parameter='" + Type + "' and Month!='" + prevs_month + "'  and class=t1.Class_id) as Adv_collected from Temp_typewise_estimated_fee t1 join Month_Index t2 on t1.Month_name=t2.Month where Session_id=" + Session_id + " and  Parameter='" + Type + "' and Fee_head_type!='Late Fine' and t2.Month='" + prevs_month + "'  group by t1.Month_name,t2.Position,t1.Class_id";
                    }
                    DataTable dsgdt = My.dataTable(qryss);

                    foreach (DataRow dr in dsgdt.Rows)
                    {
                        double estimatedCollection = (My.toDouble(dr["Estimated_collection"].ToString()) + My.toDouble(dr["Total_late_fine"].ToString()) + My.toDouble(dr["Adv_collected"].ToString()));
                        double duesAmt = estimatedCollection - My.toDouble(dr["Total_collected"].ToString());
                        if (mycode.IsUserExist("select Id from Graph_amount_calculated_prev_month where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id=" + dr["Class_id"].ToString() + " and Month='" + dr["Month_name"].ToString() + "' and Type='" + Type + "'"))
                        {
                            SqlCommand cmds;
                            string querys = "INSERT INTO Graph_amount_calculated_prev_month (Session_id,Branch_id,Class_id,Month,Estimated_collection,Total_collected,Total_dues,Updated_date,Updated_idate,Updated_time,Type) values (@Session_id,@Branch_id,@Class_id,@Month,@Estimated_collection,@Total_collected,@Total_dues,@Updated_date,@Updated_idate,@Updated_time,@Type)";
                            cmds = new SqlCommand(querys);
                            cmds.Parameters.AddWithValue("@Session_id", Session_id);
                            cmds.Parameters.AddWithValue("@Branch_id", Branch_id);
                            cmds.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                            cmds.Parameters.AddWithValue("@Month", dr["Month_name"].ToString());
                            cmds.Parameters.AddWithValue("@Estimated_collection", estimatedCollection.ToString());
                            cmds.Parameters.AddWithValue("@Total_collected", dr["Total_collected"].ToString());
                            cmds.Parameters.AddWithValue("@Total_dues", duesAmt);
                            cmds.Parameters.AddWithValue("@Updated_date", mycode.date());
                            cmds.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                            cmds.Parameters.AddWithValue("@Updated_time", mycode.time());
                            cmds.Parameters.AddWithValue("@Type", Type);
                            if (My.InsertUpdateData(cmds))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmds;
                            string querys = "Update Graph_amount_calculated_prev_month set Estimated_collection=@Estimated_collection,Total_collected=@Total_collected,Total_dues=@Total_dues  where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id=" + dr["Class_id"].ToString() + " and Month='" + dr["Month_name"].ToString() + "'  and Type='" + Type + "'";
                            cmds = new SqlCommand(querys);
                            cmds.Parameters.AddWithValue("@Estimated_collection", estimatedCollection.ToString());
                            cmds.Parameters.AddWithValue("@Total_collected", dr["Total_collected"].ToString());
                            cmds.Parameters.AddWithValue("@Total_dues", duesAmt);
                            if (My.InsertUpdateData(cmds))
                            {
                            }
                        }
                    }


                    string columnName = "";
                    string columnNameDues = "";
                    if (Type == "MonthlyFee")
                    {
                        columnName = "Is_Month";
                        columnNameDues = "Is_prev_dues_calculaed_month";
                    }
                    if (Type == "AdmissionFee")
                    {
                        columnName = "Is_Admission";
                        columnNameDues = "Is_prev_dues_calculaed_admission";
                    }
                    if (Type == "AnnualFee")
                    {
                        columnName = "Is_Annual";
                        columnNameDues = "Is_prev_dues_calculaed_annual";
                    }
                    if (Type == "OtherFee")
                    {
                        columnName = "Is_other_fee";
                        columnNameDues = "Is_prev_dues_calculaed_other_fee";
                    }
                    mycode.executequery("Update Graph_amount_calculated set " + columnNameDues + "=1  where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + prevs_month + "'");
                }
            }
        }

        private string get_s_months()
        {
            string months = "0/0/0";
            string query = "select Month,Month_Id,Position from Month_Index order by Position asc";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                months = dt.Rows[0]["Month"].ToString() + "/" + dt.Rows[0]["Month_Id"].ToString() + "/" + dt.Rows[0]["Position"].ToString();
            }
            return months;
        }

        private string get_month_position(string MonthName, string MonthNumber)
        {
            string Position = "0";
            string query = "select Position from Month_Index where Month_Id='" + MonthNumber + "'";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                Position = dt.Rows[0]["Position"].ToString();
            }
            return Position;
        }


        private static DataTable FillDatastatic(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, My.conn);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }


        //==================================ADMISSION
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report_admission(string Session_id, string Session_name, string Type, string Branch_id, string Payment_estd_class_adm)
        {
            string columnName = ""; string columnNameDues = "";
            if (Type == "AdmissionFee")
            {
                columnName = "Is_Admission";
                columnNameDues = "Is_prev_dues_calculaed_admission";
            }
            if (Type == "AnnualFee")
            {
                columnName = "Is_Annual";
                columnNameDues = "Is_prev_dues_calculaed_annual";
            }

            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Estimated Collection");
            dtDatas.Columns.Add("Total Collected");
            dtDatas.Columns.Add("Total Dues");


            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1; string prevs_month = dtm.Rows[0]["Month"].ToString();
                    //PreviouS
                    string prv_month_id_in_two_dgt = My.getMonthS_twoDigit(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_chart_month = My.toint(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_s_year = My.check_start_months(prv_chart_month, s_year_real);
                    int prv_month_s_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "01");
                    int prv_month_e_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "31");
                    //PreviouS

                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");


                        if (mycode.IsUserExist("select Id from Graph_amount_calculated where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + dr["Month"].ToString() + "' and " + columnName + "=1"))
                        {
                            CalculateExpectedCollection.calculateExpCollectionAdmission(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                        }
                        else
                        {
                            string isnew_std = find_is_new_std_adm(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (isnew_std == "0")
                            {
                                CalculateExpectedCollection.calculateExpCollectionAdmission(Session_id, Branch_id, dr["Month"].ToString(), Type);
                                save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                            }
                        }

                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "16");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "19");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "17");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "21");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);
                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", prevs_month);
                        cmd.Parameters.AddWithValue("@Class_id", Payment_estd_class_adm);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0];


                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dt.Rows[0]["Month_name"].ToString();
                        drNewRow["Estimated Collection"] = dt.Rows[0]["Estimated Collection"].ToString();
                        drNewRow["Total Collected"] = dt.Rows[0]["Total Collected"].ToString();
                        drNewRow["Total Dues"] = dt.Rows[0]["Total Dues"].ToString();
                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        prevs_month = dr["Month"].ToString();
                        xi++;
                        prv_month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        prv_month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    }
                }



                labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
                var colors = new String[] { "#57d1c3", "#9962d0", "#fdac2b" };
                for (int i = 1; i < dtDatas.Columns.Count; i++)
                {
                    datasets.Add(new
                    {
                        label = dtDatas.Columns[i].ColumnName,
                        backgroundColor = colors[i - 1],
                        data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                    });
                }
                chartData = new
                {
                    labels = labels,
                    datasets = datasets,
                    last_data = new
                    {
                        month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                        estd_amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                        ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][2],
                        dues_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][3],
                    }
                };
            }
            return JsonConvert.SerializeObject(chartData);
        }

        //==================================ANNUAL
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report_annual(string Session_id, string Session_name, string Type, string Branch_id, string Payment_estd_class_adm)
        {
            string columnName = ""; string columnNameDues = "";
            if (Type == "AdmissionFee")
            {
                columnName = "Is_Admission";
                columnNameDues = "Is_prev_dues_calculaed_admission";
            }
            if (Type == "AnnualFee")
            {
                columnName = "Is_Annual";
                columnNameDues = "Is_prev_dues_calculaed_annual";
            }

            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Estimated Collection");
            dtDatas.Columns.Add("Total Collected");
            dtDatas.Columns.Add("Total Dues");


            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1; string prevs_month = dtm.Rows[0]["Month"].ToString();
                    //PreviouS
                    string prv_month_id_in_two_dgt = My.getMonthS_twoDigit(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_chart_month = My.toint(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_s_year = My.check_start_months(prv_chart_month, s_year_real);
                    int prv_month_s_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "01");
                    int prv_month_e_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "31");
                    //PreviouS

                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");


                        if (mycode.IsUserExist("select Id from Graph_amount_calculated where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + dr["Month"].ToString() + "' and " + columnName + "=1"))
                        {
                            CalculateExpectedCollection.calculateExpCollectionAdmission(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                        }
                        else
                        {
                            string isnew_std = find_is_new_std_adm(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (isnew_std == "0")
                            {
                                CalculateExpectedCollection.calculateExpCollectionAdmission(Session_id, Branch_id, dr["Month"].ToString(), Type);
                                save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                            }
                        }

                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "16");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "19");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "17");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "21");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_adm == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);
                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", prevs_month);
                        cmd.Parameters.AddWithValue("@Class_id", Payment_estd_class_adm);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0];


                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dt.Rows[0]["Month_name"].ToString();
                        drNewRow["Estimated Collection"] = dt.Rows[0]["Estimated Collection"].ToString();
                        drNewRow["Total Collected"] = dt.Rows[0]["Total Collected"].ToString();
                        drNewRow["Total Dues"] = dt.Rows[0]["Total Dues"].ToString();
                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        prevs_month = dr["Month"].ToString();
                        xi++;
                        prv_month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        prv_month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    }
                }



                labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
                var colors = new String[] { "#08596c", "#da8f00", "#80bace" };
                for (int i = 1; i < dtDatas.Columns.Count; i++)
                {
                    datasets.Add(new
                    {
                        label = dtDatas.Columns[i].ColumnName,
                        backgroundColor = colors[i - 1],
                        data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                    });
                }
                chartData = new
                {
                    labels = labels,
                    datasets = datasets,
                    last_data = new
                    {
                        month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                        estd_amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                        ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][2],
                        dues_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][3],
                    }
                };
            }
            return JsonConvert.SerializeObject(chartData);
        }
        //==================================OVERALL

        [WebMethod(EnableSession = true)]
        public string find_overall_estd_collections_report(string Session_id, string Session_name, string Type, string Branch_id, string Payment_estd_class_overall)
        {
            string columnName = ""; string columnNameDues = "";
            if (Type == "AdmissionFee")
            {
                columnName = "Is_Admission";
                columnNameDues = "Is_prev_dues_calculaed_admission";
            }
            if (Type == "AnnualFee")
            {
                columnName = "Is_Annual";
                columnNameDues = "Is_prev_dues_calculaed_annual";
            }

            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Estimated Collection");
            dtDatas.Columns.Add("Total Collected");
            dtDatas.Columns.Add("Total Dues");


            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1; string prevs_month = dtm.Rows[0]["Month"].ToString();
                    //PreviouS
                    string prv_month_id_in_two_dgt = My.getMonthS_twoDigit(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_chart_month = My.toint(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_s_year = My.check_start_months(prv_chart_month, s_year_real);
                    int prv_month_s_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "01");
                    int prv_month_e_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "31");
                    //PreviouS

                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");


                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_overall == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "26");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "28");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_overall == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "27");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "29");
                                }
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_overall == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "32");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "33");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_overall == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "27");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "29");
                                }
                            }
                        }
                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);
                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", prevs_month);
                        cmd.Parameters.AddWithValue("@Class_id", Payment_estd_class_overall);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0];


                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dt.Rows[0]["Month_name"].ToString();
                        drNewRow["Estimated Collection"] = dt.Rows[0]["Estimated Collection"].ToString();
                        drNewRow["Total Collected"] = dt.Rows[0]["Total Collected"].ToString();
                        drNewRow["Total Dues"] = dt.Rows[0]["Total Dues"].ToString();
                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        prevs_month = dr["Month"].ToString();
                        xi++;
                        prv_month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        prv_month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    }
                }



                labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
                var colors = new String[] { "#172fa9", "#00c13b", "#ebe400" };
                for (int i = 1; i < dtDatas.Columns.Count; i++)
                {
                    datasets.Add(new
                    {
                        label = dtDatas.Columns[i].ColumnName,
                        backgroundColor = colors[i - 1],
                        data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                    });
                }
                chartData = new
                {
                    labels = labels,
                    datasets = datasets,
                    last_data = new
                    {
                        month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                        estd_amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                        ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][2],
                        dues_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][3],
                    }
                };
            }
            return JsonConvert.SerializeObject(chartData);
        }



        //==================================ADMISSION
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report_otherfee(string Session_id, string Session_name, string Type, string Branch_id, string Payment_estd_class_otherfee)
        {
            string columnName = ""; string columnNameDues = "";
            if (Type == "OtherFee")
            {
                columnName = "Is_other_fee";
                columnNameDues = "Is_prev_dues_calculaed_other_fee";
            }
            if (Type == "AnnualFee")
            {
                columnName = "Is_Annual";
                columnNameDues = "Is_prev_dues_calculaed_annual";
            }

            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Estimated Collection");
            dtDatas.Columns.Add("Total Collected");
            dtDatas.Columns.Add("Total Dues");


            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1; string prevs_month = dtm.Rows[0]["Month"].ToString();
                    //PreviouS
                    string prv_month_id_in_two_dgt = My.getMonthS_twoDigit(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_chart_month = My.toint(dtm.Rows[0]["Month_Id"].ToString());
                    int prv_s_year = My.check_start_months(prv_chart_month, s_year_real);
                    int prv_month_s_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "01");
                    int prv_month_e_idate = My.toint(prv_s_year.ToString() + prv_month_id_in_two_dgt + "31");
                    //PreviouS

                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");


                        if (mycode.IsUserExist("select Id from Graph_amount_calculated where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + dr["Month"].ToString() + "' and " + columnName + "=1"))
                        {
                            CalculateExpectedCollection.calculateExpCollectionOtherFee(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                        }
                        else
                        {
                            if (mycode.IsUserExist("select Id from Graph_amount_calculated where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Month='" + dr["Month"].ToString() + "' and " + columnNameDues + "=1"))
                            {
                                string isnew_std = find_is_new_fee_head(Session_id, Branch_id, dr["Month"].ToString(), Type);
                                if (isnew_std == "0")
                                {
                                    CalculateExpectedCollection.calculateExpCollectionOtherFee(Session_id, Branch_id, dr["Month"].ToString(), Type);
                                    save_status(Session_id, Branch_id, dr["Month"].ToString(), prevs_month, Type, month_position, month_s_idate, month_e_idate, cunrt_session, xi, prv_month_s_idate, prv_month_e_idate);
                                }
                            }
                        }

                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_otherfee == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "34");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "35");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_otherfee == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            string is_dues_calculated = get_dues_calculated_status(Session_id, Branch_id, dr["Month"].ToString(), Type);
                            if (is_dues_calculated == "0")
                            {
                                if (Payment_estd_class_otherfee == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "36");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "37");
                                }
                            }
                            else
                            {
                                if (Payment_estd_class_otherfee == "0")  // ALL CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "18");
                                }
                                else // WITH CLASS
                                {
                                    cmd.Parameters.AddWithValue("@sp_status", "20");
                                }
                            }
                        }
                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);
                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", prevs_month);
                        cmd.Parameters.AddWithValue("@Class_id", Payment_estd_class_otherfee);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0]; 
                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dt.Rows[0]["Month_name"].ToString();
                        drNewRow["Estimated Collection"] = dt.Rows[0]["Estimated Collection"].ToString();
                        drNewRow["Total Collected"] = dt.Rows[0]["Total Collected"].ToString();
                        drNewRow["Total Dues"] = dt.Rows[0]["Total Dues"].ToString();
                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        prevs_month = dr["Month"].ToString();
                        xi++;
                        prv_month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        prv_month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    }
                }



                labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
                var colors = new String[] { "#dd9ea7", "#8f1f47", "#e8b86e" };
                for (int i = 1; i < dtDatas.Columns.Count; i++)
                {
                    datasets.Add(new
                    {
                        label = dtDatas.Columns[i].ColumnName,
                        backgroundColor = colors[i - 1],
                        data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                    });
                }
                chartData = new
                {
                    labels = labels,
                    datasets = datasets,
                    last_data = new
                    {
                        month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                        estd_amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                        ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][2],
                        dues_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][3],
                    }
                };
            }
            return JsonConvert.SerializeObject(chartData);
        }


        private string find_is_new_fee_head(string Session_id, string Branch_id, string Month, string Type)
        {
            string status = "1";
            string query = "select * from Other_Fee_For_Special_Condition where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "' and Content_Name not in(select Fee_head_type from Temp_typewise_estimated_fee where Session_id=" + Session_id + " and Branch_id='" + Branch_id + "')";
            DataTable dt = FillDatastatic(query);
            if (dt.Rows.Count > 0)
            {
                status = "0";
            }
            return status;
        }


        //==================================FORMSALE
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report_formsale(string Session_id, string Session_name, string Type, string Branch_id, string Payment_class_form_sale_fee)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Total Collection");


            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            if (My.toint(session_frst_year) == 2023)
            {
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);
                int s_year_real = s_year;
                string s_months = get_s_months();

                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
                string s_month = arrs[0];
                string s_month_id = arrs[1];
                string s_month_position = arrs[2];



                DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string MonthName = dtime.ToString("MMMM");
                string MonthNumber = dtime.ToString("MM"); //"04";//
                string month_position = get_month_position(MonthName, MonthNumber);

                if (My.toint(s_month_position) >= My.toint(month_position))
                {
                    MonthName = s_month;
                    month_position = s_month_position;
                }


                string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
                DataTable dtm = FillDatastatic(query);
                if (dtm.Rows.Count > 0)
                {
                    int xi = 1;
                    foreach (DataRow dr in dtm.Rows)
                    {
                        string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                        int chart_month = My.toint(dr["Month_Id"].ToString());
                        s_year = My.check_start_months(chart_month, s_year_real);
                        int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                        int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");


                        SqlCommand cmd = new SqlCommand("sp_Graph_Report");
                        if (xi == 1)    //====FISRT MONTH
                        {
                            if (Payment_class_form_sale_fee == "0")  // ALL CLASS
                            {
                                cmd.Parameters.AddWithValue("@sp_status", "38");
                            }
                            else // WITH CLASS
                            {
                                cmd.Parameters.AddWithValue("@sp_status", "40");
                            }
                        }
                        else   //====AFTER FISRT MONTH
                        {
                            if (Payment_class_form_sale_fee == "0")  // ALL CLASS
                            {
                                cmd.Parameters.AddWithValue("@sp_status", "39");
                            }
                            else // WITH CLASS
                            {
                                cmd.Parameters.AddWithValue("@sp_status", "41");
                            }
                        }
                        cmd.Parameters.AddWithValue("@Session", Session_name);
                        cmd.Parameters.AddWithValue("@Session_id", Session_id);
                        cmd.Parameters.AddWithValue("@Parameter", Type);
                        cmd.Parameters.AddWithValue("@MonthPosition", month_position);
                        cmd.Parameters.AddWithValue("@fromIdate", month_s_idate);
                        cmd.Parameters.AddWithValue("@toIdate", month_e_idate);
                        cmd.Parameters.AddWithValue("@MonthName", dr["Month"].ToString());
                        cmd.Parameters.AddWithValue("@PrevMonthName", "");
                        cmd.Parameters.AddWithValue("@Class_name", Payment_class_form_sale_fee);
                        DataSet ds = My.executeReaderDataSet(cmd);
                        DataTable dt = ds.Tables[0];


                        DataRow drNewRow = dtDatas.NewRow();
                        drNewRow["Month"] = dr["Month"].ToString();
                        drNewRow["Total Collection"] = dt.Rows[0]["Total Collection"].ToString();

                        dtDatas.Rows.Add(drNewRow);
                        dtDatas.AcceptChanges();
                        xi++;
                    }
                }



                labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
                var colors = new String[] { "#75b694", "#00c13b", "#ebe400" };
                for (int i = 1; i < dtDatas.Columns.Count; i++)
                {
                    datasets.Add(new
                    {
                        label = dtDatas.Columns[i].ColumnName,
                        backgroundColor = colors[i - 1],
                        data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                    });
                }
                chartData = new
                {
                    labels = labels,
                    datasets = datasets,
                    last_data = new
                    {
                        month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                        estd_amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                    }
                };
            }
            return JsonConvert.SerializeObject(chartData);
        }



        ///===========================================

        //==============================================CHART
        [WebMethod(EnableSession = true)]
        public string find_months_collections_report_days(string Session_id, string Session_name, string Branch_id, string Monthsdays)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Day");
            dtDatas.Columns.Add("Total Collected");
            List<object> datasets = new List<object>(); object labels = "";

            string cunrt_session = Session_name;
            string session_frst_year = cunrt_session.Substring(0, 4);
            int s_year = My.toint(session_frst_year);
            int pay_month = My.toint(Monthsdays);
            s_year = My.check_start_months(pay_month, s_year);
            int noofdays = DateTime.DaysInMonth(s_year, pay_month);

            object chartData = "";
            string query = "select * from Month_day order by Id asc";
            DataTable dtm = FillDatastatic(query);
            if (dtm.Rows.Count > 0)
            {
                for (int i = 0; i < noofdays; i++)
                {
                    string idateS = s_year + Monthsdays + dtm.Rows[i]["Day"].ToString();
                    DataTable dtD = My.dataTable("select isnull(sum(convert(float, paid)),0) as Total from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>=" + idateS + " and Idate<=" + idateS + "");

                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["Day"] = dtm.Rows[i]["Day"].ToString();
                    drNewRow["Total Collected"] = dtD.Rows[0]["Total"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }


            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Day")).ToList();
            var colors = new String[] { "#008CFF", "#83a82f", "#c5272a" };
            for (int i = 1; i < dtDatas.Columns.Count; i++)
            {
                datasets.Add(new
                {
                    label = dtDatas.Columns[i].ColumnName,
                    backgroundColor = colors[i - 1],
                    data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                });
            }
            chartData = new
            {
                labels = labels,
                datasets = datasets, 
            };

            return JsonConvert.SerializeObject(chartData);
        }




        [WebMethod(EnableSession = true)]
        public string find_overall_collections_report_monthwise(string Session_id, string Session_name, string Branch_id, string Payment_estd_class_overall)
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("Total Collected"); 
            List<object> datasets = new List<object>(); object labels = "";
            string cunrt_session = My.get_session();
            string session_frst_year = cunrt_session.Substring(0, 4);

            object chartData = "";
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);
            int s_year_real = s_year;
            string s_months = get_s_months(); 
            string[] stringSeparatorss = new string[] { "/" };
            string[] arrs = s_months.Split(stringSeparatorss, StringSplitOptions.None);
            string s_month = arrs[0];
            string s_month_id = arrs[1];
            string s_month_position = arrs[2];



            DateTime dtime = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string MonthName = dtime.ToString("MMMM");
            string MonthNumber = dtime.ToString("MM"); //"04";//
            string month_position = get_month_position(MonthName, MonthNumber);

            if (My.toint(s_month_position) >= My.toint(month_position))
            {
                MonthName = s_month;
                month_position = s_month_position;
            }


            string query = "select * from Month_Index where Position<='" + month_position + "' order by Position asc";
            DataTable dtm = FillDatastatic(query);
            if (dtm.Rows.Count > 0)
            {
                foreach (DataRow dr in dtm.Rows)
                {
                    string month_id_in_two_dgt = My.getMonthS_twoDigit(dr["Month_Id"].ToString());
                    int chart_month = My.toint(dr["Month_Id"].ToString());
                    s_year = My.check_start_months(chart_month, s_year_real);
                    int month_s_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "01");
                    int month_e_idate = My.toint(s_year.ToString() + month_id_in_two_dgt + "31");
                    DataTable dtfee = new DataTable();
                    if (Payment_estd_class_overall == "0")
                    {
                        dtfee = My.dataTable("select isnull(sum(convert(float, paid)),0) as Total from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>=" + month_s_idate + " and Idate<=" + month_e_idate + "");
                    }
                    else
                    {
                        dtfee = My.dataTable("select isnull(sum(convert(float, paid)),0) as Total from Monthly_Fee_Collection_Slip where session='" + Session_name + "' and Idate>=" + month_s_idate + " and Idate<=" + month_e_idate + " and class='" + Payment_estd_class_overall + "'");
                    }

                    DataRow drNewRow = dtDatas.NewRow();
                    drNewRow["Month"] = dr["Month"].ToString();
                    drNewRow["Total Collected"] = dtfee.Rows[0]["Total"].ToString();
                    dtDatas.Rows.Add(drNewRow);
                    dtDatas.AcceptChanges();
                }
            }



            labels = dtDatas.AsEnumerable().Select(row => row.Field<object>("Month")).ToList();
            var colors = new String[] { "#15CA20", "#00c13b", "#ebe400" };
            for (int i = 1; i < dtDatas.Columns.Count; i++)
            {
                datasets.Add(new
                {
                    label = dtDatas.Columns[i].ColumnName,
                    backgroundColor = colors[i - 1],
                    data = dtDatas.AsEnumerable().Select(row => row.Field<object>(dtDatas.Columns[i].ColumnName)).ToList()
                });
            }
            chartData = new
            {
                labels = labels,
                datasets = datasets,
                last_data = new
                {
                    month = dtDatas.Rows[dtDatas.Rows.Count - 1][0],
                    ttl_Amt = dtDatas.Rows[dtDatas.Rows.Count - 1][1],
                }
            };

            return JsonConvert.SerializeObject(chartData);
        }


        //--------------------------------------Hostel--------------------

     
        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_std_hostel_details(string Paramiter1)
        {
            string qry = "select distinct t2.rollnumber,t2.session,t2.studentname,t2.class as Class_name,t2.Section,(select top 1 Hostel_name from Hostels_master where Hostel_id=t1.Hostel_id) as Hostel_name,(select top 1 Category_name from Hostel_room_category_master where Category_id=t1.Category_id) as Room_category_name,(select top 1 Room_name from Hostel_room_master where Room_id=t1.Room_id) as Room_name,(select top 1 Bed_name from Hostel_room_bed_master where Bed_id=t1.Bed_id) as Bed_name,t1.Admission_no,t1.From_month_name from Hostel_assign_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where Hostel_assign_id='" + Paramiter1 + "'";
            string raring = "";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Hostel_assign_master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Show_student_hostel_details.Add(new Fetch_student_hostel_details
                    {
                        Session = dr["session"].ToString(),
                        Studentname = dr["studentname"].ToString(),
                        Class_name = dr["Class_name"].ToString(),
                        Section = dr["Section"].ToString(),
                        Hostel_name = dr["Hostel_name"].ToString(),
                        Room_category_name = dr["Room_category_name"].ToString(),
                        Room_name = dr["Room_name"].ToString(),
                        Bed_name = dr["Bed_name"].ToString(),
                        From_month_name = dr["From_month_name"].ToString(),
                        Admission_no = dr["Admission_no"].ToString(),
                        rollnumber = dr["rollnumber"].ToString()
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_student_hostel_details));
            }
        }

    }
}
