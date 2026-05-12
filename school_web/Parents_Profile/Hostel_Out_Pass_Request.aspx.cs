using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Globalization;

namespace school_web.Parents_Profile
{
    public partial class Hostel_Out_Pass_Request : System.Web.UI.Page
    {
        My mycode = new My();
        string studentname = "Select top 1 studentname from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";
        string classname = "Select top 1 class from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";
        string rollnumber = "Select top 1 rollnumber from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";

        string student_mobile = "Select top 1 father_mob from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";
        string emailid_student = "Select top 1 email_id from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";

        string studentimagepath = "Select top 1 studentimagepath from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";
        string gcm_id = "Select top 1 gcm_id from admission_registor where admissionserialnumber=hopr.Adm_No order by id desc";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {

                    try
                    {
                        ViewState["Userid"] = Request.QueryString["regid"].ToString();
                        ViewState["user_type"] = My.get_user_type(ViewState["Userid"].ToString());

                        if(ViewState["user_type"].ToString()=="0")
                        {
                            ViewState["user_type"] = "Parents";
                        }
                        txt_date.Text = mycode.dayback(15);
                        txt_enddate.Text = mycode.date();
                        bind_grd_view();




                    }
                    catch
                    {
                    }
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
        }
        protected void btn_find_by_date_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        }
        private void bind_grd_view()
        {
            string sdate = txt_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);
            string edate = txt_enddate.Text;
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

                string query1 = "select  *,format(Apply_date_time,'dd/MM/yyyy') as applydate, format(Start_date_time,'dd/MM/yyyy hh:mm:ss tt') as Start_datetime,format(End_Date_time,'dd/MM/yyyy hh:mm:ss tt') as End_Datetime,(" + studentname + ") as studentname,(" + classname + ") as classname ,(" + rollnumber + ") as rollnumber,format(Last_reply_date_time,'dd/MM/yyyy hh:mm:ss tt') as Last_replydatetime,(" + gcm_id + ") as gcm_id    from Hostel_Out_Pass_Request hopr  where  format(Apply_date_time,'yyyyMMdd')>=" + idate + " and format(hopr.Last_reply_date_time,'yyyyMMdd')<=" + idate2 + " and Adm_No in (select Student_id from Parent_Student_Mapping where Parent_id='" + ViewState["Userid"].ToString() + "' )  order by cast(format(hopr.Last_reply_date_time,'yyyyMMdd') as int) desc ";


                try
                {


                    DataTable dt = mycode.FillData(query1);
                    if (dt.Rows.Count == 0)
                    {
                        RPDetails.DataSource = null;
                        RPDetails.DataBind();


                    }
                    else
                    {
                        RPDetails.DataSource = dt;
                        RPDetails.DataBind();




                    }
                }
                catch
                {

                }



            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Request_id = (Label)row.FindControl("lbl_Request_id");

            Bind_hostel_outpass(lbl_Request_id.Text);
        }
        private void Bind_hostel_outpass(string Request_id)
        {
            ViewState["Enquiry_Id"] = Request_id;
            ViewState["gcm_id"] = "";
            row1.Visible = true;


            string query1 = "select  *,format(Apply_date_time,'dd/MM/yyyy hh:mm:ss tt') as applydate, format(Start_date_time,'dd/MM/yyyy hh:mm:ss tt') as Start_datetime,format(End_Date_time,'dd/MM/yyyy hh:mm:ss tt') as End_Datetime,(" + studentname + ") as studentname,(" + classname + ") as classname ,(" + rollnumber + ") as rollnumber,format(Last_reply_date_time,'dd/MM/yyyy hh:mm:ss tt') as Last_replydatetime,(" + student_mobile + ") as student_mobile, (" + emailid_student + ") as emailid_student,(" + studentimagepath + ") as studentimagepath,(" + gcm_id + ") as gcm_id   from Hostel_Out_Pass_Request hopr  where   hopr.Request_id='" + Request_id + "' ";

            DataTable dt = mycode.FillData(query1);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                lbl_admi_no.Text = dt.Rows[0]["Adm_No"].ToString();
                if (dt.Rows[0]["Last_status"].ToString() == "Forward to Parents")
                {
                    mycode.bind_ddl(ddl_status, "Select Status from Hostel_Out_Pass_Status_Master where Status_id in('3','4')");

                }
                else
                {
                    row1.Visible = false;
                }
                if (dt.Rows[0]["studentimagepath"].ToString() == "")
                {
                    Image1.ImageUrl = "../images/dummy-student.jpg";
                }
                else
                {
                    Image1.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                }
                lbl_Enquirydate.Text = dt.Rows[0]["applydate"].ToString();
                lbl_lastfloupdate.Text = dt.Rows[0]["Last_replydatetime"].ToString();
                lbl_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["student_mobile"].ToString();
                lbl_email.Text = dt.Rows[0]["emailid_student"].ToString();
                lbl_roll_no.Text = dt.Rows[0]["rollnumber"].ToString();
                lbl_class.Text = dt.Rows[0]["classname"].ToString();

                string query2 = "Select *,format(Date_time_chat,'dd/MM/yyyy hh:mm:ss tt') as Date_timechat  from Hostel_Out_Pass_Chat where  Request_id='" + Request_id + "' order by id desc";
                DataTable dt1 = mycode.FillData(query2);
                if (dt1.Rows.Count == 0)
                {
                    GrdView_Follow_Up.DataSource = null;
                    GrdView_Follow_Up.DataBind();
                }
                else
                {
                    GrdView_Follow_Up.DataSource = dt1;
                    GrdView_Follow_Up.DataBind();
                }
                Dictionary<string, object> dc1 = mycode.get_assined_hostel_info_adm(lbl_admi_no.Text);
                lbl_hostel_romm_no.Text = (String)dc1["Room_name"];
                lbl_hostel_bed_no.Text = (String)dc1["Bed_name"];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
        }


        #region save data
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_status.Text == "Select")
                {
                    Alertme("Please select status", "success");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                }
                else
                {
                    string mergeStartTime = mycode.date() + " " + mycode.time();
                    DateTime Created_date = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    string query = "INSERT INTO Hostel_Out_Pass_Chat (Adm_No,Request_id,Date_time_chat,Status,Remarks,Reply_By,Reply_By_User) values (@Adm_No,@Request_id,@Date_time_chat,@Status,@Remarks,@Reply_By,@Reply_By_User)";
                    SqlCommand cmd1;
                    cmd1 = new SqlCommand(query);
                    cmd1.Parameters.AddWithValue("@Adm_No", lbl_admi_no.Text);
                    cmd1.Parameters.AddWithValue("@Request_id", ViewState["Enquiry_Id"].ToString());
                    cmd1.Parameters.AddWithValue("@Date_time_chat", Created_date);
                    cmd1.Parameters.AddWithValue("@Status", ddl_status.Text);
                    cmd1.Parameters.AddWithValue("@Remarks", txt_remarks_floup.Text);
                    cmd1.Parameters.AddWithValue("@Reply_By", ViewState["user_type"].ToString());
                    cmd1.Parameters.AddWithValue("@Reply_By_User", ViewState["Userid"].ToString());
                    if (My.InsertUpdateData(cmd1))
                    {

                        SqlCommand cmd2;
                        cmd2 = new SqlCommand("update Hostel_Out_Pass_Request set Last_status=@Last_status,Last_remarks=@Last_remarks,Last_reply_date_time=@Last_reply_date_time where Request_id=@Request_id");
                        cmd2.Parameters.AddWithValue("@Request_id", ViewState["Enquiry_Id"].ToString());
                        cmd2.Parameters.AddWithValue("@Last_reply_date_time", Created_date);
                        cmd2.Parameters.AddWithValue("@Last_remarks", txt_remarks_floup.Text);
                        cmd2.Parameters.AddWithValue("@Last_status", ddl_status.Text);
                        if (My.InsertUpdateData(cmd2))
                        {



                            string gcm_id = ViewState["gcm_id"].ToString();
                            string User_id = lbl_admi_no.Text;
                            Dictionary<String, String> ss = new Dictionary<string, string>();
                            ss["notification_id"] = Guid.NewGuid().ToString();
                            ss["message"] = txt_remarks_floup.Text;
                            ss["title"] = "Hostel Outpass";
                            ss["messagetype"] = "Hostel Outpass";
                            ss["url"] = "";
                            ss["link_url"] = "";
                            ss["UserId"] = User_id;
                            UsesCode.SendNotification(gcm_id, ss);
                            txt_remarks_floup.Text = "";
                            ddl_status.Text = "Select";
                            Alertme("Record has been saved successfully", "success");
                            Bind_hostel_outpass(ViewState["Enquiry_Id"].ToString());
                            bind_grd_view();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel Outpass Parents_Profile");
            }

        }
        #endregion

    }
}