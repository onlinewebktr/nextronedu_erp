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
    public partial class over_time_work : System.Web.UI.Page
    {
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

                        mycode.bind_all_ddl_with_id(ddl_employee, "select Employee_Name+','+Emp_Code Employee_Name,Employee_id from dbo.[PRL_Employee_Master] where Status='Active'");
                        mycode.bind_all_ddl_with_id(ddl_employee1, "select Employee_Name+','+Emp_Code Employee_Name,Employee_id from dbo.[PRL_Employee_Master] where Status='Active'");
                        txt_date.Text = mycode.date();
                        txt_start_date.Text = mycode.date();
                        txt_end_date.Text = mycode.date();
                        txt_in_time.Text = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm tt");
                        txt_out_time.Text = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm tt");
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Department_Master");
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


        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select row_number() OVER(order by tw.id)as sl,  em.Employee_Name,tw.Date,tw.In_Time,tw.Out_Time,tw.working_hour FROM PRL_Over_Time_Working tw JOIN PRL_Employee_Master em ON tw.Employee_id=em.Employee_id");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (ddl_employee.SelectedItem.Text == "Select")
            {
                Alertme("Please select employee", "warning");
                ddl_employee.Focus();
                return;
            }
            if (txt_date.Text == "")
            {
                Alertme("Please choose date.", "warning");
                txt_date.Focus();
                return;
            }
            if (txt_in_time.Text == "")
            {
                Alertme("Please enter in time.", "warning");
                txt_in_time.Focus();
                return;
            }
            if (txt_out_time.Text == "")
            {
                Alertme("Please enter out time.", "warning");
                txt_out_time.Focus();
                return;
            }

            submit_details();
            bind_grd_view();
            Alertme("Over time work saved successfully", "success");
            bind_grd_view();
        }

        private void submit_details()
        {
            string idate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Over_Time_Working where Employee_id='" + ddl_employee.SelectedValue + "' and Idate='" + idate + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Over_Time_Working");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Employee_id"] = ddl_employee.SelectedValue;
                dr["Date"] = txt_date.Text;
                dr["Idate"] = idate;
                dr["In_Time"] = txt_in_time.Text;
                dr["Out_Time"] = txt_out_time.Text;
                dr["working_hour"] = (Convert.ToDateTime(txt_out_time.Text) - Convert.ToDateTime(txt_in_time.Text)).TotalHours;
                dr["day"] = "";
                dr["firm"] = "1";
                dr["user_id"] = ViewState["Userid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["In_Time"] = txt_in_time.Text;
                    dr["Out_Time"] = txt_out_time.Text;
                    dr["working_hour"] = (Convert.ToDateTime(txt_out_time.Text) - Convert.ToDateTime(txt_in_time.Text)).TotalHours;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_employee1.SelectedItem.Text == "Select")
                {
                    Alertme("Please select employee.", "warning");
                    ddl_employee1.Focus();
                    return;
                }
                if (txt_start_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_start_date.Focus();
                    return;
                }
                if (txt_end_date.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_end_date.Focus();
                    return;
                }
                string s_idate = My.toDateTime(txt_start_date.Text).ToString("yyyyMMdd");
                string e_idate = My.toDateTime(txt_end_date.Text).ToString("yyyyMMdd");
                string emp_id = ddl_employee1.SelectedValue.ToString();

                DataTable dt = mycode.FillData("select row_number() OVER(order by tw.id)as sl,  em.Employee_Name,tw.Date,tw.In_Time,tw.Out_Time,tw.working_hour FROM PRL_Over_Time_Working tw JOIN PRL_Employee_Master em ON tw.Employee_id=em.Employee_id where tw.Employee_id='" + emp_id + "' and Idate>='" + s_idate + "' and Idate<='" + e_idate + "'");
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}