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
    public partial class exam_routine_setting : System.Web.UI.Page
    {
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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_item_master");
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
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
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                }
                else if (ddl_exam.SelectedItem.Text == "Select")
                {
                    Alertme("Please select exam.", "warning");
                    ddl_exam.Focus();
                }
                else
                {
                    save_record();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_record()
        {
            My.exeSql("delete from Active_exam_setting where Class_id='" + ddl_class.SelectedValue + "'");
            SqlCommand cmd;
            string query = "INSERT INTO Active_exam_setting (Session_id,Class_id,Term_id,Exam_id,Created_by,Created_date,Created_idate) values (@Session_id,@Class_id,@Term_id,@Exam_id,@Created_by,@Created_date,@Created_idate)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
            cmd.Parameters.AddWithValue("@Exam_id", ddl_exam.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Record has been added successfully.", "success");
                bind_grd_view();
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select t1.*,Course_Name,(select top 1 Session from session_details where session_id=t1.Session_id) as Session,(select top 1 Term_Name from Exam_Term_Details where Session_Id=t1.Session_id and Class_id=t1.Class_id and Exam_Term_Id=t1.Term_id) as Term_name,(select top 1 Assessment_Name from Exam_Assessment_Details where Session_Id=t1.Session_id and Class_id=t1.Class_id and Exam_Term_Id=t1.Term_id and Assessment_Id=t1.Exam_id) as Assesment_name from Active_exam_setting t1 join Add_course_table t2 on t1.Class_id=t2.course_id order by t2.Position asc");
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
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Please select class", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Exam_Term_Id from Exam_Term_Details  where Class_id=" + ddl_class.SelectedValue + " and Session_Id=" + ddl_session.SelectedValue + " order by Sequence_No asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Please select class", "warning");
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    ddl_term.Focus();
                    Alertme("Please select term", "warning");
                }
                else
                {
                    bind_exam();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void bind_exam()
        {
            mycode.bind_all_ddl_with_id(ddl_exam, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddl_session.SelectedValue + "' and Istatus=1 and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Sequence_No asc");
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            My.exeSql("delete from Active_exam_setting where Id='" + lbl_Id.Text + "'");
            Alertme("Record has been deleted successfully.", "success");
            bind_grd_view();
        }
    }
}