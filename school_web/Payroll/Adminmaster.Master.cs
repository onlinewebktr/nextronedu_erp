using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class Adminmaster : System.Web.UI.MasterPage
    {

        My imp = new My();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                        get_uesr_type();
                        BindDetails();
                        fetch_firmdata();
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Main Master admin");
                }
            }
        }

        private void fetch_firmdata()
        {
            string query = "Select  * from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {


                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();

            }
        }

        private void get_uesr_type()
        {
            string query = "Select  * from user_details where user_id='" + Session["Admin"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                hdfUserType.Value = "";
            }
            else
            {
                hdfUserType.Value = dtTemp.Rows[0]["User_Type"].ToString();
                if (dtTemp.Rows[0]["User_Type"].ToString() == "Admin")
                {
                    lbl_Name.Text = "Admin";
                }
                else
                {
                    lbl_Name.Text = dtTemp.Rows[0]["name"].ToString();
                }
            }
        }
        private void BindDetails()
        {
            try
            {

                hdfUserID.Value = Session["Admin"].ToString();
                hd_firm.Value = Session["firm"].ToString();
                Get_name();
                BindMenu();
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Main Master admin");

            }
        }

        private void Get_name()
        {
            string query = "Select  * from Firm_Details where firm_id='" + hd_firm.Value + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                lbl_name13.Text = dtTemp.Rows[0]["firm_name"].ToString();
                imglogo.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                img_avatar.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                ViewState["Pyment_system"] = dtTemp.Rows[0]["Pyment_system"].ToString();
                Session["firm_name"] = dtTemp.Rows[0]["firm_name"].ToString();
                Session["firmLocation"] = dtTemp.Rows[0]["address1"].ToString();
                Session["firmLogo"] = dtTemp.Rows[0]["logo"].ToString();
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
                homeLnK.HRef = "Home.aspx";
            }
        }



        private void BindMenu()
        {
            string sql = "";
            string sql1 = "";
            if (hdfUserType.Value == "Admin")
            {
                sql = "select   Group_name,Group_icon,Group_id from Menu_Group_List where Type=1    order by Position ";
                sql1 = "select * from dbo.[MenuMaster] where Type=1  order by  Memnuposition";
               
            }
            else
            {
                sql = "select   Group_name,Group_icon,Group_id from Menu_Group_List where Type=1    order by Position ";
                sql1 = "select * from dbo.[MenuMaster] where Type=1  order by  Memnuposition";

                //sql = "select   Group_name,Group_icon,Group_id from Menu_Group_List where Group_id in (select MainMenuId from MenuPermissionForUser where UserID='" + hdfUserID.Value + "' and and Group_id not in (6,7) ) order by Position ";
                //sql1 = "select *,(Select top 1 Menu_name from MenuMaster where MenuID=MenuPermissionForUser_web.MenuID and Type=1) as Menu_name ,(Select top 1 Memnuposition from MenuMaster where MenuID=MenuPermissionForUser.MenuID and Type=1) as Memnuposition" +
                //"  from dbo.[MenuPermissionForUser] where UserID ='" + hdfUserID.Value + "' order by  Memnuposition";
            }

            SqlCommand cmd = new SqlCommand(sql);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                RpMainMenu.DataSource = dtTemp; RpMainMenu.DataBind();
                SqlCommand cmd1 = new SqlCommand(sql1);
                DataTable dtAllowedMenu = imp.GetData(cmd1);
                BindSubmenu(dtAllowedMenu);
            }
            else
            {
                RpMainMenu.DataSource = null;
                RpMainMenu.DataBind();
            }
        }


        private void BindSubmenu(DataTable dtc)
        {
            try
            {
                SqlCommand cmd;
                foreach (RepeaterItem item in RpMainMenu.Items)
                {
                    HiddenField hdID = (HiddenField)item.FindControl("hdMainMenu");
                    Repeater childRepeater = (Repeater)item.FindControl("RpMenu");

                    cmd = new SqlCommand("select * from MenuMaster where Group_id='" + hdID.Value + "'   and Type='1' order by Memnuposition");

                    DataTable dt = imp.GetData(cmd);
                    DataTable dtAllowedmenu = new DataTable();
                    dtAllowedmenu.Clear();
                    dtAllowedmenu.Columns.Add("Menu_page");
                    dtAllowedmenu.Columns.Add("MenuID");
                    dtAllowedmenu.Columns.Add("Menu_name");

                    foreach (DataRow row in dt.Rows)
                    {
                        string memnu = row["MenuID"].ToString();
                        DataRow[] result = dtc.Select("MenuID='" + memnu + "' ");
                        if (result.Length == 1)
                        {
                            DataTable ddd = result.CopyToDataTable();
                            DataRow al_menu = dtAllowedmenu.NewRow();
                            al_menu["Menu_page"] = row["Menu_page"].ToString();
                            al_menu["MenuID"] = row["MenuID"].ToString();
                            al_menu["Menu_name"] = row["Menu_name"].ToString();

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

        protected void lnk_back_to_main_admin_Click(object sender, EventArgs e)
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


    }
}