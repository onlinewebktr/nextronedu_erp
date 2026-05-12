using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class Apply_Income_Certificate : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["regid"] = Session["User"].ToString();
                    try
                    {
                        Bind_data();
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

        private void Bind_data()
        {
            DataTable dt = mycode.FillData("Select * from Apply_For_TC where Session_id=" + ViewState["sesssionid"].ToString() + " and Admission_no='" + ViewState["regid"].ToString() + "' and   Apply_For='Income Certificate'");
            if (dt.Rows.Count == 0)
            {
                btn_Submit.Visible = true;
            }
            else
            {
                if (dt.Rows[0]["Status"].ToString() == "Reject")
                {

                }
                else
                {
                    btn_Submit.Visible = false;
                    Alertme("Sorry, You can't apply for income certificate , because you have already applied for income certificate", "warning");
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string ImagePath = "";
            try
            {
                if (txt_description.Text == "")
                {
                    Alertme("Please write a message ", "warning");
                }
                else
                {
                    DataTable dt = mycode.FillData("Select * from Apply_For_TC where Session_id=" + ViewState["sesssionid"].ToString() + " and Admission_no='" + ViewState["regid"].ToString() + "' and   Apply_For='Income Certificate'");
                    if (dt.Rows.Count == 0)
                    {
                        if (fl_Photo.HasFile)
                        {
                            ImagePath = GetImagePath();


                        }

                        SqlCommand cmd;
                        string query = "INSERT INTO Apply_For_TC (Session_id,Admission_no,Apply_date_time,Apply_For,Apply_message,Status,Attachment) values (@Session_id,@Admission_no,@Apply_date_time,@Apply_For,@Apply_message,@Status,@Attachment)";
                        cmd = new SqlCommand(query);

                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", ViewState["regid"].ToString());
                        cmd.Parameters.AddWithValue("@Apply_date_time", My.getdate1());
                        cmd.Parameters.AddWithValue("@Apply_For", "Income Certificate");
                        cmd.Parameters.AddWithValue("@Apply_message", txt_description.Text);
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("You have been successfully applied for income certificate.", "success");
                            txt_description.Text = "";
                        }

                    }
                    else
                    {
                        if (dt.Rows[0]["Status"].ToString() == "Reject")
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Apply_For_TC (Session_id,Admission_no,Apply_date_time,Apply_For,Apply_message,Status,Attachment) values (@Session_id,@Admission_no,@Apply_date_time,@Apply_For,@Apply_message,@Status,@Attachment)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["regid"].ToString());
                            cmd.Parameters.AddWithValue("@Apply_date_time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Apply_For", "Income Certificate");
                            cmd.Parameters.AddWithValue("@Apply_message", txt_description.Text);
                            cmd.Parameters.AddWithValue("@Status", "Pending");
                            cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("You have been successfully applied for income certificate.", "success");
                                txt_description.Text = "";
                            }
                        }
                        else
                        {
                            btn_Submit.Visible = false;
                            Alertme("Sorry, You can't apply for income certificate , because you have already applied for income certificate ", "warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        UsesCode code = new UsesCode();
        private string GetImagePath()
        {
            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Notice_board/");
            return Path;
        }
    }
}