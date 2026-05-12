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

namespace school_web.Online_Test_admin
{
    public partial class Admin : System.Web.UI.MasterPage
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
                        string page_path = Path.GetFileName(Request.Url.AbsolutePath);
                        find_menu_id_for_active(page_path);
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
            string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            string[] New_originalPath1 = originalPath2.Split('?');
            string originalPath1 = New_originalPath1[0].ToString();


            hd_mg_id_active.Value = "0"; hd_m_id_active.Value = "0"; hd_is_right_menu.Value = "0";
            DataTable dt = My.dataTable("select Group_id,Menu_id  from Online_DASHBOARD_REPORT_MENU_ACTIVE where Page_name='" + page_path + "'");
            if (dt.Rows.Count > 0)
            {
                hd_mg_id_active.Value = dt.Rows[0]["Group_id"].ToString();
                hd_m_id_active.Value = dt.Rows[0]["Menu_id"].ToString();
                hd_is_right_mg_id.Value = dt.Rows[0]["Group_id"].ToString();
            }



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
                ViewState["MenuGrpId"] = "0";
                BindMenu("0");

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
                lbl_name13.Text = dtTemp.Rows[0]["firm_name"].ToString() + "{ Online Test Admin }";
                lbl_firm_name.Text = dtTemp.Rows[0]["firm_name"].ToString();
                imglogo.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                img_avatar.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                homeLnK.HRef = "Home.aspx";
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
            }
        }


        private void BindMenu(string groupId)
        {
            string sql = "";
            string sql1 = "";

            if (hdfUserType.Value == "Admin")
            {
                sql = "select   distinct Group_name,Group_icon,Group_id,Position,Colors from Menu_Group_List_web_OnlineTest where   Type=1 order by Position ";
                sql1 = "select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from dbo.[MenuMaster_web_OnlineTest] where Type=1  order by  Memnuposition";
            }
            else
            {
                sql = "select   distinct Group_name,Group_icon,Group_id,Position,Colors from Menu_Group_List_web_OnlineTest where   Group_id in (select MenuID from MenuPermissionForUser_OnlineTest where UserID='" + hdfUserID.Value + "') order by Position ";




                //  sql1 = "select *,(Select top 1 Menu_name from MenuMaster_web_OnlineTest where MenuID=MenuPermissionForUser_OnlineTest.MenuID and Type=1) as Menu_name ,(Select top 1 Memnuposition from MenuMaster_web_OnlineTest where MenuID=MenuPermissionForUser_OnlineTest.MenuID and Type=1) as Memnuposition" +
                //  "  from dbo.[MenuPermissionForUser_OnlineTest] where UserID ='" + hdfUserID.Value + "' order by  Memnuposition";

                sql1 = "Select ot.* from MenuMaster_web_OnlineTest ot join MenuPermissionForUser_OnlineTest otp on ot.Group_id=otp.MenuID where otp.UserID='" + hdfUserID.Value + "' and  ot.Type=1 order by ot.Memnuposition   ";
            }
            SqlCommand cmd = new SqlCommand(sql);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                RpMainMenu.DataSource = dtTemp;
                RpMainMenu.DataBind();
                SqlCommand cmd1 = new SqlCommand(sql1);
                DataTable dtAllowedMenu = imp.GetData(cmd1);
                BindSubmenu(dtAllowedMenu, groupId);
            }
            else
            {
                RpMainMenu.DataSource = null;
                RpMainMenu.DataBind();
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
                    cmd = new SqlCommand("select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from MenuMaster_web_OnlineTest where Group_id='" + hdID.Value + "' and  Type='1' order by Memnuposition");
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
                    Response.Redirect("~/Admin/home1.aspx", false);
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


                    if (hdMainMenu.Value == hd_mg_id_active.Value) 
                    {
                        ctrl.Attributes.Add("class", "mm-active");
                        //Repeater RpMenu = (Repeater)e.Item.FindControl("RpMenu"); 
                    }

                }
            }
            catch (Exception ex) { }
        }
    }
}