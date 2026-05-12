using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class doubt_question : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        try
                        {
                            get_student_info();
                        }
                        catch (Exception ex)
                        {
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
            }
            catch (Exception ex)
            {
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

        private void get_student_info()
        {
            DataTable dt = mycode.FillData("Select * from admission_registor where Session_id=" + ViewState["sesssionid"].ToString() + " and admissionserialnumber='" + Session["User"].ToString() + "'");
            if (dt.Rows.Count != 0)
            {
                get_question_info(dt.Rows[0]["Class_id"].ToString(), dt.Rows[0]["admissionserialnumber"].ToString());
            }
        }

        private void get_question_info(string class_id, string admission_no)
        {
            DataTable dt = mycode.FillData("select t1.*,t2.studentname,CASE WHEN t2.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN t2.studentimagepath is not null THEN t2.studentimagepath END AS Student_img,t2.class,CASE WHEN t1.Status='Pending' THEN 'hidden'  WHEN t1.Status='Replied' THEN t1.Status END AS Reply_status,(Select top 1 name from user_details where user_id=t1.Teacher_Id)   as tecahername from Student_doubt_list t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Branch=t2.Branch_id and t1.Class_Id=t2.Class_Id and t1.User_Id=t2.admissionserialnumber where t1.Class_Id='" + class_id + "' and t1.User_Id='" + admission_no + "' order by t1.Id desc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rp_doubt.DataSource = null;
                rp_doubt.DataBind();
            }
            else
            {
                rp_doubt.DataSource = dt;
                rp_doubt.DataBind();
            }
        }

        protected void lnk_ask_again_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_doubt_id = (Label)row.FindControl("lbl_doubt_id");

                DataTable dt = mycode.FillData("select top 1 * from Student_doubt_list where Doubt_Id='" + lbl_doubt_id.Text + "' and User_Id='" + Session["User"].ToString() + "' order by id desc");
                if (dt.Rows.Count > 0)
                {
                    if(dt.Rows[0]["Status"].ToString()== "Pending")
                    {

                        Alertme("You have already asked this question, and it is waiting for the teachers reply. Please wait for the teacher to respond before asking the same question again. ", "warning");
                    }
                    else
                    {
                        string doubt_id = get_doubt_id();
                        SqlCommand cmd;
                        string query = "INSERT INTO Student_doubt_list (Class_Id,Cource_Id,Teacher_Id,Cource_name,Student_question,Question_Image,Status,Date,Idate,User_Id,Doubt_Id,Session_id,Branch) values (@Class_Id,@Cource_Id,@Teacher_Id,@Cource_name,@Student_question,@Question_Image,@Status,@Date,@Idate,@User_Id,@Doubt_Id,@Session_id,@Branch);";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Class_Id", dt.Rows[0]["Class_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Cource_Id", dt.Rows[0]["Cource_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Teacher_Id", dt.Rows[0]["Teacher_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Cource_name", dt.Rows[0]["Cource_name"].ToString());
                        cmd.Parameters.AddWithValue("@Student_question", dt.Rows[0]["Student_question"].ToString());
                        cmd.Parameters.AddWithValue("@Question_Image", dt.Rows[0]["Question_Image"].ToString());
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@User_Id", dt.Rows[0]["User_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Doubt_Id", doubt_id);
                        cmd.Parameters.AddWithValue("@Session_id", dt.Rows[0]["Session_id"].ToString());
                        cmd.Parameters.AddWithValue("@Branch", dt.Rows[0]["Branch"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Your question has been submitted successfully. Teacher will reply you soon.", "success");
                        }

                    }
                  
                }
            }
            catch (Exception ex)
            {
            }
        }


        private string get_doubt_id()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string a = r.Next(2478, 9758) +
              DateTime.Now.Year.ToString("0000") +
              DateTime.Now.Day.ToString("00") +
              DateTime.Now.Month.ToString("00") +
              DateTime.Now.Hour.ToString("00") +
              DateTime.Now.Minute.ToString("00") +
              DateTime.Now.Second.ToString("00") +
              DateTime.Now.Millisecond.ToString("00") + r.Next(788, 999);
            return a;
        }

        protected void lnk_answer_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_answer = (Label)row.FindControl("lbl_answer");
                Label lbl_answerDate = (Label)row.FindControl("lbl_answerDate");
                Label lbl_answer_image = (Label)row.FindControl("lbl_answer_image");


                lbl_reply_date.Text = lbl_answerDate.Text;
                if (lbl_answer_image.Text == "")
                {
                    Image1.ImageUrl = "/Student_Profile/assets/images/icons/subject-icon.png";
                }
                else
                {
                    Image1.ImageUrl = lbl_answer_image.Text;
                }
                lbl_answerss.Text = lbl_answer.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDoubt();", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void rp_doubt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;

            if (((Label)e.Item.FindControl("lbl_Attachment")).Text == "")
            {
                a1.Visible = false;
            }
            else
            {
                a1.Visible = true;
            }
        }
    }
}