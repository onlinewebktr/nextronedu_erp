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
    public partial class user_details : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["empCoDE"] != null)
                    {
                        string regId = Request.QueryString["empCoDE"];
                        fetch_data(regId);
                    }
                    else
                    {
                        Response.Redirect("Home.aspx");
                    }
                }
            }
            catch (Exception exe)
            {
            }
        }



        private void fetch_data(string regId)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select *,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Emp_Code='" + regId + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lbl_emp_type.Text = dt.Rows[0]["employee_type"].ToString();
                lbl_emp_name.Text = dt.Rows[0]["Employee_Name"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString();

                lbl_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                lbl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_merital_status.Text = dt.Rows[0]["Marital_Status"].ToString();
                lbl_father_name.Text = dt.Rows[0]["Father_Name"].ToString();
                lbl_pan.Text = dt.Rows[0]["Pan"].ToString();
                lbl_address.Text = dt.Rows[0]["Address"].ToString();
                lbl_city.Text = dt.Rows[0]["City"].ToString();
                lbl_pincode.Text = dt.Rows[0]["Pincode"].ToString();
                lbl_state.Text = dt.Rows[0]["State"].ToString();
                lbl_email.Text = dt.Rows[0]["Email"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["Mobile"].ToString();
                lbl_bank_name.Text = dt.Rows[0]["Bank_Name"].ToString();
                lbl_branch.Text = dt.Rows[0]["Branch"].ToString();
                lbl_account_no.Text = dt.Rows[0]["Account_no"].ToString();
                lbl_ifsc_code.Text = dt.Rows[0]["Ifsc"].ToString();
                lbl_micr.Text = dt.Rows[0]["Micr"].ToString();

                lbl_emp_code.Text = dt.Rows[0]["Emp_Code"].ToString();

                lbl_punch_card.Text = dt.Rows[0]["Punch_Card_no"].ToString();
                lbl_official_email.Text = dt.Rows[0]["Official_email_id"].ToString();
                lbl_qualification.Text = dt.Rows[0]["Qualification"].ToString();
                lbl_grade.Text = dt.Rows[0]["Grade_name"].ToString();
                lbl_department.Text = dt.Rows[0]["Designation_name"].ToString();
                lbl_designation.Text = dt.Rows[0]["Designation_name"].ToString();
                lbl_epf_no.Text = dt.Rows[0]["EPF_no"].ToString();
                lbl_join_date.Text = dt.Rows[0]["EPF_Join_date"].ToString();
                lbl_pf_leaving_date.Text = dt.Rows[0]["PF_Leaving_date"].ToString();
                lbl_reson.Text = dt.Rows[0]["PF_leaving_Reagion"].ToString();
                lbl_esic_no.Text = dt.Rows[0]["ESIC_no"].ToString();
                lbl_join_date1.Text = dt.Rows[0]["ESIC_join_date"].ToString();
                lbl_esci_leaving_date.Text = dt.Rows[0]["ESIC_leaving_date"].ToString();
                lbl_reson1.Text = dt.Rows[0]["ESIC_leaving_Reagion"].ToString();
                lbl_doj1.Text = dt.Rows[0]["Date_of_Joining"].ToString();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("employee-list.aspx", false);
        }
    }
}