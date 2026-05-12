using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.InstructorProfile
{
    public partial class Assined_Class_And_Subject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
            {
                string a = "";
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
                    ViewState["teacher"] = Session["teacher"].ToString();
                    Bind_data();
                }

            }
        }
        UsesCode mycode = new UsesCode();
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Bind_data()
        {


            string query = "Select distinct   Session_id,CategoryID,section,(Select top 1 Session from session_details where session_id=TeacherCourseSubjectMaping.Session_id) as Sessionname,(Select top 1 Course_Name from Add_course_table where course_id=TeacherCourseSubjectMaping.CategoryID) as Course_Name  from TeacherCourseSubjectMaping where UserID='" + ViewState["teacher"].ToString() + "' order by Session_id";

            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                Alert(" There is no class and object assigned for you ");
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }



        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lbl_coursid = (Label)e.Row.FindControl("lbl_coursid");
                    Label lbl_Session_id = (Label)e.Row.FindControl("lbl_Session_id");
                    Label lbl_section = (Label)e.Row.FindControl("lbl_section");


                    DataList dl = (DataList)e.Row.FindControl("ddl_subject");
                    find_threade(lbl_coursid.Text, lbl_Session_id.Text, lbl_section.Text, dl);//admin



                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_threade(string classid, string Session_id, string section, DataList dl)
        {
            DataTable dt = mycode.FillTable(" Select distinct sm.Subject_name,sm.Subject_id from TeacherCourseSubjectMaping tcsm join Subject_Master  sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + classid + "' and tcsm.section='" + section + "' and tcsm.Session_id='" + Session_id + "'   order by sm.Subject_name");
            if (dt.Rows.Count == 0)
            {

                dl.DataSource = null;
                dl.DataBind();

            }
            else
            {
                dl.DataSource = dt;
                dl.DataBind();
            }
        }
    }
}