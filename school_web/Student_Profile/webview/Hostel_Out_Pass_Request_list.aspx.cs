using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Hostel_Out_Pass_Request_list : System.Web.UI.Page
    {
        My mycode = new My();
        string studentname = "Select top 1 studentname from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id() + "' order by id desc";
        string classname = "Select top 1 class from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id() + "' order by id desc";
        string rollnumber = "Select top 1 rollnumber from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id()+"' order by id desc";

        string student_mobile = "Select top 1 father_mob from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id() + "' order by id desc";
        string emailid_student = "Select top 1 email_id from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id() + "' order by id desc";

        string studentimagepath = "Select top 1 studentimagepath from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id() + "' order by id desc";
        string gcm_id = "Select top 1 gcm_id from admission_registor where admissionserialnumber=hopr.Adm_No and Session_id='" + My.get_session_id() + "' order by id desc";

        string ProfilePhotowarden = "Select top 1 ProfilePhoto from user_details where user_id=hopr.Hostel_warden_id ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {

                    try
                    {
                        ViewState["Userid"] = Request.QueryString["regid"].ToString();
                        ViewState["user_type"] = "Student"; //My.get_user_type(ViewState["Userid"].ToString());
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

                string query1 = "select  *,format(Apply_date_time,'dd/MM/yyyy') as applydate, format(Start_date_time,'dd/MM/yyyy hh:mm:ss tt') as Start_datetime,format(End_Date_time,'dd/MM/yyyy hh:mm:ss tt') as End_Datetime,(" + studentname + ") as studentname,(" + classname + ") as classname ,(" + rollnumber + ") as rollnumber,format(Last_reply_date_time,'dd/MM/yyyy hh:mm:ss tt') as Last_replydatetime,(" + gcm_id + ") as gcm_id    from Hostel_Out_Pass_Request hopr  where  format(Apply_date_time,'yyyyMMdd')>=" + idate + " and format(hopr.Last_reply_date_time,'yyyyMMdd')<=" + idate2 + " and Adm_No='" + ViewState["Userid"].ToString() + "' and  Session_id='"+ My.get_session_id() + "'     order by cast(format(hopr.Last_reply_date_time,'yyyyMMdd') as int) desc ";

                string query2 = "select Last_status,count(Id) as total   from Hostel_Out_Pass_Request hopr  where  format(hopr.Last_reply_date_time,'yyyyMMdd')>=" + idate + " and format(hopr.Last_reply_date_time,'yyyyMMdd')<=" + idate2 + " and hopr.Adm_No='" + ViewState["Userid"].ToString() + "' and  hopr.Session_id='" + My.get_session_id() + "'   group by hopr.Last_status";


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

                try
                {
                    // lbl_total_Assined_Warden.InnerText = "0";
                    lbl_total_ongoing.InnerText = "0";
                    lbl_total_reject.InnerText = "0";
                    lbl_total_Complite.InnerText = "0";
                    DataSet ds = mycode.Fill_Data_set(query2);
                    DataTable dt = ds.Tables[0];

                    int Ongoing = 0;
                    int Yet_to_start = 0;




                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {


                            if (dr["Last_status"].ToString() == "On going")
                            {
                                Ongoing = My.toint(dr["total"].ToString());
                            }


                            else if (dr["Last_status"].ToString() == "Reject")
                            {
                                lbl_total_reject.InnerText = dr["total"].ToString();
                            }
                            else if (dr["Last_status"].ToString() == "Back of the hostel")
                            {
                                lbl_total_Complite.InnerText = dr["total"].ToString();
                            }
                            int tot = My.toInt(Ongoing) + My.toInt(Yet_to_start);
                            lbl_total_ongoing.InnerText = tot.ToString();


                        }
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);


        }

        private void Bind_hostel_outpass(string Request_id)
        {
            ViewState["Enquiry_Id"] = Request_id;
            ViewState["gcm_id"] = "";
            row1.Visible = true;


            string query1 = "select  *,format(Apply_date_time,'dd/MM/yyyy hh:mm:ss tt') as applydate, format(Start_date_time,'dd/MM/yyyy hh:mm:ss tt') as Start_datetime,format(End_Date_time,'dd/MM/yyyy hh:mm:ss tt') as End_Datetime,(" + studentname + ") as studentname,(" + classname + ") as classname ,(" + rollnumber + ") as rollnumber,format(Last_reply_date_time,'dd/MM/yyyy hh:mm:ss tt') as Last_replydatetime,(" + student_mobile + ") as student_mobile, (" + emailid_student + ") as emailid_student,(" + studentimagepath + ") as studentimagepath,(" + gcm_id + ") as gcm_id,(" + ProfilePhotowarden + ") as ProfilePhotowarden   from Hostel_Out_Pass_Request hopr  where   hopr.Request_id='" + Request_id + "' ";

            DataTable dt = mycode.FillData(query1);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_request_Id.Text = dt.Rows[0]["Request_id"].ToString();
                lbl_status1.Text = dt.Rows[0]["Last_status"].ToString();

                ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                ViewState["Hostel_warden_id"] = dt.Rows[0]["Hostel_warden_id"].ToString();
                lbl_admission_no.Text = dt.Rows[0]["Adm_No"].ToString();
                lbl_created_by.Text = dt.Rows[0]["studentname"].ToString();
                string assineduser = mycode.get_user(dt.Rows[0]["Hostel_warden_id"].ToString());
                if (assineduser == "")
                {
                    img_assined_to.ImageUrl = "../images/dummy-student.jpg";
                    lbl_assined_to.Text = "xxxxxx";
                }
                else
                {
                    if (dt.Rows[0]["ProfilePhotowarden"].ToString() == "")
                    {
                        img_assined_to.ImageUrl = "../images/dummy-student.jpg";
                    }
                    else
                    {
                        img_assined_to.ImageUrl = dt.Rows[0]["ProfilePhotowarden"].ToString();
                    }
                    lbl_assined_to.Text = assineduser;
                }
                if (dt.Rows[0]["studentimagepath"].ToString() == "")
                {
                    img_created_by.ImageUrl = "../images/dummy-student.jpg";
                }
                else
                {
                    img_created_by.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                }

                Dictionary<string, object> dc1 = mycode.get_assined_hostel_info_adm(lbl_admission_no.Text);
                lbl_hostel_romm_no.Text = (String)dc1["Room_name"];
                lbl_hostel_bed_no.Text = (String)dc1["Bed_name"];


                if (dt.Rows[0]["Last_status"].ToString() == "On going")
                {
                    row1.Visible = true;
                }
                else if (dt.Rows[0]["Last_status"].ToString() == "Yet to start")
                {
                    row1.Visible = true;
                }
                else
                {
                    row1.Visible = false;
                }

                string query2 = "Select *,format(Date_time_chat,'dd/MMM/yyyy hh:mm tt') as Date_timechat  from Hostel_Out_Pass_Chat where  Request_id='" + Request_id + "' order by id asc";
                DataTable dt1 = mycode.FillData(query2);
                if (dt1.Rows.Count == 0)
                {


                }
                else
                {

                    foreach (DataRow dr in dt1.Rows)
                    {


                        if (dr["Status"].ToString() == "Pending")
                        {
                            lbl_created.Text = dr["Date_timechat"].ToString() + " " + dr["Remarks"].ToString();
                            pronumS1.Attributes.Add("class", "step step-active");
                            pronumS2.Attributes.Add("class", "step");
                            pronumS3.Attributes.Add("class", "step");
                            pronumS4.Attributes.Add("class", "step");
                            pronumS5.Attributes.Add("class", "step");

                        }

                        if (dr["Status"].ToString() == "Approved")
                        {
                            lbl_approved.Text = dr["Date_timechat"].ToString() + " " + dr["Remarks"].ToString();
                            pronumS2.Attributes.Add("class", "step step-active");
                            pronumS1.Attributes.Add("class", "step");
                            pronumS3.Attributes.Add("class", "step");
                            pronumS4.Attributes.Add("class", "step");
                            pronumS5.Attributes.Add("class", "step");
                            lblstatus.Text = "Approved";

                        }
                        if (dr["Status"].ToString() == "Reject")
                        {
                            lbl_approved.Text = dr["Date_timechat"].ToString() + " " + dr["Remarks"].ToString();
                            pronumS2.Attributes.Add("class", "step step-active");
                            pronumS1.Attributes.Add("class", "step");
                            pronumS3.Attributes.Add("class", "step");
                            pronumS4.Attributes.Add("class", "step");
                            pronumS5.Attributes.Add("class", "step");
                            lblstatus.Text = "Reject";
                        }

                        if (dr["Status"].ToString() == "On going")
                        {
                            lbl_on_going.Text = dr["Date_timechat"].ToString() + " " + dr["Remarks"].ToString();
                            pronumS3.Attributes.Add("class", "step step-active");
                            pronumS1.Attributes.Add("class", "step");
                            pronumS2.Attributes.Add("class", "step");
                            pronumS4.Attributes.Add("class", "step");
                            pronumS5.Attributes.Add("class", "step");
                        }


                        if (dr["Status"].ToString() == "Yet to start")
                        {
                            pronumS4.Attributes.Add("class", "step step-active");
                            pronumS1.Attributes.Add("class", "step");
                            pronumS2.Attributes.Add("class", "step");
                            pronumS3.Attributes.Add("class", "step");
                            pronumS5.Attributes.Add("class", "step");

                            Panel1.Visible = false;
                            string query3 = "Select *,format(Date_time_chat,'dd/MMM/yyyy hh:mm tt') as Datetimechat  from Hostel_Out_Pass_Chat where  Request_id='" + Request_id + "' and Status='Yet to start'    order by id asc";
                            DataTable dt3 = mycode.FillData(query3);
                            if (dt3.Rows.Count == 0)
                            {
                                Repeater1_yet_to_start.DataSource = null;
                                Repeater1_yet_to_start.DataBind();

                            }
                            else
                            {
                                Panel1.Visible = true;
                                Repeater1_yet_to_start.DataSource = dt3;
                                Repeater1_yet_to_start.DataBind();
                            }
                        }
                        if (dr["Status"].ToString() == "Back of the hostel")
                        {
                            lbl_end.Text = dr["Date_timechat"].ToString() + " " + dr["Remarks"].ToString();

                            pronumS5.Attributes.Add("class", "step step-active");
                            pronumS1.Attributes.Add("class", "step");
                            pronumS2.Attributes.Add("class", "step");
                            pronumS3.Attributes.Add("class", "step");
                            pronumS4.Attributes.Add("class", "step");
                        }

                    }








                }

            }
        }

        protected void imgbutton_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string mergeStartTime = mycode.date() + " " + mycode.time();
                DateTime Created_date = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                string query = "INSERT INTO Hostel_Out_Pass_Chat (Adm_No,Request_id,Date_time_chat,Status,Remarks,Reply_By,Reply_By_User) values (@Adm_No,@Request_id,@Date_time_chat,@Status,@Remarks,@Reply_By,@Reply_By_User)";
                SqlCommand cmd1;
                cmd1 = new SqlCommand(query);
                cmd1.Parameters.AddWithValue("@Adm_No", lbl_admission_no.Text);
                cmd1.Parameters.AddWithValue("@Request_id", ViewState["Enquiry_Id"].ToString());
                cmd1.Parameters.AddWithValue("@Date_time_chat", Created_date);
                cmd1.Parameters.AddWithValue("@Status", "Yet to start");
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
                    cmd2.Parameters.AddWithValue("@Last_status", "Yet to start");
                    if (My.InsertUpdateData(cmd2))
                    {

                        Dictionary<string, object> dc1 = My.get_parent_info(lbl_admission_no.Text);
                        string gcm_id = (String)dc1["gcm_id"];
                        string User_id = (String)dc1["User_id"];
                        Dictionary<String, String> ss = new Dictionary<string, string>();
                        ss["notification_id"] = Guid.NewGuid().ToString();
                        ss["message"] = txt_remarks_floup.Text;
                        ss["title"] = "Hostel Outpass";
                        ss["messagetype"] = "Hostel Outpass";
                        ss["url"] = "";
                        ss["link_url"] = "";
                        ss["UserId"] = User_id;
                        UsesCode.SendNotification(gcm_id, ss);
                        Dictionary<string, object> dc2 = My.get_user_info(ViewState["Hostel_warden_id"].ToString());
                        string gcm_id1 = (String)dc2["gcm_id"];
                        string User_id1 = (String)dc2["user_id"];
                        Dictionary<String, String> ss1 = new Dictionary<string, string>();
                        ss1["notification_id"] = Guid.NewGuid().ToString();
                        ss1["message"] = txt_remarks_floup.Text;
                        ss1["title"] = "Hostel Outpass";
                        ss1["messagetype"] = "Hostel Outpass";
                        ss1["url"] = "";
                        ss1["link_url"] = "";
                        ss1["UserId"] = User_id1;
                        UsesCode.SendNotification(gcm_id1, ss1);
                        txt_remarks_floup.Text = "";
                        Alertme("Record has been saved successfully", "success");
                        Bind_hostel_outpass(ViewState["Enquiry_Id"].ToString());
                        bind_grd_view();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                    }

                }


            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel Outpass Parents_Profile");
            }
        }
    }
}