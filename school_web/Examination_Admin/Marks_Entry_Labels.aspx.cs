using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Examination_Admin
{
    public partial class Marks_Entry_Labels : System.Web.UI.Page
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

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        Bind_All_level();

                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Marks_Entry_Labels");
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
        private void Bind_All_level()
        {
            string query = "Select  * from  Exam_Marks_Entry_Label where   Branch_Id='" + ViewState["branchid"].ToString() + "' order BY Short_Name";

            finalgride(query);
        }

        private void finalgride(string query)
        {

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no marks entry labels ", "warning");
                grid_grade.DataSource = null;
                grid_grade.DataBind();
            }
            else
            {
                grid_grade.DataSource = dt;
                grid_grade.DataBind();
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Condition = (Label)row.FindControl("lbl_Condition");
                Label lbl_Description = (Label)row.FindControl("lbl_Description");
                Label lbl_shortname = (Label)row.FindControl("lbl_shortname");
                Label lbl_Parent_Condition = (Label)row.FindControl("lbl_Parent_Condition");

                txt_Condition.Text = lbl_Condition.Text;
                txt_Short_name.Text = lbl_shortname.Text;
                ddl_condition.Text = lbl_Parent_Condition.Text;
                txt_Description.Text = lbl_Description.Text;
                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                mycode.executequery("delete from Exam_Marks_Entry_Label where Id='" + lbl_id.Text + "' ");
                Alertme("Marks Entry label has been deleted successfully", "warning");
                Bind_All_level();


                btn_cancel.Visible = false;
                btn_Submit.Text = "Add";
            }
            catch
            {
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Submit.Text == "Add")
            {
                SqlCommand cmd;
                DataTable dt = mycode.FillData("Select * from Exam_Marks_Entry_Label where    Branch_id='" + ViewState["branchid"].ToString() + "' and Condition='" + txt_Condition.Text + "' ");
                if (dt.Rows.Count == 0)
                {

                    string entryid = Examination.auto_serialS("Exam_Marks_Entry_Label_Id", ViewState["branchid"].ToString());
                    string query = " INSERT INTO Exam_Marks_Entry_Label (Condition,Description,Short_Name,Parent_Condition,Exam_Marks_Entry_Label_Id,Branch_Id,Created_by,Created_Date) values (@Condition,@Description,@Short_Name,@Parent_Condition,@Exam_Marks_Entry_Label_Id,@Branch_Id,@Created_by,@Created_Date)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Condition", txt_Condition.Text);
                    cmd.Parameters.AddWithValue("@Description", txt_Description.Text);
                    cmd.Parameters.AddWithValue("@Short_Name", txt_Short_name.Text);
                    cmd.Parameters.AddWithValue("@Parent_Condition", ddl_condition.Text);
                    cmd.Parameters.AddWithValue("@Exam_Marks_Entry_Label_Id", entryid);
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_Date", My.getdate1());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("marks entry Level has been saved Successfully.", "success");
                        txt_Condition.Text = "";
                        txt_Short_name.Text = "";
                        txt_Description.Text = "";

                        Bind_All_level();
                        btn_Submit.Text = "Add";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                }
                else
                {
                    Alertme("Your marks entry Level already added", "warning");
                }
            }
            else
            {

                SqlCommand cmd;
                DataTable dt = mycode.FillData("Select * from Exam_Marks_Entry_Label where    Branch_id='" + ViewState["branchid"].ToString() + "' and Condition='" + txt_Condition.Text + "' and Id!=" + hd_id.Value + " ");
                if (dt.Rows.Count == 0)
                {
                  
                    string query = "Update Exam_Marks_Entry_Label set Condition=@Condition,Description=@Description,Short_Name=@Short_Name,Parent_Condition=@Parent_Condition,Branch_Id=@Branch_Id,Updated_by=@Updated_by,Updated_Date=@Updated_Date where Id = @Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Condition", txt_Condition.Text);
                    cmd.Parameters.AddWithValue("@Description", txt_Description.Text);
                    cmd.Parameters.AddWithValue("@Short_Name", txt_Short_name.Text);
                    cmd.Parameters.AddWithValue("@Parent_Condition", ddl_condition.Text);
                   
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_Date", My.getdate1());
                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("marks entry Level has been update Successfully.", "success");
                        txt_Condition.Text = "";
                        txt_Short_name.Text = "";
                        txt_Description.Text = "";

                        Bind_All_level();
                        btn_Submit.Text = "Add";
                    }
                }
                else
                {
                    Alertme("Your marks entry Level already added", "warning");
                }



            }
        }

        

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_Condition.Text = "";
            txt_Short_name.Text = "";
            txt_Description.Text = "";

           
            btn_Submit.Text = "Add";
        }

        protected void ddl_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_condition.Text == "Absent")
            {
                txt_Description.Text = "Zero Is Shown Against Their Marks";
            }
            else
            {

                txt_Description.Text = "The Marks Are Not Considered For Percentage Calculation";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}