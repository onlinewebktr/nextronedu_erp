using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Send_Teacher_Message : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["Admin"] = Session["Admin"].ToString();
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["Branchid"] = code.getbranchid(Session["Admin"].ToString());
                        code.bind_all_ddl_with_all(ddl_teacher, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");


                        if (Request.QueryString["Id"] != null)
                        {
                            ViewState["id"] = Request.QueryString["Id"].ToString();
                            BindDetails();
                        }
                    }
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindDetails()
        {
            DataTable dt = code.FillTable("select * from Private_Messages_For_Teacher where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_submit.Text = "Update";

                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
                txt_notice_subject.Text = dt.Rows[0]["Subject"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachments"].ToString();



                try
                {
                    if (dt.Rows[0]["Teacher_Id"].ToString() == "ALL")
                    {
                        ddl_teacher.SelectedValue = "0";

                    }
                    else
                    {
                        ddl_teacher.Text = dt.Rows[0]["Teacher_Id"].ToString();

                    }

                }
                catch
                {
                }





            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_notice_subject.Text == "")
                {
                    Alert("Please enter subject ");
                }

                else if (txt_notice_details.Text == "")
                {
                    Alert("Please enter message details ");
                }
                else
                {
                    final_save_data();
                }




            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void final_save_data()
        {
            if (btn_submit.Text == "Send")
            {
                string ImagePath = GetImagePath();
                SqlCommand cmd = new SqlCommand("INSERT INTO Private_Messages_For_Teacher (Subject,Details,Attachments,Date,Idate,Time,Send_Status,Teacher_Id,Date_Main,Branch_id,Session_id,Createdby) values (@Subject,@Details,@Attachments,@Date,@Idate,@Time,@Send_Status,@Teacher_Id,@Date_Main,@Branch_id,@Session_id,@Createdby)");
                cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Attachments", ImagePath);
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@Time", code.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Createdby", ViewState["Admin"].ToString());
                if (ddl_teacher.SelectedItem.Text == "ALL")
                {
                    cmd.Parameters.AddWithValue("@Teacher_Id","ALL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@Date_Main", code.getdate1());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Alert("Message has been send sucessfully.");
                    txt_notice_details.Text = "";
                    txt_notice_subject.Text = "";

                }

            }
            if (btn_submit.Text == "Update")
            {
                if (fl_Photo.HasFile)
                {
                    Hd_Photo.Value = GetImagePath();
                }
                else
                {

                }
                SqlCommand cmd = new SqlCommand("Update Private_Messages_For_Teacher set Subject=@Subject,Details=@Details,Attachments=@Attachments,Date=@Date,Idate=@Idate,Time=@Time,Send_Status=@Send_Status,Teacher_Id=@Teacher_Id,Date_Main=@Date_Main where Id = @Id");

                cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Attachments", Hd_Photo.Value);
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@Time", code.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                if (ddl_teacher.SelectedItem.Text == "ALL")
                {
                    cmd.Parameters.AddWithValue("@Teacher_Id", "ALL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);
                }
                 
                cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                cmd.Parameters.AddWithValue("@Date_Main", code.getdate1());
                

                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    btn_submit.Text = "Send";
                    Alert("Message has been updated sucessfully.");
                    txt_notice_details.Text = "";
                    txt_notice_subject.Text = "";
                    
                }
            }
        }

        private string GetImagePath()
        {
            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Notice_board/");
            return Path;
        }
    }
}