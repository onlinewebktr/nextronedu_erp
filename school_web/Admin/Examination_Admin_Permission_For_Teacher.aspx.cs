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
    public partial class Examination_Admin_Permission_For_Teacher : System.Web.UI.Page
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
                        FetchDetals();
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



        private void FetchDetals()
        {
            try
            {
                string query = "";

                query = "Select Group_name, Group_id from Exam_Menu_Group_List_web where  Type=1  order by Position";

                DataTable dt = imp.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no any data exists.", "warning");
                    rep_menulist.DataSource = null;
                    rep_menulist.DataBind();

                }
                else
                {
                    rep_menulist.DataSource = dt;
                    rep_menulist.DataBind();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MenuDetails");
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



        private void bind_user_working_permission_list()
        {
            string sql = "  select   Group_id, Group_name from dbo.[Exam_Menu_Group_List_web] where Group_id in (select  MainMenuId from dbo.[Exam_MenuPermissionForUser_web] where UserID='" + ddl_UserName.SelectedValue + "')  ";
            SqlCommand cmd = new SqlCommand(sql);
            DataTable dt = imp.GetData(cmd);
            if (dt.Rows.Count > 0)
            {
                pnl_grid.Visible = true;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            else
            {
                pnl_grid.Visible = false;
            }

        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblGroup_id = (Label)e.Item.FindControl("lblGroup_id");
                DataList dl_menulist = (DataList)e.Item.FindControl("dl_menulist");
                string sql = "  select  * from Exam_MenuPermissionForUser_web MP  join Exam_MenuMaster_web MD on MD.MenuID= MP.MenuID  where MP.MainMenuId='" + lblGroup_id.Text + "' and MP.UserID='" + ddl_UserName.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(sql);
                DataTable dt = imp.GetData(cmd);
                dl_menulist.DataSource = dt;
                dl_menulist.DataBind();
            }
        }
        protected void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem i in Repeater1.Items)
                {
                    Label lblGroup_id = (Label)i.FindControl("lblGroup_id");
                    DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                    int count = dl_menulist.Items.Count;
                    for (int j = 0; j < count; j++)
                    {
                        CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                        Label lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                        if (chk_menu.Checked)
                        {
                            delete_user_permission_tb(lblGroup_id.Text, lblmenu_id.Text);
                        }
                    }
                }
                bind_user_working_permission_list();
            }
            catch (Exception ex)
            {
            }
        }
        private void delete_user_permission_tb(string groupid, string menu_id)
        {
            string sql = "delete from  Exam_MenuPermissionForUser_web where UserID='" + ddl_UserName.SelectedValue + "' and MenuID='" + menu_id + "' and  MainMenuId='" + groupid + "'";
            imp.executequery(sql);
            Alertme("User working permission successfully deleted.", "success");

            string desc = "Menu permission removed for user name : " + ddl_UserName.SelectedItem.Text + " & user id : " + ddl_UserName.SelectedValue;
            log_hostory.delete_log(My.get_session_id(), "0", ddl_UserName.SelectedValue, "RemoveMenuPermission", desc, "Examination_Admin_Permission_For_Teacher.aspx", Session["Admin"].ToString());
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



        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem i in rep_menulist.Items)
            {
                CheckBox chkHeader = (CheckBox)i.FindControl("chkHeader");
                DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                for (int j = 0; j < dl_menulist.Items.Count; j++)
                {
                    CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                    if (chkHeader.Checked)
                    {
                        chk_menu.Checked = true;

                    }
                    else
                    {
                        chk_menu.Checked = false;

                    }
                }

            }
        }

        protected void chk_menu_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem i in rep_menulist.Items)
            {
                CheckBox chkHeader = (CheckBox)i.FindControl("chkHeader");
                DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                for (int j = 0; j < dl_menulist.Items.Count; j++)
                {
                    CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                    if (chk_menu.Checked == false)
                    {
                        chkHeader.Checked = false;
                        return;
                    }
                    else
                    {
                        chkHeader.Checked = true;
                    }
                }

            }
        }




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
                    Label lblmenu_id = null;
                    Label lblMainMenu_id = null;
                    foreach (RepeaterItem i in rep_menulist.Items)
                    {
                        lblMainMenu_id = (Label)i.FindControl("lblGroup_id");
                        DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                        int count = dl_menulist.Items.Count;
                        for (int j = 0; j < count; j++)
                        {
                            CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                            lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                            if (chk_menu.Checked)
                            {
                                InsertMenu(ddl_UserName.SelectedValue, lblmenu_id.Text, lblMainMenu_id.Text);
                            }
                        }

                    }
                }
                bind_user_working_permission_list();
            }
            catch (Exception ex)
            {
            }
        }

        My mycode = new My();
        private void InsertMenu(string UserId, string menu_id, string MainMenu_id)
        {
            try
            {
                string strQuery = "Select  * from Exam_MenuPermissionForUser_web where MenuID='" + menu_id + "' and UserID='" + UserId + "'  ";
                SqlCommand cmd = new SqlCommand(strQuery);
                DataTable dt = imp.GetData(cmd);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    string sqlstring = " INSERT INTO Exam_MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Created_by,Created_date,Created_time) VALUES  ('" + menu_id + "', '" + UserId + "', '" + MainMenu_id + "','" + "','" + Session["Admin"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "')";
                    imp.executequery(sqlstring);
                }
                else { }
            }
            catch { }
        }

        protected void rep_menulist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblGroup_id = (Label)e.Item.FindControl("lblGroup_id");
                DataList dl_menulist = (DataList)e.Item.FindControl("dl_menulist");

                sql = "select  * from Exam_MenuMaster_web where Group_id='" + lblGroup_id.Text + "' and Type='1' ";



                SqlCommand cmd = new SqlCommand(sql);
                DataTable dt = imp.GetData(cmd);
                dl_menulist.DataSource = dt;
                dl_menulist.DataBind();
            }
        }

    }
}