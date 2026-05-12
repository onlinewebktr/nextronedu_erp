using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Print_Online_Reg_Admit_Card : System.Web.UI.Page
    {
        My mycode = new My();
        string classname = "select top 1 Course_Name from Add_course_table where course_id=Online_Reg_Exam_Time_Table.Class_id";
        string session = "select top 1 Session from session_details where session_id=Online_Reg_Exam_Time_Table.Session_Id";

        string testname = "Select top 1 Test_name from Online_reg_exam_test_master where Test_id=ore.Test_id";
        protected void Page_Load(object sender, EventArgs e)
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

                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                   
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    string pagename_current = "Print_Online_Reg_Admit_Card.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];


                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");
                    ddl_class.SelectedValue = My.get_top_one_class();
                    ddl_session.SelectedValue = My.get_session_id_onlinereg();

                    fetch_exam();
                   
                    ddl_test_name.SelectedValue = My.get_top_one_test_name(ddl_session.SelectedValue);

                    Bind_all_data_all();

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
        private void Bind_all_data_all()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select test name", "warning");
            }
            else
            {
                Bind_data();
            }
        }

        private void Bind_data()
        {
            string query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Type,(" + testname + ") as Test_name  from Online_Admission oa join Online_Reg_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Class_id=" + ddl_class.SelectedValue + " and oa.Session_id=" + ddl_session.SelectedValue + " and ore.Test_id="+ddl_test_name.SelectedValue+" and oa.Payment_Status='Paid' order by oa.Name ";

            Bind_finl_grid_data(query);


        }

        private void Bind_finl_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);

            if (dt.Rows.Count == 0)
            {

                grid_grade.DataSource = null;
                grid_grade.DataBind();

            }
            else
            {

                grid_grade.DataSource = dt;
                grid_grade.DataBind();



            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data();
        }

        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
            }
            else
            {
 

                string query = "Select oa.*,oa.Registration_id as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Type,(" + testname + ") as Test_name  from Online_Admission oa join Online_Reg_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Registration_id='" + txt_admission_no.Text + "'  and oa.Payment_Status='Paid' order by oa.Name ";


                Bind_finl_grid_data(query);



            }
        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_Registration_id = (Label)row.FindControl("lbl_Registration_id");
                    mycode.executequery("delete from Online_Reg_Exam_Time_Table where  Admission_no=" + lbl_Registration_id.Text + "");
                    Bind_finl_grid_data(ViewState["query"].ToString());
                    Alertme("Deletion process has been successfully done", "success");
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_Registration_id = (Label)row.FindControl("lbl_Registration_id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");

                    Response.Redirect("Online_Reg_Admit_Card_Create.aspx?Regid=" + lbl_Registration_id.Text + "&Sessionid=" + lbl_session_id.Text, false);
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

        protected void grid_grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Panel Panel1 = (Panel)e.Row.FindControl("Panel1");
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }

            }
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_exam();
            }
            catch (Exception ex)
            {
            }

        }

        private void fetch_exam()
        {
            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session.SelectedValue + "'   order by  Test_name asc");
        }
    }
}