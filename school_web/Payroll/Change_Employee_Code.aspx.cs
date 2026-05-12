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
    public partial class Change_Employee_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = My.url();
                a1.HRef = url + "home";
                a2.HRef = url + "home";
                ViewState["Userid"] = "";

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
            if (txt_employee_code.Text == "")
            {
                Alertme("Please enter employee code ", "warning");
            }
            else
            {
                string query = "Select *   from HR_Employee_Master where Emp_Code='" + txt_employee_code.Text + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    employee_update_new.Visible = false;
                    employee_details.Visible = false;
                    Alertme("Sorry your enter employee code is not exist ", "warning");
                }
                else
                {
                    lbl_employeecode.Text = dt.Rows[0]["Emp_Code"].ToString();
                    lbl_Employee_name.Text= dt.Rows[0]["Employee_Name"].ToString();
                    lbl_mon_no.Text= dt.Rows[0]["Emp_Code"].ToString();

                    employee_update_new.Visible = true;
                    employee_details.Visible = true;




                }
            }

        }

        protected void btn_update_employee_code_Click(object sender, EventArgs e)
        {
            if (txt_employee_code.Text == "")
            {
                Alertme("Please enter employee code ", "warning");
            }
            else if (txt_new_employee_code.Text == "")
            {
                Alertme("Please enter new employee code ", "warning");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_Update_Employee_code";
                cmd.Parameters.AddWithValue("@Old_employee_code", txt_employee_code.Text);
                cmd.Parameters.AddWithValue("@New_employee_code", txt_new_employee_code.Text.Trim());
                cmd.Parameters.AddWithValue("@updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Status", "UpdateEmployeeCode");
                if (UsesCode.InsertUpdateData_sp(cmd))
                {

                    Alertme("Your employee code has been updated ", "success");
                    employee_update_new.Visible = false;
                    employee_details.Visible = false;
                    txt_employee_code.Text = "";
                    txt_new_employee_code.Text = "";
                }
            }
        }
    }
}