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
    public partial class Online_Terms_Condition : System.Web.UI.Page
    {
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
                        try
                        {
                            const string quote = "\"";
                            string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                            if (tinyMC != "")
                            {
                                lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                            }
                            else
                            {
                                lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        ViewState["attechment"] = "0";
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Online_Terms_Condition.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session");
                        Bind_classess();
                        ddl_session.SelectedValue = My.get_session_id_onlinereg();
                        mycode.bind_all_ddl_with_id(ddl_class, "select Course_Name,course_id from Add_course_table order by Position asc");
                        //fetch_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Online_Terms_Condition");
            }
        }

        private void Bind_classess()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
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
        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
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



        My mycode = new My();
        UsesCode usesmycode = new UsesCode();
        private void fetch_data()
        {
            DataTable dt = usesmycode.FillTable("select * from dbo.[Online_terms_condition] where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "'");
            if (dt.Rows.Count != 0)
            {
                
                ViewState["courseID"] = ddl_class.SelectedValue;
                txt_info.Value = dt.Rows[0]["Terms"].ToString();
                ViewState["attechment"] = dt.Rows[0]["attechment"].ToString();
                hd_id.Value = dt.Rows[0]["Id"].ToString();
                btn_add.Text = "Update";

                if (dt.Rows[0]["attechment"].ToString() == "")
                {
                    ViewState["attechment"] = "";
                    file1.Visible = false;

                }
                else if (dt.Rows[0]["attechment"].ToString() == "0")
                {
                    ViewState["attechment"] = "";
                    file1.Visible = false;

                }
                else
                {
                    ViewState["attechment"] = dt.Rows[0]["attechment"].ToString();
                    file1.Visible = true;
                    file1.HRef = dt.Rows[0]["attechment"].ToString();
                }
            }
            else
            {
                ViewState["courseID"] = "0";
                txt_info.Value = "";
                btn_add.Text = "Add";
                hd_id.Value = "";
                file1.Visible = false;
            }
            Bind_classess();
        }



        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Is_add"].ToString() == "1")
                {
                    string filepath = "";
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileBytes.Length < 500000)
                        {
                            filepath = upload_thumb_attachment();
                            if (filepath == "")
                            {
                                Alertme("Please choose valid image.", "warning");
                                return;
                            }

                            update_images(filepath);
                        }
                        else
                        {
                            Alertme("Please Reduce or compress size of image max(500 kb).", "warning");
                        }
                    }
                    else
                    {
                        filepath = ViewState["attechment"].ToString();
                        update_images(filepath);
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    string filepath = "";
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileBytes.Length < 500000)
                        {
                            filepath = upload_thumb_attachment();
                            if (filepath == "")
                            {
                                Alertme("Please choose valid image.", "warning");
                                return;
                            } 
                            update_images(filepath);
                        }
                        else
                        {
                            Alertme("Please Reduce or compress size of image max(500 kb).", "warning");
                        }
                    }
                    else
                    {
                        filepath = ViewState["attechment"].ToString();
                        update_images(filepath);
                    } 
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {
            }
        }

        private string upload_thumb_attachment()
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    Session["WorkingImage"] = FileUpload1.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
                    string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".pdf" };
                    for (int i = 0; i < allowedExtension.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtension[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }
                else
                {
                    Alertme("Please image size maximum 500kb.", "warning");
                }
            }
            else
            {
                Alertme("Please upload image first.", "warning");
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../UploadedImage/schoollogo")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["renamedfile"]);
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
                dbfilePath = originalPath1 + "/UploadedImage/schoollogo/" + fileName;
            }
            return dbfilePath;
        }

        private void update_images(string filepath)
        {

            try
            {
                ViewState["isSuccess"] = "0";
                save_guidelines(filepath);
                if (ViewState["isSuccess"].ToString() == "1")
                {
                    ViewState["courseID"] = "0";
                    txt_info.Value = "";
                    btn_add.Text = "Add";
                    hd_id.Value = "";
                    file1.Visible = false;
                    Bind_classess();

                    Alertme("Registration Guideline has been successfully updated.", "success");
                    // fetch_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_guidelines(string filepath)
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int ix = 0; ix < growcount; ix++)
            {
                CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                if (chk.Checked == true)
                {
                    Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");

                    DataTable dtD = My.dataTable("select * from Online_terms_condition where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + lbl_class_id.Text + "'");
                    if (dtD.Rows.Count > 0)
                    {
                        SqlCommand cmd;
                        string query = "Update Online_terms_condition set Terms=@Terms,date=@date,attechment=@attechment,User_id=@User_id where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + lbl_class_id.Text + "'";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Terms", txt_info.Value);
                        cmd.Parameters.AddWithValue("@date", mycode.date());
                        cmd.Parameters.AddWithValue("@attechment", filepath);
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            ViewState["isSuccess"] = "1";
                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Online_terms_condition (Terms,attechment,date,User_id,Session_id,Class_id) values (@Terms,@attechment,@date,@User_id,@Session_id,@Class_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Terms", txt_info.Value);
                        cmd.Parameters.AddWithValue("@date", mycode.date());
                        cmd.Parameters.AddWithValue("@attechment", filepath);
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", lbl_class_id.Text);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            ViewState["isSuccess"] = "1";
                        }
                    }
                }
                else
                {
                    k++;
                }
            }


            if (k == growcount)
            {
                Alertme("Please check minimum one class.", "warning");
                return;
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session.Focus();
                    return;
                }
                fetch_data();
            }
            catch (Exception ex)
            {
            }
        }
    }
}