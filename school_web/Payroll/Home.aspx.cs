using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class Home : System.Web.UI.Page
    {

        My mycod = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Session["Admin"] = "edunext2021";
                    //Session["firm"] = "1";

                    string TodaydatEtim = mycod.date();

                    //TenDayS
                    DateTime TenstartTime = DateTime.ParseExact(TodaydatEtim, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string TenDaysDate = TenstartTime.AddDays(-10).ToShortDateString();
                    int TenDayS = My.DateConvertToIdate(TenDaysDate);
                    hd_TenDayS.Value = TenDayS.ToString();

                     
                    bind_data();
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "DefaultPage");
                }
            }
        }




        private void bind_data()
        {
            string ttl_emp = "0"; string ttl_present = "0";
            string ttl_leave = "0";
            
            string idate = mycod.idate();
            string totalemplye = "select count(Employee_Name) total_emp from dbo.[PRL_Employee_Master] where Status='Active'";

            string totalemplyepersent = "select * from(  select *,( select count(Employee_id) from dbo.[PRL_Attendance_Log] where Employee_id=PRL_Employee_Master.Emp_Code and format(DateTime,'yyyyMMdd')=" + idate + ") atten from dbo.[PRL_Employee_Master]  )t where atten>0";
            string totalemplyeabsent = " select * from(  select *,( select count(Employee_id) from dbo.[PRL_Attendance_Log] where Employee_id=PRL_Employee_Master.Emp_Code and format(DateTime,'yyyyMMdd')='20220210') atten from dbo.[PRL_Employee_Master]  )t where atten=0";


             DataTable dt = mycod.FillData(totalemplye);
             if (dt.Rows.Count == 0)
             {
                 ttl_employees.InnerText = dt.Rows[0]["total_emp"].ToString();
                 ttl_emp = dt.Rows[0]["total_emp"].ToString();
             }
             else
             {
                 ttl_employees.InnerText = dt.Rows[0]["total_emp"].ToString();
                 ttl_emp = dt.Rows[0]["total_emp"].ToString();
             }
            //--------------------------------------------------
             DataTable dt1 = mycod.FillData(totalemplyepersent);
             if (dt1.Rows.Count == 0)
             {
                 ttl_p_today.InnerText = "0";
                 ttl_present = "0";
             }
             else
             {
                 ttl_p_today.InnerText = dt1.Rows.Count.ToString();
                 ttl_present = dt1.Rows.Count.ToString();
             }
           // ----------------------leave-------------------

             string leave = "select count(Employee_id) leave_total from dbo.[PRL_Employee_Daily_attendance_chart] where idate='" + idate + @"' and Shift_1_Working='-2'";

             DataTable dt2 = mycod.FillData(leave);
             if (dt2.Rows.Count == 0)
             {
                 in_leave_tdy.InnerText = "0";
                 ttl_leave = "0";



             }
             else
             {
                 in_leave_tdy.InnerText = dt2.Rows[0]["leave_total"].ToString();
                 ttl_leave = dt2.Rows[0]["leave_total"].ToString();
             }
             ttl_absent.InnerText = (My.toDouble(ttl_emp) - My.toDouble(ttl_present) - My.toDouble(ttl_leave)).ToString();


          
//            string sql = @";
//                                       select count(Employee_id) present_total from dbo.[PRL_Employee_Daily_attendance_chart] where idate='" + idate + @"' and isnull(Shift_1_Working,0)+isnull(Shift_2_Working,0)>='3';
//                                       ;";

//            DataSet ds = mycod.Fill_Data_set(sql);
//            if (ds.Tables[0].Rows.Count > 0)
//            {
//                DataTable dtTemp = ds.Tables[0];
//                if (dtTemp.Rows.Count != 0)
//                {
//                    ttl_employees.InnerText = dtTemp.Rows[0]["total_emp"].ToString();
//                    ttl_emp = dtTemp.Rows[0]["total_emp"].ToString();
//                }
//                else
//                {
//                    ttl_employees.InnerText = "00";
//                }
//            }
//            else
//            {
//                ttl_employees.InnerText = "00"; ;
//            }


//            if (ds.Tables[1].Rows.Count > 0)
//            {
//                DataTable dtTemp = ds.Tables[1];
//                if (dtTemp.Rows.Count != 0)
//                {
//                    ttl_p_today.InnerText = dtTemp.Rows[0]["present_total"].ToString();
//                    ttl_present = dtTemp.Rows[0]["present_total"].ToString();
//                }
//                else
//                {
//                    ttl_p_today.InnerText = "00";
//                }
//            }
//            else
//            {
//                ttl_p_today.InnerText = "00"; ;
//            }

//            //============

//            if (ds.Tables[2].Rows.Count > 0)
//            {
//                DataTable dtTemp = ds.Tables[2];
//                if (dtTemp.Rows.Count != 0)
//                {
//                    in_leave_tdy.InnerText = dtTemp.Rows[0]["leave_total"].ToString();
//                    ttl_leave = dtTemp.Rows[0]["leave_total"].ToString();
//                }
//                else
//                {
//                    in_leave_tdy.InnerText = "00";
//                }
//            }
//            else
//            {
//                in_leave_tdy.InnerText = "00"; ;
//            }

           
        }

    }
}