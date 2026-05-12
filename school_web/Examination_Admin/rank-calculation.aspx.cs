using school_web.AppCode;
using school_web.AppCode.Exam;
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
    public partial class rank_calculation1 : System.Web.UI.Page
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
                        ViewState["rankClculteType"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        find_firm_details();
                        bind_session();
                        bind_class();
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_term.Items.Insert(0, new ListItem("Select", "0"));

                        find_rank_calculation_type();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void find_rank_calculation_type()
        {
            try
            {
                DataTable dt = My.dataTable("select Is_rank_calculation_classwise from Exam_report_card_setting where Session_id='" + ddlsession.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "True")
                    {
                        ViewState["rankClculteType"] = "1";
                        secctionDV.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_firm_details()
        {
            DataTable dt = My.dataTable("select firm_id from Firm_Details");
            ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
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


        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section");
                    bind_terms();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_terms()
        {
            mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Sequence_No asc");
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ViewState["rankClculteType"].ToString() == "1")
                { }
                else
                {
                    if (ddl_section.SelectedItem.Text == "Select")
                    {
                        Alertme("please select section.", "warning");
                        ddl_section.Focus();
                        return;
                    }
                }

                if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("please select term.", "warning");
                    ddl_term.Focus();
                    return;
                }

                get_exam_setting();
                ///DELETE
                string qrys = "delete from Exam_total_no_for_rank_temp where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id='" + ddlclass.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Section='" + ddl_section.Text + "'";
                mycode.executequery(qrys);
                ///DELETE

                calculate_rank();

                if (hd_rank_for_pass_std.Value == "1")
                {
                    check_pass_or_fail();
                }
                save_rank();

                fetch_rank();

            }
            catch (Exception ex)
            {
            }
        }


        private void get_exam_setting()
        {
            hd_rank_for_pass_std.Value = "0";
            DataTable dt = My.dataTable("select * from Exam_report_card_setting where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Is_rank_pass_student"].ToString() == "True")
                {
                    hd_rank_for_pass_std.Value = "1";
                }
            }
        }

        private void check_pass_or_fail()
        {
            string qry = "select  DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and em.Section='" + ddl_section.SelectedItem.Text + "' and em.Branch_id=" + ViewState["Branchid"].ToString() + "  and  em.Term=" + ddl_term.SelectedValue + " and ar.Status='1'  order by ar.rollnumber asc";
            DataTable dt = mycode.FillData(qry);
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    My.exeSql("delete from Exam_subject_no_for_rank_temp where Session_id='" + dr["Session_id"].ToString() + "' and Branch_id='" + dr["Branch_id"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Term_id='" + ddl_term.SelectedValue + "' and Section='" + dr["Section"].ToString() + "'  and Admission_no='" + dr["Admission_no"].ToString() + "'");
                    string query = "select DISTINCT Session_id,Branch_id,Class_id,Term_id,Subject_id from Exam_total_no_for_rank_temp where Session_id='" + dr["Session_id"].ToString() + "' and Branch_id='" + dr["Branch_id"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Term_id='" + dr["Term"].ToString() + "' and Admission_no='" + dr["Admission_no"].ToString() + "' and Section='" + ddl_section.Text + "'";
                    DataTable dtSubj = My.dataTable(query);
                    foreach (DataRow drS in dtSubj.Rows)
                    {
                        string obt_subject_mark = ""; string Full_marks = "";
                        string isAbsentT1 = checkisAbsent(dr["Session_id"].ToString(), dr["Branch_id"].ToString(), dr["Class_id"].ToString(), drS["Subject_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString());
                        string isPass = "Pass";
                        if (isAbsentT1 == "0")
                        {
                            isPass = "Fail";
                            string Total_marks = rank_calculate.get_subject_total_full_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), drS["Subject_id"].ToString(), dr["Branch_id"].ToString(), "Scholastic");
                            string[] stringSeparatorss = new string[] { "~" };
                            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
                            obt_subject_mark = arrs[0];
                            Full_marks = arrs[1];
                        }
                        else
                        {
                            string Total_marks = rank_calculate.get_subject_total_full_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Admission_no"].ToString(), dr["Term"].ToString(), drS["Subject_id"].ToString(), dr["Branch_id"].ToString(), "Scholastic");
                            string[] stringSeparatorss = new string[] { "~" };
                            string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
                            obt_subject_mark = arrs[0];
                            Full_marks = arrs[1];

                            if (hd_rank_for_pass_std.Value == "1")
                            {
                                if (My.toint(Full_marks) == 100)
                                {
                                    if (My.toint(obt_subject_mark) < 40)
                                    {
                                        isPass = "Fail";
                                    }
                                }
                                else
                                {
                                    if (My.toint(obt_subject_mark) < 20)
                                    {
                                        isPass = "Fail";
                                    }
                                }
                            }
                        }


                        SqlCommand cmd;
                        string querySave = "INSERT INTO Exam_subject_no_for_rank_temp (Session_id,Branch_id,Class_id,Term_id,Subject_id,Admission_no,Section,Marks,Full_marks,Is_pass_or_fail,Created_by,Created_date,Created_idate) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Subject_id,@Admission_no,@Section,@Marks,@Full_marks,@Is_pass_or_fail,@Created_by,@Created_date,@Created_idate)";
                        cmd = new SqlCommand(querySave);
                        cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", dr["Branch_id"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                        cmd.Parameters.AddWithValue("@Term_id", drS["Term_id"].ToString());
                        cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                        cmd.Parameters.AddWithValue("@Subject_id", drS["Subject_id"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", dr["Admission_no"].ToString());
                        cmd.Parameters.AddWithValue("@Marks", obt_subject_mark);
                        cmd.Parameters.AddWithValue("@Full_marks", Full_marks);
                        cmd.Parameters.AddWithValue("@Is_pass_or_fail", isPass);
                        cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }

        private string checkisAbsent(string Session_id, string Branch_id, string Class_id, string Subject_id, string Admission_no, string Term_id)
        {
            string retuRn = "0";
            try
            {
                DataTable dt = My.dataTable("select sum(convert(float, Marks)) as ttobt from Exam_check_student_absent where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Subject_id='" + Subject_id + "' and Admission_no='" + Admission_no + "'");
                retuRn = "1";
            }
            catch (Exception ex)
            {
            }
            return retuRn;
        }

        private void fetch_rank()
        {
            string qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t1.*,(select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=t1.Term_id) as Term_name from Exam_rank_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Term_id=" + ddl_term.SelectedValue + " and t2.Status='1' order by Rank asc";
            if (ViewState["rankClculteType"].ToString() == "1")
            {
                qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t1.*,(select top 1 Term_Name from Exam_Term_Details where Exam_Term_Id=t1.Term_id) as Term_name from Exam_rank_master t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Term_id=" + ddl_term.SelectedValue + " and t2.Status='1' order by Rank asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                grdswpr.Visible = false;
            }
            else
            {
                //try
                //{
                //    My.exeSql("update Exam_report_card_setting set Is_show_rank where Session_id='" + ddlsession.SelectedValue + "'");
                //}
                //catch (Exception ex)
                //{
                //}

                Alertme("Rank has been calculated successfully.", "success");
                rd_view.DataSource = dt;
                rd_view.DataBind();
                grdswpr.Visible = true;
            }
        }

        private void save_rank()
        {
            string qrys = "select * from (select t1.Session_id,t1.Branch_id,t1.Class_id,t1.Term_id,t1.Admission_no,t2.Section,isnull(sum(convert(float, Marks)),0) as Total_obtained_mark,isnull(sum(convert(float, Full_marks)),0) as Total_full_mark,(isnull(sum(convert(float, Marks)),0)*(100/isnull(sum(convert(float, Full_marks)),0))) as Persentage from Exam_total_no_for_rank_temp t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t2.Section='" + ddl_section.SelectedItem.Text + "' and t1.Term_id=" + ddl_term.SelectedValue + "  and t2.Status='1'  and Admission_no not in (select Admission_no from Exam_subject_no_for_rank_temp where Session_id=t1.Session_id and Branch_id=t1.Branch_id and Class_id=t1.Class_id and Admission_no=t1.Admission_no and Term_id=t1.Term_id and Is_pass_or_fail='Fail') group by t1.Session_id,t1.Branch_id,t1.Class_id,t1.Term_id,t1.Admission_no,t2.Section) t order by Persentage desc";
            if (ViewState["rankClculteType"].ToString() == "1")
            {
                qrys = "select * from (select t1.Session_id,t1.Branch_id,t1.Class_id,t1.Term_id,t1.Admission_no,t2.Section,isnull(sum(convert(float, Marks)),0) as Total_obtained_mark,isnull(sum(convert(float, Full_marks)),0) as Total_full_mark,(isnull(sum(convert(float, Marks)),0)*(100/isnull(sum(convert(float, Full_marks)),0))) as Persentage from Exam_total_no_for_rank_temp t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Term_id=" + ddl_term.SelectedValue + " and t2.Status='1' and Admission_no not in (select Admission_no from Exam_subject_no_for_rank_temp where Session_id=t1.Session_id and Branch_id=t1.Branch_id and Class_id=t1.Class_id and Admission_no=t1.Admission_no and Term_id=t1.Term_id and Is_pass_or_fail='Fail') group by t1.Session_id,t1.Branch_id,t1.Class_id,t1.Term_id,t1.Admission_no,t2.Section) t order by Persentage desc";
                My.exeSql("delete from Exam_rank_master where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " and Term_id='" + ddl_term.SelectedValue + "'");
            }
            else
            {
                My.exeSql("delete from Exam_rank_master where Session_id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.SelectedItem.Text + "' and Term_id='" + ddl_term.SelectedValue + "'");
            }
            DataTable dt = mycode.FillData(qrys);
            int i = 0;
            double prev_mark = 0;
            foreach (DataRow dr in dt.Rows)
            {
                double percentage = My.toDouble(dr["Persentage"].ToString());
                if (prev_mark != percentage)
                {
                    i++;
                    prev_mark = percentage;
                }

                SqlCommand cmd;
                string query = "INSERT INTO Exam_rank_master (Session_id,Branch_id,Class_id,Section,Admission_no,Term_id,Total_obtained_mark,Total_full_mark,Mark_percentage,Rank,Created_by,Created_date,Created_idate) values (@Session_id,@Branch_id,@Class_id,@Section,@Admission_no,@Term_id,@Total_obtained_mark,@Total_full_mark,@Mark_percentage,@Rank,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                cmd.Parameters.AddWithValue("@Branch_id", dr["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                cmd.Parameters.AddWithValue("@Admission_no", dr["Admission_no"].ToString());
                cmd.Parameters.AddWithValue("@Term_id", dr["Term_id"].ToString());
                cmd.Parameters.AddWithValue("@Total_obtained_mark", dr["Total_obtained_mark"].ToString());
                cmd.Parameters.AddWithValue("@Total_full_mark", dr["Total_full_mark"].ToString());
                cmd.Parameters.AddWithValue("@Mark_percentage", My.toDouble(dr["Persentage"].ToString()).ToString("0.000"));
                cmd.Parameters.AddWithValue("@Rank", i);
                cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        private void calculate_rank()
        {
            string qryS = "select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' order by Section";
            if (ViewState["rankClculteType"].ToString() == "1")
            {
                qryS = "select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section";
            }
            DataTable dts = My.dataTable(qryS);
            if (dts.Rows.Count > 0)
            {
                foreach (DataRow drsc in dts.Rows)
                {
                    string qry = "";
                    qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and em.Section='" + drsc["Section"].ToString() + "' and em.Branch_id=" + ViewState["Branchid"].ToString() + "  and  em.Term=" + ddl_term.SelectedValue + " and ar.Status='1' order by ar.rollnumber asc";
                    DataTable dt = mycode.FillData(qry);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            My.exeSql("delete from Exam_total_no_for_rank_temp where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["Admission_no"].ToString() + "'; delete from EXAM_SUBJECT_NO_FOR_RANK_TEMP where Session_id='" + dr["Session_id"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Admission_no='" + dr["Admission_no"].ToString() + "' and Term_id='" + ddl_term.SelectedValue + "'; delete from Exam_check_student_absent where Session_id='" + dr["Session_id"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Admission_no='" + dr["Admission_no"].ToString() + "' and Term_id='" + ddl_term.SelectedValue + "'");
                            calculate_rank(dr["Admission_no"].ToString(), dr["Session_id"].ToString(), dr["Class_id"].ToString(), ViewState["Branchid"].ToString(), ddl_term.SelectedValue, drsc["Section"].ToString());
                        }
                    }
                }
            }

        }

        private void calculate_rank(string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id, string section)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Status='1') as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Status='1') as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Status='1') as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Status='1') as Section from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Admission_no='" + Admission_no + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and t2.Is_mandatory=1 and (t2.Is_optional = 0 OR t2.Is_optional IS NULL) and t1.Subject in (select Sub_id from Subject_Mapping_New where Class_id='" + Class_id + "' and Admission_no='" + Admission_no + "' and Session_id='" + Session_id + "')  order by t2.Subject_position asc";
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
                    findmyMarks(dr["Subject"].ToString(), Admission_no, Session_id, Class_id, Branch_id, Term_id);
                }
            }
        }

        private void findmyMarks(string Subject_id, string Admission_no, string Session_id, string Class_id, string Branch_id, string Term_id)
        {
            string query = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No from Exam_Assessment_Details t1 where t1.Session_Id=" + Session_id + " and t1.Branch_id='" + Branch_id + "' and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Scholastic_Co_scholastic='Scholastic' order by t1.Sequence_No asc";
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
                    string marks = rank_calculate.get_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", ViewState["firm_id"].ToString());
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];
                    string marks_type = arrs[2];
                    string total_no = arrs[3];

                    if (hd_rank_for_pass_std.Value == "1")
                    {
                        //=====================
                        if (My.toDouble(full_marks) > 0)
                        {
                            SqlCommand cmd;
                            string querys = "INSERT INTO Exam_check_student_absent (Session_id,Branch_id,Class_id,Term_id,Assesment_id,Subject_id,Admission_no,Marks,Full_marks,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assesment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Section)";
                            cmd = new SqlCommand(querys);
                            cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                            cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Term_id", dr["Term"].ToString());
                            cmd.Parameters.AddWithValue("@Assesment_id", dr["Assessment"].ToString());
                            cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                            cmd.Parameters.AddWithValue("@Admission_no", Admission_no);
                            cmd.Parameters.AddWithValue("@Marks", obt_marks);
                            cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                            cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        //==============================
                    }

                    try
                    {
                        bool tot = mycode.cheknumer_Double(obt_marks);
                        if (tot == false)
                        {
                            obt_marks = "0";
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        bool tots = mycode.cheknumer_Double(full_marks);
                        if (tots == false)
                        {
                            full_marks = "0";
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        bool totss = mycode.cheknumer_Double(total_no);
                        if (totss == false)
                        {
                            total_no = "0";
                        }
                    }
                    catch
                    {
                    }

                    save_marks(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Admission_no, dr["Term"].ToString(), dr["Assessment"].ToString(), Subject_id, dr["Branch_id"].ToString(), obt_marks, full_marks, marks_type, total_no);
                }
            }
        }


        private void save_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no)
        {
            string tableName = "Exam_total_no_for_rank_temp";
            if (mycode.IsUserExist("select Id from " + tableName + " where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "' and Section='" + ddl_section.Text + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO " + tableName + " (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Section) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Section)";
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
                cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update " + tableName + " set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
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
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }



        #region FinalRankCalculatE
        protected void btn_calculate_final_rank_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (ViewState["rankClculteType"].ToString() == "1")
                { }
                else
                {
                    if (ddl_section.SelectedItem.Text == "Select")
                    {
                        Alertme("please select section.", "warning");
                        ddl_section.Focus();
                        return;
                    }
                }

                ViewState["ISUpdated"] = "0";
                final_rank_student();
                if (ViewState["ISUpdated"].ToString() == "1")
                {
                    Alertme("Final Year rank has been calculated successfully.", "success");
                    string qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t1.Percentage as Mark_percentage,t1.* from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Class_id=" + ddlclass.SelectedValue + " and t1.Section='" + ddl_section.SelectedItem.Text + "' and t2.Status='1'  order by Rank asc";
                    if (ViewState["rankClculteType"].ToString() == "1")
                    {
                        qry = "select t2.class,t2.rollnumber,t2.session,t2.studentname,t1.Percentage as Mark_percentage,t1.* from Exam_rank_master_final_year t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id=" + ddlsession.SelectedValue + " and t1.Branch_id='" + ViewState["Branchid"].ToString() + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t2.Status='1'  order by Rank asc";
                    }
                    DataTable dt = mycode.FillData(qry);
                    if (dt.Rows.Count == 0)
                    {
                        Alertme("Sorry there are no data list exist", "warning");
                        rd_view.DataSource = null;
                        rd_view.DataBind();
                        grdswpr.Visible = false;
                    }
                    else
                    {
                        rd_view.DataSource = dt;
                        rd_view.DataBind();
                        grdswpr.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void final_rank_student()
        {
            string qry = "";
            if (ViewState["rankClculteType"].ToString() == "1")
            {
                mycode.executequery("delete from Exam_rank_master_final_year where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'");
                qry = "select * from(select Session_id, Branch_id, Class_id, Section, Admission_no,Obt_mark,Full_mark,(convert(float, Obt_mark)/ convert(float, Full_mark))*100 as Persentage from(select Session_id, Branch_id, Class_id, Section, Admission_no, isnull(sum(convert(float, Total_obtained_mark)), 0) as Obt_mark, isnull(sum(convert(float, Total_full_mark)), 0) as Full_mark from Exam_rank_master where Session_id = '" + ddlsession.SelectedValue + "' and Branch_id = '" + ViewState["Branchid"].ToString() + "' and Class_id = '" + ddlclass.SelectedValue + "' and Admission_no not in (select Admission_no from Exam_subject_no_for_rank_temp where Session_id=Exam_rank_master.Session_id and Branch_id=Exam_rank_master.Branch_id and Class_id=Exam_rank_master.Class_id and Admission_no=Exam_rank_master.Admission_no and Is_pass_or_fail='Fail') GROUP by Session_id, Branch_id, Class_id, Section, Admission_no) t) y order by Persentage desc";
            }
            else
            {
                mycode.executequery("delete from Exam_rank_master_final_year where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'");
                qry = "select * from(select Session_id, Branch_id, Class_id, Section, Admission_no,Obt_mark,Full_mark,(convert(float, Obt_mark)/ convert(float, Full_mark))*100 as Persentage from(select Session_id, Branch_id, Class_id, Section, Admission_no, isnull(sum(convert(float, Total_obtained_mark)), 0) as Obt_mark, isnull(sum(convert(float, Total_full_mark)), 0) as Full_mark from Exam_rank_master where Session_id = '" + ddlsession.SelectedValue + "' and Branch_id = '" + ViewState["Branchid"].ToString() + "' and Class_id = '" + ddlclass.SelectedValue + "' and Section = '" + ddl_section.Text + "'  and Admission_no not in (select Admission_no from Exam_subject_no_for_rank_temp where Session_id=Exam_rank_master.Session_id and Branch_id=Exam_rank_master.Branch_id and Class_id=Exam_rank_master.Class_id and Admission_no=Exam_rank_master.Admission_no and Is_pass_or_fail='Fail') GROUP by Session_id, Branch_id, Class_id, Section, Admission_no) t) y order by Persentage desc";
            }
            DataTable dt = mycode.FillData(qry);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                Alertme("Please calculate term rank first.", "warning");
            }
            else
            {
                double prev_mark = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    double percentage = My.toDouble(dr["Persentage"].ToString());
                    if (prev_mark != percentage)
                    {
                        i++;
                        prev_mark = percentage;
                    }

                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_rank_master_final_year (Session_id,Branch_id,Class_id,Section,Admission_no,Percentage,Rank,Created_by,Created_date,Created_idate,Total_obtained_mark,Total_full_mark) values (@Session_id,@Branch_id,@Class_id,@Section,@Admission_no,@Percentage,@Rank,@Created_by,@Created_date,@Created_idate,@Total_obtained_mark,@Total_full_mark)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", dr["Session_id"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", dr["Branch_id"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Section", dr["Section"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", dr["Admission_no"].ToString());
                    cmd.Parameters.AddWithValue("@Percentage", My.toDouble(dr["Persentage"].ToString()).ToString("0.0000"));
                    cmd.Parameters.AddWithValue("@Rank", i);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Total_obtained_mark", dr["Obt_mark"].ToString());
                    cmd.Parameters.AddWithValue("@Total_full_mark", dr["Full_mark"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        ViewState["ISUpdated"] = "1";
                    }
                }
            }
        }
        #endregion

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_rank_calculation_type();
            }
            catch (Exception ex)
            {
            }
        }
    }
}