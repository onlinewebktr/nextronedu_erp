using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class Salary_Report : System.Web.UI.Page
    {
        My imp = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = My.url();
                a1.HRef = url + "home";
                a2.HRef = url + "home";

                imp.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Month_Id asc");
                string a = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("MM");
                ddl_month.SelectedValue = a;
                imp.bind_ddl_year(ddlyear);
                ddlyear.Text = imp.year();


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

        #region region 
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_month.SelectedItem.Text == "Select")
            {
                Alertme("Please  select month name ", "warning");
            }
            else if (ddlyear.SelectedItem.Text == "")
            {
                Alertme("Please  select year_name ", "warning");
            }

            else
            {
                Bind_data();


            }

        }

        private void Bind_data()
        {
            string employee_id = "0";
            string Calculation_Id = "0";

            DataTable fdt = new DataTable();

            string qry = "";
            qry = " select ROW_NUMBER() OVER (ORDER BY Id) AS Sl,  EmployeeName as EMPLOYEE_NAME ,EmployeeCode as EMPLOYEE_CODE,Month as MONTH,Year as YEAR,Days_in_Month as DAYS_IN_MONTH,Working_Days_In_a_Month as WORKING_DAYS_IN_A_MONTH,Net_Wroked_Days as PAID_DAYS,Calculation_Id,EmployeeId,Calculeted_Gross as totaleraning,Calculeted_Deduction as totaldeduction,Calculeted_Net as netpay  from dbo.[HR_Salary_Calculation_Table] where   Month='" + ddl_month.SelectedItem.Text + "' and Year='" + ddlyear.Text + "'   order by Id asc";

            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;

            if (srowcount > 0)
            {
                pnl_grid.Visible = true;

                DataTable dtincome = imp.FillData(" select   distinct IncomeHead from dbo.[Hr_Employee_Salary_Income_Head_Wise] where    Month='" + ddl_month.SelectedItem.Text + "' and   Year='" + ddlyear.Text + "' and IncomeHead not in ('Special Allowance','Special Allowances') ");
                if (dtincome.Rows.Count == 0)
                {

                }
                for (int ixi = 0; ixi < dtincome.Rows.Count; ixi++)
                {
                    string IncomeHead = dtincome.Rows[ixi]["IncomeHead"].ToString();
                    dt.Columns.Add(IncomeHead, Type.GetType("System.Double"));
                    fdt.Columns.Add(IncomeHead, Type.GetType("System.Double"));
                }



                DataTable dtDeduction = imp.FillData(" select   distinct Deduction_Head from dbo.[Hr_Employee_Salary_Deduction_Head_Wise] where    Month='" + ddl_month.SelectedItem.Text + "' and   Year='" + ddlyear.Text + "' ");
                if (dtDeduction.Rows.Count == 0)
                {

                }
                for (int ixd = 0; ixd < dtDeduction.Rows.Count; ixd++)
                {
                    string Deduction_Head = dtDeduction.Rows[ixd]["Deduction_Head"].ToString();
                    dt.Columns.Add(Deduction_Head, Type.GetType("System.Double"));
                    fdt.Columns.Add(Deduction_Head, Type.GetType("System.Double"));
                }

                dt.Columns.Add("TOTAL EARNINGS", Type.GetType("System.Double"));
                dt.Columns.Add("Total Deductions", Type.GetType("System.Double"));
                dt.Columns.Add("Net Pay", Type.GetType("System.Double"));


                foreach (DataRow dr in dt.Rows)
                {
                    employee_id = dr["EmployeeId"].ToString();
                    Calculation_Id = dr["Calculation_Id"].ToString();


                    DataTable dtincome1 = imp.FillData(" select   distinct IncomeHead from dbo.[Hr_Employee_Salary_Income_Head_Wise] where    Month='" + ddl_month.SelectedItem.Text + "' and   Year='" + ddlyear.Text + "' and IncomeHead not in ('Special Allowance','Special Allowances')");
                    if (dtincome1.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int ixi = 0; ixi < dtincome1.Rows.Count; ixi++)
                        {
                            string IncomeHead = dtincome1.Rows[ixi]["IncomeHead"].ToString();
                            dr[IncomeHead] = find_IncomeHead(IncomeHead, employee_id, Calculation_Id);
                        }

                    }
                    DataTable dtDeduction1 = imp.FillData("  select   distinct Deduction_Head from dbo.[Hr_Employee_Salary_Deduction_Head_Wise] where    Month='" + ddl_month.SelectedItem.Text + "' and   Year='" + ddlyear.Text + "' ");
                    if (dtDeduction.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int ixd1 = 0; ixd1 < dtDeduction.Rows.Count; ixd1++)
                        {


                            string Deduction_Head = dtDeduction.Rows[ixd1]["Deduction_Head"].ToString();

                            dr[Deduction_Head] = find_DeductionHead(Deduction_Head, employee_id, Calculation_Id);
                        }
                    }


                    dr["TOTAL EARNINGS"] = dr["totaleraning"].ToString(); ;
                    dr["TOTAL DEDUCTIONS"] = dr["totaldeduction"].ToString();
                    dr["NET PAY"] = dr["netpay"].ToString();
                    grvExcelData.DataSource = dt.DefaultView;
                    grvExcelData.DataBind();

                    btn_excels.Visible = true;
                    print1.Visible = true;
                }

            }
            else
            {
                pnl_grid.Visible = false;
                Alertme("Date not found.", "warning");
                grvExcelData.DataSource = null;
                grvExcelData.DataBind();
                btn_excels.Visible = false;
                print1.Visible = false;

            }
        }

        private object find_DeductionHead(string deduction_Head, string employee_id, string calculation_Id)
        {
            DataTable feedt = new DataTable();

            double headFee = 0.00;
            feedt = My.dataTable(" select * from dbo.[Hr_Employee_Salary_Deduction_Head_Wise] where Employee_Id='" + employee_id + "' and Month='" + ddl_month.SelectedItem.Text + "' and Year='" + ddlyear.Text + "' and Calculation_Id='" + calculation_Id + "' and Deduction_Head='" + deduction_Head + "'");
            if (feedt.Rows.Count.ToString() != "0")
            {
                double Employee_Deduction = My.toDouble(feedt.Rows[0]["Employee_Deduction"].ToString());
                ///  double Employer_Deduction = My.toDouble(feedt.Rows[0]["Employer_Deduction"].ToString());
                headFee = Employee_Deduction;// + Employer_Deduction;
            }
            else
            {
                headFee = 0.00;
            }

            return headFee.ToString("0.00");

        }

        private object find_IncomeHead(string incomeHead, string employee_id, string Calculation_Id)
        {
            DataTable feedt = new DataTable();

            double headFee = 0.00;
            feedt = My.dataTable(" select * from dbo.[Hr_Employee_Salary_Income_Head_Wise] where Employee_Id='" + employee_id + "' and Month='" + ddl_month.SelectedItem.Text + "' and Year='" + ddlyear.Text + "' and Calculation_Id='" + Calculation_Id + "' and IncomeHead='" + incomeHead + "'");
            if (feedt.Rows.Count.ToString() != "0")
            {
                headFee = My.toDouble(feedt.Rows[0]["NetValue"].ToString());
            }
            else
            {
                headFee = 0;
            }

            return headFee.ToString("0.00");
        }
        #endregion


        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvExcelData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = true;

        }

        protected void grvExcelData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 8; i < e.Row.Cells.Count; i++)
                    {
                        decimal value;
                        if (decimal.TryParse(e.Row.Cells[i].Text.Trim(), out value))
                        {
                            e.Row.Cells[i].Text = value.ToString("0.00");
                        }
                    }
                }


            }
            catch
            {

            }
        }
        #region excel data
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    pnl_grid.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion
    }
}