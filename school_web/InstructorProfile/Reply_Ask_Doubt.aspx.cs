using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.InstructorProfile
{
    public partial class Reply_Ask_Doubt : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        My im = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    

                    if (Request.QueryString["id"] != null)
                    {
                        hd_request_id.Value = Request.QueryString["id"];
                        string date = im.get_date_uploading_askdout(hd_request_id.Value);
                        ViewState["sessionid"] = im.get_session_date_dat_time(date);

                       
                        fetch_req_detsils();
                    }
                    else
                    {
                        Response.Redirect("Home.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void fetch_req_detsils()
        {
            a3.Visible = false;
            string sql = " select t1.*,t2.admissionserialnumber,t2.gcm_id,t2.rollnumber,t2.Section,t2.studentname+',(Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t2.class as CategoryName,(select top 1 name from user_details where user_id=t1.Teacher_Id) as teachername  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber     where    t1.Id=" + hd_request_id.Value + " and t2.Session_id='" + ViewState["sessionid"].ToString() + "' ";
            DataTable dt = mycode.FillTable(sql);
            if (dt.Rows.Count.ToString() != "0")
            {
                mycode.RptrData(dt, RPDetails);

                ViewState["admissionserialnumber"] = dt.Rows[0]["admissionserialnumber"].ToString();
                ViewState["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                ViewState["Doubt_Id"] = dt.Rows[0]["Doubt_Id"].ToString();
                lbl_question.Text = dt.Rows[0]["Student_question"].ToString();
                if (dt.Rows[0]["Question_Image"].ToString() == "")
                {
                    a1.HRef = "";
                    a1.Visible = false;
                }
                else
                {
                    a1.HRef = dt.Rows[0]["Question_Image"].ToString();
                    a1.Visible = true;
                }
                
               
                if (dt.Rows[0]["Status"].ToString() == "Pending")
                { 
                    pnl_msg_sends.Visible = true;
                    admin.Visible = false;
                    pnl_msg_sends.Visible = true;
                }
                else
                {
                    pnl_msg_sends.Visible = false;
                    admin.Visible = true;
                    pnl_closes.Visible =  true;
                    lbl_answer.Text = dt.Rows[0]["Answer"].ToString();
                    lbl_replydate.Text = dt.Rows[0]["AnswerDate"].ToString();
                    lbl_teachername.Text = dt.Rows[0]["teachername"].ToString();
                    if (dt.Rows[0]["Answer_Image"].ToString() == "")
                    {
                        a3.Visible = false;
                    }
                    else
                    {
                        a3.Visible = true;

                    }
                }
            }
            else
            {
                txt_message.Text = "";
                a1.Visible = false;
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
        }

        protected void bntn_send_reply_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_message.Text == "")
                {
                    lblmessage.Text = "Please enter your reply  ";
                    
                    string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
                else
                {
                    string ImagePath = GetImagePath();

                    SqlCommand cmd = new SqlCommand("Update Student_doubt_list set Answer=@Answer,AnswerDate=@AnswerDate,AnswerIdate=@AnswerIdate,Answer_Image=@Answer_Image,Status=@Status where Id = @Id");
                    cmd.Parameters.AddWithValue("@Answer", txt_message.Text);
                    cmd.Parameters.AddWithValue("@AnswerDate", mycode.date());
                    cmd.Parameters.AddWithValue("@AnswerIdate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Answer_Image", ImagePath);
                    cmd.Parameters.AddWithValue("@Status", "Replied");
                    cmd.Parameters.AddWithValue("@Id", hd_request_id.Value);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        lblmessage.Text = "Your reply has been sucessfully submited   ";
                        string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        fetch_req_detsils();

                        try
                        {
                            mycode.pushnotification("Your doubt has been resloved please check", "Doubt", ViewState["gcm_id"].ToString(), ViewState["Doubt_Id"].ToString(), ViewState["admissionserialnumber"].ToString(), "RequestReplyByTeacher");
                        }
                        catch
                        {
                        }
                    }
                }

            }
            catch
            {
            }
            
        }

        private string GetImagePath()
        {
            string Path = mycode.Upload_doc_images(fl_Photo, "/UploadedImage/Noticeboard/");
            return Path;
        }

        protected void lnk_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Student_Doubt_List_Pending.aspx", false);
        }
    }
}