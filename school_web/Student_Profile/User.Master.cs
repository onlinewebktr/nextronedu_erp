using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class User : System.Web.UI.MasterPage
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] == null)
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
                    string UserCode = Session["User"].ToString();
                    find_name(UserCode);
                    Find_data();

                }
            }
        }

        private void Find_data()
        {
             string query = "Select Name_of_company,Logo,Address,Footer_Copy_Right from Comapny_Profile";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = UsesCode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {

                lbl_school.Text = dtTemp.Rows[0]["Name_of_company"].ToString();
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();

            }
             
        }

        private void find_name(string UserCode)
        {
            try
            {
                string sql = "select studentname,dateofadmission,profile_img from dbo.[admission_registor] where admissionserialnumber ='" + UserCode + "'";
                DataTable dt = code.FillTable(sql);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    // do nothing
                }
                else
                {
                    lbl_membername.Text = dt.Rows[0]["studentname"].ToString();
                    lbl_membercode.Text = "ID : " + UserCode;
                    ltName.Text = dt.Rows[0]["studentname"].ToString();
                    if (dt.Rows[0]["studentname"].ToString().Length > 12)
                    {
                        lbl_membername.Text = dt.Rows[0]["studentname"].ToString().Split(' ')[0].ToString();
                    }

                    if (dt.Rows[0]["profile_img"].ToString() != "")
                    { imgMember.ImageUrl = dt.Rows[0]["profile_img"].ToString(); }
                    else { imgMember.ImageUrl = "/images/pic.jpg"; }
                }


                //string sql = "select Name,RegDate,ProfilePhoto from UserProfile where UserID ='" + UserCode + "'";
                //DataTable dt = code.FillTable(sql);
                //int rowcount = dt.Rows.Count;
                //if (rowcount == 0)
                //{
                //    // do nothing
                //}
                //else
                //{
                //    lbl_membername.Text = dt.Rows[0]["Name"].ToString();
                //    lbl_membercode.Text = "ID : " + UserCode;
                //    ltName.Text = dt.Rows[0]["Name"].ToString();
                //    if (dt.Rows[0]["Name"].ToString().Length > 12)
                //    {
                //        lbl_membername.Text = dt.Rows[0]["Name"].ToString().Split(' ')[0].ToString();
                //    }

                //    if (dt.Rows[0]["ProfilePhoto"].ToString() != "")
                //    { imgMember.ImageUrl = dt.Rows[0]["ProfilePhoto"].ToString(); }
                //    else { imgMember.ImageUrl = "/images/pic.jpg"; }
                //}
            }
            catch
            {
            }
        }

        protected void lnk_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
            Response.Write("<script language=javascript>wnd.close();</script>");
            Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
            Response.Write("<script language=javascript>wnd.close();</script>");
            Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
        }
    }
}