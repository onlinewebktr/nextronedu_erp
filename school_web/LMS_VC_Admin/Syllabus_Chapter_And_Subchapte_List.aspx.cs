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
    public partial class Syllabus_Chapter_And_Subchapte_List : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        My mycode = new My();
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
                    if (Session["MsgSession"] != null)
                    {
                        Alert(Session["MsgSession"].ToString());
                        Session["MsgSession"] = null;
                    }
                    ViewState["User"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["User"].ToString());
                    imp.bind_all_ddl_with_id(ddl_sseion, "Select Session,session_id from session_details order by Use_mode asc ");
                    ddl_sseion.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id_cap_All(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term  where Session_id=" + ddl_sseion.SelectedValue + " order by Position asc ");

                    Bid_grid();
                }
            }
        }
        public void Alert(string Message)
        {

            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Bid_grid()
        {
            bind_final_grid_data();
        }

        private void bind_final_grid_data()
        {
            try
            {
                string query = "";
                if (ddl_term.SelectedItem.Text == "ALL")
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ddl_sseion.SelectedValue + " order by act.Position asc";
                }
                else if (ddl_CourseCat.SelectedItem.Text == "ALL")
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ddl_sseion.SelectedValue + " and scs.Term_id=" + ddl_term.SelectedValue + " order by act.Position asc";
                }
                else if (ddl_subject.SelectedItem.Text == "ALL")
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ddl_sseion.SelectedValue + "  and scs.Term_id=" + ddl_term.SelectedValue + " and scs.Class_id=" + ddl_CourseCat.SelectedValue + " order by act.Position asc";
                }
                else
                {
                    query = "Select scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  join Add_course_table act on scs.Class_id=act.course_id where scs.Session_id=" + ddl_sseion.SelectedValue + " and scs.Term_id=" + ddl_term.SelectedValue + " and scs.Class_id=" + ddl_CourseCat.SelectedValue + " and scs.Subject_id=" + ddl_subject.SelectedValue + " order by act.Position asc";
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

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "select DISTINCT t2.Subject_name,t1.Subject_id from Syllubsh_Chapter_SubChapter t1 join Subject_Master t2 on t1.Subject_id=t2.Subject_id  where t1.Session_id='" + ddl_sseion.SelectedValue + "' and t1.Term_id='" + ddl_term.SelectedValue + "' and t1.Class_id='" + ddl_CourseCat.SelectedValue + "' order by t2.Subject_name asc");
            }
            catch (Exception ex)
            { 
            } 
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_sseion.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else
            {
                bind_final_grid_data();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_id");
                Response.Redirect("Syllabus_Chapter_And_Subchapte.aspx?Id=" + lbl_Id.Text + "&from=0", false);
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
                bind_final_grid_data();
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_CourseCat, "select DISTINCT t2.Course_Name, course_id from Syllubsh_Chapter_SubChapter t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddl_sseion.SelectedValue + "' and t1.Term_id='" + ddl_term.SelectedValue + "' order by t2.Course_Name asc");
            }
            catch (Exception ex)
            { 
            }
        }
    }
}