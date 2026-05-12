using school_web.AppCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace school_web.Payroll
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
        //OrderSummarY
        [WebMethod(EnableSession = true)]
        public string find_order_summary_report_report(string Ten_Days)
        {

            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            cmd.Parameters.AddWithValue("@sp_status", "1");
            cmd.Parameters.AddWithValue("@fromdateV", Ten_Days);

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["apdate"].ToString());
                    yaxis.Add(dr["present_total"].ToString());
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

        //=====================
        [WebMethod(EnableSession = true)]
        public string find_staff_summary_report_report(string GendeR)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            if (GendeR == "0")
            {
                cmd.Parameters.AddWithValue("@sp_status", "2");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Genders", GendeR);
                cmd.Parameters.AddWithValue("@sp_status", "3");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Design_name"].ToString());
                    yaxis.Add(dr["No_of_emp"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        //=====================
        [WebMethod(EnableSession = true)]
        public string find_staff_summary_report_report_dep(string GendeR)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            if (GendeR == "0")
            {
                cmd.Parameters.AddWithValue("@sp_status", "4");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Genders", GendeR);
                cmd.Parameters.AddWithValue("@sp_status", "5");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Dep_name"].ToString());
                    yaxis.Add(dr["No_of_emp"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        //=====================
        [WebMethod(EnableSession = true)]
        public string find_staff_summary_report_report_grade()
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");

            cmd.Parameters.AddWithValue("@sp_status", "6");
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["grade_name"].ToString());
                    yaxis.Add(dr["No_of_emp"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        //=====================
        [WebMethod(EnableSession = true)]
        public string find_staff_summary_report_report_religion()
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");

            cmd.Parameters.AddWithValue("@sp_status", "7");
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Religion"].ToString());
                    yaxis.Add(dr["No_of_emp"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        //=====================AttendanceSummaryY
        [WebMethod(EnableSession = true)]
        public string find_staff_attendance_summary_report(string DatE)
        {
            int idate = My.DateConvertToIdate(DatE);
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            cmd.Parameters.AddWithValue("@sp_status", "8");
            cmd.Parameters.AddWithValue("@Sidate", idate);
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["AttType"].ToString());
                    yaxis.Add(dr["TypeCount"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }


        //=====================AttendanceSummaryY
        [WebMethod(EnableSession = true)]
        public string find_staff_attendance_summary_report_depwise(string DatE)
        {
            int idate = My.DateConvertToIdate(DatE);
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            cmd.Parameters.AddWithValue("@sp_status", "9");
            cmd.Parameters.AddWithValue("@Sdate", idate);
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["Dep_name"].ToString());
                    yaxis.Add(dr["TypeCount"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }
        //=====================AttendanceSummaryY
        [WebMethod(EnableSession = true)]
        public string find_staff_attendance_summary_report_gradewise(string DatE)
        {
            int idate = My.DateConvertToIdate(DatE);
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            cmd.Parameters.AddWithValue("@sp_status", "10");
            cmd.Parameters.AddWithValue("@Sdate", idate);
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    xaxis.Add(dr["grade_name"].ToString());
                    yaxis.Add(dr["TypeCount"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        My mycode = new My();
        //=====================AttendanceSummaryY
        [WebMethod(EnableSession = true)]
        public string find_staff_salary_monthly_report(string DEP)
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            if (DEP == "0")
            {
                cmd.Parameters.AddWithValue("@sp_status", "11");
                cmd.Parameters.AddWithValue("@year", mycode.year());
            }
            else
            {
                cmd.Parameters.AddWithValue("@sp_status", "12");
                cmd.Parameters.AddWithValue("@Department_id", DEP);
                cmd.Parameters.AddWithValue("@year", mycode.year());
            }

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string mnth = My.getMonthS_name(dr["month"].ToString());
                    xaxis.Add(mnth);
                    yaxis.Add(dr["Total_salary"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        //=====================AttendanceSummaryY
        [WebMethod(EnableSession = true)]
        public string find_staff_pf_monthly_report()
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            cmd.Parameters.AddWithValue("@year", mycode.year());
            cmd.Parameters.AddWithValue("@sp_status", "13");

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string mnth = My.getMonthS_name(dr["month"].ToString());
                    xaxis.Add(mnth);
                    yaxis.Add(dr["Total_Pf"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }

        //=====================AttendanceSummaryY
        [WebMethod(EnableSession = true)]
        public string find_staff_esi_monthly_report()
        {
            List<string> xaxis = new List<string>();
            List<string> yaxis = new List<string>();
            SqlCommand cmd = new SqlCommand("sp_Payroll_Graph_Report");
            cmd.Parameters.AddWithValue("@year", mycode.year());
            cmd.Parameters.AddWithValue("@sp_status", "14");

            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            inpsection_Area ir = new inpsection_Area();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string mnth = My.getMonthS_name(dr["month"].ToString());
                    xaxis.Add(mnth);
                    yaxis.Add(dr["Total_Esi"].ToString());
                }
                ir.xaxis = xaxis;
                ir.yaxis = yaxis;
            }
            return JsonConvert.SerializeObject(ir, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
