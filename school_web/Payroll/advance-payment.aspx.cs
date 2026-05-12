using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class advance_payment : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_employee, "select Employee_Name+','+Emp_Code Employee_Name,Employee_id,Grade_id,Emp_Code from dbo.[PRL_Employee_Master] order by Employee_Name asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Adv_payment");
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

        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,format(Payment_Date,'" + My.Format_Sample + "') as date,( select Employee_Name from dbo.[PRL_Employee_Master] em where em.Employee_id=ap.Employee_id ) Employee_Name from PRL_Advance_Payment ap where Employee_id='" + ddl_employee.SelectedValue + "'");
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_employee.Text == "")
                {
                    Alertme("Please Select Employee", "warning");
                    ddl_employee.Focus();
                    return;
                }
                if (txt_advance.Text == "")
                {
                    Alertme("Please Enter Advance Amount", "warning");
                    txt_advance.Focus();
                    return;
                }
                if (txt_advance_adjust.Text == "")
                {
                    Alertme("Please Enter Advance Adjust", "warning");
                    txt_advance_adjust.Focus();
                    return;
                }
                if (ddl_payment_mode.Text == "")
                {
                    Alertme("Please Select Payment Mode", "warning");
                    ddl_payment_mode.Focus();
                    return;
                }
                if (txt_payment_date.Text == "")
                {
                    Alertme("Please Enter Payment Date", "warning");
                    txt_payment_date.Focus();
                    return;
                }

                try
                {
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
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Advance_Payment where Id='" + ViewState["edtID"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = ddl_employee.SelectedValue;
                dr[2] = txt_advance.Text;
                dr[3] = txt_advance_adjust.Text;
                dr["Payment_Mode"] = ddl_payment_mode.Text;
                dr["Payment_Date"] = txt_payment_date.Text;
                dr["Remarks"] = txt_remarks.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Advance Payment  Updated Successfully", "success");

        }

        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Advance_Payment", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = ddl_employee.SelectedValue;
            dr[2] = txt_advance.Text;
            dr[3] = txt_advance_adjust.Text;
            dr["Payment_Mode"] = ddl_payment_mode.Text;
            dr["Payment_Date"] = txt_payment_date.Text;
            dr["Remarks"] = txt_remarks.Text;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Advance Payment Created Successfully", "success");
        }


        private void empty_form()
        {
            txt_prev_advance.Text = "";
            txt_total_advance.Text = "";
            txt_advance.Text = "";
            txt_advance_adjust.Text = "";
            txt_remarks.Text = "";
            txt_payment_date.Text = mycode.date();
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            ddl_employee.Focus();
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adjust_per_month = (Label)row.FindControl("lbl_adjust_per_month");
                 
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_pay_mode = (Label)row.FindControl("lbl_pay_mode");
                Label lbl_remark = (Label)row.FindControl("lbl_remark");
                Label lbl_Employee_id = (Label)row.FindControl("lbl_Employee_id");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_payment_date = (Label)row.FindControl("lbl_payment_date");
                Label lbl_advance = (Label)row.FindControl("lbl_advance");
                ViewState["edtID"] = lbl_Id.Text;
                ddl_employee.SelectedValue = lbl_Employee_id.Text;
                find_pre_adv();

                txt_advance.Text = lbl_advance.Text;
                txt_advance_adjust.Text = lbl_adjust_per_month.Text;

                ddl_payment_mode.Text = lbl_pay_mode.Text;
                txt_payment_date.Text = lbl_payment_date.Text;
                txt_remarks.Text = lbl_remark.Text;

                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {
            }
        }

        protected void ddl_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            find_pre_adv();
        }

        private void find_pre_adv()
        {
            DataTable dt = My.dataTable(" select sum(Advance-Adjusted_Amount) Previous_Advance from dbo.[PRL_Advance_Payment] where Employee_id='" + ddl_employee.SelectedValue + "'");
            txt_prev_advance.Text = My.toDouble(dt.Rows[0]["Previous_Advance"].ToString()).ToString();
            txt_total_advance.Text = (My.toDouble(txt_prev_advance.Text) + My.toDouble(txt_advance.Text)).ToString();
            bind_grd_view();
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Advance_Payment where Id='" + lbl_Id.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Advance Payment deleted Successfully", "success");
            bind_grd_view();
        }

        protected void txt_advance_TextChanged(object sender, EventArgs e)
        {
            txt_total_advance.Text = (My.toDouble(txt_prev_advance.Text) + My.toDouble(txt_advance.Text)).ToString();
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }
    }
}