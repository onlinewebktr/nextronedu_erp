using school_web.AppCode;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace school_web.Admin
{
    public partial class main : System.Web.UI.MasterPage
    {
        My imp = new My();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Session["Admin"] = HttpContext.Current.User.Identity.Name;
                Session["userType"] = HttpContext.Current.User.Identity.getUserData("UserType");
                Session["branchid"] = HttpContext.Current.User.Identity.getUserData("branchid");
                Session["firm"] = HttpContext.Current.User.Identity.getUserData("Firm");
            }
            if (!IsPostBack)
            {
                try
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
                        try
                        {
                            string page_path = Path.GetFileName(Request.Url.AbsolutePath);
                            find_menu_id_for_active(page_path);
                        }
                        catch
                        {
                        }

                        imp.bind_all_ddl_with_id_notselect(ddl_session, "Select Session,session_id from session_details order by ID asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        lbl_session_name.Text = My.get_session();
                        get_uesr_type();
                        BindDetails();
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Main Master admin");
                }
            }
        }

        private void find_menu_id_for_active(string page_path)
        {
            hd_mg_id_active.Value = "0"; hd_m_id_active.Value = "0"; hd_is_right_menu.Value = "0";
            DataTable dt = My.dataTable("select Group_id,Menu_id,isnull((select top 1 Is_right_menu from Menu_Group_List_web where Group_id=Dashboard_report_menu_active.Group_id),'0') as Is_right_menu from Dashboard_report_menu_active where Page_name='" + page_path + "'");
            if (dt.Rows.Count > 0)
            {
                hd_mg_id_active.Value = dt.Rows[0]["Group_id"].ToString();
                hd_m_id_active.Value = dt.Rows[0]["Menu_id"].ToString();
                hd_is_right_menu.Value = dt.Rows[0]["Is_right_menu"].ToString();
                hd_is_right_mg_id.Value = dt.Rows[0]["Group_id"].ToString();
            }


            if (hd_is_right_menu.Value == "0")
            {
                DataTable dtg = My.dataTable("select Group_id,(select top 1 Is_right_menu from Menu_Group_List_web where Group_id=MenuMaster_web.Group_id) as Is_right_menu from MenuMaster_web where Menu_page='" + page_path + "'");
                if (dtg.Rows.Count > 0)
                {
                    if (dtg.Rows[0]["Is_right_menu"].ToString() != "")
                    {
                        hd_is_right_menu.Value = dtg.Rows[0]["Is_right_menu"].ToString();
                    }
                    hd_is_right_mg_id.Value = dtg.Rows[0]["Group_id"].ToString();
                }
            }
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter ad = new SqlDataAdapter("Select  * from session_details where session_id='" + ddl_session.SelectedValue + "'", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Salesman_Master");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                //string Use_mode = dt.Rows[0]["Use_mode"].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Use_mode"] = 1;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt); 
                My.exeSql("update session_details set Use_mode='0' where session_id!=" + ddl_session.SelectedValue + "");
            }


            Response.Redirect("home.aspx", false);

        }
        private void get_uesr_type()
        {
            string query = "Select  * from user_details where user_id='" + Session["Admin"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                backBtnS.Visible = false;
                lnkbacktoteacher.Visible = false;
                hdfUserType.Value = "";
            }
            else
            {
                hdfUserType.Value = dtTemp.Rows[0]["User_Type"].ToString();
                Session["userTypeFee"] = dtTemp.Rows[0]["User_Type"].ToString();
                lbl_Name.Text = dtTemp.Rows[0]["name"].ToString();
                lbl_namess.Text = dtTemp.Rows[0]["name"].ToString();
                if (dtTemp.Rows[0]["User_Type"].ToString() == "Teacher")
                {
                    lnkbacktoteacher.Visible = true;
                    backBtnS.Visible = true;
                }
                else
                {
                    lnkbacktoteacher.Visible = false;
                    backBtnS.Visible = false;
                }
            }
        }



        private void BindDetails()
        {
            try
            {
                hdfUserID.Value = Session["Admin"].ToString();
                Get_name();

                if (hd_is_right_menu.Value != "0")
                {
                    ViewState["MenuGrpId"] = hd_is_right_mg_id.Value;
                    BindMenu(hd_is_right_mg_id.Value);
                }
                else
                {
                    ViewState["MenuGrpId"] = "0";
                    BindMenu("0");
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Main Master admin");

            }
        }


        private void Get_name()
        {
            string query = "Select  * from Firm_Details  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                lbl_name13.Text = dtTemp.Rows[0]["firm_name"].ToString();
                lbl_firm_name.Text = dtTemp.Rows[0]["firm_name"].ToString();
                imglogo.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                img_avatar.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                ViewState["Pyment_system"] = dtTemp.Rows[0]["Pyment_system"].ToString();
                if (dtTemp.Rows[0]["Pyment_system"].ToString() == "New")
                {
                    homeLnK.HRef = "dashboard.aspx";
                }
                else
                {
                    homeLnK.HRef = "home1.aspx";
                }
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();

                try
                {
                    if (dtTemp.Rows[0]["Is_notification_show"].ToString() == "1")
                    {
                        todayDate.InnerText = mycode.date();
                        fetch_notification();
                    }
                    else
                    {
                        notificationBell.Attributes.Add("class", "hidden");
                    }
                }
                catch (Exception ex)
                {
                    notificationBell.Attributes.Add("class", "hidden");
                }


               /* try
                {
                    if (dtTemp.Rows[0]["Online_payment_failed_Notification_show"].ToString() == "1")
                    {
                        todayDate.InnerText = mycode.date();
                        fetch_payment_failed();
                    }
                    else
                    {
                        notificationBell_payment.Attributes.Add("class", "hidden");
                    }
                }
                catch
                {
                    notificationBell_payment.Attributes.Add("class", "hidden");

                }*/
            }
        }

      /*  private void fetch_payment_failed()
        {
            ttlCount_payment.InnerText = "0";
            ttlCount_payment_failed.InnerText =   "0";
            Paymentfailed.InnerText =   "0";

            DataTable dt = My.dataTable("select count(Id) as Count from Payment_transaction_process where status='Pending' and Session_id='"+ My.get_session_id() + "'");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                ttlCount_payment.InnerText = dt.Rows[0][0].ToString();
                ttlCount_payment_failed.InnerText = dt.Rows[0][0].ToString();
                Paymentfailed.InnerText = dt.Rows[0][0].ToString();
            }
        }
*/
        My mycode = new My();
        private void fetch_notification()
        {
            ttlCount.InnerText = "0";
            evnttoStdCount.InnerText = "0";
            noticetoStdCount.InnerText = "0";
            msgtoStdCount.InnerText = "0";
            teachrLeveRqcount.InnerText = "0";
            double totalC = 0;
            string todayIdate = mycode.idate();
            //DataTable dt = My.dataTable("select count(Id) as Count from News_Events_Details union all  select count(Id) as Count from Notice_Board_Details union all select count(Id) as Count from Private_Messages union all select count(Id) as Count from Staff_leave_details");
            DataTable dt = My.dataTable("select count(Id) as Count from News_Events_Details where Posted_Idate='" + todayIdate + "' union all  select count(Id) as Count from Notice_Board_Details where Posted_Idate='" + todayIdate + "'  union all select count(Id) as Count from Private_Messages where Idate='" + todayIdate + "'  union all select count(Id) as Count from Staff_leave_details where idate='" + todayIdate + "'");
            if (dt.Rows.Count > 0)
            {
                evnttoStdCount.InnerText = dt.Rows[0][0].ToString();
                noticetoStdCount.InnerText = dt.Rows[1][0].ToString();
                msgtoStdCount.InnerText = dt.Rows[2][0].ToString();
                teachrLeveRqcount.InnerText = dt.Rows[3][0].ToString();

                totalC = (My.toDouble(dt.Rows[0][0].ToString()) + My.toDouble(dt.Rows[1][0].ToString()) + My.toDouble(dt.Rows[2][0].ToString()) + My.toDouble(dt.Rows[3][0].ToString()));
                ttlCount.InnerText = totalC.ToString();
            }
        }

        private void BindMenu(string groupId)
        {
            string sql = ""; string sqlSideMenu = ""; string sqlTopMenu = "";
            string sql1 = "";
            if (groupId == "0")
            {
                if (hdfUserType.Value == "Admin")
                {
                    sql = "select   distinct Group_name,Group_icon,Group_id,Position,Colors from Menu_Group_List_web where (Is_right_menu='0' or Is_right_menu is null) and (Is_top_menu='0' or Is_top_menu is null) and  Type=1 order by Position ";
                    sql1 = "select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from dbo.[MenuMaster_web] where Type=1  order by  Memnuposition";



                    sqlSideMenu = "select * from Side_manu_master where Is_menu_menu=1 and Status=1 order by Position asc";
                    //sqlSideMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link,CASE WHEN Is_top_menu = '1' THEN 'hideInDeskTop' END AS ForTopMenu from Menu_Group_List_web where (Is_right_menu=1 or Is_top_menu=1) and Type=1 order by Position ";
                    sqlTopMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link from Menu_Group_List_web where Is_top_menu=1 and  Type=1 order by Position ";
                }
                else
                {
                    sql = "select   distinct Group_name,Group_icon,Group_id,Position,Colors from Menu_Group_List_web where (Is_right_menu='0' or Is_right_menu is null) and (Is_top_menu='0' or Is_top_menu is null) and  Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') order by Position ";
                    sql1 = "select *,(Select top 1 Menu_name from MenuMaster_web where MenuID=MenuPermissionForUser_web.MenuID and Type=1) as Menu_name ,(Select top 1 Memnuposition from MenuMaster_web where MenuID=MenuPermissionForUser_web.MenuID and Type=1) as Memnuposition" +
                    "  from dbo.[MenuPermissionForUser_web] where UserID ='" + hdfUserID.Value + "' order by  Memnuposition";

                    sqlSideMenu = "select * from(select * from Side_manu_master where Status=1 and Is_menu_menu=1 and Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') union all select distinct sm.* from Side_manu_master sm join Side_manu_master sm1 on sm.Group_id = sm1.Main_group_id  where sm.Status = 1 and sm1.Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID = '" + hdfUserID.Value + "'))t order by Position asc";

                    //sqlSideMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link,CASE WHEN Is_top_menu = '1' THEN 'hideInDeskTop' END AS ForTopMenu from Menu_Group_List_web where (Is_right_menu=1 or Is_top_menu=1) and Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') order by Position ";
                    sqlTopMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link from Menu_Group_List_web where Is_top_menu=1 and Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') order by Position ";
                }
            }
            else
            {
                if (hdfUserType.Value == "Admin")
                {
                    sql = "select distinct Group_name,Group_icon,Group_id,Position,Colors from Menu_Group_List_web where Type=1 and Group_id='" + groupId + "' order by Position ";
                    sql1 = "select distinct Menu_name, Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from dbo.[MenuMaster_web] where Type=1  order by  Memnuposition";

                    sqlSideMenu = "select * from Side_manu_master where Is_menu_menu=1 and Status=1 order by Position asc";
                    //sqlSideMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link,CASE WHEN Is_top_menu = '1' THEN 'hideInDeskTop' END AS ForTopMenu from Menu_Group_List_web where (Is_right_menu=1 or Is_top_menu=1) and Type=1 order by Position ";
                    sqlTopMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link from Menu_Group_List_web where Is_top_menu=1 and  Type=1 order by Position ";
                }
                else
                {
                    sql = "select distinct Group_name,Group_icon,Group_id,Position,Colors from Menu_Group_List_web where Type=1 and Group_id='" + groupId + "' and  Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') order by Position ";
                    sql1 = "select *,(Select top 1 Menu_name from MenuMaster_web where MenuID=MenuPermissionForUser_web.MenuID and Type=1) as Menu_name ,(Select top 1 Memnuposition from MenuMaster_web where MenuID=MenuPermissionForUser_web.MenuID and Type=1) as Memnuposition" +
                    "  from dbo.[MenuPermissionForUser_web] where UserID ='" + hdfUserID.Value + "' order by  Memnuposition";

                    sqlSideMenu = "select * from(select * from Side_manu_master where Status=1 and Is_menu_menu=1 and Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') union all select distinct sm.* from Side_manu_master sm join Side_manu_master sm1 on sm.Group_id = sm1.Main_group_id  where sm.Status = 1 and sm1.Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID = '" + hdfUserID.Value + "'))t order by Position asc";
                    sqlTopMenu = "select distinct Group_name,Group_icon,Group_id,Position,Colors,Page_link from Menu_Group_List_web where Is_top_menu=1 and Group_id in (select MainMenuId from MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "') order by Position ";
                }
            }

            SqlCommand cmd = new SqlCommand(sql);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                RpMainMenu.DataSource = dtTemp; RpMainMenu.DataBind();
                SqlCommand cmd1 = new SqlCommand(sql1);
                DataTable dtAllowedMenu = imp.GetData(cmd1);
                BindSubmenu(dtAllowedMenu, groupId);
            }
            else
            {
                RpMainMenu.DataSource = null;
                RpMainMenu.DataBind();
            }
            get_side_menu(sqlSideMenu);
            get_top_menu(sqlTopMenu);

        }
        private void get_top_menu(string sqlTopMenu)
        {
            SqlCommand cmd = new SqlCommand(sqlTopMenu);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count > 0)
            {
                rp_top_menu.DataSource = dtTemp;
                rp_top_menu.DataBind();
            }
            else
            {
                rp_top_menu.DataSource = null;
                rp_top_menu.DataBind();
            }
        }


        private void get_side_menu(string sqlSideMenu)
        {
            SqlCommand cmd = new SqlCommand(sqlSideMenu);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count > 0)
            {
                rp_side_menu.DataSource = dtTemp;
                rp_side_menu.DataBind();
            }
            else
            {
                rp_side_menu.DataSource = null;
                rp_side_menu.DataBind();
            }
        }

        private void BindSubmenu(DataTable dtc, string groupId)
        {
            try
            {
                SqlCommand cmd;
                foreach (RepeaterItem item in RpMainMenu.Items)
                {
                    HiddenField hdID = (HiddenField)item.FindControl("hdMainMenu");
                    Repeater childRepeater = (Repeater)item.FindControl("RpMenu");



                    if (groupId == "0")
                    {
                        if (ViewState["Pyment_system"].ToString() == "New")
                        {
                            cmd = new SqlCommand("select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from MenuMaster_web where Group_id='" + hdID.Value + "' and Type='1' order by Memnuposition");
                        }
                        else
                        {
                            cmd = new SqlCommand("select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from MenuMaster_web where Group_id='" + hdID.Value + "' and  Type='1' order by Memnuposition");
                        }
                    }
                    else
                    {
                        if (ViewState["Pyment_system"].ToString() == "New")
                        {
                            cmd = new SqlCommand("select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from MenuMaster_web where Group_id='" + hdID.Value + "' and Type='1' order by Memnuposition");
                        }
                        else
                        {
                            cmd = new SqlCommand("select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from MenuMaster_web where Group_id='" + hdID.Value + "' and  Type='1' order by Memnuposition");
                        }
                    }

                    DataTable dt = imp.GetData(cmd);
                    DataTable dtAllowedmenu = new DataTable();
                    dtAllowedmenu.Clear();
                    dtAllowedmenu.Columns.Add("Menu_page");
                    dtAllowedmenu.Columns.Add("MenuID");
                    dtAllowedmenu.Columns.Add("Menu_name");
                    dtAllowedmenu.Columns.Add("Menu_active");

                    foreach (DataRow row in dt.Rows)
                    {
                        string menuactV = "";
                        string memnu = row["MenuID"].ToString();
                        if (hd_m_id_active.Value == memnu)
                        {
                            menuactV = "mm-active";
                        }
                        DataRow[] result = dtc.Select("MenuID='" + memnu + "' ");
                        if (result.Length == 1)
                        {
                            DataTable ddd = result.CopyToDataTable();
                            DataRow al_menu = dtAllowedmenu.NewRow();
                            al_menu["Menu_page"] = row["Menu_page"].ToString();
                            al_menu["MenuID"] = row["MenuID"].ToString();
                            al_menu["Menu_name"] = row["Menu_name"].ToString();
                            al_menu["Menu_active"] = menuactV;

                            dtAllowedmenu.Rows.Add(al_menu);
                        }
                    }

                    if (dtAllowedmenu.Rows.Count > 0)
                    {
                        childRepeater.DataSource = dtAllowedmenu;
                        childRepeater.DataBind();
                    }
                }
            }
            catch { }
        }

        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
            Response.Write("<script language=javascript>wnd.close();</script>");
            Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
        }



        protected void lnkbacktoteacher_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdfUserType.Value == "Principal")
                {
                    Session["teacher"] = Session["Admin"].ToString();
                    Session["userType"] = "Principal";
                    Response.Redirect("~/InstructorProfile/Home.aspx", false);
                }
                else if (hdfUserType.Value == "Coordinator")
                {
                    Session["teacher"] = Session["Admin"].ToString();
                    Session["userType"] = "Coordinator";
                    Response.Redirect("~/InstructorProfile/Home.aspx", false);
                }
                else if (hdfUserType.Value == "Teacher")
                {
                    Session["teacher"] = Session["Admin"].ToString();
                    Session["userType"] = "Teacher";
                    Response.Redirect("~/InstructorProfile/Home.aspx", false);
                }
                else
                {
                    Response.Redirect("~/Admin/home.aspx", false);
                }
            }
            catch
            {
            }
        }

        protected void RpMainMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;

                    HiddenField hdMainMenu = (HiddenField)e.Item.FindControl("hdMainMenu");
                    HtmlGenericControl ctrl = e.Item.FindControl("mainLi") as HtmlGenericControl;

                    if (ViewState["MenuGrpId"].ToString() == "0")
                    { }
                    else
                    {
                        if (hdMainMenu.Value == ViewState["MenuGrpId"].ToString())
                        {
                            ctrl.Attributes.Add("class", "mm-active");
                            //Repeater RpMenu = (Repeater)e.Item.FindControl("RpMenu"); 
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void rp_side_menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_group_id = (Label)e.Item.FindControl("lbl_group_id");
                Repeater rp_side_submenu = (Repeater)e.Item.FindControl("rp_side_submenu");
                HtmlGenericControl subMenuUL = e.Item.FindControl("subMenuUL") as HtmlGenericControl;
                HtmlGenericControl menMenuLi = e.Item.FindControl("menMenuLi") as HtmlGenericControl;
                //Panel pnl_submenu = (Panel)e.Item.FindControl("pnl_submenu");



                DataTable dtSMenu = My.dataTable("select * from Side_manu_master where  Main_group_id='" + lbl_group_id.Text + "' and Is_sub_menu=1 and Status=1 order by Position asc");
                subMenuUL.Visible = false;
                if (dtSMenu.Rows.Count > 0)
                {
                    subMenuUL.Visible = true;
                    rp_side_submenu.DataSource = dtSMenu;
                    rp_side_submenu.DataBind();
                }
                else
                {
                    menMenuLi.Attributes.Remove("menu-item-has-children");
                }

                if (lbl_group_id.Text == "12"|| lbl_group_id.Text == "21" || lbl_group_id.Text == "33" || lbl_group_id.Text == "10" || lbl_group_id.Text == "22")
                {
                    menMenuLi.Attributes.Add("class", "hide-in-desk"); 
                }
            }
        }
    }
}