using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;
namespace school_web.LMS_VC_Admin
{
    public partial class VIew_Syllabus_Report_Subject_Class_wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
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
                    code.bind_all_ddl_with_id(ddl_sseion, "Select Session,session_id from session_details order by Use_mode asc ");
                    ddl_sseion.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term  where Session_id=" + ddl_sseion.SelectedValue + " order by Position asc ");
                }
            }
        }


        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }





        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            if (ddl_sseion.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
                ddl_sseion.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
                ddl_term.Focus();
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
                ddl_class.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section.");
                ddl_section.Focus();
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
                ddl_subject.Focus();
            }
            else if (ddl_chapter.SelectedItem.Text == "Select")
            {
                Alert("Please select chapter.");
                ddl_chapter.Focus();
            }
            else
            {
                find_data();
            }
        }

        private void find_data()
        {
            string query = "";
            query = "Select ('" + ddl_section.Text + "') as Section,scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN scs.Is_sub_subject=0 THEN 'NA' WHEN scs.Is_sub_subject!=0 THEN scs.Sub_Subject END AS SubSubjName,CASE WHEN scs.Is_sub_chapter=0 THEN 'NA' WHEN scs.Is_sub_chapter!=0 THEN scs.Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id where scs.Session_id=" + ddl_sseion.SelectedValue + " and scs.Term_id=" + ddl_term.SelectedValue + " and scs.Class_id=" + ddl_class.SelectedValue + "  and scs.Subject_id=" + ddl_subject.SelectedValue + " and scs.Chapter_Name='" + ddl_chapter.SelectedValue + "' order by scs.Position asc";
            DataTable dt = mycode.FillData(query);
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

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow rd_subject_view1 = ((HtmlTableRow)e.Item.FindControl("subtable"));
                Repeater rd_subject_view = ((Repeater)e.Item.FindControl("rd_subject_view"));
                Label lbl_Session_id = ((Label)e.Item.FindControl("lbl_Session_id")) as Label;
                Label lbl_Term_id = ((Label)e.Item.FindControl("lbl_Term_id")) as Label;
                Label lbl_Subject_id = ((Label)e.Item.FindControl("lbl_Subject_id")) as Label;
                Label lbl_Class_id = ((Label)e.Item.FindControl("lbl_Class_id")) as Label;
                Label lbl_Chapter_and_Subchapter_id = ((Label)e.Item.FindControl("lbl_Chapter_and_Subchapter_id")) as Label;

                string query = "select * from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Session_id=" + lbl_Session_id.Text + " and Class_id=" + lbl_Class_id.Text + " and Subject_id=" + lbl_Subject_id.Text + " and Term_id=" + lbl_Term_id.Text + " and Chapter_and_Subchapter_id='" + lbl_Chapter_and_Subchapter_id.Text + "' and Section='" + ddl_section.Text + "' ";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    rd_subject_view.DataSource = null;
                    rd_subject_view.DataBind();
                    rd_subject_view1.Visible = false;
                }
                else
                {
                    rd_subject_view1.Visible = true;
                    rd_subject_view.DataSource = dt;
                    rd_subject_view.DataBind();
                }
            }
        }


        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_class, "select DISTINCT t2.Course_Name, course_id from Syllubsh_Chapter_SubChapter t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddl_sseion.SelectedValue + "' and t1.Term_id='" + ddl_term.SelectedValue + "' order by t2.Course_Name asc");
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Class_id=" + ddl_class.SelectedValue + " ");
                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct  sm.Subject_name, sm.Subject_id,sm.Subject_position from Subject_Master sm join Syllubsh_Chapter_SubChapter scs on sm.course_id=scs.Class_id and sm.Subject_id=scs.Subject_id where  scs.Session_id=" + ddl_sseion.SelectedValue + "  and scs.Class_id=" + ddl_class.SelectedValue + " and scs.Term_id=" + ddl_term.SelectedValue + " order by sm.Subject_position asc");
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddl(ddl_chapter, "Select distinct Chapter_Name from Syllubsh_Chapter_SubChapter where Session_id=" + ddl_sseion.SelectedValue + " and  Class_id=" + ddl_class.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Term_id=" + ddl_term.SelectedValue + " order by Chapter_Name asc");
            }
            catch
            {
            }
        }
    }
}