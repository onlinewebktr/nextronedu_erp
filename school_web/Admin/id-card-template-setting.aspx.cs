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

namespace school_web.Admin
{
    public partial class id_card_template_setting : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        if (Session["MsG"] != null)
                        {
                            Alertme(Session["MsG"].ToString(), "success");
                            Session["MsG"] = null;
                        }
                        chk_is_upload_template.Checked = true;
                        templateFile.Visible = true;
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        get_template();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "IdCardTemplate");
            }
        }

        private void get_template()
        {
            DataTable dt = mycode.FillData("select * from Id_card_template_setting where Branch_id='" + ViewState["Branchid"].ToString() + "' and Type='Student'");
            if (dt.Rows.Count > 0)
            {
                grddv.Visible = true;
                lbl_type.Text = dt.Rows[0]["Id_card_type"].ToString();
                if (dt.Rows[0]["Is_use_template"].ToString() == "True")
                {
                    lbl_is_templte_use.Text = "Yes";
                    Image1.ImageUrl = dt.Rows[0]["Template_filepath"].ToString();
                    imgTemp.Visible = true;
                }
                else
                {
                    imgTemp.Visible = false;
                    lbl_is_templte_use.Text = "No";
                }
            }
            else
            {
                grddv.Visible = false;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["IsDone"] = "0";
                if (ddl_id_type.SelectedItem.Text == "Select")
                {
                    Alertme("Please select id card type.", "warning");
                    ddl_id_type.Focus();
                }
                else
                {
                    if (chk_is_upload_template.Checked == true)
                    {
                        string filepath = "#";
                        if (FileUpload1.PostedFile.ContentLength > 0)
                        {
                            filepath = upload_template();
                            if (filepath == "")
                            {
                                Alertme("Max size 400 kb.", "warning");
                            }
                            else
                            {
                                final_submit(filepath);
                                if (ViewState["IsDone"].ToString() == "1")
                                {
                                    Session["MsG"] = "Template setting has been updated successfully.";
                                    Response.Redirect("id-card-template-setting.aspx", false);
                                }
                            }
                        }
                        else
                        {
                            Alertme("Please choose valid Image", "warning");
                        }
                    }
                    else
                    {
                        final_submit("0");
                        if (ViewState["IsDone"].ToString() == "1")
                        {
                            Session["MsG"] = "Template setting has been updated successfully.";
                            Response.Redirect("id-card-template-setting.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        My mycode = new My();
        private void final_submit(string filepath)
        {
            if (mycode.IsUserExist("select Id from Id_card_template_setting where Branch_id='" + ViewState["Branchid"].ToString() + "'  and Type='Student'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Id_card_template_setting (Id_card_type,Is_use_template,Template_filepath,Branch_id,Type) values (@Id_card_type,@Is_use_template,@Template_filepath,@Branch_id,@Type)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id_card_type", ddl_id_type.SelectedItem.Text);
                if (filepath == "0")
                {
                    cmd.Parameters.AddWithValue("@Is_use_template", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_use_template", 1);
                }

                cmd.Parameters.AddWithValue("@Template_filepath", filepath);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                cmd.Parameters.AddWithValue("@Type", "Student");
                if (My.InsertUpdateData(cmd))
                {
                    ViewState["IsDone"] = "1";
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Id_card_template_setting set Id_card_type=@Id_card_type,Is_use_template=@Is_use_template,Template_filepath=@Template_filepath where Branch_id='" + ViewState["Branchid"].ToString() + "' and Type='Student'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id_card_type", ddl_id_type.SelectedItem.Text);
                if (filepath == "0")
                {
                    cmd.Parameters.AddWithValue("@Is_use_template", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_use_template", 1);
                }

                cmd.Parameters.AddWithValue("@Template_filepath", filepath);
                if (My.InsertUpdateData(cmd))
                {
                    ViewState["IsDone"] = "1";
                }
            }
        }



        protected void chk_is_upload_template_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_is_upload_template.Checked == true)
                {
                    templateFile.Visible = true;
                }
                else
                {
                    templateFile.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }


        private string upload_template()
        {
            string filepath = "";
            if (FileUpload1.PostedFile.ContentLength <= 400000)
            {
                string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dt.ToString("dd_MM_yyyy");
                string time = dt.ToString("hh_mm_ss");
                String FileName1 = date + time + extension;
                string originalPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                HttpPostedFile postedf = FileUpload1.PostedFile;
                postedf.SaveAs(MapPath("~/UploadedImage/IdTemplate/" + FileName1));
                filepath = originalPath + "/UploadedImage/IdTemplate/" + Path.GetFileName(FileName1);
            }
            else
            {
            }
            return filepath;
        }
    }
}