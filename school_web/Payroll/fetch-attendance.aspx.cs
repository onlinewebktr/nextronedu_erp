using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class fetch_attendance : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();

                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fetch_Attendance");
            }
        }





        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose start date.", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose end date.", "warning");
                    txt_e_date.Focus();
                }
                else
                {
                    DateTime start_date, end_date;
                    start_date = Convert.ToDateTime(txt_s_date.Text);
                    end_date = Convert.ToDateTime(txt_e_date.Text);
                    fetch_emp_attendance(start_date, end_date);
                }
            }
            catch (Exception ex)
            {
            }
        }


        String fetch_log = "";
        DataTable adt = new DataTable();
        private void fetch_emp_attendance(DateTime start_date, DateTime end_date)
        {
            try
            {
                fetch_log = "";
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Device_Details", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (My.connect_device(dr["Device_Ip"].ToString(), dr["Device_Name"].ToString()))
                    {
                        fetch_log += dr["Device_Name"].ToString() + " Connected\n";
                        fetch_log += ("Fetching Attendance From ") + dr["Device_Name"].ToString() + "\n";
                        fetch_att(1, start_date, end_date);
                        fetch_log += ("Fetching Attendance Completed from ") + dr["Device_Name"].ToString() + "\n";
                        fetch_log += ("Disconnecting... " + dr["Device_Name"].ToString() + "\n");
                        My.objZkeeper.Disconnect();
                        fetch_log += dr["Device_Name"].ToString() + " Disconnected\n";
                    }
                    else
                    {
                        fetch_log += (dr["Device_Name"].ToString() + " Not Connected") + "\n";
                    }
                }
            }
            catch (Exception Ex)
            {
                My.Save_Exception(Ex.StackTrace);
            }


            Alertme(fetch_log, "success");
            txt_attendance.Text = "Auto Attendance fetching Start in : " + DateTime.Now.AddMinutes(15).ToString("hh:mm tt");
            ViewState["txt_attendance_UID"] = DateTime.Now.AddMinutes(15).ToString("hh:mm tt");
        }



        public static string machine_code_of_emp = "";
        public static string employee_id1 = "";
        private void fetch_att(int machine_no, DateTime start_date, DateTime end_date)
        {
            try
            {
                ICollection<MachineInfo> lstMachineInfo = My.manipulator.GetLogData(My.objZkeeper, machine_no);
                if (lstMachineInfo != null && lstMachineInfo.Count > 0)
                {
                    send_to_attn_log(lstMachineInfo, start_date, end_date);
                }
            }

            catch (Exception ex)
            {
                My.Save_Exception("fetch_att-->" + ex.Message + "-->" + ex.StackTrace.ToString());
            }
        }







        private static void send_to_attn_log(ICollection<MachineInfo> lstMachineInfo, DateTime start_date, DateTime end_date)
        {
            DataTable dt = My.dataTable("sp_Attendance_Log");
            foreach (MachineInfo mi in lstMachineInfo)
            {
                try
                {
                    int idate = My.toIntS(Convert.ToDateTime(mi.Attendance_time).ToString("yyyyMMdd"));
                    if (idate >= My.toIntS(start_date.ToString("yyyyMMdd")) && idate <= My.toIntS(end_date.ToString("yyyyMMdd")))
                    {
                        string staff_id = mi.Staff_ID.ToString();
                        DateTime attendance_time = mi.Attendance_time;
                        DataRow[] drs = dt.Select("Employee_id='" + staff_id + "' and  DateTime='" + attendance_time + "'");
                        if (drs.Length == 0)
                        {
                            My.exeSql("insert into PRL_Attendance_Log(Employee_id,DateTime) values ('" + staff_id + "',N'" + attendance_time.ToString("dd-MMM-yyyy hh:mm:ss tt") + "');");
                        }
                    }
                }
                catch (Exception Ex)
                {

                    My.Save_Exception("send_to_attn_log -->" + Ex.Message + "-->" + Ex.StackTrace.ToString());
                }
            }
        }
    }
}