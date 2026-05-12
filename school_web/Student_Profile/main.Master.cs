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
    public partial class main : System.Web.UI.MasterPage
    {
        My mycode = new My();
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
                    ViewState["Session_id"] = My.get_session_id();
                    find_name(UserCode);
                    Find_data();

                }
            }
        }


        private void Find_data()
        {
            string query = "Select * from Firm_Details";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count != 0)
            {
                lbl_school_name.Text = dtTemp.Rows[0]["firm_name"].ToString();
                lbl_footer.Text = dtTemp.Rows[0]["Footer_Copy_Right"].ToString();
            }
        }

        private void find_name(string UserCode)
        {
            try
            {
                string sql = "select Id,admissionserialnumber,studentname,dateofadmission,studentimagepath,class,Section,rollnumber,session from dbo.[admission_registor] where admissionserialnumber ='" + UserCode + "' and Session_id='" + ViewState["Session_id"].ToString() + "' order by Id desc";
                DataTable dt = mycode.FillData(sql);
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                    // do nothing
                }
                else
                {
                    student_name.InnerText = dt.Rows[0]["studentname"].ToString();

                    student_class.InnerText = dt.Rows[0]["class"].ToString();
                    student_section.InnerText = dt.Rows[0]["Section"].ToString();
                    student_roll.InnerText = dt.Rows[0]["rollnumber"].ToString();

                    lbl_psessions.Text = dt.Rows[0]["session"].ToString();
                    lbl_adm_No.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                    lbl_class_name.Text = dt.Rows[0]["class"].ToString();
                    lbl_section.Text = dt.Rows[0]["Section"].ToString();
                    lbl_namess.Text = dt.Rows[0]["studentname"].ToString();

                    if (dt.Rows[0]["studentimagepath"].ToString() != "")
                    {
                        Image1.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                        Image2.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                        Image3.ImageUrl = dt.Rows[0]["studentimagepath"].ToString();
                    }
                    else
                    {
                        Image1.ImageUrl = "/images/dummy-student.jpg";
                        Image2.ImageUrl = "/images/dummy-student.jpg";
                        Image3.ImageUrl = "/images/dummy-student.jpg";
                    }
                }
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

    }
}