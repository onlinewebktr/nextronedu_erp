using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;

namespace school_web.Admin
{
    public partial class LMS_Admin_Permission_For_Teacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Admin"] != null)
                    {

                        bind_user_list();

                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch
            {
            }
        }
        private void bind_user_list()
        {
            DataTable dt = imp.FillData(" select user_id, name from user_details where status='Active' and  User_Type  in  ('Teacher','Principal','Coordinator') order by Name asc");
            imp.PopulateDropDown(ddl_UserName, dt, "name", "user_id");
        }

        My imp = new My();
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
        protected void btn_allow_permission_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_UserName.SelectedItem.Text == "Select")
                {
                    Alertme("Please select user name.", "warning");
                }
                else
                {
                    if (chk_LMS_Admin.Checked == true)
                    {
                        string sql = "  select  * from dbo.[Special_Permission] where  UserID='" + ddl_UserName.SelectedValue + "' and Menu='LMS'  ";
                        SqlCommand cmd = new SqlCommand(sql);
                        DataTable dt = imp.GetData(cmd);
                        if (dt.Rows.Count == 0)
                        {
                            My.exeSql("INSERT INTO Special_Permission (UserID,Menu,Created_by,Created_date,Created_time) values ('" + ddl_UserName.SelectedValue + "','LMS','" + Session["Admin"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "')");

                        }
                        Alertme("Permission Has been granted successfully", "success");
                    }
                    if (chk_payrolladmin.Checked == true)
                    {
                        string sql = "  select  * from dbo.[Special_Permission] where  UserID='" + ddl_UserName.SelectedValue + "' and Menu='Payroll'  ";
                        SqlCommand cmd = new SqlCommand(sql);
                        DataTable dt = imp.GetData(cmd);
                        if (dt.Rows.Count == 0)
                        {
                            My.exeSql("INSERT INTO Special_Permission (UserID,Menu,Created_by,Created_date,Created_time) values ('" + ddl_UserName.SelectedValue + "','Payroll','" + Session["Admin"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "')");
                        }
                        Alertme("Permission Has been granted successfully", "success");
                    }
                    bind_user_working_permission_list(); 
                }
            }
            catch
            {

            }
        }

        protected void ddl_UserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_user_working_permission_list();
            }
            catch (Exception ex)
            { }
        }

        private void bind_user_working_permission_list()
        {
            string sql = "  select  * from dbo.[Special_Permission] where  UserID='" + ddl_UserName.SelectedValue + "'  ";
            SqlCommand cmd = new SqlCommand(sql);
            DataTable dt = imp.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                pnl_grid.Visible = true;
                rp_addedmenu.DataSource = dt;
                rp_addedmenu.DataBind();
            }
            else
            {
                rp_addedmenu.DataSource = null;
                rp_addedmenu.DataBind();
                pnl_grid.Visible = false;
            }
        }

        protected void btn_remove_Click(object sender, EventArgs e)
        {


            int growcountS = rp_addedmenu.Items.Count;
            int kS = 0;
            for (int iS = 0; iS < growcountS; iS++)
            {

                CheckBox chkS = (CheckBox)rp_addedmenu.Items[iS].FindControl("rowChkBox1");
                if (chkS.Checked == true)
                {
                    Label lbl_Menu = (Label)rp_addedmenu.Items[iS].FindControl("lbl_Menu");
                    My.exeSql("delete from Special_Permission where UserID='" + ddl_UserName.SelectedValue + "' and Menu='" + lbl_Menu.Text + "'");

                }
                else
                {
                    kS++;
                }
                
            }
            if (kS == growcountS)
            {
                Alertme("Please check minimum one menu list.", "warning"); 
            }
            else
            {
                Alertme("Menu permission has been successfully removed ", "success");
                bind_user_working_permission_list();

                string desc = "Menu permission removed for user name : " + ddl_UserName.SelectedItem.Text + " & user id : " + ddl_UserName.SelectedValue;
                log_hostory.delete_log(My.get_session_id(), "0", ddl_UserName.SelectedValue, "RemoveMenuPermission", desc, "LMS_and_Payroll_Admin_Permission_For_Teacher.aspx", Session["Admin"].ToString());
            }

        }
    }
}