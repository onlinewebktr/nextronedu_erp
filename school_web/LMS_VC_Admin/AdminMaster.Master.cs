using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
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
                if (!IsPostBack)
                {
                    string AdminCode = Session["Admin"].ToString();
                    find_name(AdminCode);
                    Get_name();
                    try
                    {
                        Menu_permission();
                    }
                    catch
                    {
                    }
                    get_count_ask_dout();

                }
            }
        }

        private void get_count_ask_dout()
        {
            lbl_countdata.Text = code.Find_Name("select count(id) from dbo.[Student_doubt_list] where  Status='Pending' and Session_id='"+My.get_session_id()+"' ");
        }

        private void Menu_permission()
        {
            string query = "Select * from Comapny_Profile";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
              

                 

                 

                
                if (Convert.ToBoolean(dtTemp.Rows[0]["Is_Doubt"].ToString()) == true)
                {
                    Li7_Doubt.Visible = true;
                }
                else
                {
                    Li7_Doubt.Visible = false;
                }

                if (Convert.ToBoolean(dtTemp.Rows[0]["Complain"].ToString()) == true)
                {
                    Li7Complain.Visible = true;
                }
                else
                {
                    Li7Complain.Visible = false;
                }



            }
            else
            {
                 
                //addteacher1.Visible = true;
                Li7Complain.Visible = false;
                Li7_Doubt.Visible = false;

            }

            
        }

       

        private void Get_name()
        {
            string query = "Select firm_name,Footer_Copy_Right from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {

                lbl_school_name.Text = dtTemp.Rows[0]["firm_name"].ToString();
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();

            }
        }
        private void find_name(string AdminCode)
        {
            try
            {
                string sql = "select * from user_details where user_id ='" + AdminCode + "'";
                DataTable dt = code.FillTable(sql);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    // do nothing
                }
                else
                {

                    lbl_membername.Text = dt.Rows[0]["name"].ToString();
                    lbl_membercode.Text = "ID : " + AdminCode;
                    lbl_welcome.Text = dt.Rows[0]["name"].ToString();
                    if (dt.Rows[0]["name"].ToString().Length > 12)
                    {
                        lbl_membername.Text = dt.Rows[0]["name"].ToString().Split(' ')[0].ToString();
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