using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Hostel_Out_Pass_Request : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();

                    try
                    {
                        ViewState["pid"] = Request.QueryString["pid"].ToString();

                    }
                    catch
                    {
                        ViewState["pid"] = "";
                    }

                    txt_from_date.Text = mycode.date();
                    last_status();

                }
            }
        }

        private void last_status()
        {
            string query = "Select top 1 * from Hostel_Out_Pass_Request where Adm_No='" + ViewState["regid"].ToString() + "' order by id desc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                if (dt.Rows[0]["Last_status"].ToString() == "Back of the hostel")
                {
                    btn_submit.Visible = true;

                }
                else if (dt.Rows[0]["Last_status"].ToString() == "Reject")
                {
                    btn_submit.Visible = true;

                }
                else
                {
                    btn_submit.Visible = false;
                    Alertme("Sorry, you cannot apply for a hostel out pass because there is already an ongoing request.", "warning");
                }
                
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

            //if (panel == "success")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertSucess(" + msg + ");", true);
            //}
            //if (panel == "warning")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertError(" + msg + ");", true);

               
            //}
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string confirmValue = string.Empty;
            confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {


                save_data_apply_hostel_out_pass();


            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
        }

        private void save_data_apply_hostel_out_pass()
        {
            try
            {
                if (ViewState["regid"] == null)
                {
                    Alertme("Something went wrong. Please try again some time later.", "warning");
                }
                else if (txt_from_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_from_date.Focus();
                }
                else if (txt_time_from.Text == "")
                {
                    Alertme("Please choose from time.", "warning");
                    txt_time_from.Focus();
                }

                else if (txt_todate.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_todate.Focus();
                }
                else if (txt_to_time.Text == "")
                {
                    Alertme("Please choose to time.", "warning");
                    txt_to_time.Focus();
                }

                else if (txt_remarks.Text == "")
                {
                    Alertme("Please enter remarks", "warning");
                    txt_remarks.Focus();
                }
                else
                {
                    string filePath = "";
                    save_data(filePath);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_data(string filePath)
        {
            int No_of_day = mycode.get_no_day_towdateselection(txt_from_date.Text, txt_todate.Text);

            string sdate = txt_from_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_todate.Text;
            string eday = edate.Substring(0, 2);
            string emonth = edate.Substring(3, 2);
            string eyear = edate.Substring(6, 4);

            int idate = Convert.ToInt32(syear + smonth + sday);
            int idate2 = Convert.ToInt32(eyear + emonth + eday);

            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                string Request_id = My.get_hostel_outpass_request("Hosteloutpass");
                string start_time = My.toDateTime(txt_from_date.Text + " " + txt_time_from.Text).ToString("MM/dd/yyyy hh:mm:ss tt");
                string end_time = My.toDateTime(txt_todate.Text + " " + txt_to_time.Text).ToString("MM/dd/yyyy hh:mm:ss tt");
                SqlCommand cmd;
                string query = "INSERT INTO Hostel_Out_Pass_Request (Adm_No,Apply_date_time,Start_date_time,End_Date_time,Last_status,Last_remarks,Last_reply_date_time,Request_id,No_of_day,Session_id,Parent_id) values (@Adm_No,@Apply_date_time,@Start_date_time,@End_Date_time,@Last_status,@Last_remarks,@Last_reply_date_time,@Request_id,@No_of_day,@Session_id,@Parent_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Adm_No", ViewState["regid"].ToString());
                cmd.Parameters.AddWithValue("@Apply_date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Start_date_time", start_time);
                cmd.Parameters.AddWithValue("@End_Date_time", end_time);
                cmd.Parameters.AddWithValue("@Last_status", "Pending");
                cmd.Parameters.AddWithValue("@Last_remarks", txt_remarks.Text);
                cmd.Parameters.AddWithValue("@Last_reply_date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Request_id", Request_id);
                cmd.Parameters.AddWithValue("@No_of_day", No_of_day);
                cmd.Parameters.AddWithValue("@Session_id", My.get_session_id()) ;
                cmd.Parameters.AddWithValue("@Parent_id", ViewState["pid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    SqlCommand cmd1;
                    string query1 = "INSERT INTO Hostel_Out_Pass_Chat (Adm_No,Request_id,Date_time_chat,Status,Remarks,Reply_By,Reply_By_User) values (@Adm_No,@Request_id,@Date_time_chat,@Status,@Remarks,@Reply_By,@Reply_By_User)";
                    cmd1 = new SqlCommand(query1);
                    cmd1.Parameters.AddWithValue("@Adm_No", ViewState["regid"].ToString());
                    cmd1.Parameters.AddWithValue("@Request_id", Request_id);
                    cmd1.Parameters.AddWithValue("@Date_time_chat", My.getdate1());
                    cmd1.Parameters.AddWithValue("@Status", "Pending");
                    cmd1.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                    cmd1.Parameters.AddWithValue("@Reply_By", "Student");
                    cmd1.Parameters.AddWithValue("@Reply_By_User", ViewState["regid"].ToString());
                
                    if (My.InsertUpdateData(cmd1))
                    {
                        Alertme("Request has been submitted successfully", "success");

                        txt_remarks.Text = "";
                        txt_time_from.Text = "";
                        txt_to_time.Text = "";
                        txt_todate.Text = "";
                        txt_from_date.Text = "";
                    }

                }
            }
        }
    }
}