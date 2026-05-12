using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Examination_Admin
{
    public partial class Admin : System.Web.UI.MasterPage
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
                        lbl_session_name.Text = My.get_session();
                        get_uesr_type();
                        BindDetails();

                        try
                        {

                            imp.bind_all_ddl_with_id_notselect(ddl_session, "Select Session,session_id from session_details order by ID asc");
                            ddl_session.SelectedValue = My.get_session_id();
                        }
                        catch
                        {
                        }
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
            string query = "Select  * from user_details where user_id='" + Session["Admin"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                hdfUserType.Value = "Admin";
                hd_firm.Value = "0";
            }
            else
            {
                hdfUserType.Value = dtTemp.Rows[0]["User_Type"].ToString();
                lbl_Name.Text = dtTemp.Rows[0]["name"].ToString();
                lbl_namess.Text = dtTemp.Rows[0]["name"].ToString();
                hd_firm.Value = "1";
            }
        }



        private void BindDetails()
        {
            try
            {

                hdfUserID.Value = Session["Admin"].ToString();

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
            string query = "Select  * from Firm_Details ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = imp.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                lbl_name13.Text = dtTemp.Rows[0]["firm_name"].ToString() + "{ Examination Admin }";
                imglogo.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                img_avatar.ImageUrl = dtTemp.Rows[0]["logo"].ToString();
                ViewState["Pyment_system"] = dtTemp.Rows[0]["Pyment_system"].ToString();
                if (dtTemp.Rows[0]["Pyment_system"].ToString() == "New")
                {
                    homeLnK.HRef = "dashboard.aspx";
                }
                else
                {
                    homeLnK.HRef = "home.aspx";
                }
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
            }
        }



        private void BindMenu()
        {
            string sql = "";
            string sql1 = "";
            //if (hdfUserType.Value.ToUpper() != "TEACHER")
            //{

            //    sql = "select   Group_name,Group_icon,Group_id from Exam_Menu_Group_List_web where Type=1 order by Position ";
            //    sql1 = "select * from dbo.[Exam_MenuMaster_web] where Type=1  order by  Memnuposition";

            //}
            if (hdfUserType.Value.ToUpper() == "ADMIN")
            {

                sql = "select   Group_name,Group_icon,Group_id from Exam_Menu_Group_List_web where Type=1 order by Position ";
                sql1 = "select * from dbo.[Exam_MenuMaster_web] where Type=1  order by  Memnuposition";

            }

            //if (hdfUserType.Value == "Teacher")
            //{

            //    sql = "select   Group_name,Group_icon,Group_id from Exam_Menu_Group_List_web where Type=1 order by Position ";
            //    sql1 = "select * from dbo.[Exam_MenuMaster_web] where Type=1  order by  Memnuposition";

            //}

            else
            {

                sql = "select   Group_name,Group_icon,Group_id from Exam_Menu_Group_List_web where Group_id in (select MainMenuId from Exam_MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "'     ) order by Position ";
                sql1 = "select *,(Select top 1 Menu_name from Exam_MenuMaster_web where MenuID=Exam_MenuPermissionForUser_web.MenuID and Type=1) as Menu_name ,(Select top 1 Memnuposition from Exam_MenuMaster_web where MenuID=Exam_MenuPermissionForUser_web.MenuID and Type=1) as Memnuposition" +
                "  from dbo.[Exam_MenuPermissionForUser_web] where UserID ='" + hdfUserID.Value + "' order by  Memnuposition";

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

                //user special permission




                Bind_next_level_data();
            }
        }

        private void Bind_next_level_data()
        {
            string sql = "";
            string sql1 = "";
            sql = "select   Group_name,Group_icon,Group_id from Exam_Menu_Group_List_web where Group_id in (select MenuID from Exam_MenuPermissionForUser_web where UserID='" + hdfUserID.Value + "'     ) order by Position ";

            sql1 = "  select  distinct spmw.Menu_name,spmw.Menu_page,spmw.Memnuposition,spmw.MenuID,spmw.Group_id   from dbo.[Exam_MenuPermissionForUser_web] spmpu join Exam_MenuMaster_web spmw on spmpu.MenuID=spmw.Group_id where spmpu.UserID = '" + hdfUserID.Value + "' order by  spmw.Memnuposition";


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

                    cmd = new SqlCommand("select * from Exam_MenuMaster_web where Group_id='" + hdID.Value + "' and Type='1' order by Memnuposition");

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
            catch (Exception ex)
            { 
            }
        }

        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
            Response.Write("<script language=javascript>wnd.close();</script>");
            Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            My.exeSql("update session_details set Use_mode='1' where session_id=" + ddl_session.SelectedValue + "");

            My.exeSql("update session_details set Use_mode='0' where session_id!=" + ddl_session.SelectedValue + "");
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