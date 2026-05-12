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
    public partial class signature_setting1 : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Signature_Setting");
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
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select * from Exam_signature_setting order by Position asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_name.Text = "";
            txt_position.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Save";
            ddl_sig_type.Enabled = true;
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (txt_name.Text == "")
            {
                Alertme("Please Enter show name.", "warning");
                txt_name.Focus();
                return;
            }
            if (txt_position.Text == "")
            {
                Alertme("Please Enter position.", "warning");
                txt_position.Focus();
                return;
            }

            submit_details();
            empty_form();
            bind_grd_view();

        }



        private void submit_details()
        {
            DataTable dt = mycode.FillData("select Id from Exam_signature_setting where Signature_type='" + ddl_sig_type.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_signature_setting (Signature_type,Signature_type_e_p,Name,Position,Branch_id) values (@Signature_type,@Signature_type_e_p,@Name,@Position,@Branch_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Signature_type", ddl_sig_type.SelectedValue);
                if (ddl_sig_type.SelectedValue == "3")
                {
                    cmd.Parameters.AddWithValue("@Signature_type_e_p", ddl_sig_exam_or_parnt.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Signature_type_e_p", "0");
                }
                cmd.Parameters.AddWithValue("@Name", txt_name.Text);
                cmd.Parameters.AddWithValue("@Position", txt_position.Text);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been saved successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "update Exam_signature_setting set Signature_type=@Signature_type,Signature_type_e_p=@Signature_type_e_p,Name=@Name,Position=@Position where Id='" + dt.Rows[0]["Id"].ToString() + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Signature_type", ddl_sig_type.SelectedValue);
                if (ddl_sig_type.SelectedValue == "3")
                {
                    cmd.Parameters.AddWithValue("@Signature_type_e_p", ddl_sig_exam_or_parnt.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Signature_type_e_p", "0");
                }
                cmd.Parameters.AddWithValue("@Name", txt_name.Text);
                cmd.Parameters.AddWithValue("@Position", txt_position.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been saved successfully.", "success");
                    empty_form();
                    bind_grd_view();
                }
            }
        }

        private void empty_form()
        {
            txt_name.Text = "";
            txt_position.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Save";
            ddl_sig_type.Enabled = true;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_sig_Name = (Label)row.FindControl("lbl_sig_Name");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_sig_type = (Label)row.FindControl("lbl_sig_type");
                Label lbl_signature_type_e_p = (Label)row.FindControl("lbl_signature_type_e_p");
                Label lbl_position = (Label)row.FindControl("lbl_position");
                ddl_sig_type.Enabled = false;
                ddl_sig_type.SelectedValue = lbl_sig_type.Text;
                if (lbl_sig_type.Text == "3")
                {
                    examineeParentDV.Visible = true;
                    ddl_sig_exam_or_parnt.SelectedValue = lbl_sig_type.Text;
                }
                else
                {
                    examineeParentDV.Visible = false;
                }
                txt_name.Text = lbl_sig_Name.Text;
                txt_position.Text = lbl_position.Text;
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
            string qry = "delete from Exam_signature_setting where Id=" + lbl_Id.Text + "";
            mycode.executequery(qry);
            Alertme("Record has been deleted successfully", "success");
            bind_grd_view();

        }

        protected void ddl_sig_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_sig_type.SelectedValue == "3")
                {
                    examineeParentDV.Visible = true;
                }
                else
                {
                    examineeParentDV.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}