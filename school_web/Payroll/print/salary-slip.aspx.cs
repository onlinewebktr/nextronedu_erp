 
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cms_web.Payroll.print
{
    public partial class salary_slip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["empID"] != null && Request.QueryString["month"] != null && Request.QueryString["year"] != null)
                    {
                        string empID = Request.QueryString["empID"];
                        string month = Request.QueryString["month"];
                        string year = Request.QueryString["year"];
                        fetch_slry_slip(empID, month, year);
                    }

                    else
                    {
                    }
                }
                else
                {
                    hd_order_id.Value = "0";
                }
            }
            catch (Exception exe)
            {
            }
        }

        private void fetch_slry_slip(string empID, string month, string year)
        {
            DataTable dt;
            String query = @" select (select top 1 name from PRL_Department_Master where department_id=em.Department_id) as Dep_name,dm.name Designation ,em.*,es.*,name,Date_of_birth dob   from dbo.[PRL_Employee_Salary_chart] es join PRL_Employee_Master em on es.Employee_id=em.Employee_id join  PRL_Designation_Master dm on dm.description_id=em.Designation_id  where month='" + month + "' and year='" + year + "' and es.Employee_id='" + empID + "'";
            dt = My.dataTable(query);
            lbl_firm_name.Text = Session["firm_name"].ToString();
            lbl_frm_location.Text = Session["firmLocation"].ToString();
            lbl_firm_name1.Text = Session["firm_name"].ToString();
            Image1.ImageUrl = Session["firmLogo"].ToString();
            lbl_e_branch.Text = "Branch : " + Session["firm_name"].ToString();

            DateTime date = new DateTime(My.toIntS(year), My.toIntS(month), 1);
            lbl_salary_date.Text = "Salary Slip for the month of " + date.ToString("MMM yyyy");
            lbl_name.Text = "Name : " + dt.Rows[0]["Employee_Name"].ToString();
            lbl_emp_id.Text = "EMP. Code : " + dt.Rows[0]["Emp_Code"].ToString();
            lbl_dep.Text = "Department : " + dt.Rows[0]["Dep_name"].ToString();

            lbl_desig.Text = "Designation : " + dt.Rows[0]["name"].ToString();
            lbl_pan.Text = "PAN : " + dt.Rows[0]["Pan"].ToString();

            lbl_days_in_month.Text = "Days in Month : " + dt.Rows[0]["Days_in_month"].ToString();

            lbl_wisking_days.Text = "Working Days : " + dt.Rows[0]["total_working_days"].ToString();
            lbl_pf_ac.Text = "PF No. : " + dt.Rows[0]["EPF_no"].ToString();
            lbl_paid_days.Text = "Paid Days : " + dt.Rows[0]["total"].ToString();
            lbl_esic_no.Text = "ESIC No. : " + dt.Rows[0]["ESIC_no"].ToString();

            txt_total_earning.Text = dt.Rows[0]["Total_Salary"].ToString();
            txt_total_deduction.Text = (My.toDouble(dt.Rows[0]["emp_PF"]) + My.toDouble(dt.Rows[0]["emp_esi"]) + My.toDouble(dt.Rows[0]["p_tax"]) + My.toDouble(dt.Rows[0]["tds"]) + My.toDouble(dt.Rows[0]["other_deduct"]) + My.toDouble(dt.Rows[0]["advance"].ToString())).ToString();
            if (My.toDouble(dt.Rows[0]["advance"]) > 0)
            {
                //txt_net_title.Text = "Net Salary=(A)-(B)-(C)";
                pnl_advane.Visible = Visible;
                txt_advance_adjust.Text = dt.Rows[0]["advance"].ToString();
            }
            else
            {
                pnl_advane.Visible = false;
                // txt_net_title.Text = "Net Salary=(A)-(B)";
            }
            txt_net_salary.Text = dt.Rows[0]["Net_Salary"].ToString();
            lbl_rupees_in_words.Text = "RUPEES : " + find_amount_words(dt.Rows[0]["Net_Salary"].ToString());

            rp_income.DataSource = My.dataTable(" select   * from dbo.[PRL_Emp_Salary_Income_head_wise] where month='" + My.toDouble(dt.Rows[0]["Month"]).ToString("00") + "' and year ='" + dt.Rows[0]["year"] + "' and Employee_Code='" + dt.Rows[0]["Employee_id"] + "';").DefaultView;
            rp_deduction.DataSource = My.dataTable(" select   * from dbo.[PRL_Emp_Salary_deduction_head_wise] where month='" + My.toDouble(dt.Rows[0]["Month"]).ToString("00") + "' and year ='" + dt.Rows[0]["year"] + "' and Employee_Code='" + dt.Rows[0]["Employee_id"] + "';").DefaultView;

            rp_income.DataBind();
            rp_deduction.DataBind();
        }


        My mycode = new My();
        private string find_amount_words(string p)
        {
            if (p == "")
            {
                p = "0";
                string amount_in_words = "Zero";
                return amount_in_words;

            }
            else
            {
                Double number = Double.Parse(p);
                number = Math.Round(number, 0);

                string amount_in_words = mycode.AmountInWords(number.ToString());
                return amount_in_words;
            }
        }
    }
}