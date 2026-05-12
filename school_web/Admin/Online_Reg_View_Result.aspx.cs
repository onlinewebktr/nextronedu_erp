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
    public partial class Online_Reg_View_Result : System.Web.UI.Page
    {
        My mycode = new My();
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
                    string pagename_current = "Online_Reg_View_Result.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];

                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");
                    ddl_session.SelectedValue = My.get_session_id_onlinereg();
                    ViewState["sessionid"] = ddl_session.SelectedValue;
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    ddl_class.SelectedValue = My.get_top_one_class();

                    fetch_exam();

                    ddl_test_name.SelectedValue = My.get_top_one_test_name(ddl_session.SelectedValue);
                    Bind_data();
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


        protected void btn_find_Click(object sender, EventArgs e)
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
                Alertme("Please select testname", "warning");
            }
            else
            {
                Bind_data();
            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Online_Reg_View_Result.aspx", false);
        }

        private void Bind_data()
        {
            string query = "";
            if (ddl_status.SelectedItem.Text.ToUpper() == "ALL")
            {
                query = "Select oa.*,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Attendance_Status,ore.Exam_Result,ore.Admission_date,ore.Admission_time from Online_Admission oa join Online_Reg_Exam_Result ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id where oa.Class_id=" + ddl_class.SelectedValue + " and oa.Session_id=" + ddl_session.SelectedValue + " and oa.Payment_Status='Paid' and oa.Test_id='" + ddl_test_name.SelectedValue + "' order by oa.Name ";
            }
            else
            {
                query = "Select oa.*,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Attendance_Status,ore.Exam_Result,ore.Admission_date,ore.Admission_time from Online_Admission oa join Online_Reg_Exam_Result ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id where oa.Class_id=" + ddl_class.SelectedValue + " and oa.Session_id=" + ddl_session.SelectedValue + " and oa.Payment_Status='Paid' and oa.Test_id='" + ddl_test_name.SelectedValue + "' and ore.Exam_Result='" + ddl_status.SelectedItem.Text + "' order by oa.Name ";
            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are not data exists", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
                btn_print_all.Visible = false;
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
                btn_print_all.Visible = true;
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
                    mycode.executequery("delete from Online_Reg_Exam_Result where  Admission_no=" + lbl_Registration_id.Text + "");
                    Bind_data();
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

        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    if (ddl_session.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session.", "warning");
                        ddl_session.Focus();
                    }
                    else if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class.", "warning");
                        ddl_class.Focus();
                    }
                    else
                    {
                        Response.Redirect("slip/print-result.aspx?session_Id=" + ddl_session.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&admin=0&type=In", false);
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                GrdView.Columns[8].Visible = false;
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
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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