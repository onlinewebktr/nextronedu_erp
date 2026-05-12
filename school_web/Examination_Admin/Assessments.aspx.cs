using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.Services;
namespace school_web.Examination_Admin
{
    public partial class Assessments : System.Web.UI.Page
    {
        Examination em = new Examination();
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

                    ViewState["Userid"] = Session["Admin"].ToString();

                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id(ddl_current_session, "select Session,session_id from session_details");
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    Bind_All_Assessment();

                }
            }
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
        private void Bind_All_Assessment()
        {
            string query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=Exam_Assessment_Details.Class_id) as Course_Name,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=Exam_Assessment_Details.Grade_System_Id) as Grade_Name,(Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Session_Id=Exam_Assessment_Details.Session_Id) as Term_Name from Exam_Assessment_Details  where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ddlsession.SelectedValue + " order by Sequence_No asc";
            Bind_Gride_data(query);
        }

        private void Bind_Gride_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no assessment name added", "warning");
                grid_grade.DataSource = null;
                grid_grade.DataBind();

            }
            else
            {

                grid_grade.DataSource = dt;
                grid_grade.DataBind();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Assessments_Id = (Label)row.FindControl("lbl_Assessments_Id");
                Response.Redirect("Set_Assessment.aspx?Assessment_Id=" + lbl_Assessments_Id.Text, false);
            }
            catch
            {
            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Assessments_Id = (Label)row.FindControl("lbl_Assessments_Id");
                mycode.executequery("delete from  Exam_Assessment_Details where Assessment_Id='" + lbl_Assessments_Id.Text + "' and Branch_Id='" + ViewState["branchid"].ToString() + "'");
                Alertme("Assessment details has been deleted", "success");
                Bind_All_Assessment();
            }
            catch
            {
            }
        }

        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPathname(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Assessment_Name from Exam_Assessment_Details where Assessment_Name LIKE '%'+@SearchGetRooPath+'%' and Session_id='" + Session_id + "'  ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchGetRooPath", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Assessment_Name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }




        #endregion

        protected void grid_grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Istatus = (Label)e.Row.FindControl("lbl_Istatus");
                Label lbl_status = (Label)e.Row.FindControl("lbl_status");
                if (lbl_Istatus.Text == "True")
                {
                    lbl_status.Text = "Active";
                    lbl_status.CssClass = "badge badge-success ml-2";
                }
                else
                {
                    lbl_status.Text = "Inactive";
                    lbl_status.CssClass = "badge badge-danger ml-2";
                }
                Label lbl_Scholastic_Co_scholastic = (Label)e.Row.FindControl("lbl_Scholastic_Co_scholastic");
                if (lbl_Scholastic_Co_scholastic.Text == "Scholastic")
                {
                    lbl_Scholastic_Co_scholastic.CssClass = "badge badge-Scholastic ml-2";
                }
                else
                {
                    lbl_Scholastic_Co_scholastic.CssClass = "badge badge-coScholastic ml-2";
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_searchby.Text == "")
            {
                Alertme("Please enter search name", "warning");
            }
            else
            {
                string query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=Exam_Assessment_Details.Class_id) as Course_Name,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=Exam_Assessment_Details.Grade_System_Id) as Grade_Name,(Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Session_Id=Exam_Assessment_Details.Session_Id) as Term_Name from Exam_Assessment_Details where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ddlsession.SelectedValue + " and   Assessment_Name='" + txt_searchby.Text + "' order by Sequence_No asc";
                Bind_Gride_data(query);
            }
        }



        protected void btn_fina_class_wise_Click1(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedValue == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                if (ddl_examterm.SelectedItem.Text == "All")
                {
                    string query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=Exam_Assessment_Details.Class_id) as Course_Name,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=Exam_Assessment_Details.Grade_System_Id) as Grade_Name,(Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Session_Id=Exam_Assessment_Details.Session_Id) as Term_Name from Exam_Assessment_Details where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ddlsession.SelectedValue + " and   Class_id='" + ddl_class.SelectedValue + "' order by Sequence_No asc";
                    Bind_Gride_data(query);
                }
                else
                {
                    string query = "Select *,(Select top 1 Course_Name from Add_course_table where course_id=Exam_Assessment_Details.Class_id) as Course_Name,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=Exam_Assessment_Details.Grade_System_Id) as Grade_Name,(Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Session_Id=Exam_Assessment_Details.Session_Id) as Term_Name from Exam_Assessment_Details where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ddlsession.SelectedValue + " and   Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_examterm.SelectedValue + "' order by Sequence_No asc";
                    Bind_Gride_data(query);
                }
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_All(ddl_examterm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' order by Sequence_No asc");
            }
        }

        protected void btn_importsubject_Click(object sender, EventArgs e)
        {
            try
            {
                Button lnk = (Button)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Assessments_Id = (Label)row.FindControl("lbl_Assessments_Id");
                Label lbl_Branch_Id = (Label)row.FindControl("lbl_Branch_Id");
                Label lbl_Exam_Term_Id = (Label)row.FindControl("lbl_Exam_Term_Id");
                Label lbl_Scholastic_Co_scholastic = (Label)row.FindControl("lbl_Scholastic_Co_scholastic");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Grade_System_Id = (Label)row.FindControl("lbl_Grade_System_Id");
                Label lbl_Session_Id = (Label)row.FindControl("lbl_Session_Id");
                string full_marks_and_mm_marks = Examination.get_marks_assiment(lbl_Assessments_Id.Text, lbl_Class_id.Text, lbl_Session_Id.Text, lbl_Branch_Id.Text, lbl_Exam_Term_Id.Text);
                string[] stringSeparators = new string[] { "/" };
                string[] arr = full_marks_and_mm_marks.Split(stringSeparators, StringSplitOptions.None);
                string fullmarks = arr[0];
                string passmarks = arr[1];
                import_data_subject(lbl_Assessments_Id.Text, lbl_Branch_Id.Text, lbl_Exam_Term_Id.Text, lbl_Scholastic_Co_scholastic.Text, lbl_Class_id.Text, lbl_Session_Id.Text, fullmarks, passmarks, lbl_Grade_System_Id.Text);
                Alertme("Subject has been imported successfully done", "success");
            }
            catch
            {
            }
        }

        private void import_data_subject(string Assessments_Id, string Branch_Id, string Exam_Term_Id, string Scholastic_Co_scholastic, string Class_id, string Session_Id, string fullmarks, string passmarks, string Grade_System_Id)
        {
            //My.exeSql("delete from  Exam_Assessment_Subject_Mapping_Details where Class_id=" + Class_id + " and Branch_Id='" + Branch_Id + "' and Session_Id=" + Session_Id + " and Exam_Term_Id=" + Exam_Term_Id + " and Assessment_Id=" + Assessments_Id + " and Grade_System_Id='" + Grade_System_Id + "'; delete Exam_Subject_Sub_Level where Session_Id='" + Session_Id + "' and Branch_Id='" + Branch_Id + "' and Exam_Term_Id='" + Exam_Term_Id + "' and Assessment_Id='" + Assessments_Id + "' and Class_id='" + Class_id + "' and Grade_System_Id='" + Grade_System_Id + "'");


            string query = "Select * from  Subject_Master where course_id=" + Class_id + " and Branch_id=" + Branch_Id + " and Subject_Type_Scholastic_Co_Scholastic='" + Scholastic_Co_scholastic + "' and Is_mandatory=1 order by Subject_position asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    string Subject_name = dt.Rows[i]["Subject_name"].ToString();
                    string Subject_Short_Name = dt.Rows[i]["Subject_Short_Name"].ToString();
                    string Subject_position = dt.Rows[i]["Subject_position"].ToString();
                    string Subject_id = dt.Rows[i]["Subject_id"].ToString();




                    insert_data_Exam_Assessment_Subject_Mapping_Details(Subject_name, Subject_Short_Name, Subject_position, Subject_id, Assessments_Id, Branch_Id, Exam_Term_Id, Scholastic_Co_scholastic, Class_id, Session_Id, fullmarks, passmarks, Grade_System_Id);
                }

            }

        }

        private void insert_data_Exam_Assessment_Subject_Mapping_Details(string Subject_name, string Subject_Short_Name, string Subject_position, string Subject_id, string Assessments_Id, string Branch_Id, string Exam_Term_Id, string Scholastic_Co_scholastic, string Class_id, string Session_Id, string fullmarks, string passmarks, string Grade_System_Id)
        {
            SqlCommand cmd;
            string query = "Select * from  Exam_Assessment_Subject_Mapping_Details where Class_id=" + Class_id + " and Branch_Id=" + Branch_Id + " and  Session_Id=" + Session_Id + " and Exam_Term_Id=" + Exam_Term_Id + " and Assessment_Id=" + Assessments_Id + " and Subject_id=" + Subject_id + "";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                string Assessment_Subject_Id = Examination.auto_serialS("Assessment_Subject_Id", ViewState["branchid"].ToString());
                string query2 = "INSERT INTO Exam_Assessment_Subject_Mapping_Details (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Short_Name,Sequence_No,Created_By,Created_Date_Time,Class_id,Assessment_Subject_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Grade_System_Id,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Is_Mandatory_to_pass,Select_Data) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Short_Name,@Sequence_No,@Created_By,@Created_Date_Time,@Class_id,@Assessment_Subject_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Grade_System_Id,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Is_Mandatory_to_pass,@Select_Data)";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@Session_Id", Session_Id);
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
                cmd.Parameters.AddWithValue("@Istatus", 1);
                cmd.Parameters.AddWithValue("@Exam_Term_Id", Exam_Term_Id);
                cmd.Parameters.AddWithValue("@Assessment_Id", Assessments_Id);
                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Short_Name", Subject_Short_Name);
                cmd.Parameters.AddWithValue("@Sequence_No", Subject_position);
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                cmd.Parameters.AddWithValue("@Assessment_Subject_Id", Assessment_Subject_Id);
                cmd.Parameters.AddWithValue("@Maximum_Marks", fullmarks);
                cmd.Parameters.AddWithValue("@Cut_Off_Percentage", passmarks);
                cmd.Parameters.AddWithValue("@Calculation_Type", "Sum");
                cmd.Parameters.AddWithValue("@Grade_System_Id", Grade_System_Id);
                cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", "0");
                cmd.Parameters.AddWithValue("@Consider_best", "0");
                cmd.Parameters.AddWithValue("@Pass_criteria", "0");
                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                cmd.Parameters.AddWithValue("@Select_Data", 0);

                if (My.InsertUpdateData(cmd))
                {

                    // insert_subject activityt


                    ViewState["Subject_Sub_Level_Id"] = Examination.auto_serialS("Subject_Sub_Level_Id", ViewState["branchid"].ToString());
                    query2 = "INSERT INTO Exam_Subject_Sub_Level (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Assessment_Subject_Id,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Is_Mandatory_to_pass,Class_id,Subject_Sub_Level_Id,Subject_Activity_Name,Is_Distinction,Distinction_Marks,Start_Date_Marks,End_Date_Marks,Start_IDate_Marks,End_IDate_Marks,Is_save_marks) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Assessment_Subject_Id,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Is_Mandatory_to_pass,@Class_id,@Subject_Sub_Level_Id,@Subject_Activity_Name,@Is_Distinction,@Distinction_Marks,@Start_Date_Marks,@End_Date_Marks,@Start_IDate_Marks,@End_IDate_Marks,@Is_save_marks)";
                    cmd = new SqlCommand(query2);
                    cmd.Parameters.AddWithValue("@Is_save_marks", 0);
                    cmd.Parameters.AddWithValue("@Class_id", Class_id);
                    cmd.Parameters.AddWithValue("@Session_Id", Session_Id);
                    cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
                    cmd.Parameters.AddWithValue("@Istatus", 1);
                    cmd.Parameters.AddWithValue("@Exam_Term_Id", Exam_Term_Id);
                    cmd.Parameters.AddWithValue("@Assessment_Id", Assessments_Id);
                    cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                    cmd.Parameters.AddWithValue("@Assessment_Subject_Id", Assessment_Subject_Id);
                    cmd.Parameters.AddWithValue("@Short_Name", Subject_Short_Name);
                    cmd.Parameters.AddWithValue("@Sequence_No", Subject_position);
                    cmd.Parameters.AddWithValue("@Grade_System_Id", Grade_System_Id);
                    cmd.Parameters.AddWithValue("@Maximum_Marks", fullmarks);
                    cmd.Parameters.AddWithValue("@Cut_Off_Percentage", passmarks);
                    cmd.Parameters.AddWithValue("@Calculation_Type", "Average");
                    cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", 0);
                    cmd.Parameters.AddWithValue("@Consider_best", 0);
                    cmd.Parameters.AddWithValue("@Pass_criteria", 0);
                    cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Subject_Sub_Level_Id", ViewState["Subject_Sub_Level_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Subject_Activity_Name", Subject_name);
                    cmd.Parameters.AddWithValue("@Distinction_Marks", "");
                    cmd.Parameters.AddWithValue("@Is_Distinction", 0);
                    string startdate = mycode.date();
                    string nextdate = mycode.fiftendaysnext();
                    int idate = mycode.ConvertStringToiDateup(startdate);
                    int idate2 = mycode.ConvertStringToiDateup(nextdate);
                    cmd.Parameters.AddWithValue("@Start_Date_Marks", startdate);
                    cmd.Parameters.AddWithValue("@End_Date_Marks", nextdate);
                    cmd.Parameters.AddWithValue("@Start_IDate_Marks", idate);
                    cmd.Parameters.AddWithValue("@End_IDate_Marks", idate2);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
            else
            {
            }
        }

        protected void ddl_copy_to_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose copy from session", "warning");
                    ddl_current_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ddl_copy_to.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose copy to next term/session", "warning");
                    ddl_copy_to.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (ddl_copy_to.SelectedItem.Text == "Term")
                    {
                        mycode.bind_all_ddl_with_id(ddl_copy_from_term, "select DISTINCT Term_Name,Short_Name from Exam_Term_Details where Session_Id='" + ddl_current_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "'");
                        copy_to_termDV.Visible = true;
                        copy_to_SessionDV.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        mycode.bind_all_ddl_with_id(ddl_copy_to_session, "select Session,session_id from session_details where session_id!='" + ddl_current_session.SelectedValue + "'");
                        copy_to_SessionDV.Visible = true;
                        copy_to_termDV.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        //================================
        protected void btn_copy_setting_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["IsSaved"] = "0";
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select copy from session.", "warning");
                    ddl_current_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ddl_copy_to.SelectedItem.Text == "Select")
                {
                    Alertme("Please select copy to next term/session.", "warning");
                    ddl_copy_to.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (ddl_copy_to.SelectedItem.Text == "Term")
                    {
                        if (ddl_copy_from_term.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select copy from term.", "warning");
                            ddl_copy_from_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                        else if (ddl_copy_to_term_for_term.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select copy to next term.", "warning");
                            ddl_copy_to_term_for_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                        else
                        {
                            copy_assesment_setting("Term");
                            if (ViewState["IsSaved"].ToString() == "1")
                            {
                                Alertme("Assessment has been copied successfully for new term.", "success");
                                Bind_All_Assessment();
                            }
                        }
                    }
                    else
                    {
                        if (ddl_copy_to_session.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select copy to session.", "warning");
                            ddl_copy_to_session.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                        else
                        {
                            copy_assesment_setting("Session");
                            if (ViewState["IsSaved"].ToString() == "1")
                            {
                                Alertme("Assessment has been copied successfully for next session.", "success");
                                Bind_All_Assessment();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void copy_assesment_setting(string CopyType)
        {
            if (CopyType == "Term")
            {
                DataTable dtS = mycode.FillData("select t1.*,t2.Term_Name from Exam_Assessment_Details t1 join Exam_Term_Details t2 on t1.Exam_Term_Id=t2.Exam_Term_Id where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_id='" + ViewState["branchid"].ToString() + "' and t2.Term_Name='" + ddl_copy_from_term.SelectedItem.Text + "'");
                if (dtS.Rows.Count > 0)
                {
                    for (int i = 0; i < dtS.Rows.Count; i++)
                    {
                        string term_id = get_term_id(dtS.Rows[i]["Session_Id"].ToString(), ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), ddl_copy_to_term_for_term.SelectedItem.Text);
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = term_id.Split(stringSeparatorss, StringSplitOptions.None);
                        term_id = arrs[0];
                        string grade_id = arrs[1];

                        if (mycode.IsUserExist("select Id from Exam_Assessment_Details where Session_Id='" + ddl_current_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + term_id + "'  and Class_id='" + dtS.Rows[i]["Class_id"].ToString() + "' and Assessment_Name='" + dtS.Rows[i]["Assessment_Name"].ToString() + "'"))
                        {
                            string Assessment_Id = Examination.auto_serialS("Assessment_Id", ViewState["branchid"].ToString());
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Assessment_Details (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Assessment_Name,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Is_Mandatory_to_pass,Scholastic_Co_scholastic,Select_Data,Class_id,Term_assesment_unique_id,Term_assesment_update_status) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Assessment_Name,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Is_Mandatory_to_pass,@Scholastic_Co_scholastic,@Select_Data,@Class_id,@Term_assesment_unique_id,@Term_assesment_update_status)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_Id", ddl_current_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", term_id);
                            cmd.Parameters.AddWithValue("@Assessment_Id", Assessment_Id);
                            cmd.Parameters.AddWithValue("@Assessment_Name", dtS.Rows[i]["Assessment_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Short_Name", dtS.Rows[i]["Short_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Sequence_No", dtS.Rows[i]["Sequence_No"].ToString());
                            cmd.Parameters.AddWithValue("@Grade_System_Id", grade_id);
                            cmd.Parameters.AddWithValue("@Maximum_Marks", dtS.Rows[i]["Maximum_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Cut_Off_Percentage", dtS.Rows[i]["Cut_Off_Percentage"].ToString());
                            cmd.Parameters.AddWithValue("@Calculation_Type", dtS.Rows[i]["Calculation_Type"].ToString());
                            cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", dtS.Rows[i]["Is_Advanced_Advanced_Setting"].ToString());
                            cmd.Parameters.AddWithValue("@Consider_best", dtS.Rows[i]["Consider_best"].ToString());
                            cmd.Parameters.AddWithValue("@Pass_criteria", dtS.Rows[i]["Pass_criteria"].ToString());
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", dtS.Rows[i]["Is_Mandatory_to_pass"].ToString());
                            cmd.Parameters.AddWithValue("@Scholastic_Co_scholastic", dtS.Rows[i]["Scholastic_Co_scholastic"].ToString());
                            cmd.Parameters.AddWithValue("@Select_Data", dtS.Rows[i]["Select_Data"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Term_assesment_unique_id", dtS.Rows[i]["Term_assesment_unique_id"].ToString());
                            cmd.Parameters.AddWithValue("@Term_assesment_update_status", dtS.Rows[i]["Term_assesment_update_status"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                ViewState["IsSaved"] = "1";
                            }
                        }
                        else
                        {
                            Alertme("Assessment already exist to selected term.", "warning");
                            ddl_copy_to_term_for_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
                else
                {
                    Alertme("Assessment not found to selected term.", "warning");
                    ddl_copy_from_term.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            else
            {

                DataTable dtS = mycode.FillData("select t1.*,t2.Term_Name from Exam_Assessment_Details t1 join Exam_Term_Details t2 on t1.Exam_Term_Id=t2.Exam_Term_Id  where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_id='" + ViewState["branchid"].ToString() + "'");
                if (dtS.Rows.Count > 0)
                {
                    for (int i = 0; i < dtS.Rows.Count; i++)
                    {
                        string term_id = get_term_id(ddl_copy_to_session.SelectedValue, ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), dtS.Rows[i]["Term_Name"].ToString());
                        string[] stringSeparatorss = new string[] { "~" };
                        string[] arrs = term_id.Split(stringSeparatorss, StringSplitOptions.None);
                        term_id = arrs[0];
                        string grade_id = arrs[1];
                        if (mycode.IsUserExist("select Id from Exam_Assessment_Details where  Session_Id='" + ddl_copy_to_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + term_id + "' and Class_id='" + dtS.Rows[i]["Class_id"].ToString() + "' and Assessment_Name='" + dtS.Rows[i]["Assessment_Name"].ToString() + "'"))
                        {
                            string Assessment_Id = Examination.auto_serialS("Assessment_Id", ViewState["branchid"].ToString());
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Assessment_Details (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Assessment_Name,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Is_Mandatory_to_pass,Scholastic_Co_scholastic,Select_Data,Class_id,Term_assesment_unique_id,Term_assesment_update_status) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Assessment_Name,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Is_Mandatory_to_pass,@Scholastic_Co_scholastic,@Select_Data,@Class_id,@Term_assesment_unique_id,@Term_assesment_update_status)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", term_id);
                            cmd.Parameters.AddWithValue("@Assessment_Id", Assessment_Id);
                            cmd.Parameters.AddWithValue("@Assessment_Name", dtS.Rows[i]["Assessment_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Short_Name", dtS.Rows[i]["Short_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Sequence_No", dtS.Rows[i]["Sequence_No"].ToString());
                            cmd.Parameters.AddWithValue("@Grade_System_Id", grade_id);
                            cmd.Parameters.AddWithValue("@Maximum_Marks", dtS.Rows[i]["Maximum_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Cut_Off_Percentage", dtS.Rows[i]["Cut_Off_Percentage"].ToString());
                            cmd.Parameters.AddWithValue("@Calculation_Type", dtS.Rows[i]["Calculation_Type"].ToString());
                            cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", dtS.Rows[i]["Is_Advanced_Advanced_Setting"].ToString());
                            cmd.Parameters.AddWithValue("@Consider_best", dtS.Rows[i]["Consider_best"].ToString());
                            cmd.Parameters.AddWithValue("@Pass_criteria", dtS.Rows[i]["Pass_criteria"].ToString());
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", dtS.Rows[i]["Is_Mandatory_to_pass"].ToString());
                            cmd.Parameters.AddWithValue("@Scholastic_Co_scholastic", dtS.Rows[i]["Scholastic_Co_scholastic"].ToString());
                            cmd.Parameters.AddWithValue("@Select_Data", dtS.Rows[i]["Select_Data"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Term_assesment_unique_id", dtS.Rows[i]["Term_assesment_unique_id"].ToString());
                            cmd.Parameters.AddWithValue("@Term_assesment_update_status", dtS.Rows[i]["Term_assesment_update_status"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                ViewState["IsSaved"] = "1";
                            }
                        }
                        else
                        {
                            Alertme("Assessment already exist to selected session.", "warning");
                            ddl_copy_to_session.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
                else
                {
                    Alertme("Assessment not found to selected session.", "warning");
                    ddl_current_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
        }



        private string get_term_id(string Session_Id, string branchid, string Class_id, string term_name)
        {
            string termId = "0~0";
            try
            {
                string query = "select Exam_Term_Id,Grade_System_Id from Exam_Term_Details where Session_Id='" + Session_Id + "' and Branch_Id='" + branchid + "' and Class_id='" + Class_id + "' and Term_Name='" + term_name + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count > 0)
                {
                    termId = dt.Rows[0]["Exam_Term_Id"].ToString() + "~" + dt.Rows[0]["Grade_System_Id"].ToString();
                }
                return termId;
            }
            catch (Exception ex)
            {
                return termId;
            }
        }


        protected void ddl_copy_from_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                mycode.bind_all_ddl_with_id(ddl_copy_to_term_for_term, "select DISTINCT Term_Name,Short_Name from Exam_Term_Details where Session_Id='" + ddl_current_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Term_Name!='" + ddl_copy_from_term.SelectedItem.Text + "'");
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_current_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_copy_to_session, "select Session,session_id from session_details where session_id!='" + ddl_current_session.SelectedValue + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}