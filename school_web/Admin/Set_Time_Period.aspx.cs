using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Globalization;
using System.Data;
using System.IO;
namespace school_web.Admin
{
    public partial class Set_Time_Period : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Sessionid"] = My.get_session_id();
                        string pagename_current = "Set_Time_Period.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddlclasssearch, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclasssearch.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddl(ddl_class_time, "Select  Time_Minutes from Class_Routine order by Time_Minutes asc");
                        Bind_all_added_period();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
        private void Bind_all_added_period()
        {
            string query = "Select  crp.*,cm.Course_Name,format(crp.Start_Time, 'hh:mm:ss tt') as Start_Time_show,format(crp.End_time, 'hh:mm:ss tt') as End_time_show,format(crp.Start_Time, 'dd/MM/yyyy hh:mm:ss tt') as Start_Time1,format(crp.End_time, 'dd/MM/yyyy hh:mm:ss tt') as End_time1 from Class_Routine_period crp join Add_course_table cm on crp.course_id=cm.course_id  where crp.Session_id='" + ViewState["Sessionid"].ToString() + "' and crp.course_id='" + ddlclasssearch.SelectedValue + "' order by crp.Period asc  ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                btn_excels.Visible = false;
                print1.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();

                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                lbl_class22.Text = ddlclasssearch.SelectedItem.Text;
                btn_excels.Visible = true;

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }


                rd_view.DataSource = dt;
                rd_view.DataBind();

                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else if (ddl_Period.SelectedItem.Text == "Select")
                {
                    Alertme("Please select period name", "warning");
                }
                else if (ddl_class_time.Text == "Select")
                {
                    Alertme("Please select class time period", "warning");
                }
                else
                {
                    string mergeStartTime = mycode.date() + " " + ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;
                    // string mergeStartTime = date + " " + time_StartTime.Date.ToString("hh:mm:ss tt");
                    // DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    string date = mycode.date();
                    //  string mergeStartTime = date + " " + time_StartTime.Date.ToString("hh:mm:ss tt");
                    DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    DateTime x30MinsLater = startTime.AddMinutes(Convert.ToInt32(ddl_class_time.Text));
                    txt_end_time.Text = x30MinsLater.ToString("hh:mm:ss tt");




                    if (btn_Submit.Text == "Add")
                    {

                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            if (ddl_class.SelectedItem.Text == "ALL")
                            {
                                DataTable dtc = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
                                foreach (DataRow dr in dtc.Rows)
                                {
                                    string query = "Select *  from Class_Routine_period where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Period=" + ddl_Period.SelectedValue + " and course_id=" + dr["course_id"].ToString() + "";

                                    DataTable dt = mycode.FillData(query);
                                    if (dt.Rows.Count == 0)
                                    {
                                        SqlCommand cmd;
                                        query = "INSERT INTO Class_Routine_period (Period,Session_id,Period_Name,Branch_id,Date,Timespan,Userid,course_id,Start_Time,End_time) values (@Period,@Session_id,@Period_Name,@Branch_id,@Date,@Timespan,@Userid,@course_id,@Start_Time,@End_time)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@Period", ddl_Period.SelectedValue);
                                        cmd.Parameters.AddWithValue("@Start_Time", startTime.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                                        cmd.Parameters.AddWithValue("@End_time", x30MinsLater.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                                        cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                        cmd.Parameters.AddWithValue("@Period_Name", ddl_Period.SelectedItem.Text);
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                        cmd.Parameters.AddWithValue("@Date", My.getdate1());
                                        cmd.Parameters.AddWithValue("@Timespan", ddl_class_time.Text);
                                        cmd.Parameters.AddWithValue("@Userid", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@course_id", dr["course_id"].ToString());
                                        //Start_Time,End_time,
                                        //@Start_Time,
                                        //@End_time,
                                        if (InsertUpdate.InsertUpdateData(cmd))
                                        {
                                            Alertme("Time period has been saved successfully", "success");
                                        }
                                    }
                                    else
                                    {
                                        Alertme("Sorry you can't add this period because already added ", "warning");
                                    }
                                }
                                empty_data();
                            }
                            else
                            {
                                string query = "Select *  from Class_Routine_period where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Period=" + ddl_Period.SelectedValue + " and course_id=" + ddl_class.SelectedValue + "";

                                DataTable dt = mycode.FillData(query);
                                if (dt.Rows.Count == 0)
                                {
                                    SqlCommand cmd;
                                    query = "INSERT INTO Class_Routine_period (Period,Session_id,Period_Name,Branch_id,Date,Timespan,Userid,course_id,Start_Time,End_time) values (@Period,@Session_id,@Period_Name,@Branch_id,@Date,@Timespan,@Userid,@course_id,@Start_Time,@End_time)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Period", ddl_Period.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Start_Time", startTime.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                                    cmd.Parameters.AddWithValue("@End_time", x30MinsLater.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                    cmd.Parameters.AddWithValue("@Period_Name", ddl_Period.SelectedItem.Text);
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                    cmd.Parameters.AddWithValue("@Date", My.getdate1());
                                    cmd.Parameters.AddWithValue("@Timespan", ddl_class_time.Text);
                                    cmd.Parameters.AddWithValue("@Userid", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@course_id", ddl_class.SelectedValue);
                                    //Start_Time,End_time,
                                    //@Start_Time,
                                    //@End_time,
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                        Alertme("Time period has been saved successfully", "success");
                                        empty_data();
                                    }
                                }
                                else
                                {
                                    Alertme("Sorry you can't add this period because already added ", "warning");
                                }
                            }
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }

                    else
                    {
                        if (ViewState["Is_Edit"].ToString() == "1")
                        {
                            string query = "Select *  from Class_Routine_period where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Period=" + ddl_Period.SelectedValue + " and course_id=" + ddl_class.SelectedValue + " and Id!=" + HdID.Value + "";

                            DataTable dt = mycode.FillData(query);
                            if (dt.Rows.Count == 0)
                            {
                                SqlCommand cmd;
                                query = "Update Class_Routine_period set Period=@Period,Start_Time=@Start_Time,End_time=@End_time,Session_id=@Session_id,Period_Name=@Period_Name,Branch_id=@Branch_id,updateddate=@updateddate,Userid=@Userid,Timespan=@Timespan,course_id=@course_id where Id = @Id";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Period", ddl_Period.SelectedValue);
                                cmd.Parameters.AddWithValue("@Start_Time", startTime.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                                cmd.Parameters.AddWithValue("@End_time", x30MinsLater.ToString("dd/MMM/yyyy hh:mm:ss tt"));
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                cmd.Parameters.AddWithValue("@Period_Name", ddl_Period.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@updateddate", My.getdate1());
                                cmd.Parameters.AddWithValue("@Timespan", ddl_class_time.Text);
                                cmd.Parameters.AddWithValue("@Userid", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Id", HdID.Value);
                                cmd.Parameters.AddWithValue("@course_id", ddl_class.SelectedValue);
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                    Alertme("Routine has been saved successfully", "success");
                                    empty_data();
                                }
                            }
                            else
                            {
                                Alertme("Sorry you can't add this period because already added ", "warning");
                            }
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void empty_data()
        {
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_end_time.Text = "0";

            ddl_Period.SelectedValue = "0";
            string mergeStartTime = mycode.date() + " " + ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;

            DateTime startTime = DateTime.ParseExact(mergeStartTime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


            DateTime x30MinsLater = startTime.AddMinutes(Convert.ToInt32(ddl_class_time.Text) + 0);

            string endtime = x30MinsLater.ToString("dd/MM/yyyy hh:mm:ss tt");


            DateTime startTime2 = DateTime.ParseExact(endtime, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            string am1 = startTime2.ToString("tt");
            string hh = startTime2.ToString("hh");
            string mm = startTime2.ToString("mm");
            string ss = startTime2.ToString("ss");

            ddl_hours.Text = am1;
            ddl_minutes.Text = mm;
            ddl_am_pm.Text = am1;





            //txt_end_time.Text = x30MinsLater.ToString("hh:mm:ss tt");
            ddlclasssearch.SelectedValue = ddl_class.SelectedValue;
            ddl_class_time.Text = "Select";
            ddl_class.Enabled = true;
            ddl_Period.Enabled = true;
            Bind_all_added_period();
        }

        protected void ddl_class_time_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Period.Text == "Select")
            {
                Alertme("Please select class time", "warning");
            }
            else
            {



                string date = mycode.date();

                string customdate = date + " " + ddl_hours.Text + ":" + ddl_minutes.Text + ":00" + " " + ddl_am_pm.Text;

                // string mergeStartTime = date + " " + time_StartTime.Date.ToString("hh:mm:ss tt");


                DateTime startTime = DateTime.ParseExact(customdate, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime x30MinsLater = startTime.AddMinutes(Convert.ToInt32(ddl_class_time.Text));
                txt_end_time.Text = x30MinsLater.ToString("hh:mm:ss tt");


            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Set_Time_Period.aspx", false);

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label Id = (Label)row.FindControl("lbl_Id");
                HdID.Value = Id.Text;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Period = (Label)row.FindControl("lbl_Period");
                Label lbl_starttime1 = (Label)row.FindControl("lbl_starttime1");
                Label lbl_endtime = (Label)row.FindControl("lbl_endtime");
                Label lbl_Timespan = (Label)row.FindControl("lbl_Timespan");
                Label lbl_course_id = (Label)row.FindControl("lbl_course_id");

                 
                ddl_class.SelectedValue = lbl_course_id.Text;
                fetch_periods();
                ddl_Period.SelectedValue = lbl_Period.Text;
                get_period_type();
                ddl_class_time.Text = lbl_Timespan.Text;
                SqlCommand cmd = new SqlCommand("Select *,format(Start_Time, 'hh:mm') as Start_Time1,format(Start_Time, 'tt') as ampm from Class_Routine_period where Id=" + HdID.Value + "  ");
                DataTable dt = mycode.GetData(cmd);
                if (dt.Rows.Count > 0)
                {
                    string ampm = dt.Rows[0]["ampm"].ToString();
                    string time = dt.Rows[0]["Start_Time1"].ToString();
                    string[] stringSeparators = new string[] { ":" };
                    string[] arr = time.Split(stringSeparators, StringSplitOptions.None);
                    string hours = arr[0];
                    string minutes = arr[1];
                    ddl_hours.Text = hours;
                    ddl_minutes.Text = minutes;
                    ddl_am_pm.Text = ampm;
                }


                DateTime startTime = DateTime.ParseExact(lbl_starttime1.Text, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime x30MinsLater = startTime.AddMinutes(Convert.ToInt32(ddl_class_time.Text));
                txt_end_time.Text = x30MinsLater.ToString("hh:mm:ss tt");



                ddlclasssearch.SelectedValue = lbl_course_id.Text;

                ddl_class.Enabled = false;
                ddl_Period.Enabled = false; 
                ddl_class.CssClass = "form-select find-dv-txtbx";
                ddl_Period.CssClass = "form-select find-dv-txtbx";

                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");


            }



        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label Id = (Label)row.FindControl("lbl_Id");
                    mycode.executequery("delete from Class_Routine_period where Id=" + Id.Text + "");
                    Bind_all_added_period();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        protected void btn_fnd_by_class_Click(object sender, EventArgs e)
        {
            if (ddlclasssearch.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                Bind_all_added_period();
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_periods();
            }
            catch (Exception ex)
            {

            }
        }

        private void fetch_periods()
        {
            mycode.bind_all_ddl_with_id(ddl_Period, "Select Period_Name,Period from Class_Routine_period_Master where Class_id=" + ddl_class.SelectedValue + " and Session_id=" + ViewState["Sessionid"].ToString() + " order by Period_no asc");
        }

        protected void ddl_Period_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_period_type();
        }

        private void get_period_type()
        {
            DataTable dt = mycode.FillData("select Period_type from Class_Routine_period_Master where Class_id=" + ddl_class.SelectedValue + " and Period='" + ddl_Period.SelectedValue + "' ");
            if (dt.Rows.Count > 0)
            {
                txt_period_type.Text = dt.Rows[0]["Period_type"].ToString();
            }
        }
    }
}