using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web._adminETutorProf.webview
{
    public partial class Updated_Syllabus_Chapter : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["teacher"] = Request.QueryString["regid"].ToString();
                    try
                    {
                        Dictionary<string, object> dc1 = My.get_push_credantial();
                        ViewState["type"] = (String)dc1["type"];
                        ViewState["project_id"] = (String)dc1["project_id"];
                        ViewState["private_key_id"] = (String)dc1["private_key_id"];
                        ViewState["client_email"] = (String)dc1["client_email"];
                        ViewState["client_id"] = (String)dc1["client_id"];
                        ViewState["private_key"] = dc1["private_key"].ToString().Replace("\\n", "\n");
                        ViewState["sessionid"] = code.get_session_id_use();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["teacher"].ToString());
                        code.bind_all_ddl_with_id(ddl_term, "Select Term_Name,Term_id from Syllubsh_Term where Session_id=" + ViewState["sessionid"].ToString() + " order by Position asc");
                        lbl_date.Text = mycode.date();

                        if (Request.QueryString["Id"] != null)
                        {
                            ViewState["id"] = Request.QueryString["Id"].ToString();
                            BindDetails();
                        }



                    }
                    catch
                    {
                    }
                }


            }
        }

        private void BindDetails()
        {
            DataTable dt = mycode.FillData("select * from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                btn_save.Text = "Update";

                ddl_term.SelectedValue = dt.Rows[0]["Term_id"].ToString();
                code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");
                ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                code.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   and  UserID='" + ViewState["teacher"].ToString() + "'   order by section");

                ddl_section.Text = dt.Rows[0]["Section"].ToString();

                code.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id join Syllubsh_Chapter_SubChapter scs on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_class.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sessionid"].ToString() + "' and  scs.Term_id='" + ddl_term.SelectedValue + "' order by sm.Subject_position");

                ddl_subject.SelectedValue = dt.Rows[0]["Subject_id"].ToString();


                try
                {
                    code.bind_all_ddl_with_id(ddl_chapter, "select Chapter_Name,Chapter_and_Subchapter_id from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by Chapter_Name asc");
                    DataTable dt1 = My.dataTable("Select Sub_Subject from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Is_sub_subject=1");
                    if (dt1.Rows.Count > 0)
                    {
                        My.bind_ddl_noselect(ddl_subsubject, "Select Sub_Subject from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "'  and Is_sub_subject=1 order by Sub_Subject asc");
                        pnl_is_subsubject.Visible = true;
                    }
                    else
                    {
                        pnl_is_subsubject.Visible = false;
                    }

                    ddl_subsubject.SelectedValue = dt.Rows[0]["Sub_Subject_id"].ToString();
                    ddl_chapter.SelectedValue = dt.Rows[0]["Chapter_and_Subchapter_id"].ToString();
                    try
                    {
                        DataTable dt2 = My.dataTable("Select Subchapter_Name from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Chapter_and_Subchapter_id='" + ddl_chapter.SelectedValue + "' and Is_sub_chapter=1");
                        if (dt2.Rows.Count > 0)
                        {
                            My.bind_ddl_noselect(ddl_subchapter, "Select Subchapter_Name from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Chapter_and_Subchapter_id='" + ddl_chapter.SelectedValue + "' and Is_sub_chapter=1  order by Subchapter_Name asc");
                            pnl_is_subchapter.Visible = true;
                        }
                        else
                        {
                            pnl_is_subchapter.Visible = false;
                        }

                        ddl_subchapter.SelectedValue = dt.Rows[0]["Chapter_and_Subchapter_id"].ToString();


                        lbl_date.Text = dt.Rows[0]["Date"].ToString();
                        ddl_status.Text = dt.Rows[0]["Status"].ToString();
                        txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
                        HiddenField1.Value = ViewState["id"].ToString();

                    }
                    catch
                    {

                    }





                }
                catch
                {

                }


            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
            }
            else
            {
                code.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table where    course_id in(select CategoryID from  TeacherCourseSubjectMaping   where UserID='" + ViewState["teacher"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "')  order by Position asc");
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
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
                code.bind_ddl(ddl_section, "Select distinct section  from TeacherCourseSubjectMaping where CategoryID ='" + ddl_class.SelectedValue + "'   and  UserID='" + ViewState["teacher"].ToString() + "'   order by section");



            }

        }



        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
            }

            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {
                code.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from TeacherCourseSubjectMaping tcsm join  Subject_Master sm on tcsm.CategoryID=sm.course_id and tcsm.AssignCourseID=sm.Subject_id join Syllubsh_Chapter_SubChapter scs on scs.Class_id=sm.course_id and scs.Subject_id=sm.Subject_id  where tcsm.UserID='" + ViewState["teacher"].ToString() + "' and tcsm.CategoryID='" + ddl_class.SelectedValue + "' and tcsm.section='" + ddl_section.Text + "' and tcsm.Session_id='" + ViewState["sessionid"].ToString() + "' and  scs.Term_id='" + ddl_term.SelectedValue + "' order by sm.Subject_position");
            }

        }
        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else
            {
                code.bind_all_ddl_with_id(ddl_chapter, "select Chapter_Name,Chapter_and_Subchapter_id from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by Chapter_Name asc");
                DataTable dt = My.dataTable("Select Sub_Subject from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Is_sub_subject=1");
                if (dt.Rows.Count > 0)
                {
                    My.bind_ddl_noselect(ddl_subsubject, "Select Sub_Subject from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "'  and Is_sub_subject=1 order by Sub_Subject asc");
                    pnl_is_subsubject.Visible = true;
                }
                else
                {
                    pnl_is_subsubject.Visible = false;
                }
            }
        }


        protected void ddl_chapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else if (ddl_chapter.Text == "Select")
            {
                Alert("Please select chapter name");
            }
            else
            {
                DataTable dt = My.dataTable("Select Subchapter_Name from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Chapter_and_Subchapter_id='" + ddl_chapter.SelectedValue + "' and Is_sub_chapter=1");
                if (dt.Rows.Count > 0)
                {
                    My.bind_ddl_noselect(ddl_subchapter, "Select Subchapter_Name from Syllubsh_Chapter_SubChapter where Session_id=" + ViewState["sessionid"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Chapter_and_Subchapter_id='" + ddl_chapter.SelectedValue + "' and Is_sub_chapter=1  order by Subchapter_Name asc");
                    pnl_is_subchapter.Visible = true;
                }
                else
                {
                    pnl_is_subchapter.Visible = false;
                }
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_term.SelectedItem.Text == "Select")
            {
                Alert("Please select term");
            }

            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alert("Please select section");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alert("Please select subject");
            }
            else if (ddl_chapter.Text == "Select")
            {
                Alert("Please select chapter");
            }
            else if (txt_remarks.Text == "")
            {
                Alert("Please enter remarks");
            }
            else
            {
                if (btn_save.Text == "Save")
                {
                    DataTable cdt = My.dataTable("Select Id from Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher where Term_id=" + ddl_term.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and  Branch_id=" + ViewState["branchid"].ToString() + " and Chapter_and_Subchapter_id='" + ddl_chapter.SelectedValue + "' and Teacher_id='" + ViewState["teacher"].ToString() + "' and idate=" + mycode.idate() + " and Session_id=" + ViewState["sessionid"].ToString() + "");
                    if (cdt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher (Session_id,Term_id,Subject_id,Class_id,Branch_id,Chapter_and_Subchapter_id,Teacher_id,Date,idate,time,Status,Remarks,Section,Sub_Subject_id,Send_Status) values (@Session_id,@Term_id,@Subject_id,@Class_id,@Branch_id,@Chapter_and_Subchapter_id,@Teacher_id,@Date,@idate,@time,@Status,@Remarks,@Section,@Sub_Subject_id,@Send_Status)";
                        SqlCommand cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Chapter_and_Subchapter_id", ddl_chapter.SelectedValue);
                        cmd.Parameters.AddWithValue("@Teacher_id", ViewState["teacher"].ToString());
                        cmd.Parameters.AddWithValue("@Date", lbl_date.Text);
                        cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(lbl_date.Text));
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        cmd.Parameters.AddWithValue("@Status", ddl_status.SelectedValue);
                        cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Sub_Subject_id", "");
                        cmd.Parameters.AddWithValue("@Send_Status", "Send");


                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            save_history();
                            update_add_push();
                            Alert("Remarks has been successfully submited");
                            txt_remarks.Text = "";
                        }
                    }
                    else
                    {
                        string id = cdt.Rows[0]["Id"].ToString();
                        string query = "Update Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher set Date=@Date,idate=@idate,time=@time,Status=@Status,Remarks=@Remarks where Id = @Id";
                        SqlCommand cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Date", lbl_date.Text);
                        cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(lbl_date.Text));
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        cmd.Parameters.AddWithValue("@Status", ddl_status.SelectedValue);
                        cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                        cmd.Parameters.AddWithValue("@Id", id);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            save_history();
                            update_add_push();
                            Alert("Remarks has been successfully updated");
                            txt_remarks.Text = "";
                        }
                    }
                }
                else
                {


                    string query = "Update Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher set Session_id=@Session_id,Term_id=@Term_id,Subject_id=@Subject_id,Class_id=@Class_id,Branch_id=@Branch_id,Chapter_and_Subchapter_id=@Chapter_and_Subchapter_id,Teacher_id=@Teacher_id,Date=@Date,idate=@idate,time=@time,Status=@Status,Remarks=@Remarks,Section=@Section,Sub_Subject_id=@Sub_Subject_id,Send_Status=@Send_Status where Id = "+HiddenField1.Value+"";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Chapter_and_Subchapter_id", ddl_chapter.SelectedValue);
                    cmd.Parameters.AddWithValue("@Teacher_id", ViewState["teacher"].ToString());
                    cmd.Parameters.AddWithValue("@Date", lbl_date.Text);
                    cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(lbl_date.Text));
                    cmd.Parameters.AddWithValue("@time", mycode.time());
                    cmd.Parameters.AddWithValue("@Status", ddl_status.SelectedValue);
                    cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@Sub_Subject_id", "");
                    cmd.Parameters.AddWithValue("@Send_Status", "Send");
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        save_history();
                        update_add_push();
                        Session["msG"] = "Syllabus details has been successfully updated.";
                        Response.Redirect("View_Updated_Syllabus.aspx?regid=" + ViewState["teacher"].ToString(), false);
                    }





                }

            }
        }



        private void save_history()
        {
            string query = "INSERT INTO Syllubsh_Chapter_SubChapter_Add_Remarks_by_teacher_history (Session_id,Term_id,Subject_id,Class_id,Branch_id,Chapter_and_Subchapter_id,Teacher_id,Date,idate,time,Status,Remarks,Section,Sub_Subject_id) values (@Session_id,@Term_id,@Subject_id,@Class_id,@Branch_id,@Chapter_and_Subchapter_id,@Teacher_id,@Date,@idate,@time,@Status,@Remarks,@Section,@Sub_Subject_id)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
            cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
            cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
            cmd.Parameters.AddWithValue("@Chapter_and_Subchapter_id", ddl_chapter.SelectedValue);
            cmd.Parameters.AddWithValue("@Teacher_id", ViewState["teacher"].ToString());
            cmd.Parameters.AddWithValue("@Date", lbl_date.Text);
            cmd.Parameters.AddWithValue("@idate", mycode.ConvertStringToiDateup(lbl_date.Text));
            cmd.Parameters.AddWithValue("@time", mycode.time());
            cmd.Parameters.AddWithValue("@Status", ddl_status.SelectedValue);
            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
            cmd.Parameters.AddWithValue("@Sub_Subject_id", "");
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }
        }

        private void update_add_push()
        {
            string query = "";

            query = " select gcm_id,admissionserialnumber from dbo.[admission_registor] where  Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "'  and  Session_id='" + My.get_session_id() + "'";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcmid = dt.Rows[i]["gcm_id"].ToString();
                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    final_send_push(gcmid, admissionserialnumber );

                }

            }
        }

        private void final_send_push(string gcmid, string admissionserialnumber )
        {
            if (gcmid == "")
            {
                gcmid = "0";
            }

            if (gcmid != "")
            {
                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = txt_remarks.Text;
                ss["title"] = "Syllabush";
                ss["messagetype"] = "Syllabush";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = admissionserialnumber;
                ss["type"] = ViewState["type"].ToString();
                ss["project_id"] = ViewState["project_id"].ToString();
                ss["private_key_id"] = ViewState["private_key_id"].ToString();
                ss["client_email"] = ViewState["client_email"].ToString();
                ss["client_id"] = ViewState["client_id"].ToString();
                ss["private_key"] = ViewState["private_key"].ToString();
                My.onlypush(gcmid, ss);
            }
        }
    }
}