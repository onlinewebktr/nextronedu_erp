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
    public partial class Create_Live_Class_Credentials : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sesssionid"] = My.get_session_id();



                search_data();
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void search_data()
        {
            string query = "Select * from LiveClassCredential where Is_delete=1 order by Template_Name";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }

        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_iStatus")).Text == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnk_status")).Text = "Yes";
                   

                    ((LinkButton)e.Item.FindControl("lnk_status")).BackColor = System.Drawing.ColorTranslator.FromHtml("#10c74d");
                    ((LinkButton)e.Item.FindControl("lnk_status")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");

                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_status")).Text = "No";
                   
                    

                    ((LinkButton)e.Item.FindControl("lnk_status")).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0909");
                    ((LinkButton)e.Item.FindControl("lnk_status")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#fff");


                }
            }
        }

        protected void lnk_status_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_iStatus = (Label)row.FindControl("lbl_iStatus");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");

                string Istatus = "1";
                if (lbl_iStatus.Text == "1")
                {
                    Istatus = "0";
                }


                code.executequery("update LiveClassCredential set Status=" + Istatus + " where Id=" + lbl_Id.Text + "");
                Alert("Status has been updated successfully.");
                search_data();

            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_TemplateID = (Label)row.FindControl("lbl_TemplateID");
                bool check_TemplateIDused = get_tempalteid_is_used(lbl_TemplateID.Text);
                if (check_TemplateIDused == false)
                {
                    Alert("Sorry you cannot delete credination because this id already use  mode");
                }
                else
                {
                    My.exeSql("delete LiveClassCredential where Id=" + lbl_Id.Text + "");
                    Alert("Deletion process has been successfully");
                    search_data();
                }
            }
            catch
            {


            }

        }

        private bool get_tempalteid_is_used(string TemplateID)
        {
            string query = "Select * from LiveClassTeacherMapping where TemplateId='" + TemplateID + "'  ";
            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Template_Name = (Label)row.FindControl("lbl_Template_Name");
                Label lbl_TemplateID = (Label)row.FindControl("lbl_TemplateID");
                Label lbl_RoomId = (Label)row.FindControl("lbl_RoomId");
                Label lbl_EmailId = (Label)row.FindControl("lbl_EmailId");
                Label lbl_Password100ms = (Label)row.FindControl("lbl_Password100ms");
                Label lbl_AccessKey = (Label)row.FindControl("lbl_AccessKey");
                Label lbl_AppSecret = (Label)row.FindControl("lbl_AppSecret");
                txt_templates_name.Text = lbl_Template_Name.Text;
                txt_Templates_id.Text = lbl_TemplateID.Text;
                txt_rommid.Text = lbl_RoomId.Text;
                txt_emailid.Text = lbl_EmailId.Text;
                txt_Password.Text = lbl_Password100ms.Text;
                txt_accesskey.Text = lbl_AccessKey.Text;
                txt_app_secretkey.Text = lbl_AppSecret.Text;
                hd_id.Value = lbl_Id.Text;
                btn_submit.Text = "Update";
            }
            catch
            {

            }


        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_submit.Text == "Add")
                {
                    string query = "Select * from LiveClassCredential where TemplateID='" + txt_Templates_id.Text + "'  ";
                    DataTable dt = code.FillTable(query);
                    if (dt.Rows.Count == 0)
                    {

                        SqlCommand cmd;
                        string query1 = "INSERT INTO LiveClassCredential (AccessKey,AppSecret,TemplateID,RoomId,EmailId,Password100ms,Status,Template_Name,Create_date,Is_delete) values (@AccessKey,@AppSecret,@TemplateID,@RoomId,@EmailId,@Password100ms,@Status,@Template_Name,@Create_date,@Is_delete)";
                        cmd = new SqlCommand(query1);
                        cmd.Parameters.AddWithValue("@AccessKey", txt_accesskey.Text);
                        cmd.Parameters.AddWithValue("@AppSecret", txt_app_secretkey.Text);
                        cmd.Parameters.AddWithValue("@TemplateID", txt_Templates_id.Text);
                        cmd.Parameters.AddWithValue("@RoomId", txt_rommid.Text);
                        cmd.Parameters.AddWithValue("@EmailId", txt_emailid.Text);
                        cmd.Parameters.AddWithValue("@Password100ms", txt_Password.Text);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@Template_Name", txt_templates_name.Text);
                        cmd.Parameters.AddWithValue("@Create_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Is_delete", 1);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alert("Template has been add sucessfull");
                            empty_data();
                        }
                    }
                    else
                    {
                        Alert("This template is already already exists");
                    }

                }
                else
                {
                    string query = "Select * from LiveClassCredential where TemplateID='" + txt_Templates_id.Text + "' and ID!=" + hd_id.Value + "  ";
                    DataTable dt = code.FillTable(query);
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd;
                        string query1 = "Update LiveClassCredential set AccessKey=@AccessKey,AppSecret=@AppSecret,TemplateID=@TemplateID,RoomId=@RoomId,EmailId=@EmailId,Password100ms=@Password100ms,Status=@Status,Template_Name=@Template_Name,Is_delete=@Is_delete where Id = @Id";
                        cmd = new SqlCommand(query1);
                        cmd.Parameters.AddWithValue("@AccessKey", txt_accesskey.Text);
                        cmd.Parameters.AddWithValue("@AppSecret", txt_app_secretkey.Text);
                        cmd.Parameters.AddWithValue("@TemplateID", txt_Templates_id.Text);
                        cmd.Parameters.AddWithValue("@RoomId", txt_rommid.Text);
                        cmd.Parameters.AddWithValue("@EmailId", txt_emailid.Text);
                        cmd.Parameters.AddWithValue("@Password100ms", txt_Password.Text);
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@Template_Name", txt_templates_name.Text);
                        cmd.Parameters.AddWithValue("@Create_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Is_delete", 1);
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alert("Template has been update sucessfull");
                            empty_data();
                        }
                    }
                    else
                    {
                        Alert("This template is already already exists");

                    }

                }


            }
            catch
            {

            }
        }

        private void empty_data()
        {
            btn_submit.Text = "Add";
            txt_accesskey.Text = "";
            txt_app_secretkey.Text = "";
            txt_Templates_id.Text = "";
            txt_rommid.Text = "";
            txt_emailid.Text = "";
            txt_Password.Text = "";
            txt_templates_name.Text = "";
            search_data();


        }
    }
}