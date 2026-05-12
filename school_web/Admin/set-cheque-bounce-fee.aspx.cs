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
    public partial class set_cheque_bounce_fee : System.Web.UI.Page
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
                        ViewState["firm_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_session, "select distinct Session,session_id from dbo.[session_details]");
                        ddl_session.Text = My.get_session();
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
            DataTable dt = mycode.FillData("select *,Row_Number() Over(order by id) sl from dbo.[Cheque_Bounce_Fee]");
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
            if (ddl_session.Text == "Select")
            {
                Alertme("Please Select Session", "warning");
                ddl_session.Focus();
                return;
            }
            if (txt_amount.Text == "")
            {
                Alertme("Please Enter Amount", "warning");
                txt_amount.Focus();
                return;
            }
            if (btn_Submit.Text == "Add")
            {
                submit_details();
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
            SqlDataAdapter ad = new SqlDataAdapter("select * from Cheque_Bounce_Fee where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            { 
                dr[3] = txt_amount.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt); 
            Alertme("Cheque Bounce Fee  Updated Successfully.", "success");
        }

        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Cheque_Bounce_Fee where Session_id='" + ddl_session.SelectedValue + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = ddl_session.SelectedItem.Text;
                dr[2] = ddl_session.SelectedValue;
                dr[3] = txt_amount.Text;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt); 
                Alertme("Cheque Bounce Fee Created Successfully.", "success"); 
            }
            else
            {
                Alertme("Cheque bounce fee is already added in this session.", "success"); 
                return;
            }
            
        }

        private void empty_form()
        {
            ddl_session.Enabled = true;
            txt_amount.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_amount = (Label)row.FindControl("lbl_amount");

                hd_id.Value = lbl_Id.Text;
                ddl_session.SelectedValue = lbl_session_id.Text;
                txt_amount.Text = lbl_amount.Text;
                ddl_session.Enabled = false;
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
            SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[Cheque_Bounce_Fee] where  id='" + lbl_Id.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Cheque Bounce Fee deleted Successfully.", "success");
            bind_grd_view();
        }
    }
}