using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin
{
    public partial class Set_course_Fees : System.Web.UI.Page
    {
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Set_course_Fees.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ViewState["session_id"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_course_Fees");
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
        protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");

            }
            else
            {
                Bind_grid_data_fee();

            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton img = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)img.Parent.Parent;
                    Label lbl_Content_id = (Label)row.FindControl("lbl_Content_id");
                    Label lbl_Content_Fee = (Label)row.FindControl("lbl_Content_Fee");
                    Label lbl_Content_Name = (Label)row.FindControl("lbl_Content_Name");
                    Label lbl_is_edit = (Label)row.FindControl("lbl_is_edit");
                    txt_hading.Text = lbl_Content_Name.Text;
                    txt_headingamount.Text = lbl_Content_Fee.Text;
                    hd_contentid.Value = lbl_Content_id.Text;
                    ddl_is_amt_change.SelectedValue = lbl_is_edit.Text;
                    btn_Submit.Text = "Update";
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
                    LinkButton img = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)img.Parent.Parent;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Content_id = (Label)row.FindControl("lbl_Content_id");
                    if (mycode.IsUserExist("select Content_id from Other_Fee_Taken_For_Student where Session_id='" + ddl_session.SelectedValue + "' and  Content_id='" + lbl_Content_id.Text + "'"))
                    {
                        mycode.executequery("delete Other_Fee_For_Special_Condition where Id=" + lbl_Id.Text + "");
                        Alertme("Deletion process has been successfully done", "success");
                        Bind_grid_data_fee();
                    }
                    else
                    {
                        Alertme("Can't delete due to fee has been taken.", "warning");
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



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Set_course_Fees.aspx", false);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (txt_hading.Text == "")
            {
                Alertme("Please enter fees heading", "warning");
            }
            else if (txt_headingamount.Text == "")
            {
                Alertme("Please enter fees amount", "warning");
            }
            else
            {

                //My.auto_serialS("group_id");

                if (btn_Submit.Text == "Add")
                {
                    insert_data_Other_Fee_For_Special_Condition();
                }
                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        update_data_Other_Fee_For_Special_Condition();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");

                    }

                }


            }
        }

        private void update_data_Other_Fee_For_Special_Condition()
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Other_Fee_For_Special_Condition where  Session_id='" + ddl_session.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  and Class_id='" + ddl_course.SelectedValue + "' and Content_Name='" + txt_hading.Text + "' and Content_id!=" + hd_contentid.Value + "     ");
            if (dt.Rows.Count == 0)
            {
                string query = "Update Other_Fee_For_Special_Condition set Content_Name=@Content_Name,Content_Fee=@Content_Fee,Updated_by=@Updated_by,Updated_date=@Updated_date,Is_edit=@Is_edit where Content_id = @Content_id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Is_edit", ddl_is_amt_change.SelectedValue);
                cmd.Parameters.AddWithValue("@Content_Name", txt_hading.Text);
                cmd.Parameters.AddWithValue("@Content_id", hd_contentid.Value);
                cmd.Parameters.AddWithValue("@Content_Fee", My.toDouble(txt_headingamount.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Fees has been updated successfully", "success");
                    txt_hading.Text = "";
                    txt_headingamount.Text = "";
                    btn_Submit.Text = "Add";
                    Bind_grid_data_fee();
                }
            }
            else
            {
                Alertme("Sorry this fee heading already already exists", "warning");
            }
        }

        private void insert_data_Other_Fee_For_Special_Condition()
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Other_Fee_For_Special_Condition where  Session_id='" + ddl_session.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  and Class_id='" + ddl_course.SelectedValue + "' and Content_Name='" + txt_hading.Text + "'     ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Other_Fee_For_Special_Condition (Class_id,Branch_id,Session_id,Content_Name,Content_id,Content_Fee,Created_by,Created_date,Is_edit) values (@Class_id,@Branch_id,@Session_id,@Content_Name,@Content_id,@Content_Fee,@Created_by,@Created_date,@Is_edit)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Class_id", ddl_course.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Content_Name", txt_hading.Text);
                cmd.Parameters.AddWithValue("@Content_id", My.auto_serialS("group_id"));
                cmd.Parameters.AddWithValue("@Content_Fee", My.toDouble(txt_headingamount.Text).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                cmd.Parameters.AddWithValue("@Is_edit", ddl_is_amt_change.SelectedValue);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Fees has been saved successfully", "success");
                    txt_hading.Text = "";
                    txt_headingamount.Text = "";
                    btn_Submit.Text = "Add";
                    Bind_grid_data_fee();
                }
            }
            else
            {
                Alertme("Sorry this fees heading already already exists", "warning");
            }
        }

        private void Bind_grid_data_fee()
        {
            DataTable dt = mycode.FillData("Select * from Other_Fee_For_Special_Condition where  Session_id='" + ddl_session.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  and Class_id='" + ddl_course.SelectedValue + "'    ");
            if (dt.Rows.Count == 0)
            {
                grd_fee.DataSource = null;
                grd_fee.DataBind();
            }
            else
            {
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
            }
        }


    }
}