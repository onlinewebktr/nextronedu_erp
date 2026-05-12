using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class user_full_details : System.Web.UI.Page
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
            string password = "Select top 1 password from user_details where user_id=PRL_Employee_Master.Emp_Code";
            SqlDataAdapter ad = new SqlDataAdapter("select *,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name,(select top 1 Signature from user_details where user_id=PRL_Employee_Master.Emp_Code) as Signature,(" + password + ") as password from dbo.[PRL_Employee_Master] where Emp_Code='" + regId + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("home.aspx");
            }
            else
            {
                img_studentimages.ImageUrl = dt.Rows[0]["Employee_image"].ToString();
                img_signature.ImageUrl = dt.Rows[0]["Signature"].ToString();
                lbl_emp_type.Text = dt.Rows[0]["employee_type"].ToString();
                lbl_emp_name.Text = dt.Rows[0]["Employee_Name"].ToString();
                lbl_gender.Text = dt.Rows[0]["Gender"].ToString();

                lbl_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                lbl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                lbl_religion.Text = dt.Rows[0]["Religion"].ToString();
                lbl_merital_status.Text = dt.Rows[0]["Marital_Status"].ToString();
                lbl_email.Text = dt.Rows[0]["Email"].ToString();
                lbl_mobile_no.Text = dt.Rows[0]["Mobile"].ToString();
                lbl_emp_code.Text = dt.Rows[0]["Emp_Code"].ToString();
                lbl_pwd.Text= dt.Rows[0]["password"].ToString(); 



            }
        }

       
    }
}