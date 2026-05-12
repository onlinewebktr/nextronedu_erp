using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class modified_bill : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        txt_s_date.Text = mycode.date();
                        //txt_e_date.Text = mycode.date();

                        mycode.bind_all_ddl_with_id_cap_All(ddl_session, "select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        Bind_data_pageload_date_wise();
                        find_firm_details();

                        string pagename_current = "fee-report.aspx";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Fee_Collection_Report");
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

        private void Bind_data_pageload_date_wise()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose date", "warning");
                txt_s_date.Focus();
            }
            //else if (txt_e_date.Text == "")
            //{
            //    Alertme("Please choose to date", "warning");
            //    txt_e_date.Focus();
            //}
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text); 
                string sdate = txt_s_date.Text;
                string sday = sdate.Substring(0, 2);
                string smonth = sdate.Substring(3, 2);
                string syear = sdate.Substring(6, 4); 

                int idate1 = Convert.ToInt32(syear + smonth + sday);
                final_find_report_by_date(idate1); 
            }
        }

        private void final_find_report_by_date(int idate1)
        {
            string qrySS = "";
            if (ddl_session.SelectedItem.Text == "ALL")
            {
                qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Idate>='" + idate1 + "' group by mode";
                bind_grd_view("select (select top 1 name from user_details where user_id=t1.user_id) as ModiBy,t1.Created_date,t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id,t1.Amount,t1.Type,t1.parameter_New,t1.Pay_mode_transaction_no from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Session=t2.session join Payment_Mode_Master pmm on pmm.Type_Mode=t1.mode where t1.Created_idate='" + idate1 + "' and t1.Idate!='" + idate1 + "' order by pmm.Position,t1.Created_idate asc", qrySS);
            }
            else
            {
                qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Session='" + ddl_session.SelectedItem.Text + "' and Created_idate='" + idate1 + "' and t1.Idate!='" + idate1 + "' group by mode";
                bind_grd_view("select (select top 1 name from user_details where user_id=t1.user_id) as ModiBy,t1.Created_date,t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t1.Acamedic_Semester_Id,t2.Session_id,t1.Class_id,t1.Amount,t1.Type,t1.parameter_New,t1.Pay_mode_transaction_no from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber  and t1.Session=t2.session join Payment_Mode_Master pmm on pmm.Type_Mode=t1.mode where t2.Session_id='" + ddl_session.SelectedValue + "' and t1.Created_idate='" + idate1 + "' and t1.Idate!='" + idate1 + "' order by pmm.Position,t1.Created_idate asc", qrySS);
            }
        }


        private void bind_grd_view(string query, string qrySS)
        {
            lbl_date_period.Text = "Date : " + txt_s_date.Text;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_fnl_paid.Text = "0.00";
            }
            else
            {
                double total = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total = total + My.toDouble(dt.Rows[i]["Amount"].ToString());
                }
                //String Fnl_paid_amt = Convert.ToDouble(dt.Compute("SUM(Amount)", string.Empty)).ToString();

                lbl_fnl_paid.Text = total.ToString("0.00");
                rd_view.DataSource = dt;
                rd_view.DataBind();

                //rp_mode.Visible = false;
                //DataTable dtSS = mycode.FillData(qrySS);
                //if (dtSS.Rows.Count > 0)
                //{
                //    rp_mode.DataSource = dtSS;
                //    rp_mode.DataBind();
                //    rp_mode.Visible = true;
                //}
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_pageload_date_wise();
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=modified-receipt.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
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
    }
}