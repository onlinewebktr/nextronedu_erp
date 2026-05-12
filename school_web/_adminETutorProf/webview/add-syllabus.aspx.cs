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
    public partial class add_syllabus : System.Web.UI.Page
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

                    imp.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term  where Session_id=" + ViewState["sesssionid"].ToString() + " order by Position asc");
                    imp.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table where course_id in(select CategoryID from  TeacherCourseSubjectMaping where UserID='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "')  order by Position asc");
                    if (Request.QueryString["Id"] != null)
                    {
                        hd_id.Value = Request.QueryString["Id"];
                        string queryedt = " Select   scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name  from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id where scs.Id=" + hd_id.Value + "     ";
                        ViewState["queryEdt"] = queryedt;
                        btn_submit.Text = "Update";
                        btn_cncel.Visible = true;
                        Bind_data_list();
                    }
                }
            }
        }

        private void Bind_data_list()
        {
            DataTable dt = imp.FillTable(ViewState["queryEdt"].ToString());
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imp.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term where Session_id='" + ViewState["sesssionid"].ToString() + "' order by Position asc ");
                ddl_term.SelectedValue = dt.Rows[0]["Term_id"].ToString();
                ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                imp.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where course_id='" + ddl_class.SelectedValue + "'  order by Subject_position asc ");
                ddl_subject.SelectedValue = dt.Rows[0]["Subject_id"].ToString();
                txt_Chaptername.Text = dt.Rows[0]["Chapter_Name"].ToString();
                txt_subchapter.Text = dt.Rows[0]["Subchapter_Name"].ToString();
                txt_sub_subject.Text = dt.Rows[0]["Sub_Subject"].ToString();

                if (dt.Rows[0]["Is_sub_subject"].ToString() == "1")
                {
                    ddl_is_sub_subject.Text = "Yes";
                }
                if (dt.Rows[0]["Is_sub_chapter"].ToString() == "1")
                {
                    ddl_sub_chapter.Text = "Yes";
                }

                ViewState["editmode"] = "1";
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
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");
                }
                else
                {
                    imp.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where Subject_id in(select AssignCourseID from  TeacherCourseSubjectMaping where UserID='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and CategoryID='" + ddl_class.SelectedValue + "') and course_id='" + ddl_class.SelectedValue + "'  order by Subject_position asc"); 
                }
            }
            catch(Exception ex)
            { 
            }
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_term.SelectedItem.Text == "Select")
            {
                ddl_term.Focus();
                Alert("Please select term");
                return;
            }
            if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alert("Please select class");
                return;
            }
            if (ddl_subject.SelectedItem.Text == "Select")
            {
                ddl_subject.Focus();
                Alert("Please select subject");
                return;
            }
            if (ddl_is_sub_subject.SelectedItem.Text == "Yes")
            {
                if (txt_sub_subject.Text == "")
                {
                    txt_sub_subject.Focus();
                    Alert("Please enter sub-subject.");
                    txt_sub_subject.Focus();
                    return;
                }
            }
            if (txt_Chaptername.Text == "")
            {
                txt_Chaptername.Focus();
                Alert("Please enter chapter name.");
                txt_Chaptername.Focus();
                return;
            }
            if (ddl_sub_chapter.SelectedItem.Text == "Yes")
            {
                if (txt_subchapter.Text == "")
                {
                    txt_subchapter.Focus();
                    Alert("Please enter sub-chapter.");
                    txt_subchapter.Focus();
                    return;
                }
            }

            if (btn_submit.Text == "Add")
            {
                DataTable dtx = imp.FillTable("select Id from Syllubsh_Chapter_SubChapter order by id desc");
                if (dtx.Rows.Count > 0)
                {
                    ViewState["position"] = dtx.Rows[0]["Id"].ToString();
                }


                string sl = create_sl_no();
                string query = "INSERT INTO Syllubsh_Chapter_SubChapter (Session_id,Term_id,Chapter_and_Subchapter_id,Chapter_Name,Subchapter_Name,Date,Crated_by,Subject_id,Class_id,Branch_id,Position,Sub_Subject,Is_sub_subject,Is_sub_chapter) values (@Session_id,@Term_id,@Chapter_and_Subchapter_id,@Chapter_Name,@Subchapter_Name,@Date,@Crated_by,@Subject_id,@Class_id,@Branch_id,@Position,@Sub_Subject,@Is_sub_subject,@Is_sub_chapter)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Chapter_and_Subchapter_id", sl);
                cmd.Parameters.AddWithValue("@Chapter_Name", txt_Chaptername.Text.Trim());
                cmd.Parameters.AddWithValue("@Date", imp.getdate1());
                cmd.Parameters.AddWithValue("@Crated_by", ViewState["regid"].ToString());
                cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Position", Convert.ToInt32(ViewState["position"].ToString()) + 1);


                //=============SubSubject
                if (ddl_is_sub_subject.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Sub_Subject", txt_sub_subject.Text);
                    cmd.Parameters.AddWithValue("@Is_sub_subject", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sub_Subject", "");
                    cmd.Parameters.AddWithValue("@Is_sub_subject", 0);
                }

                if (ddl_sub_chapter.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Subchapter_Name", txt_subchapter.Text.Trim());
                    cmd.Parameters.AddWithValue("@Is_sub_chapter", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Subchapter_Name", "");
                    cmd.Parameters.AddWithValue("@Is_sub_chapter", 0);
                }
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Session["MsgSession"] = "Syllabus has been successfully added.";
                    Response.Redirect("add-syllabus.aspx?regid=" + ViewState["regid"].ToString(), false);
                }
            }
            else
            {
                string query = "Update Syllubsh_Chapter_SubChapter set Session_id=@Session_id,Term_id=@Term_id,Chapter_Name=@Chapter_Name,Subchapter_Name=@Subchapter_Name,Updated_date=@Updated_date,Updated_by=@Updated_by,Subject_id=@Subject_id,Class_id=@Class_id,Sub_Subject=@Sub_Subject,Is_sub_subject=@Is_sub_subject,Is_sub_chapter=@Is_sub_chapter where Id = @Id";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Chapter_Name", txt_Chaptername.Text.Trim());
                cmd.Parameters.AddWithValue("@Updated_date", imp.getdate1());
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["regid"].ToString());
                cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Id", hd_id.Value);

                //=============SubSubject
                if (ddl_is_sub_subject.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Sub_Subject", txt_sub_subject.Text);
                    cmd.Parameters.AddWithValue("@Is_sub_subject", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sub_Subject", "");
                    cmd.Parameters.AddWithValue("@Is_sub_subject", 0);
                }

                //=============SubChapter
                if (ddl_sub_chapter.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Subchapter_Name", txt_subchapter.Text.Trim());
                    cmd.Parameters.AddWithValue("@Is_sub_chapter", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Subchapter_Name", "");
                    cmd.Parameters.AddWithValue("@Is_sub_chapter", 0);
                }

                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    btn_submit.Text = "Add";
                    btn_cncel.Visible = false;
                    Session["MsgSession"] = "Syllabus has been successfully updated.";
                    Response.Redirect("view-self-added-syllabus.aspx?regid=" + ViewState["regid"].ToString(), false);
                }
            }
        }


        private string create_sl_no()
        {
            bool duplicate = true;
            string Term_id = My.auto_serialS("group_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Chapter_and_Subchapter_id from dbo.[Syllubsh_Chapter_SubChapter] where Term_id='" + Term_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Term_id = My.auto_serialS("group_id");
                }
            }
            return Term_id;
        }
        protected void btn_cncel_Click(object sender, EventArgs e)
        {
            Response.Redirect("view-self-added-syllabus.aspx?regid=" + ViewState["regid"].ToString(), false);
        } 
    }
}