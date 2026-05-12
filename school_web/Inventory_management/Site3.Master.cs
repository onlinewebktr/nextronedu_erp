using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Site3 : System.Web.UI.MasterPage
    {
        My imp = new My();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Admin"] = "Admin";
            //Session["name"] = "Admin";
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
                        ViewState["Admin"] = Session["Admin"].ToString();
                        Session["name"]= Session["Admin"].ToString();
                        imp.bind_all_ddl_with_id_notselect(ddl_session, "Select Session,session_id from session_details order by ID asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        lbl_session_name.Text = My.get_session();
                        get_uesr_type();
                        BindDetails();
                        Get_name();

                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Main Master admin");
                }
            }

        }

        private void Get_name()
        {
            string query = "Select  * from Firm_Details  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {

                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
            }
            else
            {
                lbl_footer.Text = "";

            }
        }

        private void get_uesr_type()
        {
            imglogo.ImageUrl = "~/images/inventoryimages.jpg";
            string query = "Select  * from user_details where user_id='" + ViewState["Admin"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

                hdfUserType.Value = "Admin";
            }
            else
            {
                hdfUserType.Value = dtTemp.Rows[0]["User_Type"].ToString();
                Session["userTypeFee"] = dtTemp.Rows[0]["User_Type"].ToString();
                if (dtTemp.Rows[0]["User_Type"].ToString() == "Admin")
                {
                    lbl_Name.Text = lbl_Name.Text = "Admin";
                    Image1.ImageUrl = "../images/blank.png";
                }
                else
                {

                    lbl_Name.Text = dtTemp.Rows[0]["name"].ToString();
                    lbl_designation.Text = dtTemp.Rows[0]["User_Type"].ToString();

                    if (dtTemp.Rows[0]["ProfilePhoto"].ToString() == "")
                    {
                        Image1.ImageUrl = "../images/blank.png";
                    }
                    else
                    {
                        Image1.ImageUrl = dtTemp.Rows[0]["ProfilePhoto"].ToString();
                    }


                }

            }
        }
        private void BindDetails()
        {
            string sql = "";
            string sql1 = "";
            if (hdfUserType.Value == "Admin")
            {

                sql = "select   distinct Group_name,Group_icon,Group_id,Position from Sale_Purchase_Menu_Group_List_web where Type=1 order by Position ";
                sql1 = "select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from dbo.[Sale_Purchase_MenuMaster_web] where Type=1  order by  Memnuposition";

            }
            else
            {
                sql = "select   distinct Group_name,Group_icon,Group_id,Position from Sale_Purchase_Menu_Group_List_web where Group_id in (select MenuID from Sale_Purchase_MenuPermissionForUser_web where UserID='" + ViewState["Admin"].ToString() + "'     ) order by Position ";
                sql1 = "select  distinct spmw.Menu_page,spmw.Menu_name,spmw.Memnuposition,spmw.MenuID,spmw.Group_id  from dbo.[Sale_Purchase_MenuPermissionForUser_web] spmpu join SALE_PURCHASE_MENUMASTER_WEB spmw on spmpu.MenuID=spmw.Group_id where spmpu.UserID = '" + ViewState["Admin"].ToString() + "' order by  spmw.Memnuposition";
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

                    cmd = new SqlCommand("select distinct Menu_name,Menu_page,Menu_icon,Group_id,Type,MenuID,Memnuposition from Sale_Purchase_MenuMaster_web where Group_id='" + hdID.Value + "' and  Type='1' order by Memnuposition");

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
    }
}