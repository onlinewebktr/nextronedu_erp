using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Inventory_management
{
    public partial class Site2 : System.Web.UI.MasterPage
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
                        ViewState["Admin"] = Session["Admin"].ToString();
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
        private void get_uesr_type()
        {
            Image2.ImageUrl = "~/images/inventoryimages.jpg";
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
                    lbl_doctor_name.Text = lbl_doctor_name1.Text = "Admin";
                    lbl_designation.Text = "";
                    lbl_designation1.Text = "";
                    Image1.ImageUrl = "../images/blank.png";
                   

                }
                else
                {




                    lbl_doctor_name.Text= lbl_doctor_name1.Text = dtTemp.Rows[0]["name"].ToString();
                    lbl_designation.Text= lbl_designation1.Text = dtTemp.Rows[0]["User_Type"].ToString();

                    if(dtTemp.Rows[0]["ProfilePhoto"].ToString()=="")
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

                sql = "select   distinct Group_name,Group_icon,Group_id,Position from Sale_Purchase_Menu_Group_List_web where Group_id in (select MainMenuId from Sale_Purchase_MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "'     ) order by Position ";


                sql1 = "select *,(Select top 1 Menu_name from Sale_Purchase_MenuMaster_web where MenuID=Sale_Purchase_MenuPermissionForUser_web.MenuID and Type=1) as Menu_name ,(Select top 1 Memnuposition from Sale_Purchase_MenuMaster_web where MenuID=Sale_Purchase_MenuPermissionForUser_web.MenuID and Type=1) as Memnuposition   from dbo.[Sale_Purchase_MenuPermissionForUser_web] where UserID = '" + hdfUserID.Value + "' order by  Memnuposition";


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



    }
}