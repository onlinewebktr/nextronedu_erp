using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{
    public partial class Result_viewdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {


                    string testid = Request.QueryString["testid"].ToString();
                    string Attempt_id = Request.QueryString["Attemptid"].ToString();
                    hd_studentid.Value = Request.QueryString["studentid"].ToString();
                    string Entry_no = "0";
                    string Packageid = "0";
                    if (!String.IsNullOrEmpty(testid))
                    {


                        hd_testid.Value = testid;
                        find_examid_mode_examtypeid();

                        hd_attempt_id.Value = Attempt_id;
                        hd_entry_id.Value = Entry_no;
                        hd_package_id.Value = Packageid;

                        find_default_language();
                        find_data();
                        bind_gridview();
                        check_question_explanation();

                        Bind_schoolinfo();
                        student_name_bind();

                        try
                        {
                           string  open = Request.QueryString["open"].ToString();
                            if(open== "teach")
                            {
                                btn_back.Visible = false;
                                btn_print.Visible = false;
                            }
                            else
                            {
                                btn_back.Visible = true;
                                btn_print.Visible = true;
                            }

                        }
                        catch
                        {
                            btn_back.Visible = true;
                            btn_print.Visible = true;
                        }
                    }
                    else
                    {

                    }

                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Result view");
                }
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
        private void find_examid_mode_examtypeid()
        {


            hd_examtype_code.Value = "4";
            hd_testmode.Value = "101";

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
                hd_Examcode.Value = hd_testid.Value;
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
        My m1 = new My();
        private void find_default_language()
        {
            DataTable dt = m1.FillData("select top 1 Language_Itype,icreated_date from user_test_attempted_details where  Studentid='" + hd_studentid.Value + "' and Test_code='" + hd_testid.Value + "'");
            if (dt.Rows.Count == 0)
            {
                hd_df_language.Value = "0";
                hd_idate.Value = "0";

            }
            else
            {
                hd_idate.Value = dt.Rows[0]["icreated_date"].ToString(); ;
                hd_df_language.Value = dt.Rows[0]["Language_Itype"].ToString();
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

            double tot_pmarks = 0;
            double tot_nmarks = 0;
            // Ip_address ='" + hd_ipaddress.Value + @"' and 
            string qry = @"select count(*),(select count(*) from user_test_details where test_code='" + hd_testid.Value + @"' and icreated_date='" + hd_idate.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done' and Attempt_id='" + hd_attempt_id.Value + @"'  and cast(marks as float)>0 ),

(select SUM(CAST(marks as float)) from user_test_details   where  test_code='" + hd_testid.Value + @"' and icreated_date='" + hd_idate.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done' and Attempt_id='" + hd_attempt_id.Value + @"' and cast(marks as float)>0  ),
(select SUM(CAST(marks as float)) from user_test_details  where   test_code='" + hd_testid.Value + @"' and icreated_date='" + hd_idate.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done'  and Attempt_id='" + hd_attempt_id.Value + @"' and cast(marks as float)<0 ),
(select count(*) from user_test_details where test_code='" + hd_testid.Value + @"' and icreated_date='" + hd_idate.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done'  and Attempt_id='" + hd_attempt_id.Value + @"' and cast(marks as float)<0 )  as totalneagitive
 from user_test_details where  test_code='" + hd_testid.Value + @"' and icreated_date='" + hd_idate.Value + @"' and Studentid='" + hd_studentid.Value + @"' and Exam_code='" + hd_Examcode.Value + @"'  and Status='Done' and Attempt_id='" + hd_attempt_id.Value + "'";

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
            lbl_no_of_question.Text = "0";
            DataTable dt = new DataTable();
            dt = m1.featch_data("select * from question_info where test_id='" + hd_testid.Value + "' and Uploding_status='Administrator'   ");
            if (dt.Rows.Count > 0)
            {
                grd_view.DataSource = dt;
                grd_view.DataBind();
                lbl_no_of_question.Text = dt.Rows.Count.ToString();

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
                    lbl_your_asnwer.Text = dt.Rows[0]["option_text_en"].ToString();//+ "/" + dt.Rows[0]["opetion_text_HN"].ToString();

                    if (lbl_Opetion_id.Text == dt.Rows[0][6].ToString())
                    {
                        lbl_your_asnwer.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_your_asnwer.ForeColor = System.Drawing.Color.Red;
                    }

                    lbl_answer_hn.Visible = true;
                    lbl_answer.Visible = true;
                    DataTable dt1 = new DataTable();
                    lbl_time.Text = dt.Rows[0][11].ToString() + "(mm:ss)";
                }
                else
                {
                    lbl_answer_hn.Visible = true;
                    lbl_answer.Visible = true;

                }
            }

        }
        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }

        protected void lbl_viewresult_Click(object sender, EventArgs e)
        {

            string testid = m1.Zip(hd_testid.Value);
            //Response.Redirect("OnlineQuestion_Explanation.aspx?id=" + Uri.EscapeDataString(testid) + "&mode=" + ViewState["modepage"].ToString()+ "&stu=" + Uri.EscapeDataString(hd_studentid.Value) );


            //string testid = Session["testid"].ToString();
            //string Attempt_id = Session["testid"].ToString();
            //hd_studentid.Value = Session["student"].ToString();
            //Session["mode"]

            Response.Redirect("OnlineQuestion_Explanation.aspx");


        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            string url = "";
            try

            {

                url = "Student_Attempted_Exam.aspx";


                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {

            }

        }
    }
}