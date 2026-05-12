using school_web.AppCode;
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
    public partial class Rank_Configuration : System.Web.UI.Page
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
                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");

                    ddl_class.SelectedValue = My.get_top_one_class();
                    rd_yearwise.Checked = true;
                    Bind_data__filed("Yearly Ranking");
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




        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            string type = "";
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else if (rd_yearwise.Checked == true)
                {
                    type = "Yearly Ranking";
                }
                else if (rd_termwise.Checked == true)
                {
                    type = "Term Wise Ranking";
                }

                else if (rd_Assessmentwise.Checked == true)
                {
                    type = "Assessment Wise Ranking";
                }
                else
                {

                }
                if (type == "")
                {
                    Alertme("Sorry Please select type of ranking", "warning");
                }
                else
                {
                    SqlCommand cmd;
                    DataTable dt1 = mycode.FillData("Select * from Exam_Rank_Configuration where  Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and Rank_Mode='" + type + "'");
                    if (dt1.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Exam_Rank_Configuration (Branch_id,Session_Id,Rank_Mode,Rank_based_On_Marks_Percentage,Is_Consider_Failed,Is_Ranks_Passed,Is_Skip_ranks,Created_By,Created_Date,Class_Id) values (@Branch_id,@Session_Id,@Rank_Mode,@Rank_based_On_Marks_Percentage,@Is_Consider_Failed,@Is_Ranks_Passed,@Is_Skip_ranks,@Created_By,@Created_Date,@Class_Id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Rank_Mode", type);

                        if (rd_marks.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Rank_based_On_Marks_Percentage", "Marks");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Rank_based_On_Marks_Percentage", "Percentage");
                        }

                        if (chk_considaer_failed_student.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Consider_Failed", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Consider_Failed", 0);
                        }
                        if (chk_rank_pass_student.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Ranks_Passed", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Ranks_Passed", 0);

                        }
                        if (chk_skip_ranks.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Skip_ranks", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Skip_ranks", 0);

                        }

                        cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"]);
                        cmd.Parameters.AddWithValue("@Created_Date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Rank Configuration has been saved successfully", "success");
                        }
                    }
                    else
                    {
                        string id = dt1.Rows[0]["Id"].ToString();
                        string query = "Update Exam_Rank_Configuration set Branch_id=@Branch_id,Session_Id=@Session_Id,Rank_Mode=@Rank_Mode,Rank_based_On_Marks_Percentage=@Rank_based_On_Marks_Percentage,Is_Consider_Failed=@Is_Consider_Failed,Is_Ranks_Passed=@Is_Ranks_Passed,Is_Skip_ranks=@Is_Skip_ranks,Updated_By=@Updated_By,Update_date=@Update_date,Class_Id=@Class_Id where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Rank_Mode", type);

                        if (rd_marks.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Rank_based_On_Marks_Percentage", "Marks");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Rank_based_On_Marks_Percentage", "Percentage");
                        }

                        if (chk_considaer_failed_student.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Consider_Failed", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Consider_Failed", 0);
                        }
                        if (chk_rank_pass_student.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Ranks_Passed", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Ranks_Passed", 0);

                        }
                        if (chk_skip_ranks.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Skip_ranks", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Skip_ranks", 0);

                        }

                        cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"]);
                        cmd.Parameters.AddWithValue("@Update_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Rank Configuration has been saved successfully", "success");
                        }


                    }
                }

            }
            catch
            {

            }

        }

        protected void btn_cancel_Click1(object sender, EventArgs e)
        {

        }



        protected void rd_yearwise_CheckedChanged(object sender, EventArgs e)
        {

            Bind_data__filed("Yearly Ranking");
        }

        protected void rd_termwise_CheckedChanged(object sender, EventArgs e)
        {
            Bind_data__filed("Term Wise Ranking");
        }

        protected void rd_Assessmentwise_CheckedChanged(object sender, EventArgs e)
        {
            Bind_data__filed("Assessment Wise Ranking");
        }

        private void Bind_data__filed(string type)
        {

            string query = "Select *  from Exam_Rank_Configuration where   Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + " and Class_Id='" + ddl_class.SelectedValue + "'  and Rank_Mode='" + type + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                chk_considaer_failed_student.Checked = false;
                chk_rank_pass_student.Checked = false;
                chk_skip_ranks.Checked = false;
                //rd_yearwise.Checked = false;
                //rd_termwise.Checked = false;
                //rd_Assessmentwise.Checked = false;
                rd_marks.Checked = false;
                rd_Percentage.Checked = false;


            }
            else
            {

                if (dt.Rows[0]["Rank_based_On_Marks_Percentage"].ToString() == "Marks")
                {
                    rd_marks.Checked = true;
                    rd_Percentage.Checked = false;
                }
                else
                {
                    rd_Percentage.Checked = true;
                    rd_marks.Checked = false;
                }
                if (dt.Rows[0]["Is_Consider_Failed"].ToString() == "True")
                {
                    chk_considaer_failed_student.Checked = true;
                }
                else
                {
                    chk_considaer_failed_student.Checked = false;
                }
                if (dt.Rows[0]["Is_Ranks_Passed"].ToString() == "True")
                {
                    chk_rank_pass_student.Checked = true;
                }
                else
                {
                    chk_rank_pass_student.Checked = false;
                }

                if (dt.Rows[0]["Is_Skip_ranks"].ToString() == "True")
                {
                    chk_skip_ranks.Checked = true;
                }
                else
                {
                    chk_skip_ranks.Checked = false;
                }





            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                if (rd_yearwise.Checked == true)
                {
                    Bind_data__filed("Yearly Ranking");
                }
                else if (rd_termwise.Checked == true)
                {
                    Bind_data__filed("Term Wise Ranking");
                }
                else if (rd_Assessmentwise.Checked == true)
                {
                    Bind_data__filed("Assessment Wise Ranking");
                }
            }

        }
    }
}