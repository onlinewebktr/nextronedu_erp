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
    public partial class create_internal_admit_card : System.Web.UI.Page
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
                    ViewState["courseID"] = "0";
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    mycode.bind_all_ddl_with_id(ddl_exam, "select Exam_name,Exam_id from Internal_Exam_master order by Exam_name asc");
                    ddlsession.SelectedValue = My.get_session_id();
                    bind_grd_view();
                    Bind_course();
                }
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select t1.*,t2.Course_Name,(select top 1 Session from session_details where session_id=t1.Session_id) as Session_name,(select top 1 Exam_name from Internal_Exam_master where Exam_id=t1.Exam_id) as Exam_name  from Internal_exam_admit_card t1 join  Add_course_table t2 on t1.Class_id=t2.course_id order by t2.Position asc");
            if (dt.Rows.Count == 0)
            {
                rd_views.DataSource = null;
                rd_views.DataBind();
            }
            else
            {
                rd_views.DataSource = dt;
                rd_views.DataBind();
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

        private void Bind_course()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
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


        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                }
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
                else if (ddl_exam.SelectedItem.Text == "Select")
                {
                    ddl_exam.Focus();
                    Alertme("Please select exam.", "warning");
                }
                else if (txt_exam_date.Text == "")
                {
                    txt_exam_date.Focus();
                    Alertme("Please choose exam date.", "warning");
                }
                else
                {
                    save_admit_card(); bind_grd_view();
                    Alertme(ViewState["msg"].ToString(), "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_admit_card()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int ix = 0; ix < growcount; ix++)
            {
                CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                if (chk.Checked == true)
                {
                    Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                    Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");

                    string exam_s_time = ddl_s_hour.Text + ":" + ddl_s_minut.Text + " " + ddl_s_ampm.Text;
                    string exam_e_time = ddl_e_hour.Text + ":" + ddl_e_minut.Text + " " + ddl_e_ampm.Text;

                    string arrival_time = ddl_arr_h.Text + ":" + ddl_arr_m.Text + " " + ddl_arr_ampm.Text;


                    DataTable dt = mycode.FillData("Select * from Internal_exam_admit_card where Session_id='" + ddlsession.SelectedValue + "' and Exam_id='" + ddl_exam.SelectedValue + "'  and Class_id='" + lbl_class_id.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Internal_exam_admit_card (Session_id,Exam_id,Class_id,Exam_date,Exam_start_time,Exam_end_time,Created_by,Created_date,Created_idate,Is_arrival_time,Exam_arrival_time) values (@Session_id,@Exam_id,@Class_id,@Exam_date,@Exam_start_time,@Exam_end_time,@Created_by,@Created_date,@Created_idate,@Is_arrival_time,@Exam_arrival_time)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Exam_id", ddl_exam.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", lbl_class_id.Text);
                        cmd.Parameters.AddWithValue("@Exam_date", txt_exam_date.Text);
                        cmd.Parameters.AddWithValue("@Exam_start_time", exam_s_time);
                        cmd.Parameters.AddWithValue("@Exam_end_time", exam_e_time);
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Is_arrival_time", ddl_is_arrival_time.Text);
                        cmd.Parameters.AddWithValue("@Exam_arrival_time", arrival_time);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "update Internal_exam_admit_card set Exam_date=@Exam_date,Exam_start_time=@Exam_start_time,Exam_end_time=@Exam_end_time where Id=@Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Exam_date", txt_exam_date.Text);
                        cmd.Parameters.AddWithValue("@Exam_start_time", exam_s_time);
                        cmd.Parameters.AddWithValue("@Exam_end_time", exam_e_time);
                        cmd.Parameters.AddWithValue("@Is_arrival_time", ddl_is_arrival_time.Text);
                        cmd.Parameters.AddWithValue("@Exam_arrival_time", arrival_time);
                        cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }

                    ViewState["msg"] = "Admit card has benn created successfully";
                }
                else
                {
                    k++;
                }
            }

            if (k == growcount)
            {
                Alertme("Please check minimum one course.", "warning");
                return;
            }
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            My.exeSql("delete from Internal_exam_admit_card where Id='" + lbl_Id.Text + "'");
            Alertme("Record has been deleted successfully.", "success");
            bind_grd_view();
        }
    }
}