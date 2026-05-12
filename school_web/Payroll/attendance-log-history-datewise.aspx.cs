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
    public partial class attendance_log_history_datewise : System.Web.UI.Page
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


                        txt_date.Text = mycode.date();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Attendance Log History Datewise");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
                if (txt_date.Text == "")
                {
                    Alertme("Please enter date", "warning");
                    txt_date.Focus();
                    return;
                }
                find_data();
            }
            catch (Exception ex)
            {
            }
        }


        private void find_data()
        {
            DataTable dt = new DataTable();
            string idate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
            dt = My.dataTable("select  Row_Number() over(order by id) sl,Employee_Name,Emp_Code,ISNULL(STUFF((SELECT ', ' + cast(format(DateTime,'hh:mm tt') as varchar(50)) FROM PRL_Attendance_Log WHERE Employee_id = em.Emp_Code and format(DateTime,'yyyyMMdd')='" + idate + "' order by DateTime  FOR XML PATH('')), 1, 1,''), 'Not Available') AS Attendance from PRL_Employee_Master em where em.Status='Active'");

            lbl_date.Text = txt_date.Text;
            lbl_ttl_no_of_emp.Text = dt.Rows.Count.ToString();
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;
            }
            else
            {
                 
                grd_attendance.DataSource = dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }
    }
}