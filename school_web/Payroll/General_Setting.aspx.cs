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
    public partial class General_Setting : System.Web.UI.Page
    {
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
        My mycode = new My();
        UsesCode code = new UsesCode();
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
                        code.bind_ddl(ddl_session, "Select distinct session  from globle_data_session_wise  order by session");
                        Bind_olddata();
                    }
                }
            }
            catch
            {

            }
        }

        private void Bind_olddata()
        {
            DataTable dt = code.FillTable("select * from Payroll_Setting");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                if (dt.Rows[0]["Emp_Code_Prefix"].ToString() == "True")
                {
                    rd_disconnect_device_Yes.Checked = true;
                }
                else
                {
                    rd_disconnect_device_No.Checked = false;

                }
                txt_emp_prefix.Text = dt.Rows[0]["Emp_Code_Prefix"].ToString();
                txt_employecode.Text = dt.Rows[0]["Emp_Code"].ToString();
                txt_employe_postfix.Text = dt.Rows[0]["Emp_Code_Postfix"].ToString();
                ddl_workingshift.Text = dt.Rows[0]["Working_Sift"].ToString();
                txt_workinghours.Text = dt.Rows[0]["hrname"].ToString();
                txt_positionname.Text = dt.Rows[0]["Position_Name"].ToString();
                txt_loiserialprifix.Text = dt.Rows[0]["LOI_Serial_Prifix"].ToString();
                txt_loiserialpostfix.Text = dt.Rows[0]["LOI_Serial_Postfix"].ToString();
                ddl_session.SelectedValue = dt.Rows[0]["Session"].ToString();
                txt_limit_fullday.Text = dt.Rows[0]["limit_fullday"].ToString();
                txt_limit_halfday.Text = dt.Rows[0]["limit_halfday"].ToString();
            }
        }
       
        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            if (ddl_session.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_emp_prefix.Text == "")
            {
                Alertme("Please enter employee prefix", "warning");
            }
            else if (txt_employecode.Text == "")
            {
                Alertme("Please enter employee code", "warning");
            }
            else if (txt_employe_postfix.Text == "")
            {
                Alertme("Please enter employee postfix", "warning");
            }
            else if (txt_workinghours.Text == "")
            {
                Alertme("Please enter working hours", "warning");

            }
            else if (txt_hrname.Text == "")
            {
                Alertme("Please enter HR name", "warning");
            }
            else
            {
                string type = "False";
                if (rd_disconnect_device_Yes.Checked == true)
                {
                    type = "True";

                }
                else
                {
                    type = "False";

                }

                try
                {
                    if (My.dataTable("select * from Payroll_Setting").Rows.Count == 0)
                    {


                        SqlCommand cmd;
                        string query = "INSERT INTO Payroll_Setting (Emp_Code_Prefix,Emp_Code,Emp_Code_Postfix,Working_Sift,Disconnect_Device,hrname,Position_Name,LOI_Serial_Prifix,LOI_Serial_Postfix,Session,Working_hours,limit_fullday,limit_halfday) values (@Emp_Code_Prefix,@Emp_Code,@Emp_Code_Postfix,@Working_Sift,@Disconnect_Device,@hrname,@Position_Name,@LOI_Serial_Prifix,@LOI_Serial_Postfix,@Session,@Working_hours,@limit_fullday,@limit_halfday)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Emp_Code_Prefix", txt_emp_prefix.Text);
                        cmd.Parameters.AddWithValue("@Emp_Code", txt_employecode.Text);
                        cmd.Parameters.AddWithValue("@Emp_Code_Postfix", txt_employe_postfix.Text);
                        cmd.Parameters.AddWithValue("@Working_Sift", ddl_workingshift.Text);
                        cmd.Parameters.AddWithValue("@Disconnect_Device", type);
                        cmd.Parameters.AddWithValue("@Disconnect_Device", type);
                        cmd.Parameters.AddWithValue("@hrname", txt_hrname.Text);
                        cmd.Parameters.AddWithValue("@Position_Name", txt_positionname.Text);
                        cmd.Parameters.AddWithValue("@LOI_Serial_Prifix", txt_loiserialprifix.Text);
                        cmd.Parameters.AddWithValue("@LOI_Serial_Postfix", txt_loiserialpostfix.Text);
                        cmd.Parameters.AddWithValue("@Session", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Working_hours", txt_hrname.Text);
                        cmd.Parameters.AddWithValue("@limit_fullday", txt_limit_fullday.Text);
                        cmd.Parameters.AddWithValue("@limit_halfday", txt_limit_halfday.Text);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Setting has been added", "success");
                            Bind_olddata();
                        }


                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "Update Payroll_Setting set Emp_Code_Prefix=@Emp_Code_Prefix,Emp_Code=@Emp_Code,Emp_Code_Postfix=@Emp_Code_Postfix,Working_Sift=@Working_Sift,Disconnect_Device=@Disconnect_Device,hrname=@hrname,Position_Name=@Position_Name,LOI_Serial_Prifix=@LOI_Serial_Prifix,LOI_Serial_Postfix=@LOI_Serial_Postfix,Session=@Session,Working_hours=@Working_hours,limit_fullday=@limit_fullday,limit_halfday=@limit_halfday";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Emp_Code_Prefix", txt_emp_prefix.Text);
                        cmd.Parameters.AddWithValue("@Emp_Code", txt_employecode.Text);
                        cmd.Parameters.AddWithValue("@Emp_Code_Postfix", txt_employe_postfix.Text);
                        cmd.Parameters.AddWithValue("@Working_Sift", ddl_workingshift.Text);
                        cmd.Parameters.AddWithValue("@Disconnect_Device", type);
                        cmd.Parameters.AddWithValue("@hrname", txt_hrname.Text);
                        cmd.Parameters.AddWithValue("@Position_Name", txt_positionname.Text);
                        cmd.Parameters.AddWithValue("@LOI_Serial_Prifix", txt_loiserialprifix.Text);
                        cmd.Parameters.AddWithValue("@LOI_Serial_Postfix", txt_loiserialpostfix.Text);
                        cmd.Parameters.AddWithValue("@Session", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Working_hours", txt_hrname.Text);
                        cmd.Parameters.AddWithValue("@limit_fullday", txt_limit_fullday.Text);
                        cmd.Parameters.AddWithValue("@limit_halfday", txt_limit_halfday.Text);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Setting has been updated", "success");
                            Bind_olddata();
                        }
                    }
                }
                catch
                {
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("General_Setting.aspx", false);
        }
    }
}