using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class fee_type_master : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_group, "select Group_name,group_id from dbo.[Fee_group_master]");
                        ddl_ledger.DataSource = new string[] { "School", "Hostel" };
                        ddl_ledger.DataBind();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
            DataTable dt = mycode.FillData("select Row_Number() over( order by cm.id) sl, cm.Id,cm.content,gm.Group_name ,cm.group_id,cm.Ledger,cm.content_id,'' ischeck from Content_master cm join Fee_group_master gm on cm.group_id=gm.group_id");
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
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_content.Text == "")
            {
                Alertme("Please Enter Content", "warning");
                txt_content.Focus();
                return;
            }
            if (ddl_group.SelectedItem.Text == "Select")
            {
                Alertme("Please Select Group", "warning");
                ddl_group.Focus();
                return;
            }
            if (btn_Submit.Text == "Add")
            {
                submit_details();
                My.send_data_to_user_log_history(ViewState["Userid"].ToString(),ViewState["Userid"].ToString() + " create content master on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                My.auto_serialS("content_id");
                empty_form();
                bind_grd_view();
            }
            else
            {
                update_update_details();
                empty_form();
                bind_grd_view();
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Content_master where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["content"] = txt_content.Text;
                dr["group_id"] = ddl_group.SelectedValue;
                dr["Ledger"] = ddl_ledger.Text;
                My.exeSql("update dbo.[Fee_master_content_wise] set Ledger='" + ddl_ledger.Text + "' where content_id='" + hd_content_id.Value + "'");
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Fee Type Master  Updated Successfully.", "success");
        }




        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Content_master", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["content"] = txt_content.Text;
            dr["content_id"] = My.auto_serialS("content_id");
            dr["group_id"] = ddl_group.SelectedValue;
            dr["Ledger"] = ddl_ledger.Text;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Content Master Created Successfully.", "success");
        }

        private void empty_form()
        {
            txt_content.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_content_id = (Label)row.FindControl("lbl_content_id");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");

                Label lbl_content = (Label)row.FindControl("lbl_content");
                Label lbl_group_id = (Label)row.FindControl("lbl_group_id");
                Label lbl_ledger = (Label)row.FindControl("lbl_ledger");

                hd_id.Value = lbl_Id.Text;
                hd_content_id.Value = lbl_content_id.Text;
                txt_content.Text = lbl_content.Text;
                ddl_group.SelectedValue = lbl_group_id.Text;
                ddl_ledger.Text = lbl_ledger.Text;

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
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[Content_master] where  id='" + lbl_Id.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Content Master deleted Successfully.", "success");
            bind_grd_view();
        }
    }
}