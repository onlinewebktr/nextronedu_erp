using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class Fee_Type_Master : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_group, "select Group_name,group_id from dbo.[Fee_group_master]");
                        mycode.bind_all_ddl_with_id(ddl_feetypegoup, "select Group_name,group_id from dbo.[Fee_group_master]");
                        ddl_feetypegoup.SelectedValue = "1";
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        Bind_grid_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
            }
        }

        protected void ddl_feetypegoup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_feetypegoup.SelectedItem.Text == "Select")
            {
                Alertme("Please select free group", "warning");
            }
            else
            {
                Bind_grid_data();
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
            SqlCommand cmd;
            if (ddl_group.SelectedItem.Text == "Select")
            { 
                Alertme("Please select free group", "warning");
            }
            else if (txt_feetype.Text == "")
            {
                Alertme("Please enter fee type", "warning");
            }
            else if (ddl_efected.Text == "Select")
            {
                Alertme("Please select effected", "warning");
            }
            else
            {
                if (btn_Submit.Text == "Add")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        DataTable dt = mycode.FillData("Select * from Content_master where content='" + txt_feetype.Text.Trim() + "' and group_id='" + ddl_group.SelectedValue + "' and Ledger='" + ddl_efected.Text + "'  ");
                        if (dt.Rows.Count == 0)
                        { 
                            string contentid = get_contentid();
                            string query = "INSERT INTO Content_master (content,content_id,group_id,Ledger,User_id,Date,time) values (@content,@content_id,@group_id,@Ledger,@User_id,@Date,@time)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@content", txt_feetype.Text.Trim());
                            cmd.Parameters.AddWithValue("@content_id", contentid);
                            cmd.Parameters.AddWithValue("@group_id", ddl_group.SelectedValue);
                            cmd.Parameters.AddWithValue("@Ledger", ddl_efected.Text);
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@time", mycode.time());
                            if (My.InsertUpdateData(cmd))
                            {
                                try
                                {
                                    if (chk_for_hostel.Checked == true || chk_admission.Checked == true)
                                    {
                                        string forType = "Hostel";
                                        if (chk_admission.Checked == true)
                                        {
                                            if (ddl_group.SelectedValue == "1")
                                            {
                                                forType = "Admission";
                                                My.exeSql("insert into Fee_type_for_hostel(Group_id,Content_id,Is_For) values ('" + ddl_group.SelectedValue + "','" + contentid + "','" + forType + "')");
                                            }
                                            if (ddl_group.SelectedValue == "2")
                                            {
                                                forType = "Annual";
                                                My.exeSql("insert into Fee_type_for_hostel(Group_id,Content_id,Is_For) values ('" + ddl_group.SelectedValue + "','" + contentid + "','" + forType + "')");
                                            }
                                        }
                                        else
                                        {
                                            My.exeSql("insert into Fee_type_for_hostel(Group_id,Content_id,Is_For) values ('" + ddl_group.SelectedValue + "','" + contentid + "','" + forType + "')");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                } 

                                Alertme("Fee type master has been update Successfully.", "success");
                                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Create Fee type master " + txt_feetype.Text + " on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                                btn_Submit.Text = "Add";
                                btn_cancel.Visible = false;
                                txt_feetype.Text = "";
                                chk_for_hostel.Checked = false;
                                chk_admission.Checked = false;
                                Bind_grid_data();
                            }
                        }
                        else
                        {
                            Alertme("Your entered fee type already added in this group", "warning");
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        DataTable dt = mycode.FillData("Select * from Content_master where content='" + txt_feetype.Text.Trim() + "' and group_id='" + ddl_group.SelectedValue + "' and Ledger='" + ddl_efected.Text + "' and Id!=" + HdID.Value + "  ");
                        if (dt.Rows.Count == 0)
                        {
                            string query = "Update Content_master set content=@content,group_id=@group_id,Ledger=@Ledger,User_id=@User_id,Date=@Date,time=@time where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@content", txt_feetype.Text.Trim());
                            cmd.Parameters.AddWithValue("@group_id", ddl_group.SelectedValue);
                            cmd.Parameters.AddWithValue("@Ledger", ddl_efected.Text);
                            cmd.Parameters.AddWithValue("@Id", HdID.Value);
                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@time", mycode.time());
                            if (My.InsertUpdateData(cmd))
                            {
                                try
                                { 
                                    My.exeSql("delete from Fee_type_for_hostel where Group_id='" + ddl_group.SelectedValue + "' and Content_id='" + ViewState["contentId"].ToString() + "'");
                                    string forType = "Hostel";
                                    if (chk_admission.Checked == true)
                                    {
                                        if (ddl_group.SelectedValue == "1")
                                        {
                                            forType = "Admission";
                                            My.exeSql("insert into Fee_type_for_hostel(Group_id,Content_id,Is_For) values ('" + ddl_group.SelectedValue + "','" + ViewState["contentId"].ToString() + "','" + forType + "')");
                                        }
                                        if (ddl_group.SelectedValue == "2")
                                        {
                                            forType = "Annual";
                                            My.exeSql("insert into Fee_type_for_hostel(Group_id,Content_id,Is_For) values ('" + ddl_group.SelectedValue + "','" + ViewState["contentId"].ToString() + "','" + forType + "')");
                                        }
                                    }
                                    else
                                    {
                                        My.exeSql("insert into Fee_type_for_hostel(Group_id,Content_id,Is_For) values ('" + ddl_group.SelectedValue + "','" + ViewState["contentId"].ToString() + "','" + forType + "')");
                                    } 
                                }
                                catch (Exception ex)
                                {
                                }
                                Alertme("Fee type master has been update Successfully.", "success");
                                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Create Fee type master " + txt_feetype.Text + " on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                                btn_Submit.Text = "Add";
                                btn_cancel.Visible = false;
                                txt_feetype.Text = "";
                                chk_for_hostel.Checked = false;
                                chk_admission.Checked = false;
                                ddl_efected.Text = "Select";
                                ddl_group.SelectedValue = "0";
                                Bind_grid_data();
                            }
                        }
                        else
                        {
                            Alertme("Your entered fee type already added in this group", "warning");
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }


                }
            }
        }

        private void Bind_grid_data()
        {
            string query = "";
            if (ddl_feetypegoup.SelectedItem.Text == "Select")
            {
                query = "select * from (Select *,(select top 1 Group_name from Fee_group_master where group_id=Content_master.group_id) as Group_nameS from Content_master) t order by group_id asc";
            }

            else
            {
                query = "select * from (select *,(select top 1 Group_name from Fee_group_master where group_id=Content_master.group_id) as Group_nameS from Content_master where group_id='" + ddl_feetypegoup.SelectedValue + "') t order by group_id asc ";
            }
            DataTable dt = mycode.FillData(query);
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

        private string get_contentid()
        {
            bool duplicate = false;
            string session_id = mycode.auto_serial("content_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select content_id from dbo.[Content_master] where content_id='" + session_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    session_id = mycode.auto_serial("content_id");
                }
            }
            return session_id;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            chk_for_hostel.Checked = false;
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_feetype.Text = "";
            ddl_efected.Text = "Select";
            ddl_group.SelectedValue = "0";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_feetype = (Label)row.FindControl("lbl_feetype");
                    Label lbl_Ledger = (Label)row.FindControl("lbl_Ledger");
                    Label lbl_group_id = (Label)row.FindControl("lbl_group_id");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_content_id = (Label)row.FindControl("lbl_content_id");
                    HdID.Value = lbl_Id.Text;

                    DataTable dt = My.dataTable("select Is_For from Fee_type_for_hostel where Content_id='" + lbl_content_id.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if(dt.Rows[0]["Is_For"].ToString()== "Hostel")
                        {
                            chk_for_hostel.Checked = true;
                            chk_admission.Checked = false;
                        }
                        else
                        {
                            chk_for_hostel.Checked = false;
                            chk_admission.Checked = true;
                        }
                    }

                    ViewState["contentId"] = lbl_content_id.Text;
                    txt_feetype.Text = lbl_feetype.Text;
                    ddl_group.Text = lbl_group_id.Text;
                    ddl_efected.Text = lbl_Ledger.Text;
                    btn_Submit.Text = "Update";
                    btn_cancel.Visible = true;
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_content_id = (Label)row.FindControl("lbl_content_id");

                    bool chk_setfee = get_fee_status(lbl_content_id.Text);
                    if (chk_setfee == true)
                    {
                        mycode.executequery("delete from Content_master where Id=" + lbl_Id.Text + "");
                        try
                        {
                            My.exeSql("delete from Fee_type_for_hostel where Content_id='" + lbl_content_id.Text + "'");
                        }
                        catch (Exception ex)
                        {
                        }
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " Delete Fee type master " + txt_feetype.Text + " on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                        Alertme("Deletion process has been Successfully done .", "success");
                        Bind_grid_data();
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_feetype.Text = "";
                        ddl_efected.Text = "Select";
                        ddl_group.SelectedValue = "0";
                    }
                    else
                    {
                        Alertme("Sorry you can't delete because this is already added in fees", "warning");
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

        private bool get_fee_status(string content)
        {

            DataTable cdt = mycode.FillData("  select content_id from dbo.[Fee_master_content_wise] where content_id='" + content + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {
                return false;
            } 
        } 
    }
}