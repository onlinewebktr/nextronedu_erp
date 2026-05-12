using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{

    public partial class Question_Details : System.Web.UI.Page
    {
        My mycode = new My();
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        try
                        {
                            string testid = Request.QueryString["testid"];
                             
                            string type = Request.QueryString["type"];
                            if (!String.IsNullOrEmpty(testid))
                            {
                                hd_test_id.Value = testid;
                                
                                hd_exam_type.Value = "";
                                Bind_dataheadind();
                                hd_user.Value = Session["Admin"].ToString();
                                hd_actiontype.Value = type;
                                bind_grid();
                                 
                                
                                
                            }

                        }
                        catch (Exception ex)
                        {
                            My.submitexception(ex.ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_grid()
        {
            string query = "select * from question_info  where test_id='" + hd_test_id.Value + "' ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                grid_view.DataSource = null;
                grid_view.DataBind();
                grid_view.Visible = false;
                hd_Objective_Entry_id.Value = "";
            }
            else
            {
                hd_Objective_Entry_id.Value = dt.Rows[0]["Objective_Entry_id"].ToString();
                grid_view.Visible = true;
                grid_view.DataSource = dt;
                grid_view.DataBind();

            }

        }

        private void Bind_dataheadind()
        {
            string query = "Select Section,Exam_name ,(Select top 1 created_date from question_info where test_id='" + hd_test_id.Value + "' ) created_date1,(Select top 1 Create_time from question_info where test_id='" + hd_test_id.Value + "' ) Create_time1,(Select top 1 created_by from question_info where test_id='" + hd_test_id.Value + "' ) created_by1,(Select count(Id) from question_info where test_id='" + hd_test_id.Value + "'  ) as noofquestion,(select top 1 Subject_name from Subject_Master where Subject_id=OlineTest_Exam_name.subjectname) as subject_name,(select top 1 Course_Name from Add_course_table where course_id=OlineTest_Exam_name.Class_id) as Course_Name   from OlineTest_Exam_name where Exam_id='" + hd_test_id.Value + "'  ";

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);

            if (dt.Rows.Count == 0)
            {
                lbl_section.Text = "";
                lbl_testname1.Text = "";
                lbl_no_ofquestion.Text = "0";
            }
            else
            {
                lbl_section.Text = dt.Rows[0]["Section"].ToString();
                lbl_testname1.Text = dt.Rows[0]["Exam_name"].ToString();
                lbl_no_ofquestion.Text = dt.Rows[0]["noofquestion"].ToString();
                lbl_upload_date.Text = dt.Rows[0]["created_date1"].ToString() + "--" + dt.Rows[0]["Create_time1"].ToString();
                lbl_upload_by.Text = mycode.get_user(dt.Rows[0]["created_by1"].ToString());
                if (dt.Rows[0]["subject_name"].ToString() == "0")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else if (dt.Rows[0]["subject_name"].ToString() == "")
                {
                    lbl_subjectname_view.Text = "All Subject";
                }
                else
                {
                    lbl_subjectname_view.Text = dt.Rows[0]["subject_name"].ToString();
                }

                lbl_classname.Text= dt.Rows[0]["Course_Name"].ToString();

                Get_language();

            }





        }
        private void Get_language()
        {
            lbl_defult_Language.Text = "English";
            lbl_secondaryLanguage.Text = "English";
            //string query1 = "select PrimeryLanguage,SecondaryLanguage from Subject_Mapping_with_Class_Exam  where Test_id='" + hd_test_id.Value + "'    ";
            //DataTable dt = mycode.FillData(query1);
            //if (dt.Rows.Count == 0)
            //{
            //    lbl_defult_Language.Text = "Null";
            //    lbl_secondaryLanguage.Text = "NUll";
            //}
            //else
            //{
            //    lbl_defult_Language.Text = dt.Rows[0]["PrimeryLanguage"].ToString();
            //    lbl_secondaryLanguage.Text = dt.Rows[0]["SecondaryLanguage"].ToString();
            //}
        }

        string query = "";
        protected void grid_view_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_explanation = (Label)e.Row.FindControl("lbl_explanation");
                Label lbl_option_en = (Label)e.Row.FindControl("lbl_option_en");
                Label lbl_option_hn = (Label)e.Row.FindControl("lbl_option_hn");
                Label lbl_questionid = (Label)e.Row.FindControl("lbl_questionid");
                Bind_data_option_en(lbl_option_en, lbl_option_hn, lbl_questionid.Text);
                bind_explnation(lbl_explanation, lbl_questionid.Text);
            }
        }

        private void bind_explnation(Label lbl_explanation, string questionid)
        {
            query = "Select Explanation_en from Question_Explanation  where test_id='" + hd_test_id.Value + "'  and questionid='" + questionid + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "question_info");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                lbl_explanation.Text = dt.Rows[0][0].ToString();
            }

        }

        private void Bind_data_option_en(Label lbl_option_en, Label lbl_option_hn, string questionid)
        {

            string options1 = "";
            string options2 = "";
            query = "Select option_text,opetion_text_HN  from question_answer_Master where quest_code='" + questionid + "' and  test_code='" + hd_test_id.Value + "'  ";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "question_info");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                for (int i = 1; i <= rowcount; i++)
                {
                    if (i == 1)
                    {
                        options1 = "(a) " + dt.Rows[i - 1]["option_text"].ToString();// + "-" + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                        options2 = "(?) " + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                    }
                    else if (i == 2)
                    {
                        options1 = options1 + "&nbsp;&nbsp;" + "(b) " + dt.Rows[i - 1]["option_text"].ToString();// + "-" + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                        options2 = options2 + "&nbsp;&nbsp;" + "(?) " + dt.Rows[i - 1]["opetion_text_HN"].ToString();

                    }
                    else if (i == 3)
                    {
                        options1 = options1 + "&nbsp;&nbsp;" + "(c) " + dt.Rows[i - 1]["option_text"].ToString();// + "-" + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                        options2 = options2 + "&nbsp;&nbsp;" + "(?) " + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                    }
                    else if (i == 4)
                    {
                        options1 = options1 + "&nbsp;&nbsp;" + "(d) " + dt.Rows[i - 1]["option_text"].ToString();// + "-" + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                        options2 = options2 + "&nbsp;&nbsp;" + "(?) " + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                    }

                    else if (i == 5)
                    {
                        options1 = options1 + "&nbsp;&nbsp;" + "(e) " + dt.Rows[i - 1]["option_text"].ToString();// + "-" + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                        options2 = options2 + "&nbsp;&nbsp;" + "(??) " + dt.Rows[i - 1]["opetion_text_HN"].ToString();
                    }

                }
                lbl_option_en.Text = options1;
                lbl_option_hn.Text = options2;

            }
        }


    }
}