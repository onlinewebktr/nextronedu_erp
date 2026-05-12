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
    public partial class ClassMaster : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { if (!IsPostBack) { BindGridView(); };}
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindGridView()
        {
            code.BindRepeater("select * from ClassMaster order by Position asc", RPDetails);
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
                if (txt_Category.Text == "")
                {
                    lbl_msg.Text = "Please Insert Class Name.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false); txt_Category.Text = ""; return;
                }
                else
                {
                    if (btn_submit.Text == "Submit")
                    {
                        string ImagePath = GetImagePath();
                        if (code.IsExist("Select * from ClassMaster where CategoryName='" + txt_Category.Text + "'"))
                        {
                            string id=code.Auto_generate_user_id("Select CategoryID from ClassMaster where CategoryID=", 1000, 9999);
                            string sql = "insert into ClassMaster (CategoryID, CategoryName,Description,Image,Istatus,Date,Idate) " +
                                "Values ('" + id + "'," +
                                "'" + txt_Category.Text + "','" + txt_info.InnerText.Replace("'", " ") + "','" + ImagePath + "','1','" + code.date() + "','" + code.idate() + "');";

                            SqlCommand cmd = new SqlCommand(sql);
                            InsertUpdate.InsertUpdateData(cmd);

                            try
                            {
                                string sql1 = "insert into ClassMaster (CategoryID, CategoryName,Description,Image,Istatus,Date,Idate) " +
                                    "Values ('" + id + "'," +
                                    "'" + txt_Category.Text + "','" + txt_info.InnerText.Replace("'", " ") + "','" + ImagePath + "','1','" + code.date() + "','" + code.idate() + "');";
                                SqlCommand cmd1 = new SqlCommand(sql1);
                                InsertUpdate.InsertUpdateData_onlinetest(cmd);
                            }
                            catch
                            {
                            }
                            BindGridView();
                            Alert("Class successfully added.");
                        }
                        else { Alert("Duplicate Class Name."); }
                    }
                    if (btn_submit.Text == "Update")
                    {
                        if (fl_Photo.HasFile) { Hd_Photo.Value = GetImagePath(); }
                        else { }
                        if (code.IsExist("Select CategoryID from ClassMaster where CategoryName='" + txt_Category.Text + "' and Id!='" + Session["lbl_id"] + "'"))
                        {
                            SqlCommand cmd = new SqlCommand("Update ClassMaster set  CategoryName= '" + txt_Category.Text + "'," +
                                "Description='" + txt_info.InnerText.Replace("'", " ") + "', Image='" + Hd_Photo.Value + "'  where Id='" + Session["lbl_id"] + "'");
                            InsertUpdate.InsertUpdateData(cmd);

                            SqlCommand cmd1 = new SqlCommand("Update ClassMaster set  CategoryName= '" + txt_Category.Text + "'," +
                               "Description='" + txt_info.InnerText.Replace("'", " ") + "', Image='" + Hd_Photo.Value + "'  where Id='" + Session["lbl_id"] + "'");
                            InsertUpdate.InsertUpdateData_onlinetest(cmd1);

                            BindGridView();
                            btn_submit.Text = "Submit";
                            Alert("Class successfully Updated.");
                        }
                        else { Alert("Duplicate Class Name."); }
                    }
                    txt_Category.Text = "";
                    txt_info.InnerText = "";
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void LinkBtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_id = (Label)row.FindControl("lbl_Id");
            Label lbl_Description = (Label)row.FindControl("lbl_Description");
            ltDescription.Text = lbl_Description.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btn_submit.Text = "Update";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label CategoryName = (Label)row.FindControl("lbl_CategoryName");
                Label Description = (Label)row.FindControl("lbl_Description");
                Image Image = (Image)row.FindControl("Image1");
                Session["lbl_id"] = lbl_id.Text;
                txt_Category.Text = CategoryName.Text;
                txt_info.InnerText = Description.Text;
                Hd_Photo.Value = Image.ImageUrl;
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from ClassMaster where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);

                SqlCommand cmd1 = new SqlCommand("Delete from ClassMaster where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData_onlinetest(cmd1);

                BindGridView();
                Alert("Class deleted successfully.");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        public string GetImagePath()
        {
            string Path = code.UploadImage(fl_Photo, "/UploadedImage/CategoryImage/");
            return Path;
        }

    }
}