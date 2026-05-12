using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class view_self_added_syllabus : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sesssionid"] = My.get_session_id();
                if (Request.QueryString["regid"] != null)
                {
                    if (Session["MsgSession"] != null)
                    {
                        Alert(Session["MsgSession"].ToString());
                        Session["MsgSession"] = null;
                    }
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["regid"].ToString());

                    mycode.bind_all_ddl_with_id_cap_All(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term  where Session_id=" + ViewState["sesssionid"].ToString() + " order by Position asc");
                    Bid_grid();
                }
            }
        }

        private void Bid_grid()
        {
            try
            {
                string query = "";
                if (ddl_term.SelectedItem.Text == "ALL")
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ViewState["sesssionid"].ToString() + " and scs.Crated_by='" + ViewState["regid"].ToString() + "' order by act.Position asc";
                }
                else if (ddl_class.SelectedItem.Text == "ALL")
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ViewState["sesssionid"].ToString() + " and scs.Term_id=" + ddl_term.SelectedValue + " and scs.Crated_by='" + ViewState["regid"].ToString() + "' order by act.Position asc";
                }
                else if (ddl_subject.SelectedItem.Text == "ALL")
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ViewState["sesssionid"].ToString() + "  and scs.Term_id=" + ddl_term.SelectedValue + " and scs.Class_id=" + ddl_class.SelectedValue + " and scs.Crated_by='" + ViewState["regid"].ToString() + "' order by act.Position asc";
                }
                else
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ViewState["sesssionid"].ToString() + " and scs.Term_id=" + ddl_term.SelectedValue + " and scs.Class_id=" + ddl_class.SelectedValue + " and scs.Subject_id=" + ddl_subject.SelectedValue + " and scs.Crated_by='" + ViewState["regid"].ToString() + "' order by act.Position asc";
                }

                DataTable dt = imp.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    Alert("Sorry! there are no any data found");
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }
                else
                {
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch
            {
            }
        }

        public void Alert(string Message)
        {

            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alert("Please select term");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else
                {
                    mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where Subject_id in(select AssignCourseID from  TeacherCourseSubjectMaping where UserID='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and CategoryID='" + ddl_class.SelectedValue + "') and course_id='" + ddl_class.SelectedValue + "'  order by Subject_position asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bid_grid();
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_id");
                Response.Redirect("add-syllabus.aspx?regid=" + ViewState["regid"].ToString() + "&Id=" + lbl_Id.Text, false);
            }
            catch
            {
            }
        }


        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_id");
                SqlCommand cmd = new SqlCommand("Delete from Syllubsh_Chapter_SubChapter where Id='" + lbl_Id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);

                Alert("Subject chapter and subchapter name  has been successfully deleted.");
                Bid_grid();
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name, course_id from Add_course_table where course_id in(select CategoryID from  TeacherCourseSubjectMaping where UserID='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "')  order by Position asc");
            }
            catch (Exception ex)
            { 
            }
        }
    }
}