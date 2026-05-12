using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Add_Feeback_Question : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admindov"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Admin"] = Session["Admindov"].ToString();
                    fetch_data_question();
                }

            }
            else
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
            }

        }
        UsesCode code = new UsesCode();
        private void fetch_data_question()
        {
            string query = "Select * from Feedback_questions_master order by Id asc ";
            try
            {


                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    hdid.Value = "";

                    RPDetails.DataSource = null;
                    RPDetails.DataBind();

                }
                else
                {


                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch
            {

            }
        }
        #region edit/delete
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label Id = (Label)row.FindControl("lbl_Id");
                My.exeSql("Delete from Feedback_questions_master where Id=" + Id.Text + "");
                Alert("Question has been successful delete");
                fetch_data_question();
            }
            catch
            {

            }
            
        }
        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Question = (Label)row.FindControl("lbl_Question");
                hdid.Value = Id.Text;
                txt_question.Text = lbl_Question.Text;
                btn_submit.Text = "Update";
            }
            catch
            {

            }

        }


        #endregion

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (btn_submit.Text == "Add")
            {
                if (txt_question.Text == "")
                {
                    Alert("Please enter zoom user id"); return;
                }
                else
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Feedback_questions_master (Question,Question_date_time,User_by) values (@Question,@Question_date_time,@User_by)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Question", txt_question.Text.Trim());
                    cmd.Parameters.AddWithValue("@Question_date_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@User_by", ViewState["Admin"].ToString());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alert("Feedback questions has been added successfully");
                        txt_question.Text = "";
                        btn_submit.Text = "Add";
                        fetch_data_question();
                    }
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Feedback_questions_master set Question=@Question,Question_date_time=@Question_date_time   where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Question", txt_question.Text.Trim());
                cmd.Parameters.AddWithValue("@Question_date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Id", hdid.Value);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Alert("Feedback questions has been updated successfully");
                    txt_question.Text = "";
                    btn_submit.Text = "Add";
                    fetch_data_question();
                }
            }
        }
    }
}