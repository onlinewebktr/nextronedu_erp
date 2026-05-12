using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class menu_permission_groupwise : System.Web.UI.Page
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
                        try
                        {
                            if (Session["useriddata"] != null)
                            {
                                ddl_UserName.SelectedValue = Session["useriddata"].ToString();
                                bind_user_working_permission_list();
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        Response.Redirect("../Default.aspx", false);
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

                query = "Select Group_name, Group_id from Menu_Group_List_web where  Type=1  order by Position";

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


            DataTable dt = imp.FillData("select distinct User_Type as name from user_details where status='Active' and Istatus='1' and  User_Type not in('Admin','visitor') order by User_Type asc");
            imp.PopulateDropDown(ddl_UserName, dt, "name", "name");
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
            //string sql = "select Group_id, Group_name from Menu_Group_List_web where Group_id in (select MainMenuId from MenuPermissionForUser_Groupwise  where UserType='" + ddl_UserName.SelectedValue + "') order by Position";

            string sql = @"select * from (select Group_id, Group_name,position from Menu_Group_List_web where Group_id in (select MainMenuId from MenuPermissionForUser_Groupwise  where UserType='" + ddl_UserName.SelectedValue + "' and Type='Admin')  union all select Group_id, Group_name, position from Menu_Group_List_web where Group_id in (select MainMenuId from MenuPermissionForUser_Groupwise where UserType = '" + ddl_UserName.SelectedValue + "' and Type = 'Exam')  union all select Group_id, Group_name, position from Menu_Group_List_web where Group_id in (select MainMenuId from MenuPermissionForUser_Groupwise where UserType = '" + ddl_UserName.SelectedValue + "' and Type = 'Payroll')  union all select Group_id, Group_name, position from Menu_Group_List_web where Group_id in (select MainMenuId from MenuPermissionForUser_Groupwise where UserType = '" + ddl_UserName.SelectedValue + "' and Type = 'Sale'))t order by position";
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
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblGroup_id = (Label)e.Item.FindControl("lblGroup_id");
                DataList dl_menulist = (DataList)e.Item.FindControl("dl_menulist");

                Repeater rp_payroll = (Repeater)e.Item.FindControl("rp_payroll_user");
                Control div_payroll_permission = e.Item.FindControl("div_payroll_permission_user");
                if (lblGroup_id.Text == "12")
                {
                    dl_menulist.Visible = false;
                    div_payroll_permission.Visible = true;
                    //string Employee_id = PayrollMy.get_employee_id_from_employee_code(ddl_UserName.SelectedValue);
                    sql = "select * from MenuPermissionForUser_Groupwise MP  join HR_Menu_Master MD on MD.Menu_Id= MP.MenuID where MP.MainMenuId='12' and MP.UserType='"+ddl_UserName.Text+"'  and MP.Type='Payroll' and MP.MenuID not in(1,33,47) and MD.Parent_id='0' and MD.RestrictedMenu='0' order by MD.Sequence";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        rp_payroll.DataSource = null;
                        rp_payroll.DataBind();
                    }
                    else
                    {
                        rp_payroll.DataSource = dt;
                        rp_payroll.DataBind();
                    }

                }
                else if (lblGroup_id.Text == "33")// sale purcase
                {
                    dl_menulist.Visible = false;
                    div_payroll_permission.Visible = true;
                    sql = "select spm.Group_name as Header ,spm.Group_id as Menu_Id,spm.Position from Sale_Purchase_Menu_Group_List_web spm where spm.Group_id in (select MenuID from MenuPermissionForUser_Groupwise where UserType = '" + ddl_UserName.Text + "' and Type='Sale') order by Position";

                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        rp_payroll.DataSource = null;
                        rp_payroll.DataBind();

                    }
                    else
                    {
                        rp_payroll.DataSource = dt;
                        rp_payroll.DataBind();

                    }
                }
                else if (lblGroup_id.Text == "21")// Exam
                {
                    dl_menulist.Visible = false;
                    div_payroll_permission.Visible = true;

                    //sql = "select distinct MP.MenuID as Menu_Id,MP.MainMenuId,MD.Menu_name as Header from MenuPermissionForUser_Groupwise MP join MenuMaster_web MD on MD.MenuID= MP.MenuID where MP.MainMenuId='" + lblGroup_id.Text + "' and MP.UserType='" + ddl_UserName.Text + "'";

                    sql = "select distinct MP.MenuID as Menu_Id,MP.MainMenuId,MD.Group_name as Header from MenuPermissionForUser_Groupwise MP join Exam_Menu_Group_List_web MD on MD.Group_id = MP.MenuID where MP.MainMenuId = '" + lblGroup_id.Text + "' and MP.Type = 'Exam' and MP.UserType = '" + ddl_UserName.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        rp_payroll.DataSource = null;
                        rp_payroll.DataBind();

                    }
                    else
                    {
                        rp_payroll.DataSource = dt;
                        rp_payroll.DataBind(); 
                    }
                }

                else
                {
                    div_payroll_permission.Visible = false;
                    dl_menulist.Visible = true;
                    sql = "select distinct MP.MenuID,MP.MainMenuId,MD.Menu_name from MenuPermissionForUser_Groupwise MP join MenuMaster_web MD on MD.MenuID= MP.MenuID where MP.MainMenuId='" + lblGroup_id.Text + "' and MP.UserType='" + ddl_UserName.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    dl_menulist.DataSource = dt;
                    dl_menulist.DataBind();
                }
            } 
        }


        protected void ddl_UserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["useriddata"] = ddl_UserName.SelectedValue;
                Response.Redirect("menu-permission-groupwise.aspx", false);
                ddl_UserName.SelectedValue = Session["useriddata"].ToString();
                bind_user_working_permission_list();
            }
            catch (Exception ex)
            { }
        }
        protected void dl_menulist_submenu_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblmenu_id = (Label)e.Item.FindControl("lblmenu_id");
                DataList dl_chiled_menu_submenu = (DataList)e.Item.FindControl("dl_chiled_menu_submenu");

                Bind_chiled_menu_submenu(lblmenu_id.Text, dl_chiled_menu_submenu);



            }
        }

        private void Bind_chiled_menu_submenu(string menuid, DataList dl_chiled_menu_submenu)
        {
            string strQuery = "Select  * from MenuMaster_web where MenuID='" + menuid + "' ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                dl_chiled_menu_submenu.DataSource = null;
                dl_chiled_menu_submenu.DataBind();
            }
            else
            {
                dl_chiled_menu_submenu.DataSource = dt;
                dl_chiled_menu_submenu.DataBind();
            }
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            foreach (RepeaterItem i in rep_menulist.Items)
            {
                CheckBox chkHeader = (CheckBox)i.FindControl("chkHeader");
                if (chk.ClientID == chkHeader.ClientID)
                {
                    DataList dl_menulist = (DataList)i.FindControl("dl_menulist_submenu");
                    Label lblGroup_id = (Label)i.FindControl("lblGroup_id");
                    if (lblGroup_id.Text == "12" || lblGroup_id.Text == "33" || lblGroup_id.Text == "21")// payroll
                    {
                        Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                        for (int j = 0; j < rp_payroll.Items.Count; j++)
                        {
                            CheckBox chk_Header = rp_payroll.Items[j].FindControl("chk_Header") as CheckBox;
                            if (chkHeader.Checked)
                            {
                                chk_Header.Checked = true;
                            }
                            else
                            {
                                chk_Header.Checked = false;
                            }

                        }
                    }
                    else
                    {
                        for (int j = 0; j < dl_menulist.Items.Count; j++)
                        {
                            CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                            Label lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                            DataList dl_chiled_menu_submenu = dl_menulist.Items[j].FindControl("dl_chiled_menu_submenu") as DataList;
                            if (chkHeader.Checked == true)
                            {
                                chk_menu.Checked = true;
                            }
                            else
                            {
                                chk_menu.Checked = false;
                            }
                            foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
                            {
                                Label lblchiled_menu_submenu = ((Label)dli.FindControl("lblchiled_menu_submenu"));

                                if (lblmenu_id.Text == lblchiled_menu_submenu.Text)
                                {
                                    CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));
                                    CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                                    CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                                    CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                                    CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                                    if (chkHeader.Checked == true)
                                    {
                                        chk_add.Checked = true;
                                        chk_edit_permission.Checked = true;
                                        chk_delete.Checked = true;
                                        chk_Download.Checked = true;
                                        chk_print.Checked = true;
                                    }
                                    else
                                    {
                                        chk_add.Checked = false;
                                        chk_edit_permission.Checked = false;
                                        chk_delete.Checked = false;
                                        chk_Download.Checked = false;
                                        chk_print.Checked = false;
                                    }
                                }
                                else
                                {


                                }

                            }



                        }
                    }
                }
            }
        }

        protected void chk_menu_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox lnk = (CheckBox)sender;
            DataListItem row = (DataListItem)lnk.NamingContainer;
            CheckBox chk_menu = (CheckBox)row.FindControl("chk_menu");
            Label lblmenu_id = (Label)row.FindControl("lblmenu_id");
            DataList dl_chiled_menu_submenu = (DataList)row.FindControl("dl_chiled_menu_submenu");
            foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
            {
                CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));
                CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                if (chk_menu.Checked == true)
                {
                    chk_add.Checked = true;
                    chk_edit_permission.Checked = true;
                    chk_delete.Checked = true;
                    chk_Download.Checked = true;
                    chk_print.Checked = true;
                }
                else
                {
                    chk_add.Checked = false;
                    chk_edit_permission.Checked = false;
                    chk_delete.Checked = false;
                    chk_Download.Checked = false;
                    chk_print.Checked = false;
                }

            }


        }



        protected void btn_allow_permission_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_UserName.SelectedItem.Text == "Select")
                {
                    ddl_UserName.Focus();
                    Alertme("Please select group name", "warning");
                    return;
                }
                DataTable dtUser = My.dataTable("select * from user_details where User_Type='" + ddl_UserName.Text + "'");
                foreach (DataRow dr in dtUser.Rows)
                {
                    Label lblmenu_id = null;
                    Label lblMainMenu_id = null;
                    foreach (RepeaterItem i in rep_menulist.Items)
                    {
                        lblMainMenu_id = (Label)i.FindControl("lblGroup_id");
                        DataList dl_menulist = (DataList)i.FindControl("dl_menulist_submenu");
                        int count = dl_menulist.Items.Count;
                        if (lblMainMenu_id.Text == "12")// payroll menu permission
                        {
                            Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                            int count2 = rp_payroll.Items.Count;
                            for (int j = 0; j < count2; j++)
                            {
                                CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                                Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                                if (chk_Header.Checked == true)
                                {
                                    InsertMenu_payroll(dr["user_id"].ToString(), lbl_value.Text, lblMainMenu_id.Text);
                                }
                            }
                        }
                        else if (lblMainMenu_id.Text == "33")// sale purchase
                        {
                            Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                            int count2 = rp_payroll.Items.Count;
                            for (int j = 0; j < count2; j++)
                            {
                                CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                                Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                                if (chk_Header.Checked == true)
                                {
                                    InsertMenu_sale_purchase(dr["user_id"].ToString(), lbl_value.Text, lblMainMenu_id.Text);
                                }

                            }
                        }
                        else if (lblMainMenu_id.Text == "21")// sale purchase
                        {
                            Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                            int count2 = rp_payroll.Items.Count;
                            for (int j = 0; j < count2; j++)
                            {
                                CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                                Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                                if (chk_Header.Checked == true)
                                {
                                    InsertMenu_Exam_MenuPermissionForUser_web(dr["user_id"].ToString(), lbl_value.Text, lblMainMenu_id.Text);
                                }

                            }
                        }

                        else
                        {
                            for (int j = 0; j < count; j++)
                            {
                                CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                                Label lblmenuid = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                                DataList dl_chiled_menu_submenu = dl_menulist.Items[j].FindControl("dl_chiled_menu_submenu") as DataList;

                                foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
                                {

                                    CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                                    CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                                    CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                                    CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                                    CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));
                                    lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                                    if (chk_menu.Checked)
                                    {
                                        InsertMenu(dr["user_id"].ToString(), lblmenu_id.Text, lblMainMenu_id.Text, chk_edit_permission, chk_delete, chk_Download, chk_print, chk_add);
                                    }
                                }
                            }
                        }

                    }
                }
                //============================================
                save_in_master_table();
                Alertme("Group menu permission has been granted successfully.", "success");
                bind_user_working_permission_list();


            }
            catch (Exception ex)
            {
            }
        }

        private void save_in_master_table()
        {
            Label lblmenu_id = null;
            Label lblMainMenu_id = null;
            foreach (RepeaterItem i in rep_menulist.Items)
            {
                lblMainMenu_id = (Label)i.FindControl("lblGroup_id");
                DataList dl_menulist = (DataList)i.FindControl("dl_menulist_submenu");
                int count = dl_menulist.Items.Count;
                if (lblMainMenu_id.Text == "12")// payroll menu permission
                {
                    Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                    int count2 = rp_payroll.Items.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                        Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                        if (chk_Header.Checked == true)
                        {
                            InsertMenuInMasterGroup(lbl_value.Text, lblMainMenu_id.Text, chk_edit_permission1, chk_delete1, chk_Download1, chk_print1, chk_add1, "Payroll");
                        }
                    }
                }
                else if (lblMainMenu_id.Text == "33")// sale purchase
                {
                    Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                    int count2 = rp_payroll.Items.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                        Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                        if (chk_Header.Checked == true)
                        {
                            InsertMenuInMasterGroup(lbl_value.Text, lblMainMenu_id.Text, chk_edit_permission1, chk_delete1, chk_Download1, chk_print1, chk_add1, "Sale");
                        }

                    }
                }
                else if (lblMainMenu_id.Text == "21")// sale purchase
                {
                    Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll");
                    int count2 = rp_payroll.Items.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                        Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                        if (chk_Header.Checked == true)
                        {
                            InsertMenuInMasterGroup(lbl_value.Text, lblMainMenu_id.Text, chk_edit_permission1, chk_delete1, chk_Download1, chk_print1, chk_add1, "Exam");
                        }

                    }
                }

                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu") as CheckBox;
                        Label lblmenuid = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                        DataList dl_chiled_menu_submenu = dl_menulist.Items[j].FindControl("dl_chiled_menu_submenu") as DataList;

                        foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
                        {

                            CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                            CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                            CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                            CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                            CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));
                            lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                            if (chk_menu.Checked)
                            {
                                InsertMenuInMasterGroup(lblmenu_id.Text, lblMainMenu_id.Text, chk_edit_permission, chk_delete, chk_Download, chk_print, chk_add, "Admin");
                            }
                        }
                    }
                }

            }
        }
        private void InsertMenuInMasterGroup(string menu_id, string MainMenu_id, CheckBox chk_edit_permission, CheckBox chk_delete, CheckBox chk_Download, CheckBox chk_print, CheckBox chk_add, string Type)
        {
            try
            {
                int edit_permission = 0;
                int delete_permission = 0;
                int delete_Download = 0;
                int delete_print = 0;
                int delete_add = 0;
                if (chk_edit_permission.Checked == true)
                {
                    edit_permission = 1;
                }
                if (chk_delete.Checked == true)
                {
                    delete_permission = 1;
                }
                if (chk_Download.Checked == true)
                {
                    delete_Download = 1;
                }
                if (chk_print.Checked == true)
                {
                    delete_print = 1;
                }
                if (chk_add.Checked == true)
                {
                    delete_add = 1;
                }
                string strQuery = "Select * from MenuPermissionForUser_Groupwise where MenuID='" + menu_id + "' and UserType='" + ddl_UserName.Text + "' and Type='" + Type + "' ";
                SqlCommand cmd = new SqlCommand(strQuery);
                DataTable dt = imp.GetData(cmd);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    string sqlstring = " INSERT INTO MenuPermissionForUser_Groupwise(MenuID, UserType,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add,Created_by,Created_date,Created_time,Type) VALUES  ('" + menu_id + "', '" + ddl_UserName.Text + "', '" + MainMenu_id + "','" + edit_permission + "','" + delete_permission + "','" + delete_Download + "','" + delete_print + "','" + delete_add + "','" + Session["Admin"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "','" + Type + "')";
                    imp.executequery(sqlstring);
                }
                else
                {
                    string query = "Update MenuPermissionForUser_Groupwise set Is_Edit=@Is_Edit,Is_delete=@Is_delete,Is_Download=@Is_Download,Is_Print=@Is_Print,Is_add=@Is_add,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,MainMenuId=@MainMenuId where MenuID=@MenuID and UserType=@UserType";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Is_Edit", edit_permission);
                    cmd.Parameters.AddWithValue("@Is_delete", delete_permission);
                    cmd.Parameters.AddWithValue("@Is_Download", delete_Download);
                    cmd.Parameters.AddWithValue("@Is_Print", delete_print);
                    cmd.Parameters.AddWithValue("@MenuID", menu_id);
                    cmd.Parameters.AddWithValue("@Is_add", delete_add);
                    cmd.Parameters.AddWithValue("@UserType", ddl_UserName.Text);
                    cmd.Parameters.AddWithValue("@MainMenuId", MainMenu_id);
                    cmd.Parameters.AddWithValue("@Updated_by", Session["Admin"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void InsertMenu_Exam_MenuPermissionForUser_web(string UserID, string menu_id, string MainMenu_id)
        {
            string strQuery = "Select  * from Exam_MenuPermissionForUser_web where MenuID='" + menu_id + "' and UserID='" + UserID + "'  ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                string sqlstring = " INSERT INTO Exam_MenuPermissionForUser_web(MenuID, UserID,MainMenuId) VALUES  ('" + menu_id + "', '" + UserID + "', '" + MainMenu_id + "')";
                imp.executequery(sqlstring);

                string strQuery1 = "Select  * from MenuPermissionForUser_web where MainMenuId='" + MainMenu_id + "' and UserID='" + UserID + "'  ";
                SqlCommand cmd1 = new SqlCommand(strQuery1);
                DataTable dt1 = imp.GetData(cmd1);
                int count1 = dt1.Rows.Count;
                if (count1 == 0)
                {
                    string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('148', '" + UserID + "', '" + MainMenu_id + "','1','1','1','1','1')";
                    imp.executequery(sqlstring3);
                }
            }
            else
            {
            }
        }

        private void InsertMenu_sale_purchase(string UserID, string menu_id, string MainMenu_id)
        {
            string strQuery = "Select  * from SALE_PURCHASE_MENUPERMISSIONFORUSER_WEB where MenuID='" + menu_id + "' and UserID='" + UserID + "'  ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                string sqlstring = " INSERT INTO SALE_PURCHASE_MENUPERMISSIONFORUSER_WEB(MenuID, UserID,MainMenuId) VALUES  ('" + menu_id + "', '" + UserID + "', '" + MainMenu_id + "')";
                imp.executequery(sqlstring);



                string strQuery1 = "Select  * from MenuPermissionForUser_web where MainMenuId='" + MainMenu_id + "' and UserID='" + UserID + "'  ";
                SqlCommand cmd1 = new SqlCommand(strQuery1);
                DataTable dt1 = imp.GetData(cmd1);
                int count1 = dt1.Rows.Count;
                if (count1 == 0)
                {

                    string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('254', '" + UserID + "', '" + MainMenu_id + "','1','1','1','1','1')";
                    imp.executequery(sqlstring3);
                }

            }
            else
            {

            }
        }

        private void InsertMenu_payroll(string UserId, string menu_id, string MainMenu_id)
        {
            string Employee_id = PayrollMy.get_employee_id_from_employee_code(UserId);
            string strQuery = "Select  * from HR_MenuPermission where MenuID='" + menu_id + "' and Employee_id='" + Employee_id + "'  ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                string sqlstring = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('" + menu_id + "', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                imp.executequery(sqlstring);
                My.exeSql("update HR_UserProfile set IsHr=1,IsAdmin=1 where UserId='" + Employee_id + "'");

                string strQuery1 = "Select  * from HR_MenuPermission where MenuID='1' and Employee_id='" + Employee_id + "'  ";
                SqlCommand cmd1 = new SqlCommand(strQuery1);
                DataTable dt1 = imp.GetData(cmd1);
                int count1 = dt1.Rows.Count;
                if (count1 == 0)
                {
                    string sqlstring2 = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('1', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                    imp.executequery(sqlstring2);

                    string sqlstring4 = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('33', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                    imp.executequery(sqlstring4);

                    string sqlstring5 = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('47', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                    imp.executequery(sqlstring5);

                    string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('253', '" + UserId + "', '12','1','1','1','1','1')";
                    imp.executequery(sqlstring3);

                }
                else
                {

                }

            }
            else
            {
                My.exeSql("update HR_UserProfile set IsHr=1,IsAdmin=1 where UserId='" + Employee_id + "'");


            }

        }

        My mycode = new My();
        private void InsertMenu(string UserId, string menu_id, string MainMenu_id, CheckBox chk_edit_permission, CheckBox chk_delete, CheckBox chk_Download, CheckBox chk_print, CheckBox chk_add)
        {
            try
            {
                int edit_permission = 0;
                int delete_permission = 0;
                int delete_Download = 0;
                int delete_print = 0;
                int delete_add = 0;

                if (chk_edit_permission.Checked == true)
                {
                    edit_permission = 1;
                }
                if (chk_delete.Checked == true)
                {
                    delete_permission = 1;
                }
                if (chk_Download.Checked == true)
                {
                    delete_Download = 1;
                }
                if (chk_print.Checked == true)
                {
                    delete_print = 1;
                }
                if (chk_add.Checked == true)
                {
                    delete_add = 1;
                }
                string strQuery = "Select  * from MenuPermissionForUser_web where MenuID='" + menu_id + "' and UserID='" + UserId + "'  ";
                SqlCommand cmd = new SqlCommand(strQuery);
                DataTable dt = imp.GetData(cmd);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    string sqlstring = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add,Created_by,Created_date,Created_time) VALUES  ('" + menu_id + "', '" + UserId + "', '" + MainMenu_id + "','" + edit_permission + "','" + delete_permission + "','" + delete_Download + "','" + delete_print + "','" + delete_add + "','" + Session["Admin"].ToString() + "','" + mycode.date() + "','" + mycode.time() + "')";
                    imp.executequery(sqlstring);

                    if (menu_id == "253")// new payroll menu
                    {
                        My.exeSql("update HR_UserProfile set IsHr=1 where UserId='" + UserId + "'");
                    }
                }
                else
                {
                    if (menu_id == "253")// new payroll menu
                    {
                        My.exeSql("update HR_UserProfile set IsHr=1 where UserId='" + UserId + "'");
                    }
                    string query = "Update MenuPermissionForUser_web set Is_Edit=@Is_Edit,Is_delete=@Is_delete,Is_Download=@Is_Download,Is_Print=@Is_Print,Is_add=@Is_add,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,MainMenuId=@MainMenuId where MenuID=@MenuID and UserID=@UserID";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Is_Edit", edit_permission);
                    cmd.Parameters.AddWithValue("@Is_delete", delete_permission);
                    cmd.Parameters.AddWithValue("@Is_Download", delete_Download);
                    cmd.Parameters.AddWithValue("@Is_Print", delete_print);
                    cmd.Parameters.AddWithValue("@MenuID", menu_id);
                    cmd.Parameters.AddWithValue("@Is_add", delete_add);
                    cmd.Parameters.AddWithValue("@UserID", UserId);
                    cmd.Parameters.AddWithValue("@MainMenuId", MainMenu_id);
                    cmd.Parameters.AddWithValue("@Updated_by", Session["Admin"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_time", mycode.time());

                    if (My.InsertUpdateData(cmd))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_UserName.SelectedItem.Text == "Select")
                {
                    ddl_UserName.Focus();
                    Alertme("Please choose employee group.", "waarning");
                }
                else
                {
                    DataTable dtUser = My.dataTable("select * from user_details where User_Type='" + ddl_UserName.Text + "'");
                    foreach (DataRow dr in dtUser.Rows)
                    {
                        foreach (RepeaterItem i in Repeater1.Items)
                        {
                            Label lblGroup_id = (Label)i.FindControl("lblGroup_id");
                            DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                            int count = dl_menulist.Items.Count;

                            if (lblGroup_id.Text == "12")// payroll menu permission
                            {
                                int k = 0;
                                Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                                int count2 = rp_payroll.Items.Count;
                                for (int j = 0; j < count2; j++)
                                {
                                    CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                                    Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                                    if (chk_Header.Checked == true)
                                    {
                                        k = k + 1;
                                        My.exeSql("delete from  HR_MenuPermission where UserID='" + dr["user_id"].ToString() + "' and MainMenuId='12'  and MenuID='" + lbl_value.Text + "'  ");
                                    }
                                }
                                if (count2 == k)
                                {
                                    My.exeSql("update HR_UserProfile set IsHr=null,IsAdmin=null where EmployeeCode='" + dr["user_id"].ToString() + "'");
                                    My.exeSql("delete from  MenuPermissionForUser_web where UserID='" + dr["user_id"].ToString() + "' and MainMenuId='12'");
                                    My.exeSql("delete from  HR_MenuPermission where UserID='" + dr["user_id"].ToString() + "' and MainMenuId='12'   ");
                                }
                            }
                            else if (lblGroup_id.Text == "33")// sale and purchase 
                            {
                                int k = 0;
                                Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                                int count2 = rp_payroll.Items.Count;
                                for (int j = 0; j < count2; j++)
                                {
                                    CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                                    Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                                    if (chk_Header.Checked == true)
                                    {
                                        k = k + 1;
                                        My.exeSql("delete from  SALE_PURCHASE_MENUPERMISSIONFORUSER_WEB where UserID='" + dr["user_id"].ToString() + "'  and MenuID='" + lbl_value.Text + "'  ");
                                    }
                                }
                                if (count2 == k)
                                {
                                    My.exeSql("delete from  MenuPermissionForUser_web where UserID='" + dr["user_id"].ToString() + "'      and MainMenuId='33'");
                                }
                            }
                            else if (lblGroup_id.Text == "21")// examnation menu
                            {
                                int k = 0;
                                Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                                int count2 = rp_payroll.Items.Count;
                                for (int j = 0; j < count2; j++)
                                {
                                    CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                                    Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                                    if (chk_Header.Checked == true)
                                    {
                                        k = k + 1;
                                        My.exeSql("delete from  Exam_MenuPermissionForUser_web where UserID='" + dr["user_id"].ToString() + "'  and MenuID='" + lbl_value.Text + "'  ");
                                    }
                                }
                                if (count2 == k)
                                {
                                    My.exeSql("delete from  MenuPermissionForUser_web where UserID='" + dr["user_id"].ToString() + "'      and MainMenuId='21'");
                                }
                            }
                            else
                            {
                                for (int j = 0; j < count; j++)
                                {
                                    CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu_permission_granted") as CheckBox;
                                    Label lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                                    DataList dl_chiled_menu_submenu = dl_menulist.Items[j].FindControl("dl_chiled_menu_submenu_user_permission") as DataList;


                                    foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
                                    {
                                        CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                                        CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                                        CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                                        CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                                        CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));

                                        if (chk_menu.Checked)
                                        {
                                            delete_user_permission_tb(lblGroup_id.Text, lblmenu_id.Text, dr["user_id"].ToString());
                                        }
                                        else
                                        {
                                            delete_user_permission_tb_for_tool(lblGroup_id.Text, lblmenu_id.Text, chk_edit_permission, chk_delete, chk_Download, chk_print, chk_add, dr["user_id"].ToString());
                                        }
                                    }
                                }
                            }
                        }

                        string desc = "Menu permission removed for user name : " + ddl_UserName.SelectedItem.Text + " & user id : " + ddl_UserName.SelectedValue;
                        log_hostory.delete_log(My.get_session_id(), "0", ddl_UserName.SelectedValue, "RemoveMenuPermission", desc, "Userpermissions.aspx", Session["Admin"].ToString());
                    }
                    remove_permissions();
                    Alertme("Menu permission has been updated successfully", "success");
                    bind_user_working_permission_list();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void remove_permissions()
        {
            foreach (RepeaterItem i in Repeater1.Items)
            {
                Label lblGroup_id = (Label)i.FindControl("lblGroup_id");
                DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                int count = dl_menulist.Items.Count;

                if (lblGroup_id.Text == "12")// payroll menu permission
                {
                    int k = 0;
                    Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                    int count2 = rp_payroll.Items.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                        Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                        if (chk_Header.Checked == true)
                        {
                            k = k + 1;
                            My.exeSql("delete from MenuPermissionForUser_Groupwise where UserType='" + ddl_UserName.Text + "' and MainMenuId='12'  and MenuID='" + lbl_value.Text + "'  ");
                        }
                    }
                }
                else if (lblGroup_id.Text == "33")// sale and purchase 
                {
                    int k = 0;
                    Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                    int count2 = rp_payroll.Items.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                        Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                        if (chk_Header.Checked == true)
                        {
                            k = k + 1;
                            My.exeSql("delete from  MenuPermissionForUser_Groupwise where  UserType='" + ddl_UserName.Text + "' and MenuID='" + lbl_value.Text + "'  ");
                        }
                    }
                }
                else if (lblGroup_id.Text == "21")// examnation menu
                {
                    int k = 0;
                    Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                    int count2 = rp_payroll.Items.Count;
                    for (int j = 0; j < count2; j++)
                    {
                        CheckBox chk_Header = (CheckBox)rp_payroll.Items[j].FindControl("chk_Header");
                        Label lbl_value = (Label)rp_payroll.Items[j].FindControl("lbl_value");
                        if (chk_Header.Checked == true)
                        {
                            k = k + 1;
                            My.exeSql("delete from MenuPermissionForUser_Groupwise where UserType='" + ddl_UserName.Text + "' and MenuID='" + lbl_value.Text + "'  ");
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu_permission_granted") as CheckBox;
                        Label lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;
                        DataList dl_chiled_menu_submenu = dl_menulist.Items[j].FindControl("dl_chiled_menu_submenu_user_permission") as DataList;


                        foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
                        {
                            CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                            CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                            CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                            CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                            CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));

                            if (chk_menu.Checked)
                            {
                                delete_user_permission_tb_groupwise(lblGroup_id.Text, lblmenu_id.Text, "Admin");
                            }
                            else
                            {
                                delete_user_permission_tb_for_tool_groupwise(lblGroup_id.Text, lblmenu_id.Text, chk_edit_permission, chk_delete, chk_Download, chk_print, chk_add, "Admin");
                            }
                        }
                    }
                }
            }

            string desc = "Menu permission removed for user name : " + ddl_UserName.SelectedItem.Text + " & user id : " + ddl_UserName.SelectedValue;
            log_hostory.delete_log(My.get_session_id(), "0", ddl_UserName.SelectedValue, "RemoveMenuPermission", desc, "Userpermissions.aspx", Session["Admin"].ToString());
        }

        private void delete_user_permission_tb_for_tool_groupwise(string Group_id, string tmenu_idext2, CheckBox chk_edit_permission, CheckBox chk_delete, CheckBox chk_Download, CheckBox chk_print, CheckBox chk_add, string type)
        {
            int edit_permission = 1;
            int delete_permission = 1;
            int delete_Download = 1;
            int delete_print = 1;
            int delete_add = 1;

            if (chk_add.Visible == true)
            {
                if (chk_add.Checked == true)
                {
                    delete_add = 0;
                }
                else
                {
                    delete_add = 1;
                }

            }
            else
            {
                delete_add = 0;
            }


            if (chk_edit_permission.Visible == true)
            {
                if (chk_edit_permission.Checked == true)
                {
                    edit_permission = 0;
                }
                else
                {
                    edit_permission = 1;
                }

            }
            else
            {
                edit_permission = 0;
            }

            if (chk_delete.Visible == true)
            {
                if (chk_delete.Checked == true)
                {
                    delete_permission = 0;
                }
                else
                {
                    delete_permission = 1;
                }
            }
            else
            {
                delete_permission = 0;
            }

            if (chk_Download.Visible == true)
            {
                if (chk_Download.Checked == true)
                {
                    delete_Download = 0;
                }
                else
                {
                    delete_Download = 1;
                }
            }
            else
            {
                delete_Download = 0;

            }
            if (chk_print.Visible == true)
            {
                if (chk_print.Checked == true)
                {
                    delete_print = 0;
                }
                else
                {
                    delete_print = 1;
                }

            }
            else
            {
                delete_print = 0;
            }

            string query = "Update MenuPermissionForUser_Groupwise set Is_Edit=@Is_Edit,Is_delete=@Is_delete,Is_Download=@Is_Download,Is_Print=@Is_Print,Is_add=@Is_add where MenuID=@MenuID and UserType=@UserType and MainMenuId=@MainMenuId and Type=@Type";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Is_Edit", edit_permission);
            cmd.Parameters.AddWithValue("@Is_delete", delete_permission);
            cmd.Parameters.AddWithValue("@Is_Download", delete_Download);
            cmd.Parameters.AddWithValue("@Is_Print", delete_print);
            cmd.Parameters.AddWithValue("@MenuID", tmenu_idext2);
            cmd.Parameters.AddWithValue("@UserType", ddl_UserName.Text);
            cmd.Parameters.AddWithValue("@Is_add", delete_add);
            cmd.Parameters.AddWithValue("@MainMenuId", Group_id);
            cmd.Parameters.AddWithValue("@Type", type);
            if (My.InsertUpdateData(cmd))
            {
            }
        }
        private void delete_user_permission_tb_groupwise(string groupid, string menu_id, string Type)
        {
            string sql = "delete from MenuPermissionForUser_Groupwise where UserType='" + ddl_UserName.Text + "' and MenuID='" + menu_id + "' and  MainMenuId='" + groupid + "' and Type='" + Type + "'";
            imp.executequery(sql);
        }
        private void delete_user_permission_tb_for_tool(string Group_id, string tmenu_idext2, CheckBox chk_edit_permission, CheckBox chk_delete, CheckBox chk_Download, CheckBox chk_print, CheckBox chk_add, string user_id)
        {
            int edit_permission = 1;
            int delete_permission = 1;
            int delete_Download = 1;
            int delete_print = 1;
            int delete_add = 1;

            if (chk_add.Visible == true)
            {
                if (chk_add.Checked == true)
                {
                    delete_add = 0;
                }
                else
                {
                    delete_add = 1;
                }

            }
            else
            {
                delete_add = 0;

            }


            if (chk_edit_permission.Visible == true)
            {
                if (chk_edit_permission.Checked == true)
                {
                    edit_permission = 0;
                }
                else
                {
                    edit_permission = 1;
                }

            }
            else
            {
                edit_permission = 0;

            }

            if (chk_delete.Visible == true)
            {
                if (chk_delete.Checked == true)
                {
                    delete_permission = 0;
                }
                else
                {
                    delete_permission = 1;
                }

            }
            else
            {
                delete_permission = 0;

            }

            if (chk_Download.Visible == true)
            {
                if (chk_Download.Checked == true)
                {
                    delete_Download = 0;
                }
                else
                {
                    delete_Download = 1;
                }

            }
            else
            {
                delete_Download = 0;

            }
            if (chk_print.Visible == true)
            {
                if (chk_print.Checked == true)
                {
                    delete_print = 0;
                }
                else
                {
                    delete_print = 1;
                }

            }
            else
            {
                delete_print = 0;

            }

            string query = "Update MenuPermissionForUser_web set  Is_Edit=@Is_Edit,Is_delete=@Is_delete,Is_Download=@Is_Download,Is_Print=@Is_Print,Is_add=@Is_add where MenuID=@MenuID and UserID=@UserID and MainMenuId=@MainMenuId";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Is_Edit", edit_permission);
            cmd.Parameters.AddWithValue("@Is_delete", delete_permission);
            cmd.Parameters.AddWithValue("@Is_Download", delete_Download);
            cmd.Parameters.AddWithValue("@Is_Print", delete_print);
            cmd.Parameters.AddWithValue("@MenuID", tmenu_idext2);
            cmd.Parameters.AddWithValue("@UserID", user_id);
            cmd.Parameters.AddWithValue("@Is_add", delete_add);
            cmd.Parameters.AddWithValue("@MainMenuId", Group_id);
            if (My.InsertUpdateData(cmd))
            {

            }

        }
        private void delete_user_permission_tb(string groupid, string menu_id, string Userid)
        {
            string sql = "delete from  MenuPermissionForUser_web where UserID='" + Userid + "' and MenuID='" + menu_id + "' and  MainMenuId='" + groupid + "'";
            imp.executequery(sql);
            Alertme("User working permission successfully deleted.", "success");
        }
        protected void rep_menulist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblGroup_id = (Label)e.Item.FindControl("lblGroup_id");
                DataList dl_menulist = (DataList)e.Item.FindControl("dl_menulist_submenu");
                Repeater rp_payroll = (Repeater)e.Item.FindControl("rp_payroll");
                Control div_payroll_permission = e.Item.FindControl("div_payroll_permission");
                if (lblGroup_id.Text == "12")
                {
                    div_payroll_permission.Visible = true;
                    sql = "select   * from HR_Menu_Master  where Parent_id='0' and RestrictedMenu='0' and Menu_Id not in(1,47,33) order by Sequence  asc";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        rp_payroll.DataSource = null;
                        rp_payroll.DataBind();
                    }
                    else
                    {
                        rp_payroll.DataSource = dt;
                        rp_payroll.DataBind();
                    }

                }
                else if (lblGroup_id.Text == "33")//sale purcse
                {
                    div_payroll_permission.Visible = true;
                    sql = "select   Group_name as Header,Group_id as Menu_Id from SALE_PURCHASE_MENU_GROUP_LIST_WEB  where Type='1'   order by Position  asc";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        rp_payroll.DataSource = null;
                        rp_payroll.DataBind();
                    }
                    else
                    {
                        rp_payroll.DataSource = dt;
                        rp_payroll.DataBind();
                    }

                }
                else if (lblGroup_id.Text == "21")//examination details
                {
                    div_payroll_permission.Visible = true;
                    sql = "select   Group_name as Header,Group_id as Menu_Id from Exam_Menu_Group_List_web  where Type='1'   order by Position  asc";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        rp_payroll.DataSource = null;
                        rp_payroll.DataBind();
                    }
                    else
                    {
                        rp_payroll.DataSource = dt;
                        rp_payroll.DataBind();
                    }
                }




                else
                {
                    div_payroll_permission.Visible = false;
                    sql = "select  * from MenuMaster_web where Group_id='" + lblGroup_id.Text + "' and Type='1' ";
                    SqlCommand cmd = new SqlCommand(sql);
                    DataTable dt = imp.GetData(cmd);
                    dl_menulist.DataSource = dt;
                    dl_menulist.DataBind();
                }





            }
        }
        protected void chk_menu_permission_granted_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox lnk = (CheckBox)sender;
            DataListItem row = (DataListItem)lnk.NamingContainer;
            CheckBox chk_menu = (CheckBox)row.FindControl("chk_menu_permission_granted");
            Label lblmenu_id = (Label)row.FindControl("lblmenu_id");
            DataList dl_chiled_menu_submenu = (DataList)row.FindControl("dl_chiled_menu_submenu_user_permission");
            foreach (DataListItem dli in dl_chiled_menu_submenu.Items)
            {


                CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));
                CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                if (chk_menu.Checked == true)
                {
                    chk_add.Checked = true;
                    chk_edit_permission.Checked = true;
                    chk_delete.Checked = true;
                    chk_Download.Checked = true;
                    chk_print.Checked = true;
                }
                else
                {
                    chk_add.Checked = false;
                    chk_edit_permission.Checked = false;
                    chk_delete.Checked = false;
                    chk_Download.Checked = false;
                    chk_print.Checked = false;
                }

            }


        }
        protected void dl_menulist_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Label lblmenu_id = (Label)e.Item.FindControl("lblmenu_id");
                Label lbl_MainMenuId = (Label)e.Item.FindControl("lbl_MainMenuId");
                DataList dl_chiled_menu_submenu_user_permission = (DataList)e.Item.FindControl("dl_chiled_menu_submenu_user_permission");

                Bind_chiled_menu_submenu(lblmenu_id.Text, lbl_MainMenuId.Text, dl_chiled_menu_submenu_user_permission);





            }
        }
        private void Bind_chiled_menu_submenu(string menu_id, string MainMenuId, DataList dl_chiled_menu_submenu_user_permission)
        {
            string strQuery = "Select  * from MenuPermissionForUser_Groupwise where UserType='" + ddl_UserName.SelectedValue + "' and MenuID='" + menu_id + "' and MainMenuId='" + MainMenuId + "'   ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                dl_chiled_menu_submenu_user_permission.DataSource = null;
                dl_chiled_menu_submenu_user_permission.DataBind();
            }
            else
            {
                dl_chiled_menu_submenu_user_permission.DataSource = dt;
                dl_chiled_menu_submenu_user_permission.DataBind();
            }
        }
        protected void chkHeaderRemove_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            foreach (RepeaterItem i in Repeater1.Items)
            {
                CheckBox chkHeader = (CheckBox)i.FindControl("chkHeaderRemove");


                if (chk.ClientID == chkHeader.ClientID)
                {
                    DataList dl_menulist = (DataList)i.FindControl("dl_menulist");
                    Label lblGroup_id = (Label)i.FindControl("lblGroup_id");
                    if (lblGroup_id.Text == "12" || lblGroup_id.Text == "21" || lblGroup_id.Text == "33")// payroll
                    {
                        Repeater rp_payroll = (Repeater)i.FindControl("rp_payroll_user");
                        for (int j = 0; j < rp_payroll.Items.Count; j++)
                        {
                            CheckBox chk_Header = rp_payroll.Items[j].FindControl("chk_Header") as CheckBox;
                            if (chkHeader.Checked)
                            {
                                chk_Header.Checked = true;
                            }
                            else
                            {
                                chk_Header.Checked = false;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dl_menulist.Items.Count; j++)
                        {
                            CheckBox chk_menu = dl_menulist.Items[j].FindControl("chk_menu_permission_granted") as CheckBox;
                            Label lblmenu_id = dl_menulist.Items[j].FindControl("lblmenu_id") as Label;

                            DataList dl_chiled_menu_submenu_user_permission = dl_menulist.Items[j].FindControl("dl_chiled_menu_submenu_user_permission") as DataList;

                            if (chkHeader.Checked == true)
                            {
                                chk_menu.Checked = true;
                            }
                            else
                            {
                                chk_menu.Checked = false;
                            }
                            foreach (DataListItem dli in dl_chiled_menu_submenu_user_permission.Items)
                            {
                                CheckBox chk_add = ((CheckBox)dli.FindControl("chk_add"));
                                CheckBox chk_edit_permission = ((CheckBox)dli.FindControl("chk_edit_permission"));
                                CheckBox chk_delete = ((CheckBox)dli.FindControl("chk_delete"));
                                CheckBox chk_Download = ((CheckBox)dli.FindControl("chk_Download"));
                                CheckBox chk_print = ((CheckBox)dli.FindControl("chk_print"));
                                if (chkHeader.Checked == true)
                                {
                                    chk_add.Checked = true;
                                    chk_edit_permission.Checked = true;
                                    chk_delete.Checked = true;
                                    chk_Download.Checked = true;
                                    chk_print.Checked = true;
                                }
                                else
                                {
                                    chk_add.Checked = false;
                                    chk_edit_permission.Checked = false;
                                    chk_delete.Checked = false;
                                    chk_Download.Checked = false;
                                    chk_print.Checked = false;
                                }
                            }



                        }
                    }
                }
            }
        }
        protected void dl_chiled_menu_submenu_user_permission_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label Is_add = (Label)e.Item.FindControl("Is_add");
                Label Is_Edit = (Label)e.Item.FindControl("Is_Edit");
                Label Is_delete = (Label)e.Item.FindControl("Is_delete");
                Label Is_Download = (Label)e.Item.FindControl("Is_Download");
                Label lbl_Is_Print = (Label)e.Item.FindControl("lbl_Is_Print");

                CheckBox chk_add = (CheckBox)e.Item.FindControl("chk_add");
                CheckBox chk_edit_permission = (CheckBox)e.Item.FindControl("chk_edit_permission");
                CheckBox chk_delete = (CheckBox)e.Item.FindControl("chk_delete");
                CheckBox chk_Download = (CheckBox)e.Item.FindControl("chk_Download");
                CheckBox chk_print = (CheckBox)e.Item.FindControl("chk_print");

                HtmlGenericControl add = (HtmlGenericControl)e.Item.FindControl("add") as HtmlGenericControl;

                HtmlGenericControl Edit = (HtmlGenericControl)e.Item.FindControl("Edit") as HtmlGenericControl;
                HtmlGenericControl Delete = (HtmlGenericControl)e.Item.FindControl("Delete") as HtmlGenericControl;
                HtmlGenericControl Download = (HtmlGenericControl)e.Item.FindControl("Download") as HtmlGenericControl;
                HtmlGenericControl Print = (HtmlGenericControl)e.Item.FindControl("Print") as HtmlGenericControl;


                if (Is_add.Text == "1")
                {
                    add.Visible = true;
                    chk_add.Visible = true;
                    chk_add.Checked = false;
                }
                else
                {
                    add.Visible = false;
                    chk_add.Visible = false;
                    chk_add.Checked = false;
                }


                if (Is_Edit.Text == "1")
                {
                    Edit.Visible = true;
                    chk_edit_permission.Visible = true;
                    chk_edit_permission.Checked = false;
                }
                else
                {
                    Edit.Visible = false;
                    chk_edit_permission.Visible = false;
                    chk_edit_permission.Checked = false;
                }
                if (Is_delete.Text == "1")
                {
                    Delete.Visible = true;
                    chk_delete.Visible = true;
                    chk_delete.Checked = false;
                }
                else
                {
                    Delete.Visible = false;
                    chk_delete.Visible = true;
                    chk_delete.Checked = false;
                }
                if (Is_Download.Text == "1")
                {
                    Download.Visible = true;
                    chk_Download.Visible = true;
                    chk_Download.Checked = false;
                }
                else
                {
                    Download.Visible = false;
                    chk_Download.Visible = false;
                    chk_Download.Checked = false;
                }
                if (lbl_Is_Print.Text == "1")
                {
                    Print.Visible = true;
                    chk_print.Visible = true;
                    chk_print.Checked = false;
                }
                else
                {
                    Print.Visible = false;
                    chk_print.Visible = false;
                    chk_print.Checked = false;
                }

            }
        }
    }
}