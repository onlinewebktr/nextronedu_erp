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

namespace school_web.Examination_Admin
{
    public partial class admit_card_sign_setting : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    ViewState["sign_image"] = "";
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    string pagename_current = "inventory-items.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    bind_grd_view();
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

        private void bind_grd_view()
        {
            string qry = "select * from Admit_card_sign_setting order by Position asc";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_sign_name.Text == "")
                {
                    txt_sign_name.Focus();
                    Alertme("Please enter signature name.", "warning");
                }
                else if (ddl_position.SelectedItem.Text == "Select")
                {
                    ddl_position.Focus();
                    Alertme("Please select signature position.", "warning");
                }
                else
                {
                    save_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_data()
        {
             
            if (btn_Submit.Text == "Add")
            {
                SqlCommand cmd;
                string query = "INSERT INTO Admit_card_sign_setting (Sign_name,Position,Sign_path,Status,Updated_by,Updated_date) values (@Sign_name,@Position,@Sign_path,@Status,@Updated_by,@Updated_date)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Sign_name", txt_sign_name.Text);
                cmd.Parameters.AddWithValue("@Position", ddl_position.SelectedValue);
                cmd.Parameters.AddWithValue("@Sign_path", ViewState["sign_image"].ToString());
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been added successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "update Admit_card_sign_setting set Sign_name=@Sign_name,Position=@Position,Sign_path=@Sign_path,Status=@Status,Updated_by=@Updated_by,Updated_date=@Updated_date where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Sign_name", txt_sign_name.Text);
                cmd.Parameters.AddWithValue("@Position", ddl_position.SelectedValue);
                cmd.Parameters.AddWithValue("@Sign_path", ViewState["sign_image"].ToString());
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been updated successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }
        private void empty_form()
        {
            img_sign_image.Visible = false;
            txt_sign_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_sign_name = (Label)row.FindControl("lbl_sign_name");
                Label lbl_position = (Label)row.FindControl("lbl_position");
                Label lbl_sign_path = (Label)row.FindControl("lbl_sign_path");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;
                img_sign_image.Visible = true;

                txt_sign_name.Text = lbl_sign_name.Text;
                ddl_position.SelectedValue = lbl_position.Text;

                ViewState["sign_image"] = lbl_sign_path.Text;
                img_sign_image.ImageUrl = lbl_sign_path.Text;
                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            My.exeSql("delete from Admit_card_sign_setting where Id='" + lbl_Id.Text + "'");
            Alertme("Record has been deleted successfully.", "success");
            bind_grd_view();
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        } 


        protected void btn_upload_sign_image_Click(object sender, EventArgs e)
        { 
            string filepath1 = "";
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    filepath1 = upload_image(FileUpload1, "signature_img");
                    if (filepath1 == "")
                    {
                        Alertme("Please upload valid signature image.", "warning");
                        FileUpload1.Focus();
                        return;
                    }
                    else
                    {
                        img_sign_image.Visible = true;
                        ViewState["sign_image"] = filepath1;
                        img_sign_image.ImageUrl = filepath1;
                    }
                }
                else
                {
                    Alertme("Please Reduce or compress size of signature image max(300kb)", "warning");
                    FileUpload1.Focus();
                }
            }
            else
            {
                Alertme("Please upload valid signature image.", "warning");
            }
        }


        #region UploaD
        private string upload_image(FileUpload Files, string name)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = name + date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = Files.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".JPEG" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;
                    break;
                }
            }


            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("~/Master_Img/doc/")).ToString();
                    Files.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alertme("File has not save.", "warning");
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/Master_Img/doc/" + fileName;
            }
            return dbfilePath;
        }
        #endregion
    }
}