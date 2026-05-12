using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class create_income_head : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master]");
                        mycode.bind_all_ddl_with_id(ddl_income_type, "select Income_Type,Income_Type_id from dbo.[PRL_Income_Type_Master]");

                        //ddl_formula_type.ItemsSource = new string[] { "Percent", "Rupees" };
                        //ddl_formula_type.SelectedIndex = 0;
                        txt_applicable_from.Text = mycode.date();
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
            DataTable dt = mycode.FillData("select am.*,gm.grade_name,itp.Income_Type Income_Type_Name from PRL_Allowance_Master am join  PRL_Grade_Master gm on am.Grade_id = gm.grade_id join PRL_Income_Type_Master itp on am.Income_Type=itp.Income_Type_id  order by am.Grade_id,am.Serial");
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
            if (ddl_grade.SelectedItem.Text == "Select")
            {
                Alertme("Please Select Grade", "warning");
                ddl_grade.Focus();
                return;
            }
            if (txt_income_head.Text == "")
            {
                Alertme("Please Enter Income Head", "warning");
                txt_income_head.Focus();
                return;
            }
            if (ddl_income_type.SelectedItem.Text == "Select")
            {
                Alertme("Please Enter Income Type", "warning");
                ddl_income_type.Focus();
                return;
            }
            if (pnl_is_basic.Visible == true)
            {
                if (chk_variable_head.Checked == false)
                {
                    if (txt_formula.Text == "")
                    {
                        Alertme("Please Enter Income Value", "warning");
                        txt_formula.Focus();
                        return;
                    }

                    if (My.toDouble(txt_formula.Text) <= 0)
                    {
                        Alertme("Please Enter valid Income Value", "warning");
                        txt_formula.Focus();
                        return;
                    }
                }
            }
            else
            {
                txt_formula.Text = "0";
            }
            if (txt_applicable_from.Text == "")
            {
                Alertme("Please Enter Applicable From", "warning");
                txt_applicable_from.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                submit_details();
                empty_form();
                bind_grd_view();
                int gradeid = Convert.ToInt32(ddl_grade.SelectedValue);
                bind_based_on(gradeid);
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
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Allowance_Master where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = ddl_grade.SelectedValue;
                dr[2] = txt_income_head.Text;
                dr[3] = ddl_income_type.SelectedValue;
                dr[4] = chk_variable_head.Checked;
                dr[5] = chk_pf.Checked;
                dr[6] = chk_esi.Checked;
                dr[7] = chk_ptax.Checked;
                dr[8] = chk_ot.Checked;
                dr[9] = chk_leave_encash.Checked;
                dr[10] = chk_lwf.Checked;
                dr[11] = My.toDouble(txt_formula.Text);
                dr[12] = ddl_based_on.Text;
                dr[15] = txt_applicable_from.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Income Head  Updated Successfully", "success");
        }

        ArrayList ar = new ArrayList();
        string cond = "";
        private void bind_based_on(int grade_id)
        {
            DataTable dt = My.dataTable(" select Income_head from PRL_Allowance_Master where Grade_id='" + grade_id + "' and Income_Type!='" + ddl_income_type.SelectedValue + "' " + cond);
            ar = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                ar.Add(dr["Income_head"].ToString());
            }
            ddl_based_on.DataSource = ar;
            ddl_based_on.DataBind();
        }


        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Allowance_Master", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = ddl_grade.SelectedValue;
            dr[2] = txt_income_head.Text;
            dr[3] = ddl_income_type.SelectedValue;
            dr[4] = chk_variable_head.Checked;
            dr[5] = chk_pf.Checked;
            dr[6] = chk_esi.Checked;
            dr[7] = chk_ptax.Checked;
            dr[8] = chk_ot.Checked;
            dr[9] = chk_leave_encash.Checked;
            dr[10] = chk_lwf.Checked;
            dr[11] = txt_formula.Text;
            dr[12] = ddl_based_on.Text;
            dr[13] = "";
            dr[14] = "";
            dr[15] = txt_applicable_from.Text;
            dr["Serial"] = auto_serial_Income_head_serial();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Income Head Created Successfully", "success");
        }

        private object auto_serial_Income_head_serial()
        {
            int result = 0;
            DataTable dt = My.dataTable(" select * from PRL_Allowance_Master where Grade_id='" + ddl_grade.SelectedValue + "'");
            int count = dt.Rows.Count;
            result = count + 1;
            return result;
        }


        private void empty_form()
        {
            txt_income_head.Text = "";
            txt_formula.Text = "";

            chk_pf.Checked = false;
            chk_esi.Checked = false;
            chk_ptax.Checked = false;
            chk_ot.Checked = false;
            chk_leave_encash.Checked = false;
            chk_lwf.Checked = false;
            txt_applicable_from.Text = mycode.date();
            bind_data();

            ddl_grade.Focus();
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        private void bind_data()
        {
            int gradeid = Convert.ToInt32(ddl_grade.SelectedValue);
            bind_based_on(gradeid);
            if (ddl_income_type.SelectedValue.ToString() == "1")
            {
                if (txt_income_head.Text != "")
                {
                    ar.Add(txt_income_head.Text);
                }
            }
            ddl_based_on.DataSource = ar;
            ddl_based_on.DataBind();
            //ddl_based_on.SelectedIndex = 0;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;
                DataTable dt = mycode.FillData("select * from PRL_Allowance_Master where Id='" + lbl_Id.Text + "' ");

                ddl_grade.SelectedValue = dt.Rows[0]["Grade_id"].ToString();
                txt_income_head.Text = dt.Rows[0]["Income_head"].ToString();

                chk_variable_head.Checked = My.toBool(dt.Rows[0]["Variable_Head"].ToString());
                ddl_income_type.SelectedValue = dt.Rows[0]["Income_Type"].ToString();
                bind_formula_type();
                chk_pf.Checked = My.toBool(dt.Rows[0][5].ToString());
                chk_esi.Checked = My.toBool(dt.Rows[0][6].ToString());
                chk_ptax.Checked = My.toBool(dt.Rows[0][7].ToString());
                chk_ot.Checked = My.toBool(dt.Rows[0][8].ToString());
                chk_leave_encash.Checked = My.toBool(dt.Rows[0][9].ToString());
                chk_lwf.Checked = My.toBool(dt.Rows[0][10].ToString());
                txt_formula.Text = dt.Rows[0][11].ToString();
                ddl_based_on.Text = dt.Rows[0][12].ToString();
                txt_applicable_from.Text = dt.Rows[0][15].ToString();

                int serial = My.toIntS(dt.Rows[0]["Serial"].ToString());
                if (serial == 1)
                {
                    cond = " and Serial<='" + serial + "'";
                }
                else
                {
                    cond = " and Serial<'" + serial + "'";
                }
                bind_based_on(My.toIntS(ddl_grade.SelectedValue));
                cond = "";
                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch(Exception ex)
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            My.exeSql("delete from PRL_Allowance_Master where Id='" + lbl_Id.Text + "'");
            Alertme("Income head deleted Successfully", "success");
            bind_grd_view();
        }

        protected void ddl_income_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_formula_type();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_formula_type()
        {
            if (ddl_income_type.SelectedItem != null)
            {
                if (ddl_income_type.SelectedValue == "1")
                {
                    pnl_is_basic.Visible = false;
                    //chk_variable_head.Checked = true;
                    txt_formula.Text = "";
                    ddl_based_on.DataSource = null;
                }
                else
                {
                    pnl_is_basic.Visible = true;
                    chk_variable_head.Checked = false;
                    bind_data();
                }
            }
        }

        protected void chk_variable_head_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_variable_head.Checked == true)
            {
                pnl_is_veriable.Visible = false;
                txt_formula.Text = "0";
            }
            else
            {
                pnl_is_veriable.Visible = true;
                txt_formula.Text = "";
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk_pf = ((CheckBox)e.Item.FindControl("chk_pf")) as CheckBox;
                CheckBox chk_esi = ((CheckBox)e.Item.FindControl("chk_esi")) as CheckBox;
                CheckBox chk_ptax = ((CheckBox)e.Item.FindControl("chk_ptax")) as CheckBox;
                CheckBox chk_otax = ((CheckBox)e.Item.FindControl("chk_otax")) as CheckBox;

                Label lbl_pf = ((Label)e.Item.FindControl("lbl_pf")) as Label;
                Label lbl_esi = ((Label)e.Item.FindControl("lbl_esi")) as Label;
                Label lbl_ptac = ((Label)e.Item.FindControl("lbl_ptac")) as Label;
                Label lbl_otax = ((Label)e.Item.FindControl("lbl_otax")) as Label;

                chk_pf.Checked = My.toBool(lbl_pf.Text);
                chk_esi.Checked = My.toBool(lbl_esi.Text);
                chk_ptax.Checked = My.toBool(lbl_ptac.Text);
                chk_otax.Checked = My.toBool(lbl_otax.Text);
            }
        }
    }
}