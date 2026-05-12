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
    public partial class Change_Password_User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = My.url();
                a1.HRef = url + "home";
                a2.HRef = url + "home";
                //

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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if(txt_employeecode.Text=="")
            {
                Alertme("Please enter employee iD ", "warning");
            }
            else if (txt_newpaswword.Text == "")
            {
                Alertme("Please enter new password ", "warning");
            }
            else
            {
                try
                {
                    Change_Password();
                }
                catch
                {

                }
                

            }


        }

        private void Change_Password()
        {
            SqlConnection con = new SqlConnection(connection.conn);
            SqlCommand cmd;

            SqlDataAdapter ad = new SqlDataAdapter("select * from user_details where  user_id= @user_id   ", con);
            ad.SelectCommand.Parameters.AddWithValue("@user_id", txt_employeecode.Text);
            DataSet ds = new DataSet();
            ad.Fill(ds, "user_details");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alertme("Invalid Employee ID", "warning");

            }
            else
            {
                string query = "Update user_details set password=@password where    user_id= @user_id ";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@password", txt_newpaswword.Text);
                cmd.Parameters.AddWithValue("@user_id", txt_employeecode.Text);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Alertme("Password has been successfully changed", "success");

                    PayrollMy.execute("update HR_UserProfile set Password='" + txt_newpaswword.Text + "' where EmployeeCode ='" + txt_employeecode.Text + "'");

                    PayrollMy.execute("update HR_Employee_Master set Password='" + txt_newpaswword.Text + "' where Emp_Code ='" + txt_employeecode.Text + "'");

                    FormReset();

                }
            }
        }

        private void FormReset()
        {
            txt_employeecode.Text = "";
           
            txt_newpaswword.Text = "";
        }
    }
}