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
    public partial class create_internal_exams : System.Web.UI.Page
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
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
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
            string qry = "select *,(select top 1 Session from session_details where session_id=Internal_Exam_master.Session_id) as Session_name from Internal_Exam_master order by Exam_name asc";
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please select session.", "warning");
                }
                else if (txt_exam_name.Text == "")
                {
                    txt_exam_name.Focus();
                    Alertme("Please enter exam name.", "warning");
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
            DataTable dt = My.dataTable("select Id from Internal_Exam_master where Session_id='" + ddlsession.SelectedValue + "' and Exam_name='" + txt_exam_name.Text + "' and Status='1'");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Internal_Exam_master (Exam_id,Session_id,Exam_name,Created_by,Created_date,Created_idate,Status) values (@Exam_id,@Session_id,@Exam_name,@Created_by,@Created_date,@Created_idate,@Status)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_id", My.auto_serialS("Global_sl_id"));
                cmd.Parameters.AddWithValue("@Exam_name", txt_exam_name.Text);
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
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
                string query = "update Internal_Exam_master set Exam_name=@Exam_name,Created_by=@Created_by,Created_date=@Created_date,Created_idate=@Created_idate where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Exam_name", txt_exam_name.Text);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
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
            txt_exam_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_exam_id");
                Label lbl_class_name = (Label)row.FindControl("lbl_class_name");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;
                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            My.exeSql("delete from Internal_Exam_master where Id='" + lbl_Id.Text + "'");
            Alertme("Record has been deleted successfully.", "success");
            bind_grd_view();
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        }
    }
}