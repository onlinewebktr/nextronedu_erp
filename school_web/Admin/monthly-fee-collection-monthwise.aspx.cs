using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class monthly_fee_collection_monthwise : System.Web.UI.Page
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
                        Session["reprintadmission"] = "4";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        find_firm_details();

                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by Session desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "select Course_Name,course_id from Add_course_table order by Position asc");
                        My.bind_ddl_select(ddl_month, "select Month from Month_Index order by Position asc");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
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
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose session.", "warning");
                    ddl_session.Focus();
                }
                else if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose month.", "warning");
                    ddl_month.Focus();
                }
                else
                {
                    ViewState["flag"] = "1";
                    find_report();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void find_report()
        {
            string qry = "";
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                lbl_class22.Text = "Monthly Fee Collection Report of Session : " + ddl_session.SelectedItem.Text + " & Month : " + ddl_month.SelectedItem.Text;
                qry = "select *,(select top 1 studentname from admission_registor where session=t.session and admissionserialnumber=t.adno) as Student_name,(select top 1 class from admission_registor where session=t.session and admissionserialnumber=t.adno) as Class_name,(select top 1 Section from admission_registor where session=t.session and admissionserialnumber=t.adno) as Section_name,(select top 1 mode from Student_Payment_History where Session=t.session and Addmission_no=t.adno and Slip_no=t.slipno) as Payment_mode from (select session,class,adno,slipno,Month,Date,Idate,sum(convert(float, paid)) as Paid from Monthly_Fee_Collection_Slip where (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and session='" + ddl_session.SelectedItem.Text + "' and Month='" + ddl_month.SelectedItem.Text + "' group by session,slipno,class,adno,Month,Date,Idate) t where Paid>0 order by Idate asc";
            }
            else
            {
                lbl_class22.Text = "Monthly Fee Collection Report of Session : " + ddl_session.SelectedItem.Text + " & Class : " + ddl_class.SelectedItem.Text + " & Month : " + ddl_month.SelectedItem.Text;
                qry = "select *,(select top 1 studentname from admission_registor where session=t.session and admissionserialnumber=t.adno) as Student_name,(select top 1 class from admission_registor where session=t.session and admissionserialnumber=t.adno) as Class_name,(select top 1 Section from admission_registor where session=t.session and admissionserialnumber=t.adno) as Section_name,(select top 1 mode from Student_Payment_History where Session=t.session and Addmission_no=t.adno and Slip_no=t.slipno) as Payment_mode from (select session,class,adno,slipno,Month,Date,Idate,sum(convert(float, paid)) as Paid from Monthly_Fee_Collection_Slip where (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and session='" + ddl_session.SelectedItem.Text + "' and Month='" + ddl_month.SelectedItem.Text + "' and class='" + ddl_class.SelectedValue + "' group by session,slipno,class,adno,Month,Date,Idate) t where Paid>0 order by Idate asc";
            }


            btn_excels.Visible = false;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
                lbl_fnl_paid.Text = Convert.ToDouble(dt.Compute("SUM(Paid)", string.Empty)).ToString("0.00");
            }
        }






        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    string file_name = My.with_excel_name("Fee-report"); 
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + file_name + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
    }
}