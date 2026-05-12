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
    public partial class calculate_highest_mark : System.Web.UI.Page
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
                else
                {
                    find_terms();
                    string query = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,ar.rollnumber from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Section='" + ddl_section.Text + "' and em.Branch_id=" + ViewState["Branchid"].ToString() + "  and  em.Term=" + hd_term1.Value + " order by ar.rollnumber asc";
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
                            My.exeSql("delete from Exam_highest_mark where Session_id='" + dr["Session_id"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'  and Admission_no='" + dr["Admission_no"].ToString() + "'");
                            prep_final_term(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Branch_id"].ToString(), hd_term1.Value);
                            prep_final_term(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Branch_id"].ToString(), hd_term2.Value);
                            prep_final_term(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Branch_id"].ToString(), hd_term3.Value);
                        }
                    }

                    //==========================
                    Alertme("Highest mark calculation has been done of Class : " + ddlclass.SelectedItem.Text + " & Section : " + ddl_section.SelectedItem.Text, "success");
                }
            }
            catch (Exception ex)
            {
            }
        }



        #region TempSaveMarks
        //====================================== 
        private void prep_final_term(string Session_id, string Class_id, string Admission_no, string Branch_id, string Term_id)
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
                mycode.executequery("delete from Exam_temp_mark_for_word where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Admission_no='" + Admission_no + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    update_assesment_marks(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_id, dr["Section"].ToString());
                }
            }
        }

        private void update_assesment_marks(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id, string Section)
        {
            string querys = "select Session_Id,Branch_Id,Exam_Term_Id,Assessment_Name,Grade_System_Id,Class_id,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks,Term_assesment_unique_id from Exam_Assessment_Details where Session_Id=" + Session_id + " and  Class_id='" + Class_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            DataTable dts = My.dataTable(querys);
            foreach (DataRow drs in dts.Rows)
            {
                string systm_grade_id = stMarry_final.get_systm_grd_id(Subject_id, Session_id, Class_id, Branch_id, Term_id, drs["Assessment_Id"].ToString());
                string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Subject_Activity_Name as Assessment_Name,t1.Sequence_No,t1.Assessment_Subject_Id,t1.Subject_Sub_Level_Id from Exam_Subject_Sub_Level t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Exam_Term_Id=" + Term_id + "  and t1.Grade_System_Id=" + systm_grade_id + " and t1.Assessment_Id=" + drs["Assessment_Id"].ToString() + " and t1.Subject_id=" + Subject_id + "  order by t1.Sequence_No asc";
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

                        string marks = exam_ips_I.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", dr["Subject_Sub_Level_Id"].ToString());

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        string obt_marks = arrs[0];
                        string full_marks = arrs[1];
                        string marks_type = arrs[2];
                        string total_no = arrs[3];




                        save_assesment_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no, drs["Term_assesment_unique_id"].ToString(), Section);


                        try
                        {
                            bool isNum = valid_amount(obt_marks);
                            if (isNum == false)
                            {
                                save_if_not_no(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, drs["Term_assesment_unique_id"].ToString(), Section);
                            }
                        }
                        catch
                        {
                        }
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


        private void save_if_not_no(string session_id, string class_id, string Admission_no, string term_id, string assessment_id, string Subject_id, string branch_id, string obt_marks, string term_assesment_unique_id, string Section)
        {
            if (mycode.IsUserExist("select Id from Exam_temp_mark_for_word where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assessment_id + "' and Subject_id='" + Subject_id + "' and Admission_no='" + Admission_no + "' and Term_assesment_unique_id='" + term_assesment_unique_id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_temp_mark_for_word (Session_id,Branch_id,Class_id,Term_id,Admission_no,Subject_id,Assessment_id,Term_assesment_unique_id,Mark,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Admission_no,@Subject_id,@Assessment_id,@Term_assesment_unique_id,@Mark,@Section)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Branch_id", branch_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Term_id", term_id);
                cmd.Parameters.AddWithValue("@Assessment_id", assessment_id);
                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Admission_no", Admission_no);
                cmd.Parameters.AddWithValue("@Term_assesment_unique_id", term_assesment_unique_id);
                cmd.Parameters.AddWithValue("@Mark", obt_marks);
                cmd.Parameters.AddWithValue("@Section", Section);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_temp_mark_for_word set Mark=@Mark where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assessment_id + "' and Subject_id='" + Subject_id + "' and Admission_no='" + Admission_no + "' and Term_assesment_unique_id='" + term_assesment_unique_id + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Mark", obt_marks);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }





        private void save_assesment_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no, string Term_assesment_unique_id, string Section)
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


            //string first_term = Exam_setting.get_first_term(session_id, Branch_id, class_id);
            //if (first_term == term_id)
            //{

            SqlCommand cmd;
            string query = "INSERT INTO Exam_highest_mark (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id,@Section)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", session_id);
            cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
            cmd.Parameters.AddWithValue("@Class_id", class_id);
            cmd.Parameters.AddWithValue("@Term_id", term_id);
            cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
            cmd.Parameters.AddWithValue("@Subject_id", subject_id);
            cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            cmd.Parameters.AddWithValue("@Section", Section);
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
            if (My.InsertUpdateData(cmd))
            {
            }

            //}
            //else
            //{
            //    if (mycode.IsUserExist("select Id from Exam_highest_mark where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
            //    {
            //        SqlCommand cmd;
            //        string query = "INSERT INTO Exam_highest_mark (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Exam_termwise_assesment_id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Exam_termwise_assesment_id)";
            //        cmd = new SqlCommand(query);
            //        cmd.Parameters.AddWithValue("@Session_id", session_id);
            //        cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
            //        cmd.Parameters.AddWithValue("@Class_id", class_id);
            //        cmd.Parameters.AddWithValue("@Term_id", term_id);
            //        cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
            //        cmd.Parameters.AddWithValue("@Subject_id", subject_id);
            //        cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            //        if (marks_type == "Marks")
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", marks);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", total_no);
            //        }
            //        cmd.Parameters.AddWithValue("@Full_marks", full_marks);
            //        cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
            //        if (My.InsertUpdateData(cmd))
            //        {
            //        }
            //    }
            //    else
            //    {
            //        SqlCommand cmd;
            //        string query = "Update Exam_temp_assesment_total_no_term_II set Marks=@Marks,Full_marks=@Full_marks,Exam_termwise_assesment_id=@Exam_termwise_assesment_id where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
            //        cmd = new SqlCommand(query);
            //        if (marks_type == "Marks")
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", marks);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", total_no);
            //        }
            //        cmd.Parameters.AddWithValue("@Full_marks", full_marks);
            //        cmd.Parameters.AddWithValue("@Exam_termwise_assesment_id", Term_assesment_unique_id);
            //        if (My.InsertUpdateData(cmd))
            //        {
            //        }
            //    }
            //}
        }
        #endregion

        private void find_terms()
        {
            string query = "select Short_Name,Grade_System_Id,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Sequence_No asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 2)
            {
                hd_term1.Value = dt.Rows[0]["Exam_Term_Id"].ToString();
                hd_term2.Value = dt.Rows[1]["Exam_Term_Id"].ToString();
                hd_term3.Value = "0";
            }

            if (dt.Rows.Count == 3)
            {
                hd_term1.Value = dt.Rows[0]["Exam_Term_Id"].ToString();
                hd_term2.Value = dt.Rows[1]["Exam_Term_Id"].ToString();
                hd_term3.Value = dt.Rows[2]["Exam_Term_Id"].ToString();
            }
        }
    }
}