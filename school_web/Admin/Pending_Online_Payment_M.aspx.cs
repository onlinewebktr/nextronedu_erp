using school_web.AppCode;
using school_web.Student_Profile.webview.EazyPay;
using school_web.Student_Profile.webview.icic;
using school_web.Student_Profile.webview.RazorPay;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Pending_Online_Payment_M : System.Web.UI.Page
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
                        
                        string pagename_current = "Pending_Online_Payment_M.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        ViewState["flag"] = "0";

                        find_all_pageload();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Pending_Online_Payment_M");
            }
        }

        private void find_all_pageload()
        {
            string query = "Select pt.Session_id,pt.Class_id,pt.Id,pt.Total_pay,pt.Date,pt.ordertrackingid,pt.status,pt.razorpay_order_id,ar.studentname,ar.session,ar.class,ar.Section,ar.rollnumber,ar.fathername,ar.father_mob,ar.admissionserialnumber from Payment_transaction_process pt join admission_registor ar on pt.Admission_no=ar.admissionserialnumber and pt.Session_id=ar.Session_id and pt.Class_id=ar.Class_id  and pt.status='Pending' and pt.Session_id='" + My.get_session_id() + "' and ar.Transfer_Status in ('NT','New')  order by pt.Idate asc";
            Bind_grid_data(query);
        }

        private void Bind_grid_data(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("There are currently no pending online payment records.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        private void find_all(int idate1,int idate2)
        {
            string  query = "Select pt.Session_id,pt.Class_id,pt.Id,pt.Total_pay,pt.Date,pt.ordertrackingid,pt.status,pt.razorpay_order_id,ar.studentname,ar.session,ar.class,ar.Section,ar.rollnumber,ar.fathername,ar.father_mob,ar.admissionserialnumber from Payment_transaction_process pt join admission_registor ar on pt.Admission_no=ar.admissionserialnumber and pt.Session_id=ar.Session_id and pt.Class_id=ar.Class_id and pt.Idate>= " + idate1 + " and pt.Idate<= " + idate2 + " and pt.status='Pending' and ar.Transfer_Status in ('NT','New')  order by pt.Idate asc";
            Bind_grid_data(query);


            
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
        

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string excelname = My.with_excel_name("Pending_list_for_onlinepayment");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename="+ excelname + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_grid.RenderControl(hw);
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            string sdate = txt_s_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_e_date.Text;
            string eday = edate.Substring(0, 2);
            string emonth = edate.Substring(3, 2);
            string eyear = edate.Substring(6, 4);

            int idate = Convert.ToInt32(syear + smonth + sday);
            int idate2 = Convert.ToInt32(eyear + emonth + eday);

            if (idate > idate2)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                find_all(idate, idate2);
            }
        }

        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        protected void lnkupdate_payment_Click(object sender, EventArgs e)
        {
            string order_id = "";
            string Status = "";
            string Payment_order_id = "";
            string error_description = "";
            bool finalcheck = false;
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                finalcheck = true;
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                finalcheck = true;
            }
            if(finalcheck==true)
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_razorpay_order_id = (Label)row.FindControl("lbl_razorpay_order_id");
                Label lbl_ordertrackingid = (Label)row.FindControl("lbl_ordertrackingid");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type_monthly(lbl_Class_id.Text);
                if (ViewState["Getwey_Type"].ToString() != "0")
                {
                   // a1.Visible = true;
                    if (ViewState["Getwey_Type"].ToString() == "Razorpay" || ViewState["Getwey_Type"].ToString() == "Razorpay_new")
                    {
                        try
                        {
                            Dictionary<string, object> dc1 = Re_payment_check_payment.get_payment_status_Razorpay(lbl_admission_no.Text, lbl_Session_id.Text, lbl_razorpay_order_id.Text);
                            order_id = (String)dc1["order_id"];
                            Status = (String)dc1["Status"];
                            Payment_order_id = (String)dc1["Payment_order_id"];
                            error_description= (String)dc1["error_description"];
                             
                        }
                        catch
                        {
                            Status = "The user clicked the back button.";
                            order_id = lbl_razorpay_order_id.Text;
                            Payment_order_id = lbl_ordertrackingid.Text;
                            error_description = "The user clicked the back button.";
                        }

                    }
                    else if (ViewState["Getwey_Type"].ToString() == "EGPay")
                    {
                        Dictionary<string, object> dc1 = Re_payment_check_payment_EZpay.get_payment_status_EZ_Pay(lbl_admission_no.Text, lbl_Session_id.Text, lbl_ordertrackingid.Text);
                        order_id = (String)dc1["order_id"];
                        Status = (String)dc1["Status"];
                        Payment_order_id = (String)dc1["Payment_order_id"];
                        error_description = (String)dc1["error_description"];

                    }
                    else if (ViewState["Getwey_Type"].ToString() == "ICICI")
                    {
                        Dictionary<string, object> dc1 = Re_payment_check_payment_ICIC.get_payment_status_ICIC_Pay(lbl_admission_no.Text, lbl_Session_id.Text, lbl_ordertrackingid.Text);
                        order_id = (String)dc1["order_id"];
                        Status = (String)dc1["Status"];
                        Payment_order_id = (String)dc1["Payment_order_id"];
                        error_description = (String)dc1["error_description"];
                    }


                    if (Status == "Success")
                    {
                        My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Payment_order_id + "',status='Success',Manual_Process_By='" + ViewState["Userid"].ToString() + "',Manual_Process_Date='" + mycode.date() + "',Manual_Process_time='" + mycode.time() + "',Remarks='" + error_description + "'  where  razorpay_order_id='" + order_id + "' ");

                        bool status = mypayment.save_final_payment(lbl_admission_no.Text, lbl_ordertrackingid.Text);
                        if (status == true)
                        {
                            string message = "Student Name:-" + lbl_studentname.Text + " Order Id:-" + lbl_razorpay_order_id.Text + " has made payment successfully";

                            Alertnew(message, "success");
                        }
                        else
                        {
                             
                        }

                    }
                    else
                    {
                        My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Payment_order_id + "',status='"+ Status + "',Manual_Process_By='"+ ViewState["Userid"].ToString() + "',Manual_Process_Date='"+mycode.date()+"',Manual_Process_time='"+mycode.time()+ "',Remarks='"+ error_description + "'  where  razorpay_order_id='" + order_id + "'");

                        string message = "Student Name:-" + lbl_studentname.Text + " Order Id:-" + lbl_razorpay_order_id.Text + " has not made payment successfully, Error message: "+ error_description;

                        Alertnew(message, "warning");

                    }

                   // a1.Visible = false;

                }
                else
                {
                    Alertme("Sorry gateway is not active", "warning");
                }

            }
           else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
            Bind_grid_data(ViewState["query"].ToString());
        }
        private void Alertnew(string message, string panel)
        {

            if (panel == "success")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertSuccess('" + message + "');", true);
            }
            if (panel == "warning")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "alertError('" + message + "');", true);
            }
        }
    }
}