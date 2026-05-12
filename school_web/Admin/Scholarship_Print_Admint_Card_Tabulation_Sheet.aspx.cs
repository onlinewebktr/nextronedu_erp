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
    public partial class Scholarship_Print_Admint_Card_Tabulation_Sheet : System.Web.UI.Page
    {
        My mycode = new My();

        string centername = "Select top 1 Centre_Name from Scholarship_Exam_Centre where Exam_Centre_Id=ore.Exam_Centre_Id and Test_id=ore.Test_id ";

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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Scholarship_Print_Admint_Card_Tabulation_Sheet.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["session_id"] = My.get_session_id_onlinereg();
                        mycode.bind_all_ddl_with_id(ddl_Scholarship_name, "select Test_name,Test_id from Scholarship_Program  order by  Test_name asc");

                        ddl_Scholarship_name.SelectedValue = My.get_top_one_Scholarship_name(My.get_session_id_onlinereg());

                        mycode.bind_all_ddl_with_id(ddl_center_name, "select distinct Centre_Name,Exam_Centre_Id from Scholarship_Exam_Centre where Test_id='" + ddl_Scholarship_name.SelectedValue + "' order by  Centre_Name asc");


                        mycode.bind_all_ddl_with_id(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where Test_id=" + ddl_Scholarship_name.SelectedValue + " order by ac.Position asc");


                        find_firm_details();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Course_Fee");
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
        protected void ddl_Scholarship_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_All_New(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where Test_id=" + ddl_Scholarship_name.SelectedValue + " order by ac.Position asc");
                
            }
        }

        protected void ddl_center_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Scholarship_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select scholorship name", "warning");
                }
                else if (ddl_center_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select exam centre", "warning");
                }
                else
                {
                    mycode.bind_ddl(ddl_room, "select distinct Room_no from Scholarship_Exam_Centre_room_no where Test_id='" + ddl_Scholarship_name.SelectedValue + "' and Exam_centre_id='" + ddl_center_name.SelectedValue + "' order by  Room_no asc");
                }

            }
            catch
            {

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship name", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship for", "warning");
            }
            else if (ddl_center_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam centre", "warning");
            }
            else if (ddl_room.Text == "Select")
            {
                Alertme("Please select room number", "warning");
            }
            else
            {
                try
                {

                    string query = "Select oa.*,'0'  as admissionnumber,ore.Roll_no,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Type,format(Exam_Date_time, 'hh:mm') as exm_time,format(Exam_Date_time, 'tt') as exm_ampm,format(Reporting_datetime, 'hh:mm') as rp_time,format(Reporting_datetime, 'tt') as rp_ampm,format(Gate_close_datetime, 'hh:mm') as gc_time,format(Gate_close_datetime, 'tt') as gc_ampm,format(Exam_end_date_time, 'hh:mm') as ed_time,format(Exam_end_date_time, 'tt') as ed_ampm,ore.Roll_start_from,ore.Exam_Type,ore.Exam_Date,ore.Remarks,ore.Exam_Shift,ore.Exam_end_time,ore.Reporting_time,Room_no,(" + centername + ") as Centre_Name,ore.Exam_Centre_Id from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where   oa.Test_id=" + ddl_Scholarship_name.SelectedValue + "  and ore.Exam_Centre_Id=" + ddl_center_name.SelectedValue + " and ore.Room_no='" + ddl_room.Text + "' and ore.Class_id="+ddl_class.SelectedValue+" order by ore.Roll_no ";

                    bind_grid_data(query);

                }
                catch
                {
                }
            }
        }

        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no admit card created", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                        Panel1.RenderControl(hw);
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
    }
}