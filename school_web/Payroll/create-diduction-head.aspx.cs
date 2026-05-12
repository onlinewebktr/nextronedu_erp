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
    public partial class create_diduction_head : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master]");
                        mycode.bind_all_ddl_with_id(ddl_deduction_type, "select deduction_type,deduction_type_id from PRL_Deduction_Type_Master");
                        ddl_formula_type.DataSource = new string[] { "Percent", "Rupees" };
                        ddl_formula_type.SelectedIndex = 0;
                        ddl_grade.Focus();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Designation_Master");
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
            DataTable dt = mycode.FillData("select dm.*,gm.grade_name,dtp.deduction_type deduction_type_name from PRL_Deduction_Master dm join PRL_Grade_Master gm on dm.Grade_id = gm.grade_id join PRL_Deduction_Type_Master dtp on dm.Deduction_Type=dtp.deduction_type_id");
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
            if (txt_deduction_head.Text == "")
            {
                Alertme("Please Enter Deduction Head", "warning");
                txt_deduction_head.Focus();
                return;
            }
            if (ddl_deduction_type.Text == "")
            {
                Alertme("Please Select Deduction Type", "warning");
                ddl_deduction_type.Focus();
                return;
            }
            if (chk_variable_head.Checked == false)
            {
                if (txt_deduction_value.Text == "")
                {
                    Alertme("Please Enter Deduction Value", "warning");
                    txt_deduction_value.Focus();
                    return;
                }
            }
            if (txt_applicable_from.Text == "")
            {
                Alertme("Please Enter Applicable from", "warning");
                txt_applicable_from.Focus();
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
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Deduction_Master where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["Grade_id"] = ddl_grade.SelectedValue;
                dr["Deduction_head"] = txt_deduction_head.Text;
                dr["Deduction_Type"] = ddl_deduction_type.SelectedValue;
                dr["Variable_head"] = chk_variable_head.Checked;
                dr["Formula"] = My.toDouble(txt_deduction_value.Text);
                dr["Formula_type"] = ddl_formula_type.Text;
                dr["Applicable_from"] = txt_applicable_from.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Deduction Master  Updated Successfully", "success");
        }

        private void submit_details()
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Deduction_Master", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "PRL_Deduction_Master");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Grade_id"] = ddl_grade.SelectedValue;
            dr["Deduction_head"] = txt_deduction_head.Text;
            dr["Deduction_Type"] = ddl_deduction_type.SelectedValue;
            dr["Variable_head"] = chk_variable_head.Checked;
            dr["Formula"] = My.toDouble(txt_deduction_value.Text);
            dr["Formula_type"] = ddl_formula_type.Text;
            dr["Applicable_from"] = txt_applicable_from.Text;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Deduction Master Created Successfully.", "success");
        }

        private void empty_form()
        {
            txt_deduction_head.Text = "";
            txt_deduction_value.Text = "";
            ddl_formula_type.Text = "";
            ddl_formula_type.Text = "";
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
                hd_id.Value = lbl_Id.Text;

                DataTable dt = mycode.FillData("select * from PRL_Deduction_Master where Id='" + lbl_Id.Text + "' ");
                ddl_grade.SelectedValue = dt.Rows[0]["Grade_id"].ToString();
                txt_deduction_head.Text = dt.Rows[0]["Deduction_head"].ToString();
                ddl_deduction_type.SelectedValue = dt.Rows[0]["Deduction_Type"].ToString();
                chk_variable_head.Checked = My.toBool(dt.Rows[0]["Variable_head"].ToString());
                txt_deduction_value.Text = dt.Rows[0]["Formula"].ToString();
                ddl_formula_type.Text = dt.Rows[0]["Formula_type"].ToString();
                txt_applicable_from.Text = dt.Rows[0]["Applicable_from"].ToString();
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
            My.exeSql("delete from PRL_Deduction_Master where Id='" + lbl_Id.Text + "'");
            Alertme("Deduction Master deleted Successfully.", "success");
            bind_grd_view();
        }

        protected void chk_variable_head_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_variable_head.Checked == true)
            {
                pnl_is_variable.Visible = false;
                txt_deduction_value.Text = "0";
            }
            else
            {
                pnl_is_variable.Visible = true;
                txt_deduction_value.Text = "";
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk_v_head = ((CheckBox)e.Item.FindControl("chk_v_head")) as CheckBox; 
                Label lbl_v_head = ((Label)e.Item.FindControl("lbl_v_head")) as Label;

                chk_v_head.Checked = My.toBool(lbl_v_head.Text); 
            }
        }
    }
}