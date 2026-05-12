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
    public partial class Course_or_Subject_Master : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { if (!IsPostBack) { BindCourse(); BindGridView(); AddMultipleSUb(); };}
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void BindCourse()
        {
            code.bind_all_ddl_with_id(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster order by Position asc");
            code.bind_all_ddl_with_id(ddl_SearchCategory, "Select CategoryName, CategoryID from ClassMaster order by Position asc");
        }
        private void BindGridView()
        {
            code.BindRepeater("Select csm.*,cm.CategoryName,cm.CategoryID from Course_or_Subject_Master csm join ClassMaster cm on csm.CategoryID=cm.CategoryID order by cm.Position", RPDetails);
        }


        #region SubInfo
        private void AddMultipleSUb()
        {
            DataTable dt;
            dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("CourseName", typeof(string)));
            dr = dt.NewRow();
            dr["CourseName"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["SubInfo"] = dt;
            GrdSub.DataSource = ViewState["SubInfo"];
            GrdSub.DataBind();
        }
        protected void img_btnFam_add_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AddNewRowToGrid();
            }
            catch (Exception exc)
            {

            }
        }


        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["SubInfo"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["SubInfo"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox CourseName = (TextBox)GrdSub.Rows[rowIndex].Cells[0].FindControl("txt_Course");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["CourseName"] = CourseName.Text;
                        rowIndex++;


                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    GrdSub.DataSource = dtCurrentTable;
                    GrdSub.DataBind();
                    ViewState["SubInfo"] = dtCurrentTable;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["SubInfo"] != null)
            {
                DataTable dt = (DataTable)ViewState["SubInfo"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox CourseName = (TextBox)GrdSub.Rows[rowIndex].Cells[0].FindControl("txt_Course");
                        CourseName.Text = dt.Rows[i]["CourseName"].ToString();
                        rowIndex++;
                    }
                }

            }
        }
        #endregion

        private void send_Info()
        {
            try
            {
                int growcount = GrdSub.Rows.Count;
                int k = 0;
                if (growcount != 0)
                {
                    for (int i = 0; i < growcount; i++)
                    {
                        TextBox txt_Course = (TextBox)GrdSub.Rows[i].FindControl("txt_Course");
                        if (txt_Course.Text != "")
                        {
                            sendItToDB(txt_Course.Text);
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void sendItToDB(string Course)
        {
            string ImagePath = GetImagePath();
            if (code.IsExist("Select * from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and CourseName='" + Course + "'"))
            {
                SqlCommand cmd = new SqlCommand("insert into Course_or_Subject_Master (CategoryID, CourseID, CourseName,Description,Image,Istatus,Date,Idate) " +
                    "Values ('" + ddl_CourseCat.SelectedValue + "','" + code.Auto_generate_user_id("Select CourseID from Course_or_Subject_Master where CourseID=", 1000, 9999) + "'," +
                    "'" + Course + "','" + txt_info.InnerText.Replace("'", " ") + "','" + ImagePath + "','1','" + code.date() + "','" + code.idate() + "')");
                InsertUpdate.InsertUpdateData(cmd);
                BindGridView();
                Alert("successfully added.");
            }
            else { 
                //Alert("Duplicate Subject Name." + Course); 
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
                if (btn_submit.Text == "Submit")
                {
                    send_Info(); AddMultipleSUb();
                }
                if (btn_submit.Text == "Update")
                {
                    if (fl_Photo.HasFile) { Hd_Photo.Value = GetImagePath(); }
                    else { }
                    if (code.IsExist("Select CourseID from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and CourseName='" + txt_Course.Text + "' and Id!='" + Session["lbl_id"] + "'"))
                    {
                        SqlCommand cmd = new SqlCommand("Update Course_or_Subject_Master set CategoryID='" + ddl_CourseCat.SelectedValue + "', CourseName= '" + txt_Course.Text + "'," +
                            "Description='" + txt_info.InnerText.Replace("'", " ") + "', Image='" + Hd_Photo.Value + "'  where Id='" + Session["lbl_id"] + "'");
                        InsertUpdate.InsertUpdateData(cmd);
                        BindGridView();
                        btn_submit.Text = "Submit"; txt_Course.Visible = false; GrdSub.Visible = true;
                        Alert("Subject successfully Updated.");
                    }
                    else { Alert("Duplicate Subject Name."); }
                }
                txt_info.InnerText = "";

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public string GetImagePath()
        {
            string Path = code.UploadImage(fl_Photo, "/UploadedImage/CourseImage/");
            return Path;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btn_submit.Text = "Update"; txt_Course.Visible = true; GrdSub.Visible = false;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label CategoryName = (Label)row.FindControl("lbl_CourseName");
                Label Description = (Label)row.FindControl("lbl_Description");
                HiddenField CategoryID = (HiddenField)row.FindControl("hdCategoryID");
                Image Image = (Image)row.FindControl("Image1");
                Session["lbl_id"] = lbl_id.Text;
                code.bind_all_ddl_with_id(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster order by CategoryName");
                ddl_CourseCat.SelectedValue = CategoryID.Value;
                txt_Course.Text = CategoryName.Text;
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
                SqlCommand cmd = new SqlCommand("Delete from Course_or_Subject_Master where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                BindGridView();
                Alert("successfully deleted.");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            try
            {
                code.BindRepeater("Select csm.*,cm.CategoryName,cm.CategoryID from Course_or_Subject_Master csm join ClassMaster cm on csm.CategoryID=cm.CategoryID  where cm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' order by cm.Position  ", RPDetails);
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
    }
}