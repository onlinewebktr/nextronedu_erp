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
    public partial class Add_Certificate_Signature : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddl_Certificate, "select  Menu_name,Menu_id from Dashboard_report_menu where Group_id='19' order by Position");







                        get_template();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Certificate_Signature");
            }
        }
        protected void ddl_Certificate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Certificate.SelectedItem.Text == "Select")
            {
                Alertme("Please select certificate name", "warning");
            }
            else
            {
                get_template();
                mycode.bind_all_ddl_with_id(ddl_Designation_name, "Select Designation_Name,Designation_Name as Designation_Name2 from Setting_Desig_Master_with_Certi_master where Menu_id='" + ddl_Certificate.SelectedValue + "' order by Designation_Name");

            }
        }

        protected void ddl_Designation_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Certificate.SelectedItem.Text == "Select")
            {
                Alertme("Please select certificate name", "warning");
            }
            else if (ddl_Designation_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select certificate name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_username, "Select name,user_id from user_details where User_Type='" + ddl_Designation_name.SelectedItem.Text + "' and Istatus=1 and User_Type not in ('Admin','Visitor') order by name");

            }
        }
        private void get_template()
        {
            DataTable dt;
            if (ddl_Certificate.SelectedItem.Text == "Select")
            {
                dt = mycode.FillData("Select  ssm.*,drm.Menu_name as Certificate_name,ud.name as user_name,CASE WHEN ssm.Is_class_teacher= 1 THEN 'Yes' WHEN ssm.Is_class_teacher = '0' THEN 'No'  END AS isclass_teacher,CASE WHEN ssm.Is_signature_display= 1 THEN 'Yes' WHEN ssm.Is_signature_display = '0' THEN 'No'  END AS issignature_display,CASE WHEN ssm.Istatus= 1 THEN 'ON' WHEN ssm.Istatus = '0' THEN 'OFF'  END AS Status from Setting_Signature_Master ssm join Dashboard_report_menu drm on ssm.Menu_id=drm.Menu_id join user_details ud on ssm.user_id=ud.user_id");

            }
            else
            {
                dt = mycode.FillData("Select  ssm.*,drm.Menu_name as Certificate_name,ud.name as user_name,CASE WHEN ssm.Is_class_teacher= 1 THEN 'Yes' WHEN ssm.Is_class_teacher = '0' THEN 'No'  END AS isclass_teacher,CASE WHEN ssm.Is_signature_display= 1 THEN 'Yes' WHEN ssm.Is_signature_display = '0' THEN 'No'  END AS issignature_display,CASE WHEN ssm.Istatus= 1 THEN 'ON' WHEN ssm.Istatus = '0' THEN 'OFF'  END AS Status from Setting_Signature_Master ssm join Dashboard_report_menu drm on ssm.Menu_id=drm.Menu_id join user_details ud on ssm.user_id=ud.user_id where drm.Menu_name='"+ ddl_Certificate.SelectedItem.Text + "' ");
            }

            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry, no signature records exist.", "warning");
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
                        if (ddl_Certificate.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select Designation name", "warning");

                        }

                        else if (ddl_Designation_name.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select Designation name", "warning");

                        }
                        else if (ddl_username.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select name", "warning");
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

                        if (ddl_Certificate.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select Designation name", "warning");

                        }

                        else if (ddl_Designation_name.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select Designation name", "warning");

                        }
                        else if (ddl_username.SelectedItem.Text == "Select")
                        {
                            Alertme("Please select name", "warning");
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

        private void final_update(string filepath)
        {
            int displayedcertificatesignature = 0;
            int class_teacher = 0;
            if (chk_displayedcertificatesignature.Checked == true)
            {
                displayedcertificatesignature = 1;
            }
            if (chk_class_teacher.Checked == true)
            {
                class_teacher = 1;
            }

            if (filepath == "")
            {
                filepath = ViewState["Signature"].ToString();
            }
            SqlCommand cmd;
            string query = "Update Setting_Signature_Master set Designation_Name=@Designation_Name,user_id=@user_id,Signature=@Signature,Updated_by=@Updated_by,Updated_Date=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),Menu_id=@Menu_id,Is_signature_display=@Is_signature_display,Is_class_teacher=@Is_class_teache,Position=@Position where Id = @Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Designation_Name", ddl_Designation_name.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@user_id", ddl_username.SelectedValue);
            cmd.Parameters.AddWithValue("@Signature", filepath);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Menu_id", ddl_Certificate.SelectedValue);
            cmd.Parameters.AddWithValue("@Is_signature_display", displayedcertificatesignature);
            cmd.Parameters.AddWithValue("@Is_class_teache", class_teacher);
            cmd.Parameters.AddWithValue("@Id", ViewState["Id"].ToString());
            cmd.Parameters.AddWithValue("@Position", ddl_position.Text);

            if (My.InsertUpdateData(cmd))
            {
                Alertme("Signature has been updated successfully.", "success");
                ddl_Designation_name.SelectedValue = "0";
                ddl_username.SelectedValue = "0";
                ddl_Certificate.SelectedValue = "0";
                chk_displayedcertificatesignature.Checked = false;
                chk_class_teacher.Checked = false;

                btn_cancel.Visible = false;
                btn_Submit.Text = "Save";
                get_template();
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

        private void final_submit(string filepath)
        {
            string Signature_id = My.create_random_no();
            int displayedcertificatesignature = 0;
            int class_teacher = 0;
            if (chk_displayedcertificatesignature.Checked == true)
            {
                displayedcertificatesignature = 1;
            }
            if (chk_class_teacher.Checked == true)
            {
                class_teacher = 1;
            }

            SqlCommand cmd;
            string query = "INSERT INTO Setting_Signature_Master (Designation_Name,user_id,Signature,Created_by,Created_date,Module_type,Signature_id,Menu_id,Istatus,Is_signature_display,Position,Is_class_teacher) values (@Designation_Name,@user_id,@Signature,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Module_type,@Signature_id,@Menu_id,@Istatus,@Is_signature_display,@Position,@Is_class_teacher)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Designation_Name", ddl_Designation_name.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@user_id", ddl_username.SelectedValue);
            cmd.Parameters.AddWithValue("@Signature", filepath);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Module_type", "Certificate");
            cmd.Parameters.AddWithValue("@Signature_id", Signature_id);
            cmd.Parameters.AddWithValue("@Menu_id", ddl_Certificate.SelectedValue);
            cmd.Parameters.AddWithValue("@Istatus", "1");
            cmd.Parameters.AddWithValue("@Is_signature_display", displayedcertificatesignature);
            cmd.Parameters.AddWithValue("@Is_class_teacher", class_teacher);
            cmd.Parameters.AddWithValue("@Position", ddl_position.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Signature has been successfully added.", "success");
                //ddl_Designation_name.SelectedValue = "0";
                ddl_username.SelectedValue = "0";
                //ddl_Certificate.SelectedValue = "0";
                chk_displayedcertificatesignature.Checked = false;
                chk_class_teacher.Checked = false;
                get_template();
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
                    Label lbl_Designation_Name = (Label)row.FindControl("lbl_Designation_Name");
                    Label lbl_user_id = (Label)row.FindControl("lbl_user_id");
                    Label lbl_Menu_id = (Label)row.FindControl("lbl_Menu_id");
                    Label lbl_Signature = (Label)row.FindControl("lbl_Signature");
                    Label lbl_Is_signature_display = (Label)row.FindControl("lbl_Is_signature_display");
                    Label lbl_Is_class_teacher = (Label)row.FindControl("lbl_Is_class_teacher");
                    Label lbl_id = (Label)row.FindControl("lbl_id");
                    Label lbl_Position = (Label)row.FindControl("lbl_Position");
                    ddl_Certificate.SelectedValue = lbl_Menu_id.Text;
                    mycode.bind_all_ddl_with_id(ddl_Designation_name, "Select Designation_Name,Designation_Name as Designation_Name2 from Setting_Desig_Master_with_Certi_master where Menu_id='" + ddl_Certificate.SelectedValue + "' order by Designation_Name");
                    ddl_Designation_name.SelectedValue = lbl_Designation_Name.Text;
                    mycode.bind_all_ddl_with_id(ddl_username, "Select name,user_id from user_details where User_Type='" + ddl_Designation_name.SelectedItem.Text + "' and Istatus=1 and User_Type not in ('Admin','Visitor') order by name");
                    ddl_username.SelectedValue = lbl_user_id.Text;

                    if (lbl_Is_signature_display.Text == "1")
                    {
                        chk_displayedcertificatesignature.Checked = true;
                    }
                    if (lbl_Is_class_teacher.Text == "1")
                    {
                        chk_class_teacher.Checked = true;
                    }

                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ViewState["Id"] = lbl_id.Text;
                    ViewState["Signature"] = lbl_Signature.Text;
                    ddl_position.Text = lbl_Position.Text;


                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");

                My.exeSql("delete from Setting_Signature_Master where Id='" + lbl_Id.Text + "'");
                Alertme("Your Selected signature has been deleted successfully", "success");
                get_template();

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ddl_Designation_name.SelectedValue = "0";
            ddl_username.SelectedValue = "0";
            ddl_Certificate.SelectedValue = "0";
            chk_displayedcertificatesignature.Checked = false;
            chk_class_teacher.Checked = false;
            btn_cancel.Visible = false;
            btn_Submit.Text = "Save";

        }
        protected void lnk_bnr_status_Click1(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            try
            {

                if (ViewState["Is_add"].ToString() == "1")
                {
                    change_status(lnk);
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    change_status(lnk);

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

        private void change_status(LinkButton lnk)
        {
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label id = (Label)row.FindControl("lbl_id");
            Label lbl_Istatus = ((Label)(row.FindControl("lbl_Istatus")));
            update_product_status(id.Text, lbl_Istatus.Text);
        }
        private void update_product_status(string Id, string c_status)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Setting_Signature_Master where Id='" + Id + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Header_templete");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (c_status == "1")
                    {
                        dr["Istatus"] = "0";
                    }
                    else
                    {
                        dr["Istatus"] = "1";
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    get_template();
                    Alertme("Status updated successfully.", "success");
                }
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnk_bnr_status = (LinkButton)(e.Item.FindControl("lnk_bnr_status"));
                Label lbl_Status = ((Label)(e.Item.FindControl("lbl_Istatus")));
                if (lbl_Status.Text == "1")
                {
                    lnk_bnr_status.Text = "ON";
                    lnk_bnr_status.Attributes.Add("class", "lnk-btn-actv");
                }
                else
                {
                    lnk_bnr_status.Text = "OFF";
                    lnk_bnr_status.Attributes.Add("class", "lnk-red-bg");
                }
            }
        }
    }
}