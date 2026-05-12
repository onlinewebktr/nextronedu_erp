using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.InstructorProfile
{
    public partial class Home : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Session["teacher"] = "82";
                if (Session["teacher"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");

                }
                else
                {
                    if (!IsPostBack)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["teacher"] = Session["teacher"].ToString();
                        hdStartDate.Value = code.iMonthBackdate(); hdEndDate.Value = code.idate();
                        BindCount();
                        fetch_firm_details();

                        //BindChart();
                        // sync and push send  
                        //if (!code.syncdatastatusyes_or_no())
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowProgress();", true);
                        //    string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "load", script, true);
                        //    // ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                        //}
                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void fetch_firm_details()
        {
            try
            { 
                string query = "Select * from Firm_Details";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count > 0)
                {
                    try
                    { 
                        if (dt.Rows[0]["Is_internal_chat_active"].ToString() == "1")
                        {
                            chatDV.Visible = true;
                            chatLink.HRef = "../Chat/home?regid=" + Session["teacher"].ToString();
                        }
                    }
                    catch (Exception ex) 
                    { 
                    }
                } 
            }
            catch
            { 
            }
        }

        private void BindCount()
        {
            try
            {
                lbl_Teachers.Text = "1";
                lbl_Students.Text = code.Find_Name("select count(id) from dbo.[admission_registor] where   Transfer_Status in ('New','NT') and  StudentStatus='AV' and  Session_id=" + ViewState["sesssionid"].ToString() + "  and Status='1' ");
                lbl_Licence.Text = "0";

                lblenroll.Text = code.Find_Name("select count(ip.user_id) from user_details ip join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id where zvcs.Teacher_Id='" + ViewState["teacher"].ToString() + "' ");
            }
            catch { }
        }

        private void BindChart()
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();
            StringBuilder strScriptOnline = new StringBuilder();
            StringBuilder strScriptTeacher = new StringBuilder();
            StringBuilder strScriptStudents = new StringBuilder();

            try
            {
                SqlCommand cmd = new SqlCommand("Chart_Teacher");
                cmd.Parameters.AddWithValue("@StartDate", hdStartDate.Value);
                cmd.Parameters.AddWithValue("@EndDate", hdEndDate.Value);
                cmd.Parameters.AddWithValue("@User_id", ViewState["teacher"].ToString());
                dsChartData = code.GetDatastore(cmd);

                strScript.Append(@" <script type='text/javascript'>  
                            google.load('visualization', '1', {packages: ['corechart']});</script>  
                        <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Online Classes', 'Students', 'Teacher'],");

                strScriptOnline.Append(@"  
                    <script type='text/javascript'>  
                    function drawOnline() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Online Classes'],");

                strScriptTeacher.Append(@"  
                    <script type='text/javascript'>  
                    function drawTeacher() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Teacher'],");

                strScriptStudents.Append(@" 
                    <script type='text/javascript'>  
                    function drawStudents() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Date', 'Students'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["Date"] + "'," + row["OnlineClasses"] + "," + row["TotSTudent"] + "," + row["TotTeacher"] + "],");

                    strScriptOnline.Append("['" + row["Date"] + "'," + row["OnlineClasses"] + "],");

                    strScriptTeacher.Append("['" + row["Date"] + "'," + row["TotTeacher"] + "],");

                    strScriptStudents.Append("['" + row["Date"] + "'," + row["TotSTudent"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScriptOnline.Remove(strScriptOnline.Length - 1, 1);
                strScriptOnline.Append("]);");

                strScriptTeacher.Remove(strScriptTeacher.Length - 1, 1);
                strScriptTeacher.Append("]);");

                strScriptStudents.Remove(strScriptStudents.Length - 1, 1);
                strScriptStudents.Append("]);");

                strScript.Append("var options = { width:1000, height:300,colors: ['#0ba360','#46aef7','#1e3c72'], vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'line', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");


                strScriptOnline.Append("var options = { width:1000, height:300,colors: ['#0ba360'],vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {1: {type: 'area'}} };");
                strScriptOnline.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chartOnline'));  chart.draw(data, options); } google.setOnLoadCallback(drawOnline);");
                strScriptOnline.Append(" </script>");

                strScriptTeacher.Append("var options = { width:1000, height:300,colors: ['#1e3c72'],vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {1: {type: 'area'}} };");
                strScriptTeacher.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chartTeachers'));  chart.draw(data, options); } google.setOnLoadCallback(drawTeacher);");
                strScriptTeacher.Append(" </script>");

                strScriptStudents.Append("var optionsStudents = { width:1000, height:300,colors: ['#46aef7'],vAxis: {title: 'Reports'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {1: {type: 'area'}} };");
                strScriptStudents.Append(" var chartStudent = new google.visualization.ComboChart(document.getElementById('chartStudents'));  chartStudent.draw(data, optionsStudents); } google.setOnLoadCallback(drawStudents);");
                strScriptStudents.Append(" </script>");

                ltScripts.Text = strScript.ToString();
                ltScriptsOnline.Text = strScriptOnline.ToString();
                ltScriptsTeacher.Text = strScriptTeacher.ToString();
                ltScriptsStudents.Text = strScriptStudents.ToString();
            }
            catch
            {
            }
            finally
            {
                dsChartData.Dispose();
                strScript.Clear();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LtTime.Visible = true; lblDateText.Visible = false;
            BindChart(); LtTime.Text = lblDateText.Text = hdIsactive.Value;
        }


    }
}