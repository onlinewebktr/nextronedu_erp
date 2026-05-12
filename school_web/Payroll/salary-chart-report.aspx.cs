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
    public partial class salary_chart_report : System.Web.UI.Page
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
                bind_data_on_grid();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_data_on_grid()
        {
            string month, year;
            DataTable dt;
            month = (ddl_month.SelectedIndex + 1).ToString(); ;
            year = ddl_year.Text;
            //  txt_period.Text = "Salary Chart for the month of : " + month + "/" + year;
            String query = @" select dm.name Designation ,em.*,es.*,name,Date_of_birth dob   from dbo.[PRL_Employee_Salary_chart] es join PRL_Employee_Master em on es.Employee_id=em.Employee_id join  PRL_Designation_Master dm on dm.description_id=em.Designation_id  where month='" + month + "' and year='" + year + "'";
            dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                pnl_grids.Visible = false;
                Alertme("Salary not calculated for the month of " + month + "/" + year, "warning");
            }
            else
            {
                pnl_grids.Visible = true;
                grd_salary.DataSource = dt.DefaultView;
                grd_salary.DataBind();



                lbl_frm_name.Text = Session["firm_name"].ToString();
                lbl_date_period.Text = "Salary Chart for the month of : " + month + "/" + year;

                DataTable dtS = My.dataTable("select row_number() over(order by em.id) sl, em.Employee_Name,(select name from dbo.[PRL_Designation_Master] dm  where dm.description_id=em.Designation_id) Designation,es.* from dbo.[PRL_Employee_Salary_chart] es join PRL_Employee_Master em on es.Employee_id=em.Employee_id where month='" + month + "' and year='" + year + "'");

                lbl_emp_pf.Text = "PF " + My.employer_pf + "% on Basic Salary (Employer)";
                txt_emp_pf.Text = My.toDouble(dtS.Compute("sum(com_PF)", "")).ToString();
                lbl_emp_esi.Text = "ESI " + My.employer_esi + "% on Basic Salary (Employer)";
                txt_emp_esi.Text = My.toDouble(dtS.Compute("sum(com_ESI)", "")).ToString();
                lbl_em_pf.Text = "PF 12% on Basic Salary (Employee)";
                txt_em_pf.Text = My.toDouble(dtS.Compute("sum(emp_PF)", "")).ToString();
                lbl_em_esi.Text = "ESI 0.755% on Basic Salary (Employee)";
                txt_em_esi.Text = My.toDouble(dtS.Compute("sum(emp_esi)", "")).ToString();

                txt_net_payable.Text = My.toDouble(dtS.Compute("sum(Net_Salary)", "")).ToString();
                txt_bt.Text = My.toDouble(dtS.Compute("sum(Through_Bank)", "")).ToString();
                txt_check_amount.Text = My.toDouble(dtS.Compute("sum(Through_Cheque)", "")).ToString();
                txt_cash.Text = My.toDouble(dtS.Compute("sum(Through_Cash)", "")).ToString();
            }
        }
    }
}