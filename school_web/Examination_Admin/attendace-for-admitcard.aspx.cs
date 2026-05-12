using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class attendace_for_admitcard : System.Web.UI.Page
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
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    string pagename_current = "inventory-items.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];

                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    mycode.bind_all_ddl_with_id_cap_All(ddl_find_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    ddl_find_session.SelectedValue = ddlsession.SelectedValue;
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    

                    bind_grd_view();
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

        private void bind_grd_view()
        {
            string qry = "select t1.*,(select top 1 Session from session_details where session_id=t1.session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=t1.Class_id) as Class_name,(select top 1 Short_Name from Exam_Term_Details where Class_id=t1.Class_id and Session_id=t1.Session_id and Exam_Term_Id=t1.Term_id) as Term_name,(select top 1 Assessment_Name from Exam_Assessment_Details where Assessment_Id=t1.Exam_id and Class_id=t1.Class_id and Session_Id=t1.Session_id and Exam_Term_Id=t1.Term_id) as Exam_name from Exam_validation_for_admit_card_attendance t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddl_find_session.SelectedValue + "' order by t2.Position asc";
            if (ddl_find_session.SelectedItem.Text == "ALL")
            {
                qry = "select t1.*,(select top 1 Session from session_details where session_id=t1.session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=t1.Class_id) as Class_name,(select top 1 Short_Name from Exam_Term_Details where Class_id=t1.Class_id and Session_id=t1.Session_id and Exam_Term_Id=t1.Term_id) as Term_name,(select top 1 Assessment_Name from Exam_Assessment_Details where Assessment_Id=t1.Exam_id and Class_id=t1.Class_id and Session_Id=t1.Session_id and Exam_Term_Id=t1.Term_id) as Exam_name from Exam_validation_for_admit_card_attendance t1 join Add_course_table t2 on t1.Class_id=t2.course_id order by t2.Position asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else
            {
                bind_exam_term();
            }
        }

        private void bind_exam_term()
        {
            mycode.bind_all_ddl_with_id(ddl_exam_term, "Select Term_Name,Exam_Term_Id from Exam_Term_Details  where Class_id=" + ddl_class.SelectedValue + " and Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["branchid"].ToString() + "' order by Sequence_No asc");
        }

        protected void ddl_exam_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_exam_only();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_exam_only()
        {
            mycode.bind_all_ddl_with_id(ddl_exam, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Sequence_No asc");
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please select session.", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Please select class.", "warning");
                }
                else if (ddl_exam_term.SelectedItem.Text == "Select")
                {
                    ddl_exam_term.Focus();
                    Alertme("Please select exam term.", "warning");
                }
                else if (ddl_exam.SelectedItem.Text == "Select")
                {
                    ddl_exam.Focus();
                    Alertme("Please select exam.", "warning");
                }
                else if (txt_attendance_per_need.Text == "")
                {
                    txt_attendance_per_need.Focus();
                    Alertme("Please enter attendance (%) needed.", "warning");
                }
                else if (txt_from_date.Text == "")
                {
                    txt_from_date.Focus();
                    Alertme("Please enter attendance calculate from date.", "warning");
                }
                else if (txt_to_date.Text == "")
                {
                    txt_to_date.Focus();
                    Alertme("Please enter attendance calculate to date.", "warning");
                }
                else
                {
                    save_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_data()
        {

            DataTable dt = My.dataTable("select Id from Exam_validation_for_admit_card_attendance where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_exam_term.SelectedValue + "' and Exam_id='" + ddl_exam.SelectedValue + "'");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_validation_for_admit_card_attendance (Session_id,Class_id,Term_id,Exam_id,From_date,From_idate,To_date,To_idate,Status,Updated_by,Updated_date,Updated_idate,Attendance_percent) values (@Session_id,@Class_id,@Term_id,@Exam_id,@From_date,@From_idate,@To_date,@To_idate,@Status,@Updated_by,@Updated_date,@Updated_idate,@Attendance_percent)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Term_id", ddl_exam_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_id", ddl_exam.SelectedValue);
                cmd.Parameters.AddWithValue("@Attendance_percent", txt_attendance_per_need.Text);
                cmd.Parameters.AddWithValue("@From_date", txt_from_date.Text);
                cmd.Parameters.AddWithValue("@From_idate", My.DateConvertToIdate(txt_from_date.Text));
                cmd.Parameters.AddWithValue("@To_date", txt_to_date.Text);
                cmd.Parameters.AddWithValue("@To_idate", My.DateConvertToIdate(txt_to_date.Text));
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been added successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "update Validation_for_admit_report_card set Session_id=@Session_id,Class_id=@Class_id,Term_id=@Term_id,Exam_id=@Exam_id,Paid_month_id=@Paid_month_id,Month_name=@Month_name,Session_name=@Session_name,Updated_by=@Updated_by,Updated_date=@Updated_date where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Term_id", ddl_exam_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_id", ddl_exam.SelectedValue);
                
                cmd.Parameters.AddWithValue("@Session_name", ddlsession.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Type", "AdmitCard");
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been updated successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }
        private void empty_form()
        {
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");

                    Label lbl_term_id = (Label)row.FindControl("lbl_term_id");

                    Label lbl_exam_id = (Label)row.FindControl("lbl_exam_id_id");
                    Label lbl_month_id = (Label)row.FindControl("lbl_month_id");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;

                    ddlsession.SelectedValue = lbl_session_id.Text;
                    ddl_class.SelectedValue = lbl_class_id.Text;
                    bind_exam_term();
                    ddl_exam_term.SelectedValue = lbl_term_id.Text;
                    bind_exam_only();
                    ddl_exam.SelectedValue = lbl_exam_id.Text; 
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                My.exeSql("delete from Exam_validation_for_admit_card_attendance where Id='" + lbl_Id.Text + "'");
                Alertme("Record has been deleted successfully.", "success");
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        }
    }
}