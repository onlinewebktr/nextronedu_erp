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
    public partial class assign_for_hostel_attendance : System.Web.UI.Page
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
                        ViewState["userid"] = Session["Admin"].ToString();
                        string pagename_current = "assign-for-hostel-attendance.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_session_search, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session_search.SelectedValue = ddl_session.SelectedValue;



                        mycode.bind_all_ddl_with_id(ddl_teacher, "select name,user_id from user_details where User_Type in ('Teacher','Principal','Coordinator') and  Istatus='1'  order by name  asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_searchInstructor, " select name,user_id   from user_details where User_Type in ('Teacher','Principal','Coordinator') and  Istatus='1'  order by name  asc");
                        search_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
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
        #region search data

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session_search.Focus();
                }
                else
                {
                    search_data();
                }
            }
            catch
            {
            }
        }

        private void search_data()
        {
            if (ddl_session_search.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                return;
            }

            string query;
            if (ddl_searchInstructor.SelectedItem.Text != "ALL")
            {
                query = "select *,(select top 1 name from user_details where user_id=ptm.User_id) as UserName,(select top 1 password from user_details where user_id=ptm.User_id) as Password from Permission_for_hostel_attendance ptm  where ptm.Session_id='" + ddl_session_search.SelectedValue + "' and ptm.User_id='" + ddl_searchInstructor.SelectedValue + "' order by Id desc";
                BindRepeater(query);
            }
            else
            {
                query = "select *,(select top 1 name from user_details where user_id=ptm.User_id) as UserName,(select top 1 password from user_details where user_id=ptm.User_id) as Password from Permission_for_hostel_attendance ptm  where ptm.Session_id='" + ddl_session_search.SelectedValue + "' order by Id desc";
                BindRepeater(query);
            }
        }

        private void BindRepeater(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                Alertme("Sorry there are no data list exist", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }
        #endregion
        UsesCode code = new UsesCode();
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            } 
            else if (ddl_teacher.SelectedItem.Text == "Select")
            {
                Alertme("Please select teacher", "warning");
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    save_or_update_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    save_or_update_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void save_or_update_data()
        {
            if (btn_Submit.Text == "Add")
            {
                if (code.IsExist("Select * from Permission_for_hostel_attendance where User_id='" + ddl_teacher.SelectedValue + "' and Session_Id='" + ddl_session.SelectedValue + "'"))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Permission_for_hostel_attendance (Session_id,User_id,Assign_by,Assign_date,Assign_time) values (@Session_id,@User_id,@Assign_by,@Assign_date,@Assign_time)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@User_id", ddl_teacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assign_by", ddl_teacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assign_date", code.date());
                    cmd.Parameters.AddWithValue("@Assign_time", code.time());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alertme("Teacher/Warden has been mapped successfully.", "success");
                        search_data();
                    }
                }
                else
                {
                    Alertme("Already added", "warning");
                }
            }
            else
            {
                if (code.IsExist("Select * from Permission_for_hostel_attendance where User_id='" + ddl_teacher.SelectedValue + "' and Session_Id=" + ddl_session.SelectedValue + " and Id!='" + hd_id.Value + "' "))
                {
                    SqlCommand cmd;
                    string query = "Update Permission_for_hostel_attendance set Session_id=@Session_id,User_id=@User_id,Assign_by=@Assign_by,Assign_date=@Assign_date,Assign_time=@Assign_time where Id=@Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@User_id", ddl_teacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assign_by", ddl_teacher.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assign_date", code.date());
                    cmd.Parameters.AddWithValue("@Assign_time", code.time());
                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alertme("Teacher/Warden has been mapped successfully.", "success");
                        btn_Submit.Text = "Add";
                        search_data();
                    }
                }
                else
                {
                    Alertme("Already added", "warning");
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("assign-for-hostel-attendance.aspx");
        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_id = (Label)row.FindControl("lbl_Id");
                    SqlCommand cmd = new SqlCommand("Delete from Permission_for_hostel_attendance where Id='" + lbl_id.Text + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                    search_data();
                    Alertme("successfully Deleted.", "success");
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                { 
                    btn_Submit.Text = "Update";
                    btn_cancel.Visible = true;
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_UserID = (Label)row.FindControl("lbl_UserID");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    hd_id.Value = lbl_Id.Text;
                    ddl_session.SelectedValue = lbl_session_id.Text;
                    ddl_teacher.SelectedValue = lbl_UserID.Text;
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
         


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=hostel-attendance-permission" + mycode.date() + "_" + mycode.time() + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
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