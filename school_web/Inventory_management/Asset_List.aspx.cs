using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Asset_List : System.Web.UI.Page
    {
        My mycode = new My();
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
            if (panel == "Warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {


                if (!IsPostBack)
                {
                    try
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Session["list"] = "3";
                        ViewState["Add_recode_his"] = "0";
                        bind_Asset_list();
                        Bind_ddl_attributename();
                        Bind_attribute_master();

                    }
                    catch (Exception ex)
                    {
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_Asset_list()
        {
            lnk_excel_download.Visible = false;
            print1.Visible = false;
            SqlCommand cmd = new SqlCommand("sp_asset_list");
            cmd.Parameters.AddWithValue("@sp_status ", "fetch_asset");
            DataSet ds = Store_procedure_code.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {


                GrdViewlist.DataSource = null;
                GrdViewlist.DataBind();
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                Alertme("Sorry! There are no any asset list here", "Warning");
            }
            else
            {
                lnk_excel_download.Visible = true;
                print1.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                GrdViewlist.DataSource = dt;
                GrdViewlist.DataBind();

            }

        }

        protected void lnk_excel_download_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=AssetList_" + mycode.date() + "_" + mycode.time() + ".xls");

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GrdViewlist.RenderControl(hw);
                    //string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    //Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void lnk_Update_Asset_Attribute_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                ViewState["unique_entry_id"] = lbl_unique_entry_id.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {

            }

        }

        protected void lnk_view_Attribute_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                Bind_Asset_Attribute_details(lbl_unique_entry_id.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
            catch
            {

            }
        }

        private void Bind_Asset_Attribute_details(string unique_entry_id)
        {
            DataTable cdt = My.dataTable(" select   ad.*,am.Attribute_Name from Asset_Attribute_details ad join  Asset_Attribute_Master am on ad.Attribute_id=am.Attribute_id where ad.unique_entry_id='" + unique_entry_id + "' order by am.Attribute_Name,ad.id desc");

            if (cdt.Rows.Count == 0)
            {
                rep_Attribute_his.DataSource = null;
                rep_Attribute_his.DataBind();
            }
            else
            {
                rep_Attribute_his.DataSource = cdt;
                rep_Attribute_his.DataBind();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (ddl_attributename.SelectedItem.Text == "Select")
            {
                Alertme("Please select attribute name", "Warning");
            }
            else
            {

                if (btn_add.Text == "Update")
                {

                    string query = "Update Asset_Attribute_details set Attribute_id=@Attribute_id,Attribute_no=@Attribute_no,Attribute_valid_to_date=@Attribute_valid_to_date,Attribute_valid_to_idate=@Attribute_valid_to_idate,Attribute_Reminder_Date=@Attribute_Reminder_Date,Attribute_Reminder_IDate=@Attribute_Reminder_IDate,Updated_by=@Updated_by,Updated_date_time=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE()))  where Attribute_entry_id = @Attribute_entry_id";
                    SqlCommand cmd;
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Attribute_id", ddl_attributename.SelectedValue);
                    cmd.Parameters.AddWithValue("@Attribute_no", txt_attributeno.Text.Trim());
                    cmd.Parameters.AddWithValue("@Attribute_valid_to_date", txt_Attribute_valid_date.Text);
                    cmd.Parameters.AddWithValue("@Attribute_valid_to_idate", My.DateConvertToIdate(txt_Attribute_valid_date.Text));
                    cmd.Parameters.AddWithValue("@Attribute_Reminder_Date", txt_attributeReminder.Text);
                    cmd.Parameters.AddWithValue("@Attribute_Reminder_IDate", My.DateConvertToIdate(txt_attributeReminder.Text));
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Attribute_entry_id", ViewState["Attribute_entry_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                       
                        if (FileUpload1.HasFile)
                        {
                            upload_imagesss(ViewState["Attribute_entry_id"].ToString());
                        }
                        Bind_Asset_Attribute_details(ViewState["unique_entry_id"].ToString());
                        Alertme("Record has been successfully updated", "success");
                        ddl_attributename.SelectedValue = "0";
                        txt_attributeno.Text = "";
                        txt_attributeReminder.Text = "";

                    }
                }
                else
                {
                    string Attribute_entry_id = "0";
                    //DataTable dt = mycode.FillData("Select top 1 * from Asset_Attribute_details where Attribute_id='" + ddl_attributename.SelectedValue + "'");
                    //if (dt.Rows.Count == 0)
                    //{
                    Attribute_entry_id = create_Attribute_entry_id();
                    string query = "INSERT INTO Asset_Attribute_details (Attribute_id,Attribute_no,Attribute_valid_to_date,Attribute_valid_to_idate,Attribute_Reminder_Date,Attribute_Reminder_IDate,Created_by,Created_date_time,unique_entry_id,Attribute_entry_id,Status) values (@Attribute_id,@Attribute_no,@Attribute_valid_to_date,@Attribute_valid_to_idate,@Attribute_Reminder_Date,@Attribute_Reminder_IDate,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@unique_entry_id,@Attribute_entry_id,@Status)";
                    SqlCommand cmd;
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Attribute_id", ddl_attributename.SelectedValue);
                    cmd.Parameters.AddWithValue("@Attribute_no", txt_attributeno.Text.Trim());
                    cmd.Parameters.AddWithValue("@Attribute_valid_to_date", txt_Attribute_valid_date.Text);
                    cmd.Parameters.AddWithValue("@Attribute_valid_to_idate", My.DateConvertToIdate(txt_Attribute_valid_date.Text));
                    cmd.Parameters.AddWithValue("@Attribute_Reminder_Date", txt_attributeReminder.Text);
                    cmd.Parameters.AddWithValue("@Attribute_Reminder_IDate", My.DateConvertToIdate(txt_attributeReminder.Text));
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@unique_entry_id", ViewState["unique_entry_id"].ToString());
                    cmd.Parameters.AddWithValue("@Attribute_entry_id", Attribute_entry_id);
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Record has been successfully Added", "success");
                     


                        if (FileUpload1.HasFile)
                        {
                            upload_imagesss(Attribute_entry_id);
                        }
                        try
                        {
                            if (ViewState["Add_recode_his"].ToString() == "1")
                            {
                                My.exeSql("update Asset_Attribute_details set Status='Inactive' where Attribute_id='" + ddl_attributename.SelectedValue + "' and Attribute_entry_id!='" + Attribute_entry_id + "'");
                                Bind_Asset_Attribute_details(ViewState["unique_entry_id"].ToString());
                            }
                        }
                        catch
                        {

                        }
                        ddl_attributename.SelectedValue = "0";
                        txt_attributeno.Text = "";
                        txt_attributeReminder.Text = "";
                    }
                    //}
                    //else
                    //{
                    //    Alertme("Your selected attribute already added", "Warning");
                    //}
                }
            }


            if (ViewState["Add_recode_his"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            btn_add.Text = "Add";
            ViewState["Add_recode_his"] = "0";
            ViewState["unique_entry_id"] = "0";
            ViewState["Attribute_entry_id"] = "0";
            lbl_head.Text = "Update Asset Attribute";
        }

        private void upload_imagesss(string attribute_entry_id)
        {
            foreach (var file in FileUpload1.PostedFiles)
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                string dbfilepath = "";
                Boolean FileOK = false;
                Boolean FileSaved = false;
                int k = 0;

                string value = Guid.NewGuid().ToString();
                Session["WorkingImage"] = Path.GetFileName(file.FileName);
                string extension = new FileInfo(file.FileName).Extension;
                Session["WorkingImage1"] = attribute_entry_id + idate + time + extension;

                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    file.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                }

                if (FileSaved)
                {
                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                    string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                    dbfilepath = originalPath1 + "/Master_Img/Student/" + fileName;
                }


                try
                {

                    SqlCommand cmd;
                    string query = "INSERT INTO Asset_Attribute_attachment (Attribute_entry_id,attachment_path) values (@Attribute_entry_id,@attachment_path)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Attribute_entry_id", attribute_entry_id);
                    cmd.Parameters.AddWithValue("@attachment_path", dbfilepath);

                    if (My.InsertUpdateData(cmd))
                    {
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }

        private string create_Attribute_entry_id()
        {
            bool duplicate = true;
            string Attribute_entry_id = My.auto_serialS("Attribute_entry_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Attribute_entry_id from dbo.[Asset_Attribute_details] where Attribute_entry_id='" + Attribute_entry_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Attribute_entry_id = My.auto_serialS("Attribute_entry_id");
                }
            }
            return Attribute_entry_id;
        }


        #region add/ edit  attribute master
        protected void btn_attribute_name_Click(object sender, EventArgs e)
        {
            if (txt_attributename_add.Text == "")
            {
                Alertme("Please enter attribute name", "Warning");
            }
            else
            {
                if (btn_attribute_name.Text == "Add")
                {
                    DataTable dt = mycode.FillData("Select top 1 * from Asset_Attribute_Master where Attribute_Name='" + txt_attributename_add.Text + "'");
                    if (dt.Rows.Count == 0)
                    {
                        string Attribute_id = create_sl_no();
                        string query = "INSERT INTO Asset_Attribute_Master (Attribute_Name,Created_by,Created_date_time,Attribute_id) values (@Attribute_Name,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Attribute_id)";
                        SqlCommand cmd;
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Attribute_Name", txt_attributename_add.Text.Trim());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Attribute_id", Attribute_id);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Record has been saved successfully", "success");
                            Bind_attribute_master();
                            txt_attributename_add.Text = "";
                            Bind_ddl_attributename();
                            btn_attribute_name.Text = "Add";
                        }
                    }
                    else
                    {
                        Alertme("Your entered attribute name already exists.", "Warning");
                    }
                }
                else
                {
                    DataTable dt = mycode.FillData("Select * from Asset_Attribute_Master where Attribute_Name='" + txt_attributename_add.Text + "'  and Id!='" + ViewState["id"].ToString() + "'  ");
                    if (dt.Rows.Count == 0)
                    {

                        string query = "Update Asset_Attribute_Master set Attribute_Name=@Attribute_Name,  Updated_by=@Updated_by,Updated_date_time=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())) where Id = @Id";
                        SqlCommand cmd;
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Attribute_Name", txt_attributename_add.Text.Trim());
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Record has been updated successfully", "success");
                            Bind_attribute_master();
                            txt_attributename_add.Text = "";
                            Bind_ddl_attributename();
                            btn_attribute_name.Text = "Add";
                        }
                    }
                    else
                    {
                        Alertme("Your entered attribute name already exists.", "Warning");
                    }

                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal();", true);

        }

        private void Bind_ddl_attributename()
        {
            mycode.bind_all_ddl_with_id(ddl_attributename, "Select Attribute_Name,Attribute_id from Asset_Attribute_Master order by Attribute_Name");
        }

        private void Bind_attribute_master()
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Asset_Attribute_Master]  order by Attribute_Name");

            if (cdt.Rows.Count == 0)
            {
                rd_view_Attribute_master.DataSource = null;
                rd_view_Attribute_master.DataBind();
            }
            else
            {
                rd_view_Attribute_master.DataSource = cdt;
                rd_view_Attribute_master.DataBind();
            }
        }

        private string create_sl_no()
        {
            bool duplicate = true;
            string Attribute_id = My.auto_serialS("Attribute_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Attribute_id from dbo.[Asset_Attribute_Master] where Attribute_id='" + Attribute_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Attribute_id = My.auto_serialS("Attribute_id");
                }
            }
            return Attribute_id;
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_name = (Label)row.FindControl("lbl_name");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                txt_attributename_add.Text = lbl_name.Text;
                ViewState["id"] = lbl_Id.Text;
                btn_attribute_name.Text = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal();", true);
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Attribute_id = (Label)row.FindControl("lbl_Attribute_id");

                bool chk_Attribute = Find_Attribute(lbl_Attribute_id.Text);
                if (chk_Attribute == false)
                {
                    Alertme("The selected attribute name is used in asset attribute details, so it cannot be deleted", "Warning");
                }
                else
                {
                    My.exeSql("delete from Asset_Attribute_Master where Id=" + lbl_Id.Text + "");
                    btn_attribute_name.Text = "Add";
                    Bind_attribute_master();
                    Bind_ddl_attributename();
                    Alertme("Deleteiation process han been sucessfully done", "success");

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal();", true);
               
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        private bool Find_Attribute(string Attribute_id)
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Asset_Attribute_details]  where Attribute_id='" + Attribute_id + "'");

            if (cdt.Rows.Count == 0)
            {
                return true;

            }
            else
            {
                return false;

            }
        }

        #endregion

        protected void rep_Attribute_his_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DataRowView rowView = (DataRowView)e.Item.DataItem;
                DataList DataList1 = ((DataList)e.Item.FindControl("DataList1"))
                    as DataList;
                Label lbl_Attribute_entry_id = ((Label)e.Item.FindControl("lbl_Attribute_entry_id")) as Label;

                Label lbl_Attribute_valid_to_idate = ((Label)e.Item.FindControl("lbl_Attribute_valid_to_idate")) as Label;

                Label lbl_status = ((Label)e.Item.FindControl("lbl_status")) as Label;

                if (My.toint(lbl_Attribute_valid_to_idate.Text) >= My.toint(mycode.idate()))
                {
                    lbl_status.Text = "Active";
                }
                else
                {
                    lbl_status.Text = "Expired";
                }
                fetch_document(lbl_Attribute_entry_id.Text, DataList1);

            }
        }

        private void fetch_document(string Attribute_entry_id, DataList dataList1)
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Asset_Attribute_attachment]  where Attribute_entry_id='" + Attribute_entry_id + "'");

            if (cdt.Rows.Count == 0)
            {
                dataList1.DataSource = null;
                dataList1.DataBind();

            }
            else
            {


                dataList1.DataSource = cdt;
                dataList1.DataBind();

            }
        }

        #region add and update asset atribute
        protected void Btn_Add_Asset_attribute_Click(object sender, EventArgs e)
        {
            try
            {
                Button lnk = (Button)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Attribute_id = (Label)row.FindControl("lbl_Attribute_id");
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                ViewState["unique_entry_id"] = lbl_unique_entry_id.Text;
                ViewState["Add_recode_his"] = "1";
                ddl_attributename.Text = lbl_Attribute_id.Text;
                lbl_head.Text = "Add Asset Attribute";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal2();", true);
                
                
            }
            catch
            {
            }
        }
        protected void btn_Update_Asset_Aattribute_Click(object sender, EventArgs e)
        {
            try
            {
                Button lnk = (Button)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Attribute_id = (Label)row.FindControl("lbl_Attribute_id");
                Label lbl_unique_entry_id = (Label)row.FindControl("lbl_unique_entry_id");
                Label lbl_Attribute_entry_id = (Label)row.FindControl("lbl_Attribute_entry_id");
                Label lbl_Attribute_no = (Label)row.FindControl("lbl_Attribute_no");
                Label lbl_Attribute_valid_to_date = (Label)row.FindControl("lbl_Attribute_valid_to_date");
                Label lbl_Attribute_Reminder_Date = (Label)row.FindControl("lbl_Attribute_Reminder_Date");
                txt_attributeno.Text = lbl_Attribute_no.Text;
                txt_Attribute_valid_date.Text = lbl_Attribute_valid_to_date.Text;
                txt_attributeReminder.Text = lbl_Attribute_Reminder_Date.Text;
                lbl_head.Text = "Update Asset Attribute";

                ViewState["unique_entry_id"] = lbl_unique_entry_id.Text;
                ViewState["Attribute_entry_id"] = lbl_Attribute_entry_id.Text;
                ViewState["Id"] = lbl_unique_entry_id.Text;
                ViewState["Add_recode_his"] = "1";
                btn_add.Text = "Update";
                ddl_attributename.Text = lbl_Attribute_id.Text;
               
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop2", "openModal2();", true);
            }
            catch
            {
            }
        }
        #endregion
    }
}