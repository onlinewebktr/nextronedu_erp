using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class hostel_attendance_report : System.Web.UI.Page
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
                        ViewState["Is_roll_no_class_attendance"] = My.get_Is_roll_no_class_attendance();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_house, "select house_name,house_id from house_master order by house_name asc");
                        txt_date.Text = mycode.date();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "hostel-attendance-report.aspx");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddl_house.SelectedItem.Text == "Select")
                {
                    Alertme("Please select house", "warning");
                    ddl_house.Focus();
                }
                else if (txt_date.Text == "")
                {
                    Alertme("Please enter date", "warning");
                    txt_date.Focus();
                }
                else
                {
                    find_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_data()
        {
            string query = "Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_attendance_saved_hostel_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no and ar.Session_id=sas.Session_id where ar.session='" + ddlsession.SelectedItem.Text + "' and sas.House_id='" + ddl_house.SelectedValue + "' and sas.Attendance_IDate='" + My.DateConvertToIdate(txt_date.Text) + "' order by ar.rollnumber";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbll_section_and_class.Text = "";
                Alertme("Sorry there are no any data found", "warning");
                imgexcel2.Visible = false;
                print1.Visible = false;
                GrdView.DataSource = null;
                GrdView.DataBind();
                pnl_grids.Visible = false;
                lbltotal_student.Text = "0";
                lbl_persenstudent.Text = "0";
                lbl_totalabsentstudent.Text = "0";
                lbl_leave_student.Text = "0";
            }
            else
            {
                lbll_section_and_class.Text = " - House : " + ddl_house.SelectedItem.Text + ", Date : " + txt_date.Text;
                imgexcel2.Visible = true;
                lbltotal_student.Text = dt.Rows.Count.ToString();
                GrdView.DataSource = dt;
                GrdView.DataBind();
                pnl_grids.Visible = true;
                bind_student_count();
            }
        }

        private void bind_student_count()
        {
            string query = "Select sas.Attendance_Status,count(sas.Id) as total from admission_registor ar join Student_attendance_saved_hostel_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no and ar.Session_id=sas.Session_id where sas.Session_id='" + ddlsession.SelectedValue + "' and sas.House_id='" + ddl_house.SelectedValue + "' and sas.Attendance_IDate='" + My.DateConvertToIdate(txt_date.Text) + "' group by Attendance_Status";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        lbl_persenstudent.Text = dr["total"].ToString();
                    }
                    else if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        lbl_totalabsentstudent.Text = dr["total"].ToString();
                    }
                    else
                    {
                        lbl_leave_student.Text = dr["total"].ToString();
                    }
                }
            }
        }

        protected void imgexcel2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=HostelAttendanceExport" + "_" + ".xls");
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Attendance_Status = (Label)e.Row.FindControl("lbl_Attendance_Status");
                if (lbl_Attendance_Status.Text == "Present")
                {
                    lbl_Attendance_Status.Style.Add("background", "#009f25");
                    lbl_Attendance_Status.Style.Add("padding", "3px 5px 3px 5px");
                    lbl_Attendance_Status.Style.Add("color", "#000");
                    lbl_Attendance_Status.Style.Add("width", "69px");
                    lbl_Attendance_Status.Style.Add("float", "left");
                    lbl_Attendance_Status.Style.Add("text-align", "center");
                }
                else if (lbl_Attendance_Status.Text == "Absent")
                {
                    lbl_Attendance_Status.Style.Add("background", "#f00");
                    lbl_Attendance_Status.Style.Add("padding", "3px 5px 3px 5px");
                    lbl_Attendance_Status.Style.Add("color", "#000");
                    lbl_Attendance_Status.Style.Add("width", "69px");
                    lbl_Attendance_Status.Style.Add("float", "left");
                    lbl_Attendance_Status.Style.Add("text-align", "center");
                }
                else
                {
                    lbl_Attendance_Status.Style.Add("background", "#ff6a00");
                    lbl_Attendance_Status.Style.Add("padding", "3px 5px 3px 5px");
                    lbl_Attendance_Status.Style.Add("color", "#000");
                    lbl_Attendance_Status.Style.Add("width", "69px");
                    lbl_Attendance_Status.Style.Add("float", "left");
                    lbl_Attendance_Status.Style.Add("text-align", "center");
                }
            }
        }
    }
}