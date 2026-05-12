using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin
{
    public partial class Student_attendance_log_history_datewise : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddlclass, " Select Course_Name, course_id from Add_course_table order by Position ");
                        ddlsession.Text = My.get_session_id();
                        txt_date.Text = mycode.date();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_attendance_log_history_datewise");
            }
        }

        
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddlclass.SelectedValue + "'  order by Section");

                }
            }
            catch (Exception ex)
            {
            }
        }
        string type;
     
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
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select course", "warning");
                    ddlclass.Focus();
                }

                else if (ddl_section.Text== "Select")
                {
                    Alertme("Please select section", "warning");
                    ddlclass.Focus();
                }

                else if (txt_date.Text == "")
                {
                    Alertme("Please enter date", "warning");
                    txt_date.Focus();

                }
                else
                {
                    find_data();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void find_data()
        {
            DataTable dt = new DataTable();
            string idate = My.toDateTime(txt_date.Text).ToString("yyyyMMdd");
            dt = My.dataTable("select  Row_Number() over(order by id) sl,studentname,admissionserialnumber,class,session,ISNULL(STUFF((SELECT ', ' + cast(format(DateTime,'hh:mm tt') as varchar(50)) FROM Student_Attendance_Log WHERE admissionserialnumber = em.admissionserialnumber and format(DateTime,'yyyyMMdd')='" + idate + "' order by DateTime  FOR XML PATH('')), 1, 1,''), 'Not Available') AS Attendance from admission_registor em where em.Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedValue + "' and em.Status='1'");

            lbl_date.Text = txt_date.Text;
           
            if (dt.Rows.Count == 0)
            {
                lbl_ttl_no_of_emp.Text = "0";
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;
            }
            else
            {
                lbl_ttl_no_of_emp.Text = dt.Rows.Count.ToString();
                grd_attendance.DataSource = dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }
    }
}