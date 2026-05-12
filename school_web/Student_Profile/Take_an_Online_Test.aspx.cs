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

namespace school_web.Student_Profile
{
    public partial class Take_an_Online_Test : System.Web.UI.Page
    {
        My mycode = new My();
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            My mycode = new My();
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["regid"] = Session["User"].ToString();
                        bind_student_class_and_section();
                        Fil_data_pageload();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }

                }
            }
            catch
            {

            }

        }
        private void bind_student_class_and_section()
        {
            DataTable dt = code.FillTable("Select top 1 Class_id,Section,Branch_id,Session_id from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc  ");
            if (dt.Rows.Count == 0)
            {
                ViewState["classid"] = "0";
                ViewState["section"] = "0";
                ViewState["branchid"] = "1";
                ViewState["sessionid"] = "0";
            }
            else
            {
                ViewState["sessionid"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["section"] = dt.Rows[0]["Section"].ToString();
                ViewState["branchid"] = dt.Rows[0]["Branch_id"].ToString();
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
        private void Fil_data_pageload()
        {
            lbl_msg2.Text = "";
            string query1 = " Select  *,(select top 1 Subject_name from Subject_Master where Subject_id=OlineTest_Exam_name.subjectname) as subject_name, format(live_date, 'dd/MM/yyyy') as live_date1,format(live_date, 'hh:mm tt') as live_time1,format(DATEADD(Minute,  cast(Exam_duration as int), live_date ), 'dd/MM/yyyy') as End_date,format(DATEADD(Minute,  cast(Exam_duration as int), live_date ), 'hh:mm tt') as Exam_endtime, format(DATEADD(Minute,  cast(Exam_duration as int), live_date ), 'yyyyMMdd') as End_Exam_Idate,format(live_date, 'yyyyMMdd') as Exam_Start_Idate,format(DATEADD(Minute,  cast(Exam_duration as int), live_date ), 'yyyyMMddHHmmss') as Exam_intendtime,format(live_date, 'yyyyMMddHHmmss') as Exam_intstarttime from  OlineTest_Exam_name where  Status='Active'  and format(live_date, 'yyyyMMdd')>='" + code.idate() + "' and Class_id='" + ViewState["classid"].ToString() + "' and Section='" + ViewState["section"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "' ";

            SqlCommand cmd = new SqlCommand(query1);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                Alertme("Sorry, there are no live exam lists available", "warning");
                lbl_msg2.Text = "Sorry, there are no live exam lists available";
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();


                for (int i = 0; i < RPDetails.Items.Count; i++)
                {
                    Label lbl_testid = (Label)RPDetails.Items[i].FindControl("lbl_testid");

                    Label lbl_tackstatus = (Label)RPDetails.Items[i].FindControl("lbl_tackstatus");
                    Label lbltestdate = (Label)RPDetails.Items[i].FindControl("lbltestdate");
                    Button btn_view_test = (Button)RPDetails.Items[i].FindControl("btn_view_test");
                    Button btn_view_result = (Button)RPDetails.Items[i].FindControl("btn_view_result");
                    Label lbl_TestDate_dis = (Label)RPDetails.Items[i].FindControl("lbl_TestDate_dis");
                    Label lbl12 = (Label)RPDetails.Items[i].FindControl("lbl12");
                    Label lbl_Duration = (Label)RPDetails.Items[i].FindControl("lbl_Duration");
                    Label lbl_Status = (Label)RPDetails.Items[i].FindControl("lbl_Status");
                    if (lbl_Duration.Text == "0")
                    {
                        lbl12.Visible = false;
                        lbl_Duration.Visible = false;
                    }
                    else
                    {
                        var time = TimeSpan.FromMinutes(Convert.ToInt32(lbl_Duration.Text));

                        TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToInt32(lbl_Duration.Text));
                        string workHours = spWorkMin.ToString(@"hh\:mm");


                        // lbl_Duration.Text = Convert.ToString((spWorkMin.TotalHours));

                        lbl12.Visible = true;
                        // lbl_Duration.Visible = true;
                    }
                    bool chktackstatus = My.find_status_takstatus(lbl_testid.Text, ViewState["regid"].ToString());
                    if (chktackstatus == false)
                    {
                        if (lbltestdate.Text == code.date())
                        {
                            lbl_tackstatus.Text = "Not Attempted";
                            btn_view_result.Visible = false;
                            if (lbl_Status.Text == "Inactive")
                            {
                                btn_view_test.Enabled = false;
                            }
                            else
                            {
                                btn_view_test.Enabled = true;
                            }

                        }
                        else
                        {
                            lbl_tackstatus.Text = "Not Attempted";
                            btn_view_test.Enabled = false;
                        }

                    }
                    else
                    {
                        btn_view_result.Visible = true;

                        btn_view_test.Enabled = false;
                        lbl_tackstatus.Text = "Attempted";
                    }

                }
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_subject = (Label)e.Item.FindControl("lbl_subject");
                Label lbl_subjectname_view = (Label)e.Item.FindControl("lbl_subjectview");
                if (lbl_subject.Text == "0")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else if (lbl_subject.Text == "")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else
                {
                    lbl_subjectname_view.Text = lbl_subject.Text;
                }
            }
        }
        protected void btn_view_test_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_msg2.Text = "";
                HttpBrowserCapabilities objBrwInfo = Request.Browser;
                string browser_name = objBrwInfo.Browser;
                string browser_version = objBrwInfo.Version;
                if (browser_name == "Chrome" || browser_name == "Firefox" || browser_name == "Safari")
                {
                    Button btn = (Button)sender;

                    RepeaterItem row = (RepeaterItem)btn.NamingContainer;
                    Label lbl_Test_id = (Label)row.FindControl("lbl_testid");
                    Label lbl_exam_code = (Label)row.FindControl("lbl_Exam_Id");
                    Label lbl_Sub_id = (Label)row.FindControl("lbl_Sub_id");
                    Label lbl_Section = (Label)row.FindControl("lbl_Section");

                    Label lbl_examendatetime = (Label)row.FindControl("lbl_examendatetime");
                    Label lbl_Duration = (Label)row.FindControl("lbl_Duration");

                    Label lbl_Exam_Start_Idate = (Label)row.FindControl("lbl_Exam_Start_Idate");
                    Label lbl_End_Exam_Idate = (Label)row.FindControl("lbl_End_Exam_Idate");
                    Label lbl_Exam_intendtime = (Label)row.FindControl("lbl_Exam_intendtime");
                    Label lbl_Exam_intstarttime = (Label)row.FindControl("lbl_Exam_intstarttime");


                    if (btn.Text == "Resume")
                    {
                        Session["Resume"] = "1";
                    }
                    if (lbl_Duration.Text != "0")
                    {
                        DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string Datetimesystem = dtm.ToString("dd/MM/yyyy hh:mm:ss tt");
                        if (Convert.ToInt32(dtm.ToString("yyyMMdd")) == Convert.ToInt32(lbl_End_Exam_Idate.Text))
                        {

                            string Datetimesystem1 = dtm.ToString("dd/MM/yyyy hh:mm:ss tt");
                            DateTime Datetimesystemitime = DateTime.ParseExact(Datetimesystem1, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                            Int64 Datetimesystemitime1 = Convert.ToInt64(Datetimesystemitime.ToString("yyyyMMddHHmmss"));

                            if (Convert.ToInt64(lbl_Exam_intendtime.Text) >= Datetimesystemitime1)
                            {

                                if (Convert.ToInt64(lbl_Exam_intstarttime.Text) <= Datetimesystemitime1)
                                {

                                    Response.Redirect("Test_Info.aspx?tstid=" + lbl_Test_id.Text + "&subid=" + lbl_Sub_id.Text + "&section=" + lbl_Section.Text, false);
                                }
                                else
                                {

                                    Alertme("You can only take the exam during the time allotted for this test.", "warning");

                                }
                            }
                            else
                            {
                                Alertme("You can only take the exam during the time allotted for this test.", "warning");


                                Alertme("You can only take the exam during the time allotted for this test.", "warning");

                            }

                        }
                        else
                        {



                            Alertme("You can only take the exam during the time allotted for this test.", "warning");




                        }
                    }
                    else
                    {

                        Alertme("Please wait for the test time declaration", "warning");

                    }
                }
                else
                {

                    Alertme("This browser is not supported. Please use only Chrome Browser", "warning");
                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Online Exam List Student");
            }
        }
        protected void btn_view_result_Click(object sender, EventArgs e)
        {
            try
            {
                Button img = (Button)sender;
                RepeaterItem row = (RepeaterItem)img.NamingContainer;
                Label lbl_testid = (Label)row.FindControl("lbl_testid");

                Label lbl_Sub_id = (Label)row.FindControl("lbl_Sub_id");
                Dictionary<string, object> dc1 = mycode.view_result_parameter2(lbl_testid.Text, ViewState["regid"].ToString());
                string Ip_address = (String)dc1["Ip_address"];
                string icreated_date = (String)dc1["icreated_date"];
                string Attempt_id = (String)dc1["Attempt_id"];
                if (Attempt_id == "0")
                {
                    Response.Redirect("OnlineMyResultweb.aspx", false);
                }
                else
                {
                    //Response.Redirect("webview/OnlineResultview.aspx?studentid=" + Uri.EscapeDataString(ViewState["regid"].ToString()) + "&testid=" + Uri.EscapeDataString(lbl_testid.Text) +"&idate=" + Uri.EscapeDataString(icreated_date) + "&Attemptid=" + Uri.EscapeDataString(Attempt_id)+"&mode=web", false);

                    Session["testid"] = lbl_testid.Text;
                    Session["Attemptid"] = Attempt_id;
                    Session["student"] = ViewState["regid"].ToString();
                    Session["mode"] = "web";
                    Response.Redirect("webview/OnlineResultview.aspx", false);
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Exam List");
            }
        }


    }
}