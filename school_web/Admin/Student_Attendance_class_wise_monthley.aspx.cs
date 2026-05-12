using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
namespace school_web.Admin
{
    public partial class Student_Attendance_class_wise_monthley : System.Web.UI.Page
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

                        string pagename_current = "Student_Attendance_class_wise_monthley.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["sessionyearid"] = My.get_session_id();
                        mycode.bind_ddl_year(ddlyear);
                        ddlyear.Text = mycode.year();
                        mycode.bind_all_ddl_with_id(ddlclass, " Select Course_Name, course_id from Add_course_table order by Position ");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        My.bind_ddl_all(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ViewState["sessionyearid"].ToString() + "' order by Section");

                        ddl_section.Text = My.get_top_one_section();

                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Month_Id asc");
                        ddl_month.SelectedValue = mycode.get_current_month_id();
                      


                        find_firm_details();
                        find_data();
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

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            { 
                My.bind_ddl_select(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddlclass.SelectedValue + "' and Session_id='"+ ViewState["sessionyearid"].ToString()+ "' order by Section");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlyear.Text == "Select")
                {
                    Alertme("Please select year.", "warning");
                    ddlyear.Focus();
                }
                else if (ddl_month.Text == "Select")
                {
                    Alertme("Please select month.", "warning");
                    ddlyear.Focus();
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
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where   Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by rollnumber asc ";

                }
                else if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text != "All")
                {
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where  Section='" + ddl_section.Text + "' and Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by rollnumber asc ";

                }
                else if (ddlclass.SelectedItem.Text != "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "'  and Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by rollnumber asc ";

                }
                else
                {
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by rollnumber asc ";
                }



            }
            else
            {
                if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where  Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by studentname asc ";
                }
                else if (ddlclass.SelectedItem.Text == "All" && ddl_section.SelectedItem.Text != "All")
                {
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where  Section='" + ddl_section.Text + "' and Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by studentname asc ";
                }
                else if (ddlclass.SelectedItem.Text != "All" && ddl_section.SelectedItem.Text == "All")
                {
                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "'  and Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by studentname asc ";
                }
                else
                {

                    query = "Select studentname,admissionserialnumber,rollnumber,Class_id,Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_Id=" + ViewState["sessionyearid"].ToString() + " and status='1' order by studentname asc ";
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



                GrdView.DataSource = dt;
                GrdView.DataBind();
                pnl_grids.Visible = true;





            }


        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_reg_id = (Label)e.Row.FindControl("lbl_reg_id");
                Button btn_select = (Button)e.Row.FindControl("btn_select");

                Label lbl_Attendance_persent = (Label)e.Row.FindControl("lbl_Attendance_persent");
                Label lbl_Attendance_leave = (Label)e.Row.FindControl("lbl_Attendance_leave");
                Label lbl_Attendance_absent = (Label)e.Row.FindControl("lbl_Attendance_absent");
                Label lbl_class_id = (Label)e.Row.FindControl("lbl_class_id");
                Label lbl_section = (Label)e.Row.FindControl("lbl_section");
                
                Bind_student_data(lbl_reg_id.Text, lbl_Attendance_persent, lbl_Attendance_leave, lbl_Attendance_absent, btn_select, lbl_class_id.Text, lbl_section.Text);



            }

        }

        private void Bind_student_data(string admissionno, Label lbl_Attendance_persent, Label lbl_Attendance_leave, Label lbl_Attendance_absent, Button btn_select,string class_id,string section)
        {
            string date = My.getMonthS_twoDigit(ddl_month.SelectedValue) + "/" + ddlyear.Text;
            string query = "Select Attendance_Status,count(Id) as total from Student_Attendance_saved_Class_Wise where Class_id='" + class_id + "' and Section='" + section + "' and Session_Id=" + ViewState["sessionyearid"].ToString() + " and Admission_no='" + admissionno + "' and Attendance_Date like '%" + date + "%' group by Attendance_Status";

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_select.Visible = false;
                lbl_Attendance_persent.Text = "0";
                lbl_Attendance_leave.Text = "0";
                lbl_Attendance_absent.Text = "0";

                lbl_Attendance_persent.Style.Add("background", "#009f25");
                lbl_Attendance_persent.Style.Add("padding", "3px 5px 3px 5px");
                lbl_Attendance_persent.Style.Add("color", "#000");
                lbl_Attendance_persent.Style.Add("width", "69px");
                lbl_Attendance_persent.Style.Add("float", "left");
                lbl_Attendance_persent.Style.Add("text-align", "center");
                //-----------------------------------------------------
                lbl_Attendance_absent.Style.Add("background", "#f00");
                lbl_Attendance_absent.Style.Add("padding", "3px 5px 3px 5px");
                lbl_Attendance_absent.Style.Add("color", "#000");
                lbl_Attendance_absent.Style.Add("width", "69px");
                lbl_Attendance_absent.Style.Add("float", "left");
                lbl_Attendance_absent.Style.Add("text-align", "center");


                lbl_Attendance_leave.Style.Add("background", "#ff6a00");
                lbl_Attendance_leave.Style.Add("padding", "3px 5px 3px 5px");
                lbl_Attendance_leave.Style.Add("color", "#000");
                lbl_Attendance_leave.Style.Add("width", "69px");
                lbl_Attendance_leave.Style.Add("float", "left");
                lbl_Attendance_leave.Style.Add("text-align", "center");
            }
            else
            {
                btn_select.Visible = true;
                lbl_Attendance_persent.Text = "0";
                lbl_Attendance_absent.Text = "0";
                lbl_Attendance_leave.Text = "0";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Attendance_Status"].ToString() == "Present")
                    {
                        lbl_Attendance_persent.Text = dr["total"].ToString();
                    }
                    else if (dr["Attendance_Status"].ToString() == "Absent")
                    {
                        lbl_Attendance_absent.Text = dr["total"].ToString();
                    }
                    else
                    {
                        lbl_Attendance_leave.Text = dr["total"].ToString();
                    }
                }


                lbl_Attendance_persent.Style.Add("background", "#009f25");
                lbl_Attendance_persent.Style.Add("padding", "3px 5px 3px 5px");
                lbl_Attendance_persent.Style.Add("color", "#000");
                lbl_Attendance_persent.Style.Add("width", "69px");
                lbl_Attendance_persent.Style.Add("float", "left");
                lbl_Attendance_persent.Style.Add("text-align", "center");
                //-----------------------------------------------------
                lbl_Attendance_absent.Style.Add("background", "#f00");
                lbl_Attendance_absent.Style.Add("padding", "3px 5px 3px 5px");
                lbl_Attendance_absent.Style.Add("color", "#000");
                lbl_Attendance_absent.Style.Add("width", "69px");
                lbl_Attendance_absent.Style.Add("float", "left");
                lbl_Attendance_absent.Style.Add("text-align", "center");


                lbl_Attendance_leave.Style.Add("background", "#ff6a00");
                lbl_Attendance_leave.Style.Add("padding", "3px 5px 3px 5px");
                lbl_Attendance_leave.Style.Add("color", "#000");
                lbl_Attendance_leave.Style.Add("width", "69px");
                lbl_Attendance_leave.Style.Add("float", "left");
                lbl_Attendance_leave.Style.Add("text-align", "center");




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



        protected void btn_select_Click(object sender, EventArgs e)
        {
            try
            {
                string date = My.getMonthS_twoDigit(ddl_month.SelectedValue) + "/" + ddlyear.Text;
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_reg_id = (Label)row.FindControl("lbl_reg_id");
                Response.Redirect("Students_individual_Class_wise_attendance.aspx?admissionno=" + lbl_reg_id.Text + "&month=" + date, false);
            }
            catch
            {
            }
        }

    }
}