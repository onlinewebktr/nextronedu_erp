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
    public partial class add_subject_for_online_reg : System.Web.UI.Page
    {
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "online-reg-create-test.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_class, "select Course_Name,course_id from Add_course_table order by Position asc"); 
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Test_Master");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select t1.*,t2.Course_Name from Online_registration_subject t1 join Add_course_table t2 on t1.Class_id=t2.course_id order by t2.Position, Subject asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind(); 
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        { 
            txt_subject_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_class.Focus();
                return;
            }
            if (txt_subject_name.Text == "")
            {
                Alertme("Please enter subject.", "warning");
                txt_subject_name.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_update_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }
            }
        }

        private void update_update_details()
        {
            DataTable dsgdt = My.dataTable("select Subject from Online_registration_subject where Class_id ='" + ddl_class.SelectedValue + "' and Subject='" + txt_subject_name.Text + "' and id!='" + hd_id.Value + "'");
            if (dsgdt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "Update Online_registration_subject set Class_id=@Class_id,Subject=@Subject where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Subject", txt_subject_name.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Subject has been updated successfully", "success");
                    empty_form();
                    bind_grd_view();
                } 
            }
            else
            {
                Alertme("Subject name already exist with this name to selected class.", "warning");
            }
        }

        private void submit_details()
        {
            DataTable dsgdt = My.dataTable("select Subject from Online_registration_subject where Class_id ='" + ddl_class.SelectedValue + "' and Subject='" + txt_subject_name.Text + "'");
            if (dsgdt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Online_registration_subject (Class_id,Subject) values (@Class_id,@Subject)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Subject", txt_subject_name.Text); 
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Subject has been added successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("Subject name already exist with this name to selected class.", "warning");
            }
        }

        private void empty_form()
        {
            txt_subject_name.Text = "";
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
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_subj_name = (Label)row.FindControl("lbl_subj_name");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;

                    ddl_class.SelectedValue = lbl_class_id.Text;
                    txt_subject_name.Text = lbl_subj_name.Text; 
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
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                My.exeSql("delete from Online_registration_subject where Id='"+ lbl_Id.Text + "'");
                Alertme("Test deleted Successfully", "success");
                bind_grd_view(); 
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }
    }
}