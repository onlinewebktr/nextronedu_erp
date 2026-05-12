using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class reprint_payment_voucher : System.Web.UI.Page
    {
        My mycode = new My();

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

                        ViewState["flag"] = "0";
                        find_voucehr_page();

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' order by Section");
                        find_student();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void find_voucehr_page()
        {
            try
            {
                DataTable dt = mycode.FillData("select Payment_voucher_type from Firm_Details");
                if (dt.Rows[0]["Payment_voucher_type"].ToString() == "True")
                {
                    ViewState["VoucherPage"] = "payment-voucher1.aspx";
                }
                else
                {
                    ViewState["VoucherPage"] = "payment-voucher.aspx";
                }
            }
            catch (Exception ex)
            {
                ViewState["VoucherPage"] = "payment-voucher.aspx";
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

                else
                {
                    find_student();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_student()
        {
            string qry = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_month.SelectedItem.Text == "ALL")
                {
                    qry = "select t1.*,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 where t1.Session_id='" + ddlsession.SelectedValue + "'";
                }
                else
                {
                    qry = "select t1.*,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 where t1.Session_id='" + ddlsession.SelectedValue + "'  and  t1.Months='" + ddl_month.SelectedItem.Text + "'";
                }
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_month.SelectedItem.Text == "ALL")
                    {
                        qry = "select t1.*,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "'";
                    }
                    else
                    {
                        qry = "select t1.*,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' ";
                    }
                }
                else
                {
                    if (ddl_month.SelectedItem.Text == "ALL")
                    {
                        qry = "select t1.*,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 where t1.Session_id='" + ddlsession.SelectedValue + "'  and t1.Class_id='" + ddlclass.SelectedValue + "' and t1.Section='" + ddl_section.SelectedItem.Text + "'";
                    }
                    else
                    {
                        qry = "select t1.*,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 where t1.Session_id='" + ddlsession.SelectedValue + "'  and t1.Class_id='" + ddlclass.SelectedValue + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t1.Months='" + ddl_month.SelectedItem.Text + "'";
                    }
                }
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_print.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_print.Visible = true;
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' order by Section");
                find_student();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("please select session.", "warning");
                return;
            }
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                ddlclass.Focus();
                Alertme("please select class.", "warning");
                return;
            }
            if (ddl_month.SelectedItem.Text == "ALL")
            {
                ddlclass.Focus();
                Alertme("please select month.", "warning");
                return;
            }

            if (ddl_section.SelectedItem.Text == "ALL")
            {
                try
                {
                    DataTable dt = mycode.FillData("select Payment_voucher_type from Firm_Details");
                    if (dt.Rows[0]["Payment_voucher_type"].ToString() == "True")
                    {
                        if (ddl_month.SelectedItem.Text == "ALL")
                        {
                            Response.Redirect("slip/reprint-payment-voucher-classwise.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=0&month=0", false);
                        }
                        else
                        {
                            Response.Redirect("slip/reprint-payment-voucher-classwise.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=0&month=" + ddl_month.SelectedItem.Text + "", false);
                        }
                    }
                    else
                    {
                        if (ddl_month.SelectedItem.Text == "ALL")
                        {
                            Response.Redirect("slip/reprint-payment-voucher-all.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=0&month=0", false);
                        }
                        else
                        {
                            Response.Redirect("slip/reprint-payment-voucher-all.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=0&month=" + ddl_month.SelectedItem.Text + "", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("slip/reprint-payment-voucher-all.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=0&month=0", false);
                }
            }
            else
            {
                try
                {
                    DataTable dt = mycode.FillData("select Payment_voucher_type from Firm_Details");
                    if (dt.Rows[0]["Payment_voucher_type"].ToString() == "True")
                    {
                        if (ddl_month.SelectedItem.Text == "ALL")
                        {
                            Response.Redirect("slip/reprint-payment-voucher-classwise.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=" + ddl_section.SelectedItem.Text + "&month=0", false);
                        }
                        else
                        {
                            Response.Redirect("slip/reprint-payment-voucher-classwise.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=" + ddl_section.SelectedItem.Text + "&month=" + ddl_month.SelectedItem.Text, false);
                        }
                    }
                    else
                    {
                        if (ddl_month.SelectedItem.Text == "ALL")
                        {
                            Response.Redirect("slip/reprint-payment-voucher-all.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=" + ddl_section.SelectedItem.Text + "&month=0", false);
                        }
                        else
                        {
                            Response.Redirect("slip/reprint-payment-voucher-all.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=" + ddl_section.SelectedItem.Text + "&month=" + ddl_month.SelectedItem.Text, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("slip/reprint-payment-voucher-all.aspx?Session=" + ddlsession.SelectedValue + "&Class=" + ddlclass.SelectedValue + "&Section=" + ddl_section.SelectedItem.Text + "&month=0", false);
                }
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_slip_id = ((Label)e.Item.FindControl("lbl_slip_id")) as Label;
                HtmlAnchor rpcard_link = (HtmlAnchor)e.Item.FindControl("rpcard_link");

                rpcard_link.HRef = "slip/" + ViewState["VoucherPage"].ToString() + "?admissionno=" + lbl_admissionserialnumber.Text + "&sessionid=" + lbl_session_id.Text + "&classid=" + lbl_class_id.Text + "&Slip_no=" + lbl_slip_id.Text;
            }
        }

    }
}