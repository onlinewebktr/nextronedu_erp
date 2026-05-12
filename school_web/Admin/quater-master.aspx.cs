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
    public partial class quater_master : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_start_month, "select Month,Month_Id from Month_Index order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_end_month, "select Month,Month_Id from Month_Index order by Position asc");
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Quater_Master");
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
            DataTable dt = mycode.FillData("select *,(select top 1 Month from Month_Index where Month_Id=Quater_master.Start_month_id) as Start_month,(select top 1 Month from Month_Index where Month_Id=Quater_master.End_month_id) as End_month from dbo.[Quater_master] where Session_id='" + My.get_session_id() + "' order by Quater_no asc");
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
            txt_quater_no.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_quater_no.Text == "")
            {
                Alertme("Please Enter quater no.", "warning");
                txt_quater_no.Focus();
                return;
            }
            if (ddl_start_month.SelectedItem.Text == "Select")
            {
                Alertme("Please select start month.", "warning");
                ddl_start_month.Focus();
                return;
            }
            if (ddl_end_month.SelectedItem.Text == "Select")
            {
                Alertme("Please select end month.", "warning");
                ddl_end_month.Focus();
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
            SqlDataAdapter ad = new SqlDataAdapter(" select * from dbo.[Quater_master] where  id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["Quater_no"] = txt_quater_no.Text;
                dr["Start_month_id"] = ddl_start_month.SelectedValue;
                dr["End_month_id"] = ddl_end_month.SelectedValue;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Quarter Details Updated Successfully", "success");
        }




        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter(" select * from dbo.[Quater_master] where Session_id='" + My.get_session_id() + "' and Quater_no='" + txt_quater_no.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            if (dt.Rows.Count == 0)
            {
                dr["Session_id"] = My.get_session_id();
                dr["Quater_no"] = txt_quater_no.Text;
                dr["Quater_id"] = My.auto_serialS("group_id");
                dr["Start_month_id"] = ddl_start_month.SelectedValue;
                dr["End_month_id"] = ddl_end_month.SelectedValue;
                dr["Created_date"] = mycode.date();
                dr["Created_idate"] = mycode.idate();
                dr["Created_by"] = ViewState["firm_id"].ToString();
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Quarter details had been created successfully", "success");
            }
            else
            {
                Alertme("Quarter already added.", "warning");
            }
        }

        private void empty_form()
        {
            txt_quater_no.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Quater_no = (Label)row.FindControl("lbl_Quater_no");

                Label lbl_start_month_id = (Label)row.FindControl("lbl_start_month_id");
                Label lbl_end_month_id = (Label)row.FindControl("lbl_end_month_id");

                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;


                txt_quater_no.Text = lbl_Quater_no.Text;
                ddl_start_month.SelectedValue = My.getMonthS_twoDigit(lbl_start_month_id.Text);
                ddl_end_month.SelectedValue = My.getMonthS_twoDigit(lbl_end_month_id.Text);
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
            Label lbl_quarter_id = (Label)row.FindControl("lbl_quarter_id");

            DataTable dsgdt = My.dataTable("select Id from Fine_master where Session_id ='" + My.get_session_id() + "' and Quater_id='" + lbl_quarter_id.Text + "'");
            if (dsgdt.Rows.Count == 0)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[Quater_master] where  id='" + lbl_Id.Text + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr.Delete();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Quarter deleted Successfully", "success");
                bind_grd_view();
            }
            else
            {
                Alertme("You can't delete this quarter because there is a data associated with fine master.", "warning");
            }
        }
    }
}