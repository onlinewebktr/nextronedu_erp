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
    public partial class Assessment_Subject : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        string Assessment_Name = "Select top 1 Assessment_Name from Exam_Assessment_Details where Assessment_Id=easm.Assessment_Id and Branch_Id=easm.Branch_Id";
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
                    Bind_All_Assessment_Subject();
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
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if(ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_examterm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' order by Sequence_No asc");
            }
        }
        protected void ddl_examterm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam teram", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_assessment, "Select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id=" + ddl_examterm.SelectedValue + " and Session_Id="+ddlsession.SelectedValue+" order by Sequence_No asc");
            }
        }



        private void Bind_All_Assessment_Subject()
        {
            string query = " Select easm.*,(" + Assessment_Name + ") as Assessment_Name ,(Select top 1 Course_Name from Add_course_table where course_id=easm.Class_id) as Course_Name,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=easm.Grade_System_Id) as Grade_Name,(Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=easm.Exam_Term_Id and Branch_Id=easm.Branch_Id and Session_Id=easm.Session_Id) as Term_Name,sm.Subject_name,sm.Subject_Type_Scholastic_Co_Scholastic from Exam_Assessment_Subject_Mapping_Details easm join Subject_Master sm on easm.Class_id=sm.course_id and easm.Subject_id=sm.Subject_id and easm.Branch_Id=sm.Branch_id where  easm.Branch_Id=" + ViewState["branchid"].ToString() + " and easm.Session_Id=" + ddlsession.SelectedValue + "  order by easm.Sequence_No asc";
            Bind_Gride_data(query);

        }

        private void Bind_Gride_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no assessment subject name added", "warning");
                grid_grade.DataSource = null;
                grid_grade.DataBind();

            }
            else
            {

                grid_grade.DataSource = dt;
                grid_grade.DataBind();
            }
        }

        protected void btn_fina_class_wise_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedValue == "Select")
                {
                    Alertme("Please select class", "warning");

                }
                else if (ddl_examterm.SelectedItem.Text == "Select")
                {
                    Alertme("Please select exam  term", "warning");
                }
                else if (ddl_assessment.SelectedItem.Text == "Select")
                {
                    Alertme("Please select assessment", "warning");
                }
                else
                {

                    string query = " Select easm.*,(" + Assessment_Name + ") as Assessment_Name ,(Select top 1 Course_Name from Add_course_table where course_id=easm.Class_id) as Course_Name,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=easm.Grade_System_Id) as Grade_Name,(Select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=easm.Exam_Term_Id and Branch_Id=easm.Branch_Id and Session_Id=easm.Session_Id) as Term_Name,sm.Subject_name,sm.Subject_Type_Scholastic_Co_Scholastic from Exam_Assessment_Subject_Mapping_Details easm join Subject_Master sm on easm.Class_id=sm.course_id and easm.Subject_id=sm.Subject_id and easm.Branch_Id=sm.Branch_id where  easm.Branch_Id=" + ViewState["branchid"].ToString() + " and easm.Session_Id=" + ddlsession.SelectedValue + " and   easm.Class_id='" + ddl_class.SelectedValue + "' and easm.Exam_Term_Id=" + ddl_examterm.SelectedValue + " and easm.Assessment_Id=" + ddl_assessment.SelectedValue + " order by easm.Sequence_No asc";
                    Bind_Gride_data(query);
                }
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



        protected void lnkEdit_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Assessment_Subject_Id = (Label)row.FindControl("lbl_Assessment_Subject_Id");
                Response.Redirect("Set_Assessment_Subject.aspx?Assessment_Subject_Id=" + lbl_Assessment_Subject_Id.Text, false);
            }

            catch
            {
            }
        }



        //========================
        protected void ddl_copy_to_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose current session", "warning");
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
                            copy_assesment_subj_setting("Term");
                            if (ViewState["IsSaved"].ToString() == "1")
                            {
                                Alertme("Assessment Subject has been copied successfully for new term.", "success");
                                Bind_All_Assessment_Subject();
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
                            copy_assesment_subj_setting("Session");
                            if (ViewState["IsSaved"].ToString() == "1")
                            {
                                Alertme("Assessment Subject has been copied successfully for next session.", "success");
                                Bind_All_Assessment_Subject();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void copy_assesment_subj_setting(string CopyType)
        {
            if (CopyType == "Term")
            {
                DataTable dtS = mycode.FillData("select t1.*,t2.Term_Name,t3.Assessment_Name  from Exam_Assessment_Subject_Mapping_Details t1 join Exam_Term_Details t2 on t1.Session_Id=t2.Session_Id and t1.Branch_Id=t2.Branch_Id and t1.Exam_Term_Id=t2.Exam_Term_Id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Exam_Term_Id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_Id=t3.Assessment_Id where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_id='" + ViewState["branchid"].ToString() + "' and t2.Term_Name='" + ddl_copy_from_term.SelectedItem.Text + "'");
                if (dtS.Rows.Count > 0)
                {
                    for (int i = 0; i < dtS.Rows.Count; i++)
                    {
                        string term_id = get_term_id(dtS.Rows[i]["Session_Id"].ToString(), ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), ddl_copy_to_term_for_term.SelectedItem.Text);
                       
                        if (term_id == "0")
                        {
                            Alertme("Term not added.", "warning");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            return;
                        }

                        if (mycode.IsUserExist("select Id from Exam_Assessment_Subject_Mapping_Details where Session_Id='" + ddl_current_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + term_id + "'  and Class_id='" + dtS.Rows[i]["Class_id"].ToString() + "' and Assessment_Id='" + assesment_id + "' and  Subject_id='" + dtS.Rows[i]["Subject_id"].ToString() + "'"))
                        {
                            string Assessment_Subject_Id = Examination.auto_serialS("Assessment_Subject_Id", ViewState["branchid"].ToString());
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Assessment_Subject_Mapping_Details (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Short_Name,Sequence_No,Created_By,Created_Date_Time,Class_id,Assessment_Subject_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Grade_System_Id,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Is_Mandatory_to_pass,Select_Data) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Short_Name,@Sequence_No,@Created_By,@Created_Date_Time,@Class_id,@Assessment_Subject_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Grade_System_Id,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Is_Mandatory_to_pass,@Select_Data)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_Id", ddl_current_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", term_id);
                            cmd.Parameters.AddWithValue("@Assessment_Id", assesment_id);
                            cmd.Parameters.AddWithValue("@Subject_id", dtS.Rows[i]["Subject_id"].ToString());
                            cmd.Parameters.AddWithValue("@Short_Name", dtS.Rows[i]["Short_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Sequence_No", dtS.Rows[i]["Sequence_No"].ToString());
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Assessment_Subject_Id", Assessment_Subject_Id);
                            cmd.Parameters.AddWithValue("@Maximum_Marks", dtS.Rows[i]["Maximum_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Cut_Off_Percentage", dtS.Rows[i]["Cut_Off_Percentage"].ToString());
                            cmd.Parameters.AddWithValue("@Calculation_Type", dtS.Rows[i]["Calculation_Type"].ToString());
                            cmd.Parameters.AddWithValue("@Grade_System_Id", grade_id);
                            cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", dtS.Rows[i]["Is_Advanced_Advanced_Setting"].ToString());
                            cmd.Parameters.AddWithValue("@Consider_best", dtS.Rows[i]["Consider_best"].ToString());
                            cmd.Parameters.AddWithValue("@Pass_criteria", dtS.Rows[i]["Pass_criteria"].ToString());
                            cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", dtS.Rows[i]["Is_Mandatory_to_pass"].ToString());
                            cmd.Parameters.AddWithValue("@Select_Data", dtS.Rows[i]["Select_Data"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                ViewState["IsSaved"] = "1";
                            }
                        }
                        else
                        {
                            Alertme("Assessment Subject already exist to selected term.", "warning");
                            ddl_copy_to_term_for_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
                else
                {
                    Alertme("Assessment Subject not found to selected term.", "warning");
                    ddl_copy_from_term.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            else
            {
                DataTable dtS = mycode.FillData("select t1.*,t2.Term_Name,t3.Assessment_Name  from Exam_Assessment_Subject_Mapping_Details t1 join Exam_Term_Details t2 on t1.Session_Id=t2.Session_Id and t1.Branch_Id=t2.Branch_Id and t1.Exam_Term_Id=t2.Exam_Term_Id join Exam_Assessment_Details t3 on t1.Session_Id=t3.Session_Id and t1.Branch_Id=t3.Branch_Id and t1.Exam_Term_Id=t3.Exam_Term_Id and t1.Class_id=t3.Class_id and t1.Assessment_Id=t3.Assessment_Id where t1.Session_Id='" + ddl_current_session.SelectedValue + "' and t1.Branch_id='" + ViewState["branchid"].ToString() + "'");
                if (dtS.Rows.Count > 0)
                {
                    for (int i = 0; i < dtS.Rows.Count; i++)
                    {
                        string term_id = get_term_id(ddl_copy_to_session.SelectedValue, ViewState["branchid"].ToString(), dtS.Rows[i]["Class_id"].ToString(), dtS.Rows[i]["Term_Name"].ToString());
                        if (term_id == "0")
                        {
                            Alertme("Term not added.", "warning");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            return;
                        }

                        if (mycode.IsUserExist("select Id from Exam_Assessment_Subject_Mapping_Details where Session_Id='" + ddl_copy_to_session.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + term_id + "'  and Class_id='" + dtS.Rows[i]["Class_id"].ToString() + "' and Assessment_Id='" + assesment_id + "' and  Subject_id='" + dtS.Rows[i]["Subject_id"].ToString() + "'"))
                        {
                            string Assessment_Subject_Id = Examination.auto_serialS("Assessment_Subject_Id", ViewState["branchid"].ToString());
                            SqlCommand cmd;
                            string query = "INSERT INTO Exam_Assessment_Subject_Mapping_Details (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Short_Name,Sequence_No,Created_By,Created_Date_Time,Class_id,Assessment_Subject_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Grade_System_Id,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Is_Mandatory_to_pass,Select_Data) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Short_Name,@Sequence_No,@Created_By,@Created_Date_Time,@Class_id,@Assessment_Subject_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Grade_System_Id,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Is_Mandatory_to_pass,@Select_Data)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", term_id);
                            cmd.Parameters.AddWithValue("@Assessment_Id", assesment_id);
                            cmd.Parameters.AddWithValue("@Subject_id", dtS.Rows[i]["Subject_id"].ToString());
                            cmd.Parameters.AddWithValue("@Short_Name", dtS.Rows[i]["Short_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Sequence_No", dtS.Rows[i]["Sequence_No"].ToString());
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Assessment_Subject_Id", Assessment_Subject_Id);
                            cmd.Parameters.AddWithValue("@Maximum_Marks", dtS.Rows[i]["Maximum_Marks"].ToString());
                            cmd.Parameters.AddWithValue("@Cut_Off_Percentage", dtS.Rows[i]["Cut_Off_Percentage"].ToString());
                            cmd.Parameters.AddWithValue("@Calculation_Type", dtS.Rows[i]["Calculation_Type"].ToString());
                            cmd.Parameters.AddWithValue("@Grade_System_Id", grade_id);
                            cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", dtS.Rows[i]["Is_Advanced_Advanced_Setting"].ToString());
                            cmd.Parameters.AddWithValue("@Consider_best", dtS.Rows[i]["Consider_best"].ToString());
                            cmd.Parameters.AddWithValue("@Pass_criteria", dtS.Rows[i]["Pass_criteria"].ToString());
                            cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", dtS.Rows[i]["Is_Mandatory_to_pass"].ToString());
                            cmd.Parameters.AddWithValue("@Select_Data", dtS.Rows[i]["Select_Data"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                                ViewState["IsSaved"] = "1";
                            }
                        }
                        else
                        {
                            Alertme("Assessment Subject already exist to selected term.", "warning");
                            ddl_copy_to_term_for_term.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
                else
                {
                    Alertme("Assessment Subject not found to selected term.", "warning");
                    ddl_copy_from_term.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                    assessmnt_id = dt.Rows[0]["Assessment_Id"].ToString()+"~"+ dt.Rows[0]["Grade_System_Id"].ToString();
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
                if (dt.Rows.Count > 0)
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