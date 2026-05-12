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
    public partial class salary_chart : System.Web.UI.Page
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
                        if (Session["sl_year"] != null || Session["sl_month"] != null)
                        {
                            ddl_month.SelectedIndex = Convert.ToInt32(Session["sl_month"].ToString());
                            ddl_year.SelectedIndex = Convert.ToInt32(Session["sl_year"].ToString());
                            bind_data_on_grid();
                        }
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
                grd_salary.DataSource = null;
                grd_salary.DataBind();
                Alertme("Salary not calculated for the month of " + month + "/" + year, "warning");
            }
            else
            {
                grd_salary.DataSource = dt.DefaultView;
                grd_salary.DataBind();
            }
        }

        protected void lnk_print_slip_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_employee_id = (Label)row.FindControl("lbl_employee_id");
            Label lbl_month = (Label)row.FindControl("lbl_month");
            Label lbl_year = (Label)row.FindControl("lbl_year");
            Response.Redirect("salary-slip.aspx?empID=" + lbl_employee_id.Text + "&month=" + lbl_month.Text + "&year=" + lbl_year.Text, false);
        }
    }
}