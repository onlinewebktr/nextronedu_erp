using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class add_personality_trate : System.Web.UI.Page
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
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Commentary_Remark_Types");
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
            string query = "Select  * from  Exam_Personality_Traits";

            finalgride(query);
        }

        private void finalgride(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no renarks type added ", "warning");
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
                Label lbl_Remark_Types = (Label)row.FindControl("lbl_Remark_Types");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                txt_Remark_Types.Text = lbl_Remark_Types.Text;
                hd_id.Value = lbl_id.Text;


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
                mycode.executequery("delete from Exam_Commentary_Remark_Types where Id='" + lbl_id.Text + "' ");
                Alertme("remarks type has been deleted successfully", "warning");
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
                DataTable dt = mycode.FillData("Select * from Exam_Commentary_Remark_Types where    Branch_Id='" + ViewState["branchid"].ToString() + "' and Commentary_Remark_Types='" + txt_Remark_Types.Text + "' ");
                if (dt.Rows.Count == 0)
                {

                    string entryid = Examination.auto_serialS("Remaks_Id", ViewState["branchid"].ToString());
                    string query = "INSERT INTO Exam_Commentary_Remark_Types (Branch_Id,Commentary_Remark_Types,Created_By,Created_Date,Remaks_Id) values (@Branch_Id,@Commentary_Remark_Types,@Created_By,@Created_Date,@Remaks_Id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Commentary_Remark_Types", txt_Remark_Types.Text);

                    cmd.Parameters.AddWithValue("@Remaks_Id", entryid);
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_Date", My.getdate1());
                    if (My.InsertUpdateData(cmd))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        Alertme("Remarks type has been saved Successfully.", "success");
                        txt_Remark_Types.Text = "";
                        Bind_All_level();
                        btn_Submit.Text = "Add";
                    }
                }
                else
                {
                    Alertme("Your remarks type already added", "warning");
                }
            }
            else
            { 
                SqlCommand cmd;
                DataTable dt = mycode.FillData("Select * from Exam_Commentary_Remark_Types where    Branch_Id='" + ViewState["branchid"].ToString() + "' and Commentary_Remark_Types='" + txt_Remark_Types.Text + "' and Id!=" + hd_id.Value + " ");
                if (dt.Rows.Count == 0)
                { 
                    string query = "Update Exam_Commentary_Remark_Types set Commentary_Remark_Types=@Commentary_Remark_Types,Branch_Id=@Branch_Id,Updated_By=@Updated_By,Updated_date=@Updated_date where Id = @Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Commentary_Remark_Types", txt_Remark_Types.Text); 
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_Date", My.getdate1());
                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Remarks type has been saved Successfully.", "success");
                        txt_Remark_Types.Text = ""; 
                        Bind_All_level();
                        btn_Submit.Text = "Add";
                    }
                }
                else
                {
                    Alertme("Your remarks type already added", "warning");
                } 
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_Remark_Types.Text = ""; 
            btn_Submit.Text = "Add";
        }
    }
}