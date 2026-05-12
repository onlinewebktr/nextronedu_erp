using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class view_salary_headwise : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id(ddl_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master] order by grade_name asc");
                        ddl_month.DataSource = My.bindMonthName();
                        ddl_month.Text = DateTime.Now.ToString("MMMM");
                        ddl_month.DataBind();
                        ddl_year.DataSource = My.bindYear();
                        ddl_year.Text = DateTime.Now.ToString("yyyy");
                        ddl_year.DataBind();
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_grade.SelectedItem.Text == "Select")
                {
                    Alertme("Please select grade.", "warning");
                    ddl_grade.Focus();
                }
                else
                {
                    bind_data_on_grid();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_data_on_grid()
        {
            DataTable dt = new DataTable();
            int year = My.toIntS(ddl_year.Text);
            string month = (ddl_month.SelectedIndex + 1).ToString("00");
            if (My.dataTable("select id   from dbo.[PRL_Employee_Salary_chart]   where month='" + month + "' and year='" + year + "'").Rows.Count == 0)
            {
                Alertme("Salary not calculated for the month of " + month + "/" + year, "warning");
                return;
            }
            String query = "";
            try
            {
                DataTable inc_dt = My.dataTable("select Replace(Income_head,' ','_') Income_head,Income_Type from dbo.[PRL_Allowance_Master] where Grade_id='" + ddl_grade.SelectedValue + "'");
                DataTable _dt = My.dataTable("select Replace(Deduction_head,' ','_') Deduction_head,Deduction_Type from dbo.[PRL_Deduction_Master] where Grade_id='" + ddl_grade.SelectedValue + "'");
                query = @"select em.Employee_Name,dm.name Designation,es.Days_in_month Month_days,es.total_working_days  Working_days,es.salary_calculate_on,es.present_days Present,es.half_days,es.leave,es.over_time_working_days as Over_Time,es.total,";
                foreach (DataRow ir in inc_dt.Rows)
                {
                    query += " isnull((select top 1 Income_Value from dbo.[PRL_Emp_Salary_Income_head_wise]  where Employee_Code=es.Employee_id and Month='" + month + "' and Year='" + year + "' and income_type='" + ir["Income_Type"].ToString() + "' ),0) " + ir["Income_head"].ToString() + ",";
                }
                foreach (DataRow dr in _dt.Rows)
                {
                    query += " isnull((select top 1 Deduction_value from dbo.[PRL_Emp_Salary_deduction_head_wise] where Employee_Code=es.Employee_id and Deduction_Type='" + dr["Deduction_Type"].ToString() + "' and Month='" + month + "' and Year='" + year + "'),0) " + dr["Deduction_head"].ToString() + ",";
                }
                query += "es.Salary,es.Salary_Basic_fix,es.Salary_Basic,es.Gross_Salary,es.Total_Salary,es.emp_PF PF_Employee,es.emp_esi ESI_Employee,es.emp_contribution Employee_Contribution,es.com_PF PF_Employer,es.com_ESI ESI_Employer,es.com_contribution Employer_Contribution,es.Salary_After_PF_ESI,es.Advance,es.Net_Salary,es.Through_Bank,es.Through_Cheque,es.Through_Cash   from dbo.[PRL_Employee_Salary_chart] es join PRL_Employee_Master em on es.Employee_id=em.Employee_id join  PRL_Designation_Master dm on dm.description_id=em.Designation_id  where month='" + month + "' and year='" + year + "' and em.Grade_id='" + ddl_grade.SelectedValue + "'";

                dt = My.dataTable(query);
                if (dt.Rows.Count > 0)
                {
                    lnk_excel.Visible = true;
                    pnl_grids.Visible = true;
                    grd_salary.DataSource = dt.DefaultView;
                    grd_salary.DataBind();
                    lbl_ttl_no.Text = "Total : " + dt.Rows.Count.ToString();
                }
                else
                {
                    lnk_excel.Visible = false;
                    pnl_grids.Visible = false;
                    grd_salary.DataSource = null;
                    grd_salary.DataBind();
                    lbl_ttl_no.Text = "Total : 0";
                }
            }
            catch (Exception ex)
            {
            } 
        }

        protected void lnk_excel_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            int year = My.toIntS(ddl_year.Text);
            string month = (ddl_month.SelectedIndex + 1).ToString("00");
            if (My.dataTable("select id   from dbo.[PRL_Employee_Salary_chart]   where month='" + month + "' and year='" + year + "'").Rows.Count == 0)
            {
                Alertme("Salary not calculated for the month of " + month + "/" + year, "warning");
                return;
            }
            String query = "";
            try
            {
                DataTable inc_dt = My.dataTable("select Replace(Income_head,' ','_') Income_head,Income_Type from dbo.[PRL_Allowance_Master] where Grade_id='" + ddl_grade.SelectedValue + "'");
                DataTable _dt = My.dataTable("select Replace(Deduction_head,' ','_') Deduction_head,Deduction_Type from dbo.[PRL_Deduction_Master] where Grade_id='" + ddl_grade.SelectedValue + "'");
                query = @"select em.Employee_Name,dm.name Designation,es.Days_in_month Month_days,es.total_working_days  Working_days,es.salary_calculate_on,es.present_days Present,es.half_days,es.leave,es.over_time_working_days as Over_Time,es.total,";
                foreach (DataRow ir in inc_dt.Rows)
                {
                    query += " isnull((select top 1 Income_Value from dbo.[PRL_Emp_Salary_Income_head_wise]  where Employee_Code=es.Employee_id and Month='" + month + "' and Year='" + year + "' and income_type='" + ir["Income_Type"].ToString() + "' ),0) " + ir["Income_head"].ToString() + ",";
                }
                foreach (DataRow dr in _dt.Rows)
                {
                    query += " isnull((select top 1 Deduction_value from dbo.[PRL_Emp_Salary_deduction_head_wise] where Employee_Code=es.Employee_id and Deduction_Type='" + dr["Deduction_Type"].ToString() + "' and Month='" + month + "' and Year='" + year + "'),0) " + dr["Deduction_head"].ToString() + ",";
                }
                query += "es.Salary,es.Salary_Basic_fix,es.Salary_Basic,es.Gross_Salary,es.Total_Salary,es.emp_PF PF_Employee,es.emp_esi ESI_Employee,es.emp_contribution Employee_Contribution,es.com_PF PF_Employer,es.com_ESI ESI_Employer,es.com_contribution Employer_Contribution,es.Salary_After_PF_ESI,es.Advance,es.Net_Salary,es.Through_Bank,es.Through_Cheque,es.Through_Cash   from dbo.[PRL_Employee_Salary_chart] es join PRL_Employee_Master em on es.Employee_id=em.Employee_id join  PRL_Designation_Master dm on dm.description_id=em.Designation_id  where month='" + month + "' and year='" + year + "' and em.Grade_id='" + ddl_grade.SelectedValue + "'";

                dt = My.dataTable(query);
                export_to_excel(dt, "Salary-Chart-Headwise");
            }
            catch (Exception ex)
            {
            } 
        }


        private void export_to_excel(DataTable dt, string file)
        {

            string FileName = file + DateTime.Now + ".xls";

            string attachment = "attachment; filename=" + FileName;

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";

            Response.ContentEncoding = System.Text.Encoding.Unicode;

            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {

                Response.Write(tab + dc.ColumnName);

                tab = "\t";

            }

            Response.Write("\n");

            int i;

            foreach (DataRow dr in dt.Rows)
            {

                tab = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {

                    Response.Write(tab + dr[i].ToString());

                    tab = "\t";

                }

                Response.Write("\n");

            }

            Response.End();

        }
    }
}