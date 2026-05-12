using school_web.AppCode;
using school_web.AppCode.Exam;
using school_web.Examination_Admin.slip.stmarry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class calculate_highest_mark_termwise_igms : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();
                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section");

                        mycode.bind_all_ddl_with_id(ddl_session_p, "select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session_p.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_CourseCat_p, "Select Course_Name, course_id from Add_course_table order by Position asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
        }


        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    fetch_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_data()
        {
            string query = "Select *,( select top 1 Course_Name  from Add_course_table where course_id=Exam_highest_mark_of_subject_termwise.Class_id) as  Class_name,( select top 1 Subject_name  from Subject_Master where course_id=Exam_highest_mark_of_subject_termwise.Class_id and Subject_id=Exam_highest_mark_of_subject_termwise.Subject_id) as  Subject_name  from Exam_highest_mark_of_subject_termwise where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "'";
            if (ddl_subject.SelectedItem.Text == "ALL")
            {
                query = "Select *,( select top 1 Course_Name  from Add_course_table where course_id=Exam_highest_mark_of_subject_termwise.Class_id) as  Class_name,( select top 1 Subject_name  from Subject_Master where course_id=Exam_highest_mark_of_subject_termwise.Class_id and Subject_id=Exam_highest_mark_of_subject_termwise.Subject_id) as  Subject_name  from Exam_highest_mark_of_subject_termwise where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term_id='" + ddl_term.SelectedValue + "'";
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }


        #region TempSaveMarks
        //====================================== 
        private void prep_final_term(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id, string firm_id)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section,isnull((select top 1 Rank from Exam_rank_master where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Term_id=" + Term_id + "),'0') as Rank from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Exam_marks");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    update_assesment_marks(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_id, dr["Section"].ToString(), firm_id);
                }
            }
        }

        private void update_assesment_marks(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id, string Section, string firm_id)
        {
            int count = 0;
            double marksforavg = 0;
            string querys = "select Session_Id,Branch_Id,Exam_Term_Id,Assessment_Name,Grade_System_Id,Class_id,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks,Term_assesment_unique_id from Exam_Assessment_Details where Session_Id=" + Session_id + " and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            DataTable dts = My.dataTable(querys);
            foreach (DataRow drs in dts.Rows)
            {
                count++;

                string systm_grade_id = stMarry_final.get_systm_grd_id(Subject_id, Session_id, Class_id, Branch_id, Term_id, drs["Assessment_Id"].ToString());


               string  marks = Examination.get_marks(Session_id, Class_id, Admission_no, Term_id, drs["Assessment_Id"].ToString(), Subject_id, Branch_id, "Scholastic");
                string[] stringSeparatorss = new string[] { "/" };
                string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                string obt_marks = arrs[0];
                string full_marks = arrs[1];
                string marks_type = arrs[2];
                string total_no = arrs[3];

                if (obt_marks == "")
                {


                }
                else
                {
                    if (count <= 2)
                    {
                        marksforavg = marksforavg + My.toDouble(obt_marks);
                        if (count == 2)
                        {
                            double marksforavg_s = marksforavg / 2;
                            save_assesment_marks(Session_id, Class_id, Admission_no, Term_id, drs["Assessment_Id"].ToString(), Subject_id, Branch_id, marksforavg_s.ToString(), full_marks, marks_type, total_no, drs["Term_assesment_unique_id"].ToString());
                        }

                    }
                    else
                    {
                        

                        save_assesment_marks(Session_id, Class_id, Admission_no, Term_id, drs["Assessment_Id"].ToString(), Subject_id, Branch_id, obt_marks.ToString(), full_marks, marks_type, total_no, drs["Term_assesment_unique_id"].ToString());
                    }
                }


               
            }
        }

        private static bool valid_amount(string p)
        {
            try
            {
                Convert.ToDouble(p);
                return true;
            }
            catch
            {
                return false;
            }
        }







        private void save_assesment_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no, string Term_assesment_unique_id)
        {
            try
            {
                bool tot = mycode.cheknumer_Double(marks);
                if (tot == false)
                {
                    marks = "0";
                }
            }
            catch
            {

            }
            try
            {
                bool tot1 = mycode.cheknumer_Double(total_no);
                if (tot1 == false)
                {
                    total_no = "0";
                }
            }
            catch
            {
            }


            string first_term = Exam_setting.get_first_term(session_id, Branch_id, class_id);
            if (first_term == term_id)
            {
                if (mycode.IsUserExist("select Id from Exam_assesment_total_no_studentwise where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_assesment_total_no_studentwise (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id,@Section)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Term_id", term_id);
                    cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
                    cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_assesment_total_no_studentwise set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id,Section=@Section where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                    cmd = new SqlCommand(query);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
            else
            {
                if (mycode.IsUserExist("select Id from Exam_assesment_total_no_studentwise_tII where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_assesment_total_no_studentwise_tII (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id,@Section)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Term_id", term_id);
                    cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
                    cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    SqlCommand cmd;
                    string query = "Update Exam_assesment_total_no_studentwise_tII set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id,Section=@Section where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                    cmd = new SqlCommand(query);
                    if (marks_type == "Marks")
                    {
                        cmd.Parameters.AddWithValue("@Marks", marks);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Marks", total_no);
                    }
                    cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                    cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }
        #endregion


        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_classes();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_classes()
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {
                ddlclass.Focus();
                Alertme("Please select class.", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddlclass.SelectedValue + "'  order by Section");
                mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddlclass.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
                mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "select Subject_name,Subject_id from Subject_Master where course_id='" + ddlclass.SelectedValue + "' order by Subject_position asc");
            }
        }

        protected void ddl_CourseCat_p_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    ddl_CourseCat_p.Focus();
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    mycode.bind_ddl(ddl_section_p, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat_p.SelectedValue + "'  order by Section");
                    mycode.bind_all_ddl_with_id(ddl_term_p, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Session_Id='" + ddl_session_p.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");

                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_catculate_heighest_mark_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select session", "warning");
                    ddl_session_p.Focus();
                }
                else if (ddl_CourseCat_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select class", "warning");
                    ddl_CourseCat_p.Focus();
                }
                else if (ddl_section_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select section", "warning");
                    ddl_section_p.Focus();
                }
                else if (ddl_term_p.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select term", "warning");
                    ddl_term_p.Focus();
                }
                else
                {
                    My.exeSql("delete from Exam_assesment_total_no_studentwise where Session_id='" + ddl_session_p.SelectedValue + "' and Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Term_id='" + ddl_term_p.SelectedValue + "'  and Section='" + ddl_section_p.Text + "'; delete from Exam_assesment_total_no_studentwise_tII where Session_id='" + ddl_session_p.SelectedValue + "' and Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Term_id='" + ddl_term_p.SelectedValue + "'  and Section='" + ddl_section_p.Text + "'; delete from Exam_highest_mark_of_subject_termwise where Session_id='" + ddl_session_p.SelectedValue + "' and Class_id='" + ddl_CourseCat_p.SelectedValue + "' and Term_id='" + ddl_term_p.SelectedValue + "'  and Section='" + ddl_section_p.Text + "'");
                    ddlsession.SelectedValue = ddl_session_p.SelectedValue;
                    ddlclass.SelectedValue = ddl_CourseCat_p.SelectedValue;
                    fetch_classes();
                    ddl_section.Text = ddl_section_p.Text;
                    ddl_term.SelectedValue = ddl_term_p.SelectedValue;
                    string firm_id = "";
                    DataTable dtF = My.dataTable("select firm_id from Firm_Details");
                    if (dtF.Rows.Count > 0)
                    {
                        firm_id = dtF.Rows[0]["firm_id"].ToString();
                    }

                    string query = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,ar.rollnumber from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Section='" + ddl_section.Text + "' and em.Branch_id=" + ViewState["Branchid"].ToString() + "  and  em.Term=" + ddl_term.SelectedValue + " and ar.Status=1 order by ar.rollnumber asc";
                    SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Exam_marks");
                    DataTable dt = ds.Tables[0];
                    int rowcount1 = dt.Rows.Count;
                    if (rowcount1 == 0)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            prep_final_term(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Branch_id"].ToString(), ddl_term.SelectedValue, firm_id);
                        }
                    }
                    save_heist_mark(); 
                    fetch_data();
                    //==========================
                    Alertme("Highest mark calculation has been done of Class : " + ddlclass.SelectedItem.Text + " & Section : " + ddl_section.SelectedItem.Text, "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_heist_mark()
        {
            string tableName = "Exam_assesment_total_no_studentwise_tII";
            string first_term = Exam_setting.get_first_term(ddlsession.SelectedValue, ViewState["Branchid"].ToString(), ddlclass.SelectedValue);
            if (first_term == ddl_term.SelectedValue)
            {
                tableName = "Exam_assesment_total_no_studentwise";
            }
            string query = "Select * from Subject_Master where course_id='" + ddlclass.SelectedValue + "'";
            DataTable dsubjt = My.dataTable(query);
            if (dsubjt.Rows.Count > 0)
            {
                foreach (DataRow dr in dsubjt.Rows)
                {
                    DataTable dt = mycode.FillData("select top 1 * from (select sum(convert(float, Marks)) as Marks from " + tableName + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + dr["Subject_id"].ToString() + "' and Section='" + ddl_section.Text + "' group by Admission_no)t order by Marks desc");
                    if (dt.Rows.Count > 0)
                    {
                        if (mycode.IsUserExist("select Id from Exam_highest_mark_of_subject_termwise where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id='" + ddlclass.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Subject_id='" + dr["Subject_id"].ToString() + "' and Section='" + ddl_section.Text + "'"))
                        {
                            SqlCommand cmd;
                            string queryins = "INSERT INTO Exam_highest_mark_of_subject_termwise (Session_id,Branch_id,Class_id,Section,Term_id,Subject_id,Marks,Created_date,Created_idate,Created_by) values (@Session_id,@Branch_id,@Class_id,@Section,@Term_id,@Subject_id,@Marks,@Created_date,@Created_idate,@Created_by)";
                            cmd = new SqlCommand(queryins);
                            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                            cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                            cmd.Parameters.AddWithValue("@Subject_id", dr["Subject_id"].ToString());
                            cmd.Parameters.AddWithValue("@Marks", dt.Rows[0]["Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string queryins = "Update Exam_highest_mark_of_subject_termwise set Marks=@Marks where Session_id=@Session_id and Class_id=@Class_id and Term_id=@Term_id and Subject_id=@Subject_id and Branch_id=@Branch_id and Section=@Section";
                            cmd = new SqlCommand(queryins);
                            cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                            cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                            cmd.Parameters.AddWithValue("@Subject_id", dr["Subject_id"].ToString());
                            cmd.Parameters.AddWithValue("@Marks", dt.Rows[0]["Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                    }
                }
            }
        }
    }
}