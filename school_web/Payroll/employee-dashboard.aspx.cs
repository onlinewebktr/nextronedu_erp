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
    public partial class employee_dashboard : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    mycode.bind_all_ddl_with_id_All(ddl_gender, "select DISTINCT Gender,Gender as GenderS from PRL_Employee_Master");
                    mycode.bind_all_ddl_with_id_All(ddl_gender_dep, "select DISTINCT Gender,Gender as GenderS from PRL_Employee_Master");
                    hd_gender.Value = "0";
                    hd_gender_dep.Value = "0";
                    totla_employee_data();
                    
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "DefaultPage");
                }
            }
        }
        My mycod = new My();
        private void totla_employee_data()
        {

            DataTable dt = mycod.FillData("select count(Employee_Name) total_emp from dbo.[PRL_Employee_Master] where Status='Active'");
            if (dt.Rows.Count == 0)
            {
                lbl_total_employee.Text ="0";
                lbl_Employees1.Text = "0";
                lbltotal_employee_2.Text = "0";
                lbl_total_employee_grad.Text = "0";
            }
            else
            {
                lbl_total_employee.Text = dt.Rows[0]["total_emp"].ToString();
                lbl_Employees1.Text = dt.Rows[0]["total_emp"].ToString();
                lbltotal_employee_2.Text = dt.Rows[0]["total_emp"].ToString();
                lbl_total_employee_grad.Text = dt.Rows[0]["total_emp"].ToString();
            }
        }

        protected void ddl_gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            hd_gender.Value = ddl_gender.SelectedValue;
        }

        protected void ddl_gender_dep_SelectedIndexChanged(object sender, EventArgs e)
        {
            hd_gender_dep.Value = ddl_gender_dep.SelectedValue;
        }
    }
}