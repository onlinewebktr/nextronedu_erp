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
    public partial class Student_Attendance_class_wise : System.Web.UI.Page
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

                        string pagename_current = "Student_Attendance_class_wise.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All(ddlclass, " Select Course_Name, course_id from Add_course_table order by Position ");
                        ddlclass.SelectedValue = My.get_top_one_class();

                        My.bind_ddl_all(ddl_section, "Select distinct Section from admission_registor where Class_id='"+ ddlclass.SelectedValue + "' and Session_id='"+ ddlsession.SelectedValue + "' order by Section");

                        ddl_section.Text = My.get_top_one_section();

                        txt_date.Text = mycode.date();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Attendance_class_wise");
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
            if (dt.Rows.Count == 0)
            {




            }
            else
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
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select course", "warning");
                    ddlclass.Focus();
                }

                else if (ddl_section.Text == "Select")
                {
                    Alertme("Please select section", "warning");
                    ddlclass.Focus();
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
            string query = "";

            if (ViewState["Is_roll_no_class_attendance"].ToString() == "1")// roll no wise 
            {
                if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where  ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.rollnumber";

                }
                else if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text != "All")
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where     ar.section='" + ddl_section.Text + "' and ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.rollnumber";
                }
                else if (ddlclass.SelectedItem.Text != "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddlclass.SelectedValue + "' and ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.rollnumber";
                }
                else
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddlclass.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.rollnumber";

                }

            }
            else
            {
                if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where  ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    ar.studentname";

                }
                else if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text != "All")
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where     ar.section='" + ddl_section.Text + "' and ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "' ar.studentname";
                }
                else if (ddlclass.SelectedItem.Text != "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddlclass.SelectedValue + "' and ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.studentname";
                }
                else
                {
                    query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class as classname,ar.studentname,sas.Class_period,sas.Day,sas.Attendance_Date,sas.Attendance_Status,sas.Section,sas.time from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and ar.Session_id=sas.Session_id   where ar.Class_id='" + ddlclass.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddlsession.SelectedItem.Text + "' and  sas.Attendance_Date='" + txt_date.Text + "'    order by ar.studentname";

                }


            }



            SqlCommand cmd = new SqlCommand(query);
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
                lbll_section_and_class.Text = "Class:" + ddlclass.SelectedItem.Text + " Section: " + ddl_section.SelectedItem.Text;

                imgexcel2.Visible = true;

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;

                }
                else
                {
                    print1.Visible = false;
                }


                lbltotal_student.Text = dt.Rows.Count.ToString();
                GrdView.DataSource = dt;
                GrdView.DataBind();
                pnl_grids.Visible = true;

                bind_student_count();



            }
        }

        private void bind_student_count()
        {
            string query = "";
            if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text == "All")
            {

                query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where   sas.Session_id='" + ddlsession.SelectedValue + "' and   sas.Attendance_Date='" + txt_date.Text + "'  group by Attendance_Status";

            }
            else if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text != "All")
            {
                query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where  ar.section='" + ddl_section.Text + "' and sas.Session_id='" + ddlsession.SelectedValue + "' and   sas.Attendance_Date='" + txt_date.Text + "'  group by Attendance_Status";
            }
            else if (ddlclass.SelectedItem.Text != "All" && ddl_section.SelectedItem.Text == "All")
            {
                query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where ar.Class_id='" + ddlclass.SelectedValue + "'  and sas.Session_id='" + ddlsession.SelectedValue + "' and   sas.Attendance_Date='" + txt_date.Text + "'  group by Attendance_Status";
            }
            else
            {
                query = " Select   sas.Attendance_Status,count(sas.Id) as total  from admission_registor ar join Student_Attendance_saved_Class_Wise sas on ar.Class_id=sas.Class_id and ar.Section=sas.Section and ar.admissionserialnumber=sas.Admission_no  and  ar.Session_id=sas.Session_id  where ar.Class_id='" + ddlclass.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and sas.Session_id='" + ddlsession.SelectedValue + "' and   sas.Attendance_Date='" + txt_date.Text + "'  group by Attendance_Status";

            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
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
            if (ViewState["Is_Download"].ToString() == "1")
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=AttendanceExport" + "_" + ".xls");
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            { 
                My.bind_ddl_all(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddlclass.SelectedValue + "' and Session_id='" + ddlsession.SelectedValue + "' order by Section");
            }
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