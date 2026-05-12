using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Examination_Admin
{
    public partial class Subject_Activity : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        string Assessment_Name = "Select top 1 Assessment_Name from Exam_Assessment_Details where Assessment_Id=essl.Assessment_Id and Branch_Id=essl.Branch_Id";
        string Course_Name = "Select top 1 Course_Name from Add_course_table where course_id=essl.Class_id";
        string Grade_Name = "Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=essl.Grade_System_Id";

        string Term_Name = "Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=essl.Exam_Term_Id and Branch_Id=essl.Branch_Id and Session_Id=essl.Session_Id";
        string Subject_name = "Select top  1 Subject_name from Subject_Master  where  course_id=essl.Class_id and Subject_id=essl.Subject_id and Branch_id= essl.Branch_Id";
        string Subject_Type_Scholastic_Co_Scholastic = "Select top  1 Subject_Type_Scholastic_Co_Scholastic from Subject_Master  where  course_id=essl.Class_id and Subject_id=essl.Subject_id and Branch_id= essl.Branch_Id";
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
                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id_cap_All(ddl_exam_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id(ddl_current_session, "select Session,session_id from session_details");
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    mycode.bind_all_ddl_with_id(ddl_change_date_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    ddl_change_date_session.SelectedValue = ddlsession.SelectedValue;

                    try
                    {
                        ddl_class.SelectedValue = My.get_top_one_class();
                        mycode.bind_all_ddl_with_id(ddl_examterm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id=" + ViewState["branchid"].ToString() + " order by Sequence_No asc");
                    }
                    catch (Exception ex)
                    {
                    }
                    get_all_exam_of_session();
                    Bind_subject_Exam_Activity();
                }
            }
        }

        private void Bind_subject_Exam_Activity()
        {
            string query = "Select essl.*,(" + Subject_Type_Scholastic_Co_Scholastic + ") as Subject_Type_Scholastic_Co_Scholastic,(" + Assessment_Name + ") as Assessment_Name,(" + Course_Name + ") as Course_Name,(" + Grade_Name + ") as Grade_Name,(" + Term_Name + ") as Term_Name,(" + Subject_name + ") as Subject_name from Exam_Subject_Sub_Level essl where essl.Session_Id='" + ddlsession.SelectedValue + "' and essl.Branch_Id=" + ViewState["branchid"].ToString() + " and essl.Class_id='" + ddl_class.SelectedValue + "' order by essl.Sequence_No asc";
            Bind_final_data(query);
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
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_examterm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id=" + ViewState["branchid"].ToString() + " order by Sequence_No asc");
            }
        }
        protected void ddl_examterm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_assessment, "Select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id=" + ViewState["branchid"].ToString() + " and Exam_Term_Id=" + ddl_examterm.SelectedValue + " order by Sequence_No asc");

            }
        }

        protected void ddl_assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_assessment.SelectedItem.Text == "Select")
            {
                Alertme("Please select assessment", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_cap_All(ddl_subject, "Select sm.Subject_name,sm.Subject_id from Exam_Assessment_Subject_Mapping_Details easm join Subject_Master sm on easm.Class_id=sm.course_id and easm.Subject_id=sm.Subject_id and easm.Branch_Id=sm.Branch_id where easm.Class_id='" + ddl_class.SelectedValue + "' and easm.Session_Id='" + ddlsession.SelectedValue + "' and easm.Branch_Id=" + ViewState["branchid"].ToString() + " and easm.Exam_Term_Id=" + ddl_examterm.SelectedValue + " and easm.Assessment_Id=" + ddl_assessment.SelectedValue + " order by easm.Sequence_No asc");
            }
        }

        protected void btn_fina_data_search_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_assessment.SelectedItem.Text == "Select")
            {
                Alertme("Please select assessment", "warning");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject", "warning");
            }
            else
            {
                string query = "";
                if (ddl_subject.SelectedItem.Text == "ALL")
                {
                    query = "Select essl.*,(" + Subject_Type_Scholastic_Co_Scholastic + ") as Subject_Type_Scholastic_Co_Scholastic,(" + Assessment_Name + ") as Assessment_Name,(" + Course_Name + ") as Course_Name,(" + Grade_Name + ") as Grade_Name,(" + Term_Name + ") as Term_Name,(" + Subject_name + ") as Subject_name from Exam_Subject_Sub_Level essl where essl.Class_id='" + ddl_class.SelectedValue + "' and essl.Session_Id='" + ddlsession.SelectedValue + "' and essl.Branch_Id=" + ViewState["branchid"].ToString() + " and essl.Exam_Term_Id=" + ddl_examterm.SelectedValue + " and essl.Assessment_Id=" + ddl_assessment.SelectedValue + "  order by essl.Sequence_No asc ";
                    Bind_final_data(query);
                }
                else
                {
                    query = "Select essl.*,(" + Subject_Type_Scholastic_Co_Scholastic + ") as Subject_Type_Scholastic_Co_Scholastic,(" + Assessment_Name + ") as Assessment_Name,(" + Course_Name + ") as Course_Name,(" + Grade_Name + ") as Grade_Name,(" + Term_Name + ") as Term_Name,(" + Subject_name + ") as Subject_name from Exam_Subject_Sub_Level essl where essl.Class_id='" + ddl_class.SelectedValue + "' and essl.Session_Id='" + ddlsession.SelectedValue + "' and essl.Branch_Id=" + ViewState["branchid"].ToString() + " and essl.Exam_Term_Id=" + ddl_examterm.SelectedValue + " and essl.Assessment_Id=" + ddl_assessment.SelectedValue + " and essl.Subject_id=" + ddl_subject.SelectedValue + " order by essl.Sequence_No asc ";
                    Bind_final_data(query);
                }
            }
        }

        private void Bind_final_data(string query)
        {
            ViewState["query"] = query;
            DataTable dt2 = mycode.FillData(query);
            if (dt2.Rows.Count == 0)
            {
                grid_grade.DataSource = null;
                grid_grade.DataBind();
            }
            else
            {
                grid_grade.DataSource = dt2;
                grid_grade.DataBind();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Subject_Sub_Level_Id = (Label)row.FindControl("lbl_Subject_Sub_Level_Id");
                Response.Redirect("Set_Subject_Activity.aspx?Subject_Sub_Level_Id=" + lbl_Subject_Sub_Level_Id.Text, false);
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
                Label lbl_id = (Label)row.FindControl("lbl_id");
                mycode.executequery("delete from  Exam_Subject_Sub_Level where Id='" + lbl_id.Text + "' ");

                Alertme("Subject Activity details has been deleted", "success");
                Bind_final_data(ViewState["query"].ToString());
            }
            catch
            {
            }
        }

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


                Label lbl_Subject_Type_Scholastic_Co_Scholastic = (Label)e.Row.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic");
                if (lbl_Subject_Type_Scholastic_Co_Scholastic.Text == "Scholastic")
                {
                    lbl_Subject_Type_Scholastic_Co_Scholastic.CssClass = "badge badge-Scholastic ml-2";
                }
                else
                {
                    lbl_Subject_Type_Scholastic_Co_Scholastic.CssClass = "badge badge-coScholastic ml-2";
                }
            }
        }

        protected void lnk_allow_permission_fill_permission_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Subject_Sub_Level_Id = (Label)row.FindControl("lbl_Subject_Sub_Level_Id");
                Label lbl_Marks_Entry_Deadline_Date1 = (Label)row.FindControl("lbl_Marks_Entry_Deadline_Date1");
                Label lbl_Marks_Entry_Deadline_Date2 = (Label)row.FindControl("lbl_Marks_Entry_Deadline_Date2");
                Label lbl_Is_save_marks = (Label)row.FindControl("lbl_Is_save_marks");
                txt_start_date.Text = lbl_Marks_Entry_Deadline_Date1.Text;
                txt_enddate.Text = lbl_Marks_Entry_Deadline_Date2.Text;
                hd_id.Value = lbl_id.Text;
                ViewState["Subject_Sub_Level_Id"] = lbl_Subject_Sub_Level_Id.Text;
                try
                {
                    if (lbl_Is_save_marks.Text == "True")
                    {
                        chk_allow.Checked = false;
                    }
                    else
                    {
                        chk_allow.Checked = true;
                    }
                }
                catch
                {
                    chk_allow.Checked = true;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_start_date.Text == "")
            {
                Alertme("Please enter marks entry date from ", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_enddate.Text == "")
            {
                Alertme("Please enter marks entry date from ", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                int idate = mycode.ConvertStringToiDateup(txt_start_date.Text);
                int idate2 = mycode.ConvertStringToiDateup(txt_enddate.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    int allowdata = 0;
                    if (chk_allow.Checked == true)
                    {
                        allowdata = 0;//allow
                    }
                    else
                    {
                        allowdata = 1;
                    }
                    mycode.executequery("update Exam_Subject_Sub_Level set Start_Date_Marks='" + txt_start_date.Text + "',End_Date_Marks='" + txt_enddate.Text + "',Start_IDate_Marks='" + idate + "',End_IDate_Marks='" + idate2 + "',Is_save_marks=" + allowdata + " where Id=" + hd_id.Value + "");
                    mycode.executequery("update Exam_marks set  Is_save_marks=0 where  Subject_activity=" + ViewState["Subject_Sub_Level_Id"].ToString() + "  ");
                    Alertme("Marks entry permission has been successfully updated", "success");
                    Bind_final_data(ViewState["query"].ToString());
                }
            }
        }



        //========================
        protected void ddl_copy_to_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose copy from session", "warning");
                    ddl_current_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                }
                else if (ddl_copy_to.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose copy to next term/session", "warning");
                    ddl_copy_to.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                }
                else
                {
                    if (ddl_copy_to.SelectedItem.Text == "Term")
                    {
                        mycode.bind_all_ddl_with_id(ddl_copy_from_term, "select DISTINCT Term_Name,Short_Name from Exam_Term_Details where Session_Id='" + ddl_current_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "'");
                        copy_to_termDV.Visible = true;
                        copy_to_SessionDV.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                    }
                    else
                    {
                        mycode.bind_all_ddl_with_id(ddl_copy_to_session, "select Session,session_id from session_details where session_id!='" + ddl_current_session.SelectedValue + "'");
                        copy_to_SessionDV.Visible = true;
                        copy_to_termDV.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                }
                else if (ddl_copy_to.SelectedItem.Text == "Select")
                {
                    Alertme("Please select copy to next term/session.", "warning");
                    ddl_copy_to.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                }
                else
                {
                    if (ddl_copy_to.SelectedItem.Text == "Term")
                    {
                        if (ddl_copy_from_term.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select copy from term.", "warning");
                            ddl_copy_from_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                        }
                        else if (ddl_copy_to_term_for_term.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select copy to next term.", "warning");
                            ddl_copy_to_term_for_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                        }
                        else
                        {
                            copy_assesment_subj_activity_setting("Term");
                            if (ViewState["IsSaved"].ToString() == "1")
                            {
                                Alertme("Assessment Subject has been copied successfully for new term.", "success");
                                Bind_subject_Exam_Activity();
                            }
                        }
                    }
                    else
                    {
                        if (ddl_copy_to_session.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select copy to session.", "warning");
                            ddl_copy_to_session.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                        }
                        else
                        {
                            copy_assesment_subj_activity_setting("Session");
                            if (ViewState["IsSaved"].ToString() == "1")
                            {
                                Alertme("Assessment Subject has been copied successfully for next session.", "success");
                                Bind_subject_Exam_Activity();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void copy_assesment_subj_activity_setting(string CopyType)
        {
            if (CopyType == "Term")
            {
                if (mycode.IsUserExist("select t1.Id from Exam_Subject_Sub_Level t1 join Exam_Term_Details t2 on t1.Session_Id=t2.Session_Id and t1.Branch_Id=t2.Branch_Id and t1.Exam_Term_Id=t2.Exam_Term_Id and t1.Class_id=t2.Class_id where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_Id='" + ViewState["branchid"].ToString() + "' and t2.Term_Name='" + ddl_copy_to_term_for_term.SelectedItem.Text + "'"))
                {
                    DataTable dtS = mycode.FillData("select t1.*,t2.Term_Name,t3.Assessment_Name from Exam_Subject_Sub_Level t1 join Exam_Term_Details t2 on t1.Session_Id=t2.Session_Id and t1.Branch_Id=t2.Branch_Id and t1.Exam_Term_Id=t2.Exam_Term_Id and t1.Class_id=t2.Class_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Exam_Term_Id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_Id=t3.Assessment_Id where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_id='" + ViewState["branchid"].ToString() + "' and t2.Term_Name='" + ddl_copy_from_term.SelectedItem.Text + "'");
                    if (dtS.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtS.Rows.Count; i++)
                        {
                            string term_id = get_term_id(dtS.Rows[i]["Session_Id"].ToString(), ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), ddl_copy_to_term_for_term.SelectedItem.Text);
                            if (term_id == "0")
                            {
                                Alertme("Term not added.", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                                return;
                            }

                            string assesment_id = get_assesment_id(dtS.Rows[i]["Session_Id"].ToString(), ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), term_id, dtS.Rows[i]["Assessment_Name"].ToString());
                            string[] stringSeparatorss = new string[] { "~" };
                            string[] arrs = assesment_id.Split(stringSeparatorss, StringSplitOptions.None);
                            assesment_id = arrs[0];
                            string grade_id = arrs[1];
                            if (assesment_id == "0")
                            {
                                Alertme("Assessment not added.", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                                return;
                            }

                            string assessment_subject_id = em.get_Assessment_Subject_Id(dtS.Rows[i]["Session_Id"].ToString(), dtS.Rows[i]["Branch_Id"].ToString(), dtS.Rows[i]["Class_id"].ToString(), term_id, assesment_id, dtS.Rows[i]["Subject_id"].ToString());

                            string subject_Sub_Level_Id = Examination.auto_serialS("Subject_Sub_Level_Id", ViewState["branchid"].ToString());

                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Subject_Sub_Level (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Assessment_Subject_Id,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Is_Mandatory_to_pass,Class_id,Subject_Sub_Level_Id,Subject_Activity_Name,Is_Distinction,Distinction_Marks,Start_Date_Marks,End_Date_Marks,Start_IDate_Marks,End_IDate_Marks,Is_save_marks) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Assessment_Subject_Id,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Is_Mandatory_to_pass,@Class_id,@Subject_Sub_Level_Id,@Subject_Activity_Name,@Is_Distinction,@Distinction_Marks,@Start_Date_Marks,@End_Date_Marks,@Start_IDate_Marks,@End_IDate_Marks,@Is_save_marks)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_Id", ddl_current_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", term_id);
                            cmd.Parameters.AddWithValue("@Assessment_Id", assesment_id);
                            cmd.Parameters.AddWithValue("@Subject_id", dtS.Rows[i]["Subject_id"].ToString());
                            cmd.Parameters.AddWithValue("@Assessment_Subject_Id", assessment_subject_id);
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
                            cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Subject_Sub_Level_Id", subject_Sub_Level_Id);
                            cmd.Parameters.AddWithValue("@Subject_Activity_Name", dtS.Rows[i]["Subject_Activity_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Is_Distinction", dtS.Rows[i]["Is_Distinction"].ToString());
                            cmd.Parameters.AddWithValue("@Distinction_Marks", dtS.Rows[i]["Distinction_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Start_Date_Marks", dtS.Rows[i]["Start_Date_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@End_Date_Marks", dtS.Rows[i]["End_Date_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Start_IDate_Marks", dtS.Rows[i]["Start_IDate_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@End_IDate_Marks", dtS.Rows[i]["End_IDate_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Is_save_marks", dtS.Rows[i]["Is_save_marks"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                ViewState["IsSaved"] = "1";
                            }
                        }
                    }
                    else
                    {
                        Alertme("Subject activity not found to selected term.", "warning");
                        ddl_copy_from_term.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                    }
                }
                else
                {
                    Alertme("Subject activity already copied for selected term.", "warning");
                    ddl_copy_to_term_for_term.Focus();
                }
            }
            else
            {
                if (mycode.IsUserExist("select t1.Id from Exam_Subject_Sub_Level t1 join Exam_Term_Details t2 on t1.Session_Id=t2.Session_Id and t1.Branch_Id=t2.Branch_Id and t1.Exam_Term_Id=t2.Exam_Term_Id and t1.Class_id=t2.Class_id where t1.Session_Id='" + ddl_copy_to_session.SelectedValue + "' and t1.Branch_Id='" + ViewState["branchid"].ToString() + "'"))
                {
                    DataTable dtS = mycode.FillData("select t1.*,t2.Term_Name,t3.Assessment_Name from Exam_Subject_Sub_Level t1 join Exam_Term_Details t2 on t1.Session_Id=t2.Session_Id and t1.Branch_Id=t2.Branch_Id and t1.Exam_Term_Id=t2.Exam_Term_Id and t1.Class_id=t2.Class_id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Exam_Term_Id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_Id=t3.Assessment_Id where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_id='" + ViewState["branchid"].ToString() + "'");
                    if (dtS.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtS.Rows.Count; i++)
                        {
                            string term_id = get_term_id(ddl_copy_to_session.SelectedValue, ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), dtS.Rows[i]["Term_Name"].ToString());
                            if (term_id == "0")
                            {
                                Alertme("Term not added.", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                                return;
                            }

                            string assesment_id = get_assesment_id(ddl_copy_to_session.SelectedValue, ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), term_id, dtS.Rows[i]["Assessment_Name"].ToString());
                            string[] stringSeparatorss = new string[] { "~" };
                            string[] arrs = assesment_id.Split(stringSeparatorss, StringSplitOptions.None);
                            assesment_id = arrs[0];
                            string grade_id = arrs[1];
                            if (assesment_id == "0")
                            {
                                Alertme("Assessment not added.", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                                return;
                            }

                            string assessment_subject_id = em.get_Assessment_Subject_Id(ddl_copy_to_session.SelectedValue, dtS.Rows[i]["Branch_Id"].ToString(), dtS.Rows[i]["Class_id"].ToString(), term_id, assesment_id, dtS.Rows[i]["Subject_id"].ToString());
                            string subject_Sub_Level_Id = Examination.auto_serialS("Subject_Sub_Level_Id", ViewState["branchid"].ToString());

                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Subject_Sub_Level (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Assessment_Subject_Id,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Is_Mandatory_to_pass,Class_id,Subject_Sub_Level_Id,Subject_Activity_Name,Is_Distinction,Distinction_Marks,Start_Date_Marks,End_Date_Marks,Start_IDate_Marks,End_IDate_Marks,Is_save_marks) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Assessment_Subject_Id,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Is_Mandatory_to_pass,@Class_id,@Subject_Sub_Level_Id,@Subject_Activity_Name,@Is_Distinction,@Distinction_Marks,@Start_Date_Marks,@End_Date_Marks,@Start_IDate_Marks,@End_IDate_Marks,@Is_save_marks)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", term_id);
                            cmd.Parameters.AddWithValue("@Assessment_Id", assesment_id);
                            cmd.Parameters.AddWithValue("@Subject_id", dtS.Rows[i]["Subject_id"].ToString());
                            cmd.Parameters.AddWithValue("@Assessment_Subject_Id", assessment_subject_id);
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
                            cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Subject_Sub_Level_Id", subject_Sub_Level_Id);
                            cmd.Parameters.AddWithValue("@Subject_Activity_Name", dtS.Rows[i]["Subject_Activity_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Is_Distinction", dtS.Rows[i]["Is_Distinction"].ToString());
                            cmd.Parameters.AddWithValue("@Distinction_Marks", dtS.Rows[i]["Distinction_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Start_Date_Marks", dtS.Rows[i]["Start_Date_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@End_Date_Marks", dtS.Rows[i]["End_Date_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Start_IDate_Marks", dtS.Rows[i]["Start_IDate_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@End_IDate_Marks", dtS.Rows[i]["End_IDate_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Is_save_marks", dtS.Rows[i]["Is_save_marks"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                ViewState["IsSaved"] = "1";
                            }
                        }
                    }
                    else
                    {
                        Alertme("Subject activity not found to selected term.", "warning");
                        ddl_copy_from_term.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
                    }
                }
                else
                {
                    Alertme("Subject activity already copied for selected session.", "warning");
                    ddl_copy_to_term_for_term.Focus();
                }
            }
        }


        private string get_assesment_id(string Session_Id, string branchid, string Class_id, string term_id, string Assessment_Name)
        {
            string assessmnt_id = "0~0";
            try
            {
                string query = "select Assessment_Id,Grade_System_Id from Exam_Assessment_Details where Session_Id='" + Session_Id + "' and Branch_Id='" + branchid + "' and Class_id='" + Class_id + "' and Exam_Term_Id='" + term_id + "' and Assessment_Name='" + Assessment_Name + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count > 0)
                {
                    assessmnt_id = dt.Rows[0]["Assessment_Id"].ToString() + "~" + dt.Rows[0]["Grade_System_Id"].ToString();
                }
                return assessmnt_id;
            }
            catch (Exception ex)
            {
                return assessmnt_id;
            }
        }


        private string get_term_id(string Session_Id, string branchid, string Class_id, string term_name)
        {
            string termId = "0";
            try
            {
                string query = "select Exam_Term_Id from Exam_Term_Details where Session_Id='" + Session_Id + "' and Branch_Id='" + branchid + "' and Class_id='" + Class_id + "' and Term_Name='" + term_name + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    termId = "0";
                }
                else
                {
                    termId = dt.Rows[0]["Exam_Term_Id"].ToString();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSetting();", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddl_change_date_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDateSetting();", true);
                mycode.bind_all_ddl_with_id_cap_All(ddl_exam_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                get_all_exam_of_session();
            }
            catch (Exception ex)
            {
            }
        }

        private void get_all_exam_of_session()
        {
            mycode.bind_all_ddl_with_id(ddl_exam_name, "select distinct Assessment_Name, Assessment_Name as value from Exam_Assessment_Details where Session_Id='" + ddl_change_date_session.SelectedValue + "' and Istatus=1 order by Assessment_Name asc");
        }

        protected void btn_change_date_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_change_date_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_change_date_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDateSetting();", true);
                }
                else if (ddl_exam_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select exam.", "warning");
                    ddl_exam_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDateSetting();", true);
                }
                else if (txt_end_date.Text == "Select")
                {
                    Alertme("Please choose exam mark entry date.", "warning");
                    txt_end_date.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDateSetting();", true);
                }
                else
                {
                    if (ddl_exam_class.SelectedItem.Text == "ALL")
                    {
                        SqlCommand cmd;
                        string query = "update Exam_Subject_Sub_Level set End_Date_Marks=@End_Date_Marks,End_IDate_Marks=@End_IDate_Marks,Is_save_marks=@Is_save_marks where Session_Id=@Session_Id and Assessment_Id in (select Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddl_change_date_session.SelectedValue + "' and Assessment_Name='" + ddl_exam_name.SelectedValue + "')";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@End_Date_Marks", txt_end_date.Text);
                        cmd.Parameters.AddWithValue("@End_IDate_Marks", My.DateConvertToIdate(txt_end_date.Text));
                        cmd.Parameters.AddWithValue("@Is_save_marks", "0");
                        cmd.Parameters.AddWithValue("@Session_Id", ddl_change_date_session.SelectedValue); 
                        if (My.InsertUpdateData(cmd))
                        {
                            string desc = "Exam marks entry date changed by : " + ViewState["Userid"].ToString() + ", session : " + ddl_change_date_session.SelectedItem.Text + ", Exam name : " + ddl_exam_name.SelectedItem.Text + " marks entry date : " + txt_end_date.Text;
                            log_hostory.edit_log(ddlsession.SelectedValue, "0", "0", "Exam date change", desc, "Subject_Activity.aspx", ViewState["Userid"].ToString());
                            Alertme("Marks entry date has been changed successfully.", "success");
                            try
                            {
                                Bind_final_data(ViewState["query"].ToString());
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "update Exam_Subject_Sub_Level set End_Date_Marks=@End_Date_Marks,End_IDate_Marks=@End_IDate_Marks,Is_save_marks=@Is_save_marks where Session_Id=@Session_Id  and Class_id=@Class_id and Assessment_Id in (select Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddl_change_date_session.SelectedValue + "' and Assessment_Name='" + ddl_exam_name.SelectedValue + "')";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@End_Date_Marks", txt_end_date.Text);
                        cmd.Parameters.AddWithValue("@End_IDate_Marks", My.DateConvertToIdate(txt_end_date.Text));
                        cmd.Parameters.AddWithValue("@Is_save_marks", "0");
                        cmd.Parameters.AddWithValue("@Session_Id", ddl_change_date_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_exam_class.SelectedValue); 
                        if (My.InsertUpdateData(cmd))
                        {
                            string desc = "Exam marks entry date changed by : " + ViewState["Userid"].ToString() + ", session : " + ddl_change_date_session.SelectedItem.Text + ", Exam name : " + ddl_exam_name.SelectedItem.Text + " marks entry date : " + txt_end_date.Text;
                            log_hostory.edit_log(ddlsession.SelectedValue, "0", "0", "Exam date change", desc, "Subject_Activity.aspx", ViewState["Userid"].ToString());
                            Alertme("Marks entry date has been changed successfully.", "success");
                            try
                            {
                                Bind_final_data(ViewState["query"].ToString());
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_exam_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_exam_class.SelectedItem.Text == "ALL")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDateSetting();", true);
                    get_all_exam_of_session();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDateSetting();", true);
                    get_all_exam_of_session_class();
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void get_all_exam_of_session_class()
        {
            mycode.bind_all_ddl_with_id(ddl_exam_name, "select distinct Assessment_Name, Assessment_Name as value from Exam_Assessment_Details where Session_Id='" + ddl_change_date_session.SelectedValue + "' and Istatus=1 and Class_id='" + ddl_exam_class.SelectedValue + "' order by Assessment_Name asc");
        }
    }
}