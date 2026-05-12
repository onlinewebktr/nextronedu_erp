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
    public partial class Create_Certificate_Master : System.Web.UI.Page
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
        My mycode = new My();
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = Path.GetFileName("Create-Certificate-Master.aspx");
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        DataTable dt = mycode.FillData("select distinct  User_Type,User_Type as User_name   from user_details where    Istatus='1' and  User_Type not in('Admin','visitor') order by User_Type asc");
                        mycode.PopulateDropDown(ddl_UserName, dt, "User_Type", "User_Type");

                        get_template();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Create_Certificate_Master");
            }
        }

        private void get_template()
        {
            DataTable dt = mycode.FillData("Select * from Dashboard_report_menu where  Group_id='19' order by Position ");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no records exist", "warning");
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
                if (btn_Submit.Text == "Save")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {

                        if (txt_Certificate_name.Text == "")
                        {
                            Alertme("Please enter Certificate name", "warning"); 
                        } 
                        else
                        {
                            if (FileUpload1.PostedFile.ContentLength > 0)
                            {
                                string filepath = upload_template();
                                if (filepath == "")
                                {
                                    Alertme("Max size 500 kb.", "warning");
                                }
                                else
                                {
                                    final_submit(filepath); 
                                }
                            }
                            else
                            {
                                Alertme("Please choose valid signature.", "warning");
                            }
                        }
                    } 
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
                else
                {
                    bool send = true;
                    if (ViewState["Is_Edit"].ToString() == "1")
                    { 
                        if (txt_Certificate_name.Text == "")
                        {
                            Alertme("Please enter Certificate name", "warning"); 
                        }
                        else
                        {
                            string filepath = "";
                            if (FileUpload1.HasFile)
                            {
                                filepath = upload_template();
                                if (filepath == "")
                                {
                                    Alertme("Max size 500 kb.", "warning");
                                    send = false;
                                }
                                else
                                { 
                                    final_update(filepath);
                                    send = false;
                                }
                            }
                            if (send == true)
                            {
                                final_update(filepath);
                            } 
                        }
                    } 
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    } 
                } 
            }
            catch (Exception ex)
            {
            } 
        }

        private void final_submit(string filepath)
        {
            int lastposition = get_last_position();
            DataTable dt = mycode.FillData("select  * from Dashboard_report_menu where Group_id='19' and Menu_name='" + txt_Certificate_name.Text + "' ");
            if (dt.Rows.Count == 0)
            {
                string Menu_id = My.create_random_no();
                SqlCommand cmd;
                string query = "INSERT INTO Dashboard_report_menu (Group_id,Menu_name,Menu_icon,Status,Menu_page,Position,Created_by,Created_Date,Menu_id) values (@Group_id,@Menu_name,@Menu_icon,@Status,@Menu_page,@Position,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Menu_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Group_id", "19");
                cmd.Parameters.AddWithValue("@Menu_name", txt_Certificate_name.Text);
                cmd.Parameters.AddWithValue("@Menu_icon", filepath);
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@Menu_page", "");
                cmd.Parameters.AddWithValue("@Position", lastposition);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Menu_id", Menu_id);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Certificate name has been successfully added.", "success");

                    txt_Certificate_name.Text = "";
                    btn_Submit.Text = "Save";
                    btn_cancel.Visible = false;
                    get_template();
                }
            }
            else
            {
                Alertme("Your entered Certificate name already exists.", "warning");

            }

        }

        private int get_last_position()
        {
            DataTable dt = mycode.FillData("select  top 1  Position from Dashboard_report_menu where Group_id='19' order by Position desc ");
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return My.toint(dt.Rows[0]["Position"].ToString()) + 1;
            }
        }

        private void final_update(string filepath)
        {
            if (filepath == "")
            {

                filepath = ViewState["Menu_icon"].ToString();
            }
            DataTable dt = mycode.FillData("select  * from Dashboard_report_menu where Group_id='19' and Menu_name='" + txt_Certificate_name.Text + "' and Id!=" + ViewState["Id"].ToString() + " ");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "update Dashboard_report_menu set Menu_name=@Menu_name,Menu_icon=@Menu_icon,Created_by=@Created_by,Created_Date=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())) where Id=@Id ";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Menu_name", txt_Certificate_name.Text);
                cmd.Parameters.AddWithValue("@Menu_icon", filepath);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Id", ViewState["Id"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Certificate has been successfully updated.", "success");
                    txt_Certificate_name.Text = "";
                    btn_Submit.Text = "Save";
                    btn_cancel.Visible = false;
                    get_template();
                }
            }
            else
            {
                Alertme("Your entered certificate name already exists.", "warning");


            }
        }
        private string upload_template()
        {
            string filepath = "";
            if (FileUpload1.PostedFile.ContentLength <= 500000)
            {
                string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dt.ToString("dd_MM_yyyy");
                string time = dt.ToString("hh_mm_ss");
                String FileName1 = date + time + extension;
                string originalPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                HttpPostedFile postedf = FileUpload1.PostedFile;
                postedf.SaveAs(MapPath("~/Master_Img/" + FileName1));
                filepath = originalPath + "/Master_Img/" + Path.GetFileName(FileName1);
            }
            return filepath;
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            txt_Certificate_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Save";

        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Is_bydefault = (Label)row.FindControl("lbl_Is_bydefault");
                if (lbl_Is_bydefault.Text == "1")
                {
                    Alertme("Sorry, you can't delete this because it is a predefined certificate name.", "warning");
                }
                else
                {
                    My.exeSql("delete from Dashboard_report_menu where Id='" + lbl_Id.Text + "'");
                    Alertme("Certificate name has been deleted successfully", "success");
                    get_template();
                }

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Menu_name = (Label)row.FindControl("lbl_Menu_name");
                    Label lbl_Is_bydefault = (Label)row.FindControl("lbl_Is_bydefault");
                    Label lbl_Menu_icon = (Label)row.FindControl("lbl_Menu_icon");

                    if (lbl_Is_bydefault.Text == "1")
                    {
                        Alertme("Sorry, you can't edit this because it is a predefined certificate name.", "warning");
                    }
                    else
                    {
                        txt_Certificate_name.Text = lbl_Menu_name.Text;
                        Label lbl_Id = (Label)row.FindControl("lbl_Id");
                        btn_cancel.Visible = true;
                        btn_Submit.Text = "Update";
                        ViewState["Id"] = lbl_Id.Text;
                        ViewState["Menu_icon"] = lbl_Menu_icon.Text;
                    }

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        protected void lnk_Signature_Required_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Menu_id = (Label)row.FindControl("lbl_Menu_id");
                ViewState["menuid"] = lbl_Menu_id.Text;
                Bind_ddl_designation();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);

            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }

        }




        #region add Designation
        protected void btn_Designation_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_UserName.SelectedItem.Text == "Select")
                {
                    Alertme("Please select designation name", "warning");
                }
                else
                {

                    DataTable dt = mycode.FillData("Select top 1 * from Setting_Desig_Master_with_Certi_master where Designation_Name='" + ddl_UserName.SelectedItem.Text + "' and Menu_id='" + ViewState["menuid"].ToString() + "'");
                    if (dt.Rows.Count == 0)
                    {

                        string query = "INSERT INTO Setting_Desig_Master_with_Certi_master (Designation_Name,Created_by,Created_date,Menu_id) values (@Designation_Name,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Menu_id)";
                        SqlCommand cmd;
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Designation_Name", ddl_UserName.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Menu_id", ViewState["menuid"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Record has been saved successfully", "success");


                            Bind_ddl_designation();
                            ddl_UserName.SelectedValue = "0";

                        }
                    }
                    else
                    {
                        Alertme("Your selected designation name already added", "warning");
                    }



                }
            }
            catch
            {

            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);


        }

        private void Bind_ddl_designation()
        {
            DataTable dt = mycode.FillData("Select * from Setting_Desig_Master_with_Certi_master where  Menu_id='" + ViewState["menuid"].ToString() + "' order by  Designation_Name");
            if (dt.Rows.Count == 0)
            {

                rd_view_Designation.DataSource = null;
                rd_view_Designation.DataBind();
            }
            else
            {
                rd_view_Designation.DataSource = dt;
                rd_view_Designation.DataBind();
            }
        }
        protected void lnkDel_Click1(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lblId = (Label)row.FindControl("lblId");
                My.exeSql("delete from Setting_Desig_Master_with_Certi_master where Id='" + lblId.Text + "'");
                Alertme("Recode has been deleted successfully", "success");
                Bind_ddl_designation();

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
        }
        #endregion


    }
}