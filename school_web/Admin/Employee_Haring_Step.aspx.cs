using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin
{
    public partial class Employee_Haring_Step : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Employee_Haring_Step.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details   order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Employee_Haring_Step");
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Session from session_details where session_id=Employees_Create_Hiring.Session_id) as Session_name from Employees_Create_Hiring order by Hiring_name asc");
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
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ddl_session.Enabled = true;
            txt_Hiring_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }
        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                ddl_session.Focus();
                return;
            }
            if (txt_Hiring_name.Text == "")
            {
                Alertme("Please Enter Hiring Name", "warning");
                txt_Hiring_name.Focus();
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
            SqlCommand cmd;
            DataTable dsgdt = My.dataTable("select Test_name from Employees_Create_Hiring where Hiring_name ='" + txt_Hiring_name.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and id!='" + hd_id.Value + "'");
            if (dsgdt.Rows.Count == 0)
            {
                string query = "Update Employees_Create_Hiring set Session_id=@Session_id,Hiring_name=@Hiring_name,Created_date=@Created_date,Created_idate=@Created_idate,Is_active=@Is_active where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Hiring_name", txt_Hiring_name.Text);
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Employee hiring  updated successfully", "success");
                    empty_form();
                    bind_grd_view();
                }

            }
            else
            {
                Alertme("This Employee hiring  exist with this name.", "warning");
            }
        }

        private void submit_details()
        {
            SqlCommand cmd;
            DataTable dsgdt = My.dataTable("select Hiring_name from Employees_Create_Hiring where Hiring_name ='" + txt_Hiring_name.Text + "' and Session_id='" + ddl_session.SelectedValue + "'");
            if (dsgdt.Rows.Count == 0)
            {
                string Hiring_id = My.auto_serialS("Hiring_id");
                string query = "INSERT INTO Employees_Create_Hiring (Session_id,Hiring_id,Hiring_name,Created_date,Created_idate,Is_active,Created_By) values (@Session_id,@Hiring_id,@Hiring_name,@Created_date,@Created_idate,@Is_active,@Created_By)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Hiring_id", Hiring_id);
                cmd.Parameters.AddWithValue("@Hiring_name", txt_Hiring_name.Text);
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Is_active", 1);
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Employee hiring  created created successfully", "success");
                    mycode.executequery("update Employees_Create_Hiring set Is_active=0 where Hiring_id!=" + Hiring_id + " and Session_id='" + ddl_session.SelectedValue + "'");
                    empty_form();
                    bind_grd_view();
                }



            }
            else
            {
                Alertme("Employee hiring name already exist with this name.", "warning");
            }
        }

        private void empty_form()
        {
            ddl_session.Enabled = true;
            txt_Hiring_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Hiring_id = (Label)row.FindControl("lbl_Hiring_id");

                DataTable dsgdt = My.dataTable("select * from Employee_Hiring_Fee_and_Seat where Hiring_id ='" + lbl_Hiring_id.Text + "'");
                if (dsgdt.Rows.Count == 0)
                {
                    mycode.executequery(" delete Employees_Create_Hiring where Hiring_id=" + lbl_Hiring_id.Text + " ");

                    Alertme("Employee hiring deleted Successfully", "success");
                    bind_grd_view();
                }
                else
                {
                    Alertme("You can't delete this Employee hiring there is a data associated with employee", "warning");
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

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
                    Label lbl_Hiring_name = (Label)row.FindControl("lbl_Hiring_name");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;

                    ddl_session.SelectedValue = lbl_session_id.Text;
                    txt_Hiring_name.Text = lbl_Hiring_name.Text;
                    ddl_session.Enabled = false;
                    ddl_session.CssClass = "form-control";
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
    }
}