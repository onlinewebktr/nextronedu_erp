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
    public partial class Syllabus_Chapter_And_Subchapte : System.Web.UI.Page
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
                    ViewState["editmode"] = "0";
                    ViewState["User"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["User"].ToString());
                    imp.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Use_mode asc ");
                    imp.bind_all_ddl_with_id(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                    ddl_session.SelectedValue = My.get_session_id();
                    imp.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term  where Session_id=" + ddl_session.SelectedValue + " order by Position asc ");
                    if (Request.QueryString["Id"] != null)
                    {
                        hd_id.Value = Request.QueryString["Id"];
                        try
                        {
                            hd_edit_from.Value = Request.QueryString["from"];
                        }
                        catch (Exception ex)
                        {
                        }

                        string queryedt = " Select   scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name  from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id where scs.Id=" + hd_id.Value + "     ";
                        ViewState["queryEdt"] = queryedt;
                        btn_submit.Text = "Update";
                        btn_cncel.Visible = true;
                        Bind_data_list();
                    }

                    string query = "Select top 10 scs.*,(Select top 1 Term_Name from Syllubsh_Term where Term_id=scs.Term_id) as Term_Name,(Select top 1 Session from session_details where session_id=scs.Session_id) as Session,cm.Course_Name,sm.Subject_name,CASE WHEN Is_sub_subject=0 THEN 'NA' WHEN Is_sub_subject!=0 THEN Sub_Subject END AS SubSubjName,CASE WHEN Is_sub_chapter=0 THEN 'NA' WHEN Is_sub_chapter!=0 THEN Subchapter_Name END AS SubChapterName from Syllubsh_Chapter_SubChapter scs join Add_course_table cm on scs.Class_id=cm.course_id join Subject_Master sm on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id order by id desc";
                    ViewState["query"] = query;
                    Bid_grid();
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
                ddl_session.SelectedValue = dt.Rows[0]["Session_id"].ToString();
                imp.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term where Session_id='" + ddl_session.SelectedValue + "' order by Position asc ");
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
        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else
            {
                imp.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term where Session_id='" + ddl_session.SelectedValue + "' order by Position asc ");
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {
                imp.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where course_id='" + ddl_class.SelectedValue + "'  order by Subject_position asc ");
            }
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
                return;
            }
            if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
                return;
            }
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
                return;
            }
            if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
                return;
            }
            if (ddl_is_sub_subject.SelectedItem.Text == "Yes")
            {
                if (txt_sub_subject.Text == "")
                {
                    Alert("Please enter sub-subject.");
                    txt_sub_subject.Focus();
                    return;
                }
            }
            if (txt_Chaptername.Text == "")
            {
                Alert("Please enter chapter name.");
                txt_Chaptername.Focus();
                return;
            }
            if (ddl_sub_chapter.SelectedItem.Text == "Yes")
            {
                if (txt_subchapter.Text == "")
                {
                    Alert("Please enter sub-chapter.");
                    txt_subchapter.Focus();
                    return;
                }
            }

            if (btn_submit.Text == "Add")
            {
                string sl = create_sl_no();
                //DataTable cdt = My.dataTable("Select * from Syllubsh_Chapter_SubChapter where Session_id ='" + ddl_session.SelectedValue + "' and Term_id=" + ddl_term.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Chapter_Name='" + txt_Chaptername.Text.Trim() + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                //if (cdt.Rows.Count == 0)
                //{
                string query = "INSERT INTO Syllubsh_Chapter_SubChapter (Session_id,Term_id,Chapter_and_Subchapter_id,Chapter_Name,Subchapter_Name,Date,Crated_by,Subject_id,Class_id,Branch_id,Position,Sub_Subject,Is_sub_subject,Is_sub_chapter) values (@Session_id,@Term_id,@Chapter_and_Subchapter_id,@Chapter_Name,@Subchapter_Name,@Date,@Crated_by,@Subject_id,@Class_id,@Branch_id,@Position,@Sub_Subject,@Is_sub_subject,@Is_sub_chapter)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Chapter_and_Subchapter_id", sl);
                cmd.Parameters.AddWithValue("@Chapter_Name", txt_Chaptername.Text.Trim());
                cmd.Parameters.AddWithValue("@Date", imp.getdate1());
                cmd.Parameters.AddWithValue("@Crated_by", ViewState["User"].ToString());
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
                    Bid_grid();
                    Alert("Subject chapter and subchapter name has been successfully added.");
                }
                //}
                //else
                //{
                //    Alert("Sorry your enterd subject chapter and subchapter name is dublicate.");
                //}
            }
            else
            {
                //DataTable cdt = My.dataTable("Select * from Syllubsh_Chapter_SubChapter where Session_id ='" + ddl_session.SelectedValue + "' and Term_id=" + ddl_term.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Chapter_Name='" + txt_Chaptername.Text.Trim() + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                //if (cdt.Rows.Count == 0)
                //{
                string query = "Update Syllubsh_Chapter_SubChapter set Session_id=@Session_id,Term_id=@Term_id,Chapter_Name=@Chapter_Name,Subchapter_Name=@Subchapter_Name,Updated_date=@Updated_date,Updated_by=@Updated_by,Subject_id=@Subject_id,Class_id=@Class_id,Sub_Subject=@Sub_Subject,Is_sub_subject=@Is_sub_subject,Is_sub_chapter=@Is_sub_chapter where Id = @Id";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Chapter_Name", txt_Chaptername.Text.Trim());
                cmd.Parameters.AddWithValue("@Updated_date", imp.getdate1());
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["User"].ToString());
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
                    Bid_grid();
                    Alert("Subject chapter and subchapter name has been successfully updated.");

                    Session["MsgSession"] = "Subject chapter has been successfully updated.";
                    if (hd_edit_from.Value == "1")
                    {
                        Response.Redirect("Syllabus_Chapter_And_Subchapte.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("Syllabus_Chapter_And_Subchapte_List.aspx", false);
                    }
                }
                //}
                //else
                //{
                //    Alert("Sorry your enterd subject chapter and subchapter name is dublicate.");
                //}
            }
        }

        private void Bid_grid()
        {
            DataTable dt = imp.FillTable(ViewState["query"].ToString());
            if (dt.Rows.Count == 0)
            {
                ViewState["position"] = "0";
                Alert("Sorry! there are no any data found");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                ViewState["position"] = dt.Rows[0]["Id"].ToString();
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
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
            Response.Redirect("Syllabus_Chapter_And_Subchapte.aspx", false);
        }




        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Syllabus_Chapter_And_Subchapte.aspx?Id=" + lbl_Id.Text + "&from=1", false);


                //Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
                //Label lbl_Term_id = (Label)row.FindControl("lbl_Term_id");
                //Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                //Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                //Label lbl_Chapter_Name = (Label)row.FindControl("lbl_Chapter_Name");
                //Label lbl_Subchapter_Name = (Label)row.FindControl("lbl_Subchapter_Name");
                //Label lbl_Sub_Subject_id = (Label)row.FindControl("lbl_Sub_Subject_id");


                //ddl_session.SelectedValue = lbl_Session_id.Text;
                //imp.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term order by Position asc ");
                //ddl_term.SelectedValue = lbl_Term_id.Text;
                //ddl_class.SelectedValue = lbl_Class_id.Text;
                //imp.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master order by Subject_position asc ");

                //ddl_subject.SelectedValue = lbl_Subject_id.Text;
                //txt_Chaptername.Text = lbl_Chapter_Name.Text;
                ////txt_Subchapte.Text = lbl_Subchapter_Name.Text; 


                //hd_id.Value = lbl_Id.Text;
                //btn_cncel.Visible = true;
                //btn_submit.Text = "Update";
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
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from Syllubsh_Chapter_SubChapter where Id='" + lbl_Id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);

                Alert("Subject chapter and subchapter name  has been successfully deleted.");
                Bid_grid();
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
    }
}