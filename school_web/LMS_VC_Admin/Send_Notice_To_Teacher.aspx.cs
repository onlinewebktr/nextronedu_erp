using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Send_Notice_To_Teacher : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        My imp = new My();
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
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["Admin"] = Session["Admin"].ToString();
                        ViewState["Branch_id"] = imp.get_branch_id(Session["Admin"].ToString());
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
            DataTable dt = code.FillTable("select * from Notice_Board_Details_Teacher where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_submit.Text = "Update";
                txt_notice_details.Text = dt.Rows[0]["Notice"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachment"].ToString();
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
                txt_date.Text = dt.Rows[0]["Posted_Date"].ToString();
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
                if (txt_date.Text == "")
                {
                    Alert("Please enter notice date ");
                }

                else if (txt_notice_details.Text == "")
                {
                    Alert("Please enter notice ");
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details_Teacher (Notice,Posted_Date,Posted_Idate,Posted_Time,Send_Status,Attachment,Date_Main,Teacher_Id,Session_id,Branch_id) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Send_Status,@Attachment,@Date_Main,@Teacher_Id,@Session_id,@Branch_id)");
                cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Posted_Idate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Posted_Time", code.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                cmd.Parameters.AddWithValue("@Date_Main", code.getdate2(txt_date.Text));
                if (ddl_teacher.SelectedItem.Text == "ALL")
                {
                    cmd.Parameters.AddWithValue("@Teacher_Id", "ALL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Teacher_Id", ddl_teacher.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Alert("Notice have been send sucessfully.");
                    txt_notice_details.Text = "";
                    txt_date.Text = "";

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
                SqlCommand cmd = new SqlCommand("Update Notice_Board_Details_Teacher set Notice=@Notice,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Send_Status=@Send_Status,Attachment=@Attachment,Date_Main=@Date_Main,Teacher_Id=@Teacher_Id where Id = @Id");

                cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);

                cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Posted_Idate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Posted_Time", code.time());
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

                cmd.Parameters.AddWithValue("@Attachment", Hd_Photo.Value);
                cmd.Parameters.AddWithValue("@Date_Main", code.getdate2(txt_date.Text));

                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    btn_submit.Text = "Send";
                    Alert(" Notice details have been successfully Updated.");
                    txt_notice_details.Text = "";
                    txt_date.Text = "";
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