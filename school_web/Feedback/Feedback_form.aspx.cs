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

namespace school_web.Feedback
{
    public partial class Feedback_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["requestid"] != null && Request.QueryString["isparents"] != null && Request.QueryString["studentid"] != null)
                {
                    ViewState["requestid"] = Request.QueryString["requestid"].ToString();
                    ViewState["studentid"] = Request.QueryString["studentid"].ToString();
                    ViewState["isparents"] = Request.QueryString["isparents"].ToString();
                    ViewState["Name"] = Session["Name"].ToString();
                    ViewState["Mobile_no"] = Session["Mobile_no"].ToString();

                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["session_name"] = My.get_session();

                    fetch_question();
                    fetch_data();
                }
                else
                {
                    Response.Redirect("Feedback_one.aspx", false);
                }
            }

        }
        private void fetch_data()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {


            }
            else
            {
                schoollogo.Src = dt.Rows[0]["logo"].ToString();
                lbl_schoolname.Text = dt.Rows[0]["firm_name"].ToString();
                copyright1.Text = dt.Rows[0]["Footer_Copy_Right"].ToString();

            }
        }
        string scrpt;
        private void alert(string msg)
        {
            msg1.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        My mycode = new My();
        private void fetch_question()
        {
            DataTable dt = mycode.FillData("select * from Feedback_questions_master order by Id asc ");
            if (dt.Rows.Count == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }

        protected void btn_other_studentfeedback_Click(object sender, EventArgs e)
        {
            try
            {
                bool finalsend = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();

                    int growcount = GrdView.Rows.Count;
                    for (int i = 0; i < growcount; i++)
                    {
                        Label lbl_question = (Label)GrdView.Rows[i].FindControl("lbl_question");
                        DropDownList ddl_rating = (DropDownList)GrdView.Rows[i].FindControl("ddl_rating");

                        btn_savedata_feedback(lbl_question.Text, ddl_rating.Text, con);
                    }
                    btn_savedata_feedback_main(con);
                    finalsend = true;
                    con.Close();
                    scope.Complete();
                }
                if (finalsend == true)
                {
                    Session["msg2"] = "Your valuable feedback has been submitted.";
                    Response.Redirect("Feedback_one.aspx", false);
                }

            }
            catch (Exception ex)
            {
                alert("Please try again");
            }

        }

        private void btn_savedata_feedback_main(SqlConnection con)
        {
            string query = "INSERT INTO Feedback_ans_master (Feedback_id,Name,Mobile_no,Student_type,Isparents,Student_admission_no,date_time,Session_id) values (@Feedback_id,@Name,@Mobile_no,@Student_type,@Isparents,@Student_admission_no,@date_time,@Session_id)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Feedback_id", ViewState["requestid"].ToString());
            cmd.Parameters.AddWithValue("@Name", ViewState["Name"].ToString());
            cmd.Parameters.AddWithValue("@Mobile_no", ViewState["Mobile_no"].ToString());
            if (ViewState["studentid"].ToString() == "No")
            {
                cmd.Parameters.AddWithValue("@Student_type", "Other");
                cmd.Parameters.AddWithValue("@Student_admission_no", "NA");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Student_type", "Own Student");
                cmd.Parameters.AddWithValue("@Student_admission_no", ViewState["studentid"].ToString());
            }
            cmd.Parameters.AddWithValue("@Isparents", ViewState["isparents"].ToString());
            cmd.Parameters.AddWithValue("@date_time", My.getdate1());
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
            if (payments.InsertUpdateData(cmd, con))
            {

            }

        }

        private void btn_savedata_feedback(string question, string rating, SqlConnection con)
        {
            string query = "INSERT INTO Feedback_ans (Feedback_id,Question,Ans_rating,date_time) values (@Feedback_id,@Question,@Ans_rating,@date_time)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Feedback_id", ViewState["requestid"].ToString());
            cmd.Parameters.AddWithValue("@Question", question);
            cmd.Parameters.AddWithValue("@Ans_rating", rating);
            cmd.Parameters.AddWithValue("@date_time", My.getdate1());
            if (payments.InsertUpdateData(cmd, con))
            {

            }
        }
    }
}