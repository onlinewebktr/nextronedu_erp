using Microsoft.VisualBasic.ApplicationServices;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.InstructorProfile
{
    public partial class Teacher_Profile : System.Web.UI.MasterPage
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Session["Admin"] = HttpContext.Current.User.Identity.Name;
                Session["userType"] = HttpContext.Current.User.Identity.getUserData("UserType");
                Session["branchid"] = HttpContext.Current.User.Identity.getUserData("branchid");
                Session["firm"] = HttpContext.Current.User.Identity.getUserData("Firm");
            }
            if (Session["teacher"] == null)
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
                    if (!IsPostBack)
                    {
                        string AdminCode = Session["teacher"].ToString();
                        ViewState["teacher"] = AdminCode;
                        find_name(AdminCode);
                        Get_name();
                        lbl_count(AdminCode);

                        // check_class_teacher(AdminCode);
                        try
                        {
                            check_onlinetest(AdminCode);
                        }
                        catch
                        {

                        }
                        
                        menu_permission_admin();// admin
                        Menu_Permission();// exam

                        menu_permission_lms_payroll();// lms_and_payroll

                        get_menu();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        //private void check_class_teacher(string AdminCode)
        //{
        //    string query = "Select   * from Ptm_class_teacher_mapping where UserID='" + AdminCode + "'";
        //    SqlCommand cmd = new SqlCommand(query);
        //    DataTable dtTemp = UsesCode.GetData(cmd);
        //    if (dtTemp.Rows.Count == 0)
        //    {
        //        Li2.Visible = false;
        //    }
        //    else
        //    {
        //        Li2.Visible = true;
        //    }
        //}
        private void check_onlinetest(string AdminCode)
        {
            string query = "Select   * from MenuPermissionForUser_OnlineTest where UserID='" + AdminCode + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                Li2.Visible = false;
            }
            else
            {
                Li2.Visible = true;
            }
        }



        private void get_menu()
        {
            try
            {
                string query = "Select  IsAskdoubt from Firm_Details";
                SqlCommand cmd = new SqlCommand(query);
                DataTable dtTemp = UsesCode.GetData(cmd);
                if (dtTemp.Rows.Count != 0)
                {

                    if (dtTemp.Rows[0]["IsAskdoubt"].ToString() == "1")
                    {
                        Li7_Doubt.Visible = true;
                    }
                    else
                    {
                        Li7_Doubt.Visible = false;
                    }


                }
                else
                {

                    Li7_Doubt.Visible = false;

                }
            }
           catch
            {
                Li7_Doubt.Visible = true;
            }

        }



        private void lbl_count(string AdminCode)
        {
            lbl_countdata.Text = code.Find_Name("select count(id) from dbo.[Student_doubt_list] where  Teacher_Id='" + AdminCode + "' and Status='Pending' ");
        }

        private void Get_name()
        {
            string query = "Select firm_name,logo,address1,Footer_Copy_Right,Footer_URL_Link from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {

                lbl_school_name.Text = dtTemp.Rows[0]["firm_name"].ToString();
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
                a1.HRef = dtTemp.Rows[0]["Footer_URL_Link"].ToString();
            }
        }
        private void find_name(string teacher)
        {
            try
            {
                string sql = "select * from user_details where user_id ='" + teacher + "'";
                DataTable dt = code.FillTable(sql);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    // do nothing
                }
                else
                {
                    lbl_membername.Text = dt.Rows[0]["name"].ToString();
                    lbl_membercode.Text = "ID : " + teacher;
                    lbl_welcome.Text = dt.Rows[0]["name"].ToString();
                    if (dt.Rows[0]["name"].ToString().Length > 12)
                    {
                        lbl_membername.Text = dt.Rows[0]["user_id"].ToString().Split(' ')[0].ToString();
                    }

                    imgMember.ImageUrl = "/images/pic.jpg";
                }
            }
            catch
            {
            }
        }
        protected void lnk_btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
            Response.Write("<script language=javascript>wnd.close();</script>");
            Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");

        }


        private void Menu_Permission()
        {
            string query = "Select  UserID from Exam_MenuPermissionForUser_web where UserID='" + ViewState["teacher"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                lnk_exam_admin.Visible = false;
            }
            else
            {

                lnk_exam_admin.Visible = false;

            }
        }

        private void menu_permission_admin()
        {
            string query = "Select  top 1 UserID from MenuPermissionForUser_web where UserID='" + ViewState["teacher"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                lnk_Main_admin.Visible = false;
            }
            else
            {

                lnk_Main_admin.Visible = true;

            }
        }
        private void menu_permission_lms_payroll()
        {
            string query = "Select  UserID from Special_Permission where UserID='" + ViewState["teacher"].ToString() + "' and Menu='LMS'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

                lnk_lms.Visible = false;



            }
            else
            {

                lnk_lms.Visible = true;

            }


            //-----------------------------

            //string query1 = "Select  UserID from Special_Permission where UserID='" + ViewState["teacher"].ToString() + "' and Menu='LMS'";
            //SqlCommand cmd1 = new SqlCommand(query1);
            //DataTable dtTemp1 = UsesCode.GetData(cmd1);
            //if (dtTemp1.Rows.Count == 0)
            //{
            //    ln_payroll.Visible = false;
            //}
            //else
            //{

            //    ln_payroll.Visible = true;

            //}

        }

        protected void lnk_gotoonlinetest_Click1(object sender, EventArgs e)
        {
            Session["Admin"] = Session["teacher"].ToString();
            Response.Redirect("~/Examination_Admin/Home.aspx", false);
        }

        protected void lnk_go_main_admin_Click(object sender, EventArgs e)
        {
            Session["Admin"] = Session["teacher"].ToString();
            Response.Redirect("../Admin/home1.aspx", false);
        }



        protected void lnk_go_to_lms_admin_Click(object sender, EventArgs e)
        {
            Session["Admin"] = Session["teacher"].ToString();
            Response.Redirect("../LMS_VC_Admin/Dashboard.aspx", false);
        }


        My mycode = new My();
        protected void lnk_go_to_paroll_Click(object sender, EventArgs e)
        {
            string query = "Select  * from HR_UserProfile where EmployeeCode='" + ViewState["teacher"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {

            }
            else
            {
                FormsAuthentication.SetAuthCookie(dtTemp.Rows[0]["UserId"].ToString(), false);
                Session["UserType"] = dtTemp.Rows[0]["UserType"].ToString();
                Session["UserName"] = dtTemp.Rows[0]["Name"].ToString();
                Session["Userid"] = dtTemp.Rows[0]["UserId"].ToString();
                Session["IsHR"] = dtTemp.Rows[0]["IsHr"].ToString();
                Session["UserProfileImage"] = dtTemp.Rows[0]["ProfileImage"].ToString();
                // string url = "../home";
                string url = "../HR/home";
                Response.Redirect(url, false);

                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);

                //    Response.Redirect("home.aspx", false);
            }
            //Session["Admin"] = Session["teacher"].ToString();
            //Response.Redirect("../Payroll/Home.aspx", false);
        }

        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("logout.aspx");

        }

        protected void lank_onlinetestadmin_Click(object sender, EventArgs e)
        {


            Session["Admin"] = Session["teacher"].ToString();
            Response.Redirect("~/Online_Test_admin/Home.aspx", false);


        }
    }
}