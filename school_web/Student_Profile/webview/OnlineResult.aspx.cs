using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class OnlineResult : System.Web.UI.Page
    {
        string scrpt;
        My m1 = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    hd_studentid.Value = Session["student"].ToString();

                    string test_code = Session["testid"].ToString();
                    string attempt_id = Session["Attemptid"].ToString();

                    //hd_studentid.Value = Request.QueryString["studentid"];
                    //Session["student"] = hd_studentid.Value;
                    //string test_code = Request.QueryString["testid"];
                    //string attempt_id = Request.QueryString["attemptid"];

                    try
                    {
                        //ViewState["modepage"] = Request.QueryString["mode"];
                        ViewState["modepage"] = Session["mode"].ToString();
                    }
                    catch
                    {
                        ViewState["modepage"] = "mobile";
                    }

                    hd_entry_id.Value = "0";//Request.QueryString["entry_id"];
                    hd_package_id.Value = "0";//Request.QueryString["package_id"];

                    if (!String.IsNullOrEmpty(test_code))
                    {
                        hd_testid.Value = test_code;
                        hd_Examcode.Value = test_code;
                        hd_examtype_code.Value = "4";
                        hd_testmode.Value = "101";
                        hd_ipaddress.Value = m1.find_ip();
                        hd_idate.Value = m1.idate();
                        hd_attempt_id.Value = attempt_id;
                        hd_exam_category.Value = "1";
                        ViewState["dflanug"] = "English";//Request.QueryString["dflanug"];
                        find_default_language();
                        update_attempttest();
                        find_data();
                        bind_gridview();
                        save_marks_of_result();
                        bind_test_name();
                        //Send_sms();
                        try
                        {
                            add_data_user_test_total_Dummy_table();
                        }
                        catch (Exception ex)
                        {
                            My.submitException(ex, "Result");
                        }
                        // Rank_calculation.calculate_rank(hd_package_id.Value, test_code);
                        //bind_rank();
                        Session.Remove("teststart");
                        check_question_explanation();
                        Bind_schoolinfo();
                        student_name_bind();
                    }
                    else
                    {
                        Response.Redirect("OnlineMyResult.aspx");
                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Resultpage");
            }
        }

        private void student_name_bind()
        {
            DataTable dt = m1.FillData("select isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name,studentname,admissionserialnumber,class,Section,fathername,session,rollnumber,Academic_Sem_or_Year from admission_registor where admissionserialnumber='" + hd_studentid.Value + "' and Session_id='" + ViewState["Session_id"].ToString() + "' and Class_id=" + ViewState["classid"].ToString() + " ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_studentname.Text = dt.Rows[0]["studentname"].ToString();
                lbl_aadmissionno.Text = dt.Rows[0]["admissionserialnumber"].ToString();

                lbl_class.Text = dt.Rows[0]["class"].ToString() + ",  " + "   Section-" + dt.Rows[0]["Section"].ToString();
                lbl_fathername.Text = dt.Rows[0]["fathername"].ToString();
                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_rollno.Text = dt.Rows[0]["rollnumber"].ToString();

            }
        }

        private void check_question_explanation()
        {
            string query = "Select qi.test_id,Question_no,qi.questionid,Question_name,Question_name_HN,qi.Section,Explanation_en,Explanation_hn from question_info qi  join Question_Explanation qe on qi.questionid=qe.questionid where qi.test_id=" + hd_testid.Value + "     order by  cast (Question_no as int) ASC";//and  
            DataTable dt = new DataTable();
            dt = m1.featch_data(query);
            if (dt.Rows.Count == 0)
            {
                lbl_viewresult.Visible = false;
            }
            else
            {
                lbl_viewresult.Visible = true;
            }
        }
        private void add_data_user_test_total_Dummy_table()
        {
            Dictionary<string, object> dc1 = My.getstudentinfo(hd_studentid.Value, ViewState["Session_id"].ToString());
            string FullName = (String)dc1["Name"];
            Dictionary<string, object> dc2 = m1.gettestinformation(hd_testid.Value);

            string Subject_id = (String)dc2["Sub_id"].ToString();
            string Section = (String)dc2["Section"].ToString();
            string Subjectname = (String)dc2["Subjectname"].ToString();

            SqlCommand cmd1 = new SqlCommand("Select  *  from user_test_total_Dummy_table where Test_code=" + hd_testid.Value + " and  Packageid='" + hd_package_id.Value + "' and Entry_no='" + hd_entry_id.Value + "' and  Studentid='" + hd_studentid.Value + "' and Subject_id='" + Subject_id + "' and Section='" + Section + "'");
            DataTable dt = m1.GetData(cmd1);
            if (dt.Rows.Count == 0)
            {
                string date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_user_test_total_Dummy_table";
                cmd.Parameters.AddWithValue("@cmdstatus", "1");
                cmd.Parameters.AddWithValue("@Studentid", hd_studentid.Value);
                cmd.Parameters.AddWithValue("@Test_code", hd_testid.Value);
                cmd.Parameters.AddWithValue("@Exam_code", hd_Examcode.Value);
                cmd.Parameters.AddWithValue("@Examtype_code", hd_examtype_code.Value);
                cmd.Parameters.AddWithValue("@Exam_category_id", hd_exam_category.Value);
                cmd.Parameters.AddWithValue("@Obtains_Marks", lbl_obtains.Text);
                cmd.Parameters.AddWithValue("@Fullmarks", lbl_total_marks.Text);
                cmd.Parameters.AddWithValue("@No_question", get_noofqution());
                cmd.Parameters.AddWithValue("@Subject", Subjectname);
                cmd.Parameters.AddWithValue("@Attempt_id", hd_attempt_id.Value);
                cmd.Parameters.AddWithValue("@created_date", date);
                cmd.Parameters.AddWithValue("@icreated_date", idate);
                cmd.Parameters.AddWithValue("@Packageid", hd_package_id.Value);
                cmd.Parameters.AddWithValue("@Entry_no", hd_entry_id.Value);
                cmd.Parameters.AddWithValue("@student_name", FullName);

                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Section", Section);
                if (My.InsertUpdateData_sp_offline(cmd))
                {
                }
            }
            else
            {
                string date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_user_test_total_Dummy_table";
                cmd.Parameters.AddWithValue("@cmdstatus", "2");
                cmd.Parameters.AddWithValue("@Studentid", hd_studentid.Value);
                cmd.Parameters.AddWithValue("@Test_code", hd_testid.Value);
                cmd.Parameters.AddWithValue("@Exam_code", hd_Examcode.Value);
                cmd.Parameters.AddWithValue("@Examtype_code", hd_examtype_code.Value);
                cmd.Parameters.AddWithValue("@Exam_category_id", hd_exam_category.Value);
                cmd.Parameters.AddWithValue("@Obtains_Marks", lbl_obtains.Text);
                cmd.Parameters.AddWithValue("@Fullmarks", lbl_total_marks.Text);
                cmd.Parameters.AddWithValue("@No_question", get_noofqution());
                cmd.Parameters.AddWithValue("@Subject", Subjectname);
                cmd.Parameters.AddWithValue("@Attempt_id", hd_attempt_id.Value);
                cmd.Parameters.AddWithValue("@created_date", date);
                cmd.Parameters.AddWithValue("@icreated_date", idate);
                cmd.Parameters.AddWithValue("@Packageid", hd_package_id.Value);
                cmd.Parameters.AddWithValue("@Entry_no", hd_entry_id.Value);
                cmd.Parameters.AddWithValue("@student_name", FullName);

                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Section", Section);
                if (My.InsertUpdateData_sp_offline(cmd))
                {
                }
            }
        }
        private object get_noofqution()
        {
            SqlCommand cmd = new SqlCommand("Select count(test_id)   from question_info where test_id=" + hd_testid.Value + " and Uploding_status='Administrator' ");
            DataTable dt = m1.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                lbl_no_of_question.Text = "0";
                return "0";

            }
            else
            {
                lbl_no_of_question.Text = dt.Rows[0][0].ToString();
                return dt.Rows[0][0].ToString();

            }
        }

        private void Bind_schoolinfo()
        {
            DataTable dt = m1.FillData("select *,(Select top 1 Path from Header_templete) as Header_images from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                try
                {
                    if (dt.Rows[0]["Header_images"].ToString() != "")
                    {
                        textheader.Visible = false;
                        printheader.Visible = true;
                        img_header.ImageUrl = dt.Rows[0]["Header_images"].ToString();
                    }
                    else
                    {
                        textheader.Visible = true;
                        printheader.Visible = false;
                    }
                }
                catch
                {
                    textheader.Visible = true;
                    printheader.Visible = false;
                }


                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;

                }








            }
        }
        private void bind_test_name()
        {
            string query = "select Exam_name,Session_id,Class_id,(select top 1 Subject_name from Subject_Master where Subject_id=OlineTest_Exam_name.subjectname) as subjectname,format(live_date,'dd/MM/yyyy') as live_date_one from OlineTest_Exam_name  where Exam_id='" + hd_testid.Value + "'  ";

            DataTable dt = m1.FillData(query);
            if (dt.Rows.Count == 0)
            {
                ViewState["classid"] = "0";
                lbl_testname.Text = "";
                ViewState["Session_id"] = "0";
            }
            else
            {
                lbl_testname.Text = dt.Rows[0]["Exam_name"].ToString();
                ViewState["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["Subjectname"] = dt.Rows[0]["subjectname"].ToString();
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                if (ViewState["Subjectname"].ToString() == "")
                {
                    lblsubject_name.Text = "All Subject";
                }
                else if (ViewState["Subjectname"].ToString() == "0")
                {
                    lblsubject_name.Text = "All Subject";
                }
                else
                {
                    lblsubject_name.Text = ViewState["Subjectname"].ToString();
                }
                lbl_paymentdate.Text = dt.Rows[0]["live_date_one"].ToString();
            }

        }

        private void find_default_language()
        {

            DataTable dt = m1.FillData("select top 1 Language_Itype from user_test_attempted_details where  Studentid='" + hd_studentid.Value + "' and Test_code='" + hd_testid.Value + "'");
            if (dt.Rows.Count == 0)
            {
                hd_df_language.Value = "0";

            }
            else
            {
                hd_df_language.Value = dt.Rows[0][0].ToString();
            }
        }

        private void update_attempttest()
        {
            try
            {
                Dictionary<string, object> dc2 = m1.gettestinformation(hd_testid.Value);

                string Subject_id = (String)dc2["Sub_id"].ToString();
                string Section = (String)dc2["Section"].ToString();
                string Exam_Id = (String)dc2["Exam_Id"].ToString();
                SqlDataAdapter ad = new SqlDataAdapter("select Test_id from Tack_Test where Student_id='" + hd_studentid.Value + "' and Test_id='" + hd_testid.Value + "' and Packageid='" + hd_package_id.Value + "' and Entry_no='" + hd_entry_id.Value + "' and Subjectid='" + Subject_id + "'   ", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Tack_Test");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string strQuery = @"INSERT INTO Tack_Test (Test_id,Student_id,Date,Idate,Time,Status,IStatus,isActive,Section,Packageid,Entry_no,Language,Subjectid,Exam_Code) values (@Test_id,@Student_id,@Date,@Idate,@Time,@Status,@IStatus,@isActive,@Section,@Packageid,@Entry_no,@Language,@Subjectid,@Exam_Code)";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@Test_id", hd_testid.Value);
                    cmd.Parameters.AddWithValue("@Student_id", hd_studentid.Value);
                    cmd.Parameters.AddWithValue("@Date", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@Idate", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd"));
                    cmd.Parameters.AddWithValue("@Time", DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@Status", "Attempt");
                    cmd.Parameters.AddWithValue("@IStatus", 1);
                    cmd.Parameters.AddWithValue("@isActive", 1);
                    cmd.Parameters.AddWithValue("@Section", Section);
                    cmd.Parameters.AddWithValue("@Packageid", hd_package_id.Value);
                    cmd.Parameters.AddWithValue("@Entry_no", hd_entry_id.Value);
                    cmd.Parameters.AddWithValue("@Language", ViewState["dflanug"].ToString());
                    cmd.Parameters.AddWithValue("@Subjectid", Subject_id);
                    cmd.Parameters.AddWithValue("@Exam_Code", Exam_Id);

                    if (My.InsertUpdateData(cmd))
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Result");
            }
        }


        private void find_data()
        {
            int rowcount = 0;
            double tot_marks = 0;
            DataTable dt = new DataTable();
            dt = m1.featch_data("select count(*),SUM(CAST(marks as float)) from question_info where test_id='" + hd_testid.Value + "'   ");
            if (dt.Rows.Count > 0)
            {
                rowcount = Convert.ToInt32(dt.Rows[0][0].ToString());
                if (dt.Rows[0][1].ToString() != "")
                {
                    tot_marks = Convert.ToDouble(dt.Rows[0][1].ToString());
                }
            }
            lbl_total_marks.Text = tot_marks.ToString();

            int rowcount1 = 0;
            //Ip_address ='" + hd_ipaddress.Value + @
            double tot_pmarks = 0;
            double tot_nmarks = 0;
            //            


            string qry = @"select count(*),(select count(*) from user_test_details where test_code='" + hd_testid.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done' and Attempt_id='" + hd_attempt_id.Value + @"'   and Packageid='" + hd_package_id.Value + @"' and Entry_no='" + hd_entry_id.Value + @"'  and cast(marks as float)>0 ),
(select SUM(CAST(marks as float)) from user_test_details   where  test_code='" + hd_testid.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done' and Attempt_id='" + hd_attempt_id.Value + @"'  and Packageid='" + hd_package_id.Value + @"' and Entry_no='" + hd_entry_id.Value + @"'  and cast(marks as float)>0  ),
(select SUM(CAST(marks as float)) from user_test_details  where  test_code='" + hd_testid.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done'  and Attempt_id='" + hd_attempt_id.Value + @"'  and Packageid='" + hd_package_id.Value + @"' and Entry_no='" + hd_entry_id.Value + @"'  and cast(marks as float)<0 ),
(select count(*) from user_test_details where test_code='" + hd_testid.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done'  and Attempt_id='" + hd_attempt_id.Value + @"' and cast(marks as float)<0 )  as totalneagitive
 from user_test_details where  test_code='" + hd_testid.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done' and Attempt_id='" + hd_attempt_id.Value + "'  and Packageid='" + hd_package_id.Value + @"' and Entry_no='" + hd_entry_id.Value + @"' ";


            dt = new DataTable();
            dt = m1.featch_data(qry);
            if (dt.Rows.Count > 0)
            {
                rowcount1 = Convert.ToInt32(dt.Rows[0][0].ToString());
                if (dt.Rows[0][1].ToString() != "")
                {
                    lbl_correct_answer.Text = dt.Rows[0][1].ToString();

                }
                if (dt.Rows[0][2].ToString() != "")
                {
                    tot_pmarks = Convert.ToDouble(dt.Rows[0][2].ToString());
                }
                if (dt.Rows[0][3].ToString() != "")
                {
                    tot_nmarks = Convert.ToDouble(dt.Rows[0][3].ToString());
                }

            }
            lbl_no_neagtive_marks.Text = dt.Rows[0]["totalneagitive"].ToString();
            lbl_tot_answered.Text = rowcount1.ToString();

            lbl_p_marks.Text = tot_pmarks.ToString();
            lbl_n_marks.Text = tot_nmarks.ToString();

            lbl_obtains.Text = (tot_pmarks + tot_nmarks).ToString();

            lbl_marks1.Text = lbl_obtains.Text;


            lbl_unattempted.Text = (rowcount - rowcount1).ToString();
        }

        private void bind_gridview()
        {
            DataTable dt = new DataTable();
            dt = m1.featch_data("select * from question_info where test_id='" + hd_testid.Value + "'   ");
            if (dt.Rows.Count > 0)
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
            }
        }

        protected void grd_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_quest_code = (Label)e.Row.FindControl("lbl_quest_code");
                Label lbl_question = (Label)e.Row.FindControl("lbl_question");
                Label lbl_question_hn = (Label)e.Row.FindControl("lbl_question_hn");

                Label lbl_your_asnwer = (Label)e.Row.FindControl("lbl_your_asnwer");
                Label lbl_answer = (Label)e.Row.FindControl("lbl_answer");
                Label lbl_answer_hn = (Label)e.Row.FindControl("lbl_answer_hn");
                Label lbl_time = (Label)e.Row.FindControl("lbl_time");
                Label lbl_Opetion_id = (Label)e.Row.FindControl("lbl_Opetion_id");
                DataTable dt = new DataTable();
                dt = m1.featch_data("select user_test_details.*,(Select top 1 opetion_text_HN from  question_answer_Master where test_code=user_test_details.test_code and quest_code=user_test_details.quest_code and opt_code=user_test_details.opt_code) as opetion_text_HN,(Select top 1 option_text from  question_answer_Master where test_code=user_test_details.test_code and quest_code=user_test_details.quest_code and opt_code=user_test_details.opt_code) as option_text_en  from user_test_details where  Studentid='" + hd_studentid.Value + "' and test_code='" + hd_testid.Value + "' and icreated_date='" + hd_idate.Value + "' and quest_code='" + lbl_quest_code.Text + "' and Status='Done' and Attempt_id='" + hd_attempt_id.Value + "' ");//Ip_address='" + hd_ipaddress.Value + "' and
                if (dt.Rows.Count > 0)
                {
                    lbl_your_asnwer.Text = dt.Rows[0]["option_text_en"].ToString();// + "/" + dt.Rows[0]["opetion_text_HN"].ToString();
                    if (lbl_Opetion_id.Text == dt.Rows[0][6].ToString())
                    {
                        lbl_your_asnwer.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_your_asnwer.ForeColor = System.Drawing.Color.Red;
                    }
                    lbl_question_hn.Visible = true;
                    lbl_question.Visible = true;
                    lbl_answer.Visible = true;
                    lbl_time.Text = dt.Rows[0][11].ToString() + "(mm:ss)";
                }
                else
                {
                    lbl_question_hn.Visible = true;
                    lbl_question.Visible = true;
                }
            }

        }

        protected void lbl_viewresult_Click(object sender, EventArgs e)
        {
            string testid = m1.Zip(hd_testid.Value);
            // Response.Redirect("OnlineQuestion_Explanation.aspx?id=" + Uri.EscapeDataString(testid) + "&mode=" + ViewState["modepage"].ToString() + "&stu=" + Uri.EscapeDataString(hd_studentid.Value));
            Response.Redirect("OnlineQuestion_Explanation.aspx");




        }

        private void save_marks_of_result()
        {
            Dictionary<string, object> dc2 = m1.gettestinformation(hd_testid.Value);

            string Subject_id = (String)dc2["Sub_id"].ToString();

            string Exam_Id = (String)dc2["Exam_Id"].ToString();
            string Class_Id = (String)dc2["Class_Id"].ToString();
            string Section = (String)dc2["Section"].ToString();
            string Session_Id = (String)dc2["Session_Id"].ToString();
            string Exam_Activity_Id = (String)dc2["Exam_Activity_Id"].ToString();
            string date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");


            SqlDataAdapter ad = new SqlDataAdapter("Select Test_code from user_test_total_marks_details where Studentid='" + hd_studentid.Value + "' and Test_code='" + hd_testid.Value + "' and  Packageid='" + hd_package_id.Value + "' and Entry_no='" + hd_entry_id.Value + "' and Sub_id='" + Subject_id + "'", My.conn);//and Ip_address='" + hd_ipaddress.Value + "' 
            DataSet ds = new DataSet();
            ad.Fill(ds, "user_test_total_marks_details");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {

                SqlCommand cmd;
                string strQuery = @"insert into user_test_total_marks_details(Studentid,Ip_address,Test_code,Exam_code,Examtype_code,Exam_category_id,Correct_answer,Answered_questions,Unattempted_questions,Positive_Marks,Negative_Marks,Obtains_Marks,Attempt_id,created_date,icreated_date,Packageid,Entry_no,Old_Reg_id,Session_Id,Class_Id,Sub_id,Section,Full_Marks,Exam_Activity_Id,submit_time) values (@Studentid,@Ip_address,@Test_code,@Exam_code,@Examtype_code,@Exam_category_id,@Correct_answer,@Answered_questions,@Unattempted_questions,@Positive_Marks,@Negative_Marks,@Obtains_Marks,@Attempt_id,@created_date,@icreated_date,@Packageid,@Entry_no,@Old_Reg_id,@Session_Id,@Class_Id,@Sub_id,@Section,@Full_Marks,@Exam_Activity_Id,@submit_time)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Studentid", hd_studentid.Value);
                cmd.Parameters.AddWithValue("@Ip_address", hd_ipaddress.Value);
                cmd.Parameters.AddWithValue("@Test_code", hd_testid.Value);
                cmd.Parameters.AddWithValue("@Exam_code", hd_Examcode.Value);
                cmd.Parameters.AddWithValue("@Examtype_code", hd_examtype_code.Value);
                cmd.Parameters.AddWithValue("@Exam_category_id", hd_exam_category.Value);
                cmd.Parameters.AddWithValue("@Correct_answer", lbl_correct_answer.Text);
                cmd.Parameters.AddWithValue("@Answered_questions", lbl_tot_answered.Text);
                cmd.Parameters.AddWithValue("@Unattempted_questions", lbl_unattempted.Text);
                cmd.Parameters.AddWithValue("@Positive_Marks", lbl_p_marks.Text);
                cmd.Parameters.AddWithValue("@Negative_Marks", lbl_n_marks.Text);
                cmd.Parameters.AddWithValue("@Obtains_Marks", lbl_obtains.Text);
                cmd.Parameters.AddWithValue("@Attempt_id", hd_attempt_id.Value);
                cmd.Parameters.AddWithValue("@created_date", date);
                cmd.Parameters.AddWithValue("@icreated_date", idate);
                cmd.Parameters.AddWithValue("@Packageid", hd_package_id.Value);
                cmd.Parameters.AddWithValue("@Entry_no", hd_entry_id.Value);
                cmd.Parameters.AddWithValue("@Old_Reg_id", "0");
                cmd.Parameters.AddWithValue("@Session_Id", Session_Id);
                cmd.Parameters.AddWithValue("@Class_Id", Class_Id);
                cmd.Parameters.AddWithValue("@Sub_id", Subject_id);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Full_Marks", lbl_total_marks.Text);
                cmd.Parameters.AddWithValue("@Exam_Activity_Id", Exam_Activity_Id);
                cmd.Parameters.AddWithValue("@submit_time", m1.time());
                if (My.InsertUpdateData(cmd))
                {

                }

            }
            else
            {
                SqlCommand cmd;
                string strQuery = @"update    user_test_total_marks_details set Exam_Activity_Id=@Exam_Activity_Id,Ip_address=@Ip_address,Correct_answer=@Correct_answer,Answered_questions=@Answered_questions,Unattempted_questions=@Unattempted_questions,Positive_Marks=@Positive_Marks,Negative_Marks=@Negative_Marks,Obtains_Marks=@Obtains_Marks,Attempt_id=@Attempt_id,created_date=@created_date,icreated_date=@icreated_date,Full_Marks=@Full_Marks,submit_time=@submit_time where  Studentid=@Studentid and Test_code=@Test_code and Sub_id=@Sub_id and Section=@Section";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Studentid", hd_studentid.Value);
                cmd.Parameters.AddWithValue("@Ip_address", hd_ipaddress.Value);
                cmd.Parameters.AddWithValue("@Test_code", hd_testid.Value);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Correct_answer", lbl_correct_answer.Text);
                cmd.Parameters.AddWithValue("@Answered_questions", lbl_tot_answered.Text);
                cmd.Parameters.AddWithValue("@Unattempted_questions", lbl_unattempted.Text);
                cmd.Parameters.AddWithValue("@Positive_Marks", lbl_p_marks.Text);
                cmd.Parameters.AddWithValue("@Negative_Marks", lbl_n_marks.Text);
                cmd.Parameters.AddWithValue("@Obtains_Marks", lbl_obtains.Text);
                cmd.Parameters.AddWithValue("@Attempt_id", hd_attempt_id.Value);
                cmd.Parameters.AddWithValue("@created_date", date);
                cmd.Parameters.AddWithValue("@icreated_date", idate);
                cmd.Parameters.AddWithValue("@Sub_id", Subject_id);
                cmd.Parameters.AddWithValue("@Full_Marks", lbl_total_marks.Text);
                cmd.Parameters.AddWithValue("@Exam_Activity_Id", Exam_Activity_Id);
                cmd.Parameters.AddWithValue("@submit_time", m1.time());
                if (My.InsertUpdateData(cmd))
                {

                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            string url = "";
            try
            {
                if (ViewState["modepage"].ToString() == "mobile")
                {
                    url = My.URL() + "Student_Profile/webview/OnlineMyResult.aspx?regid=" + hd_studentid.Value;
                }
                else
                {
                    url = "../OnlineMyResultweb.aspx";

                }

            }
            catch
            {
                url = My.URL() + "Student_Profile/webview/OnlineMyResult.aspx?regid=" + hd_studentid.Value;
            }


            Response.Redirect(url, false);
        }
    }
}