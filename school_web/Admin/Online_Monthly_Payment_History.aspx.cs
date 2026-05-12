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
    public partial class Online_Monthly_Payment_History : System.Web.UI.Page
    {
        My mycode = new My();

        string razorpay_payment_id = "Select top 1 razorpay_payment_id from Payment_transaction_process where ordertrackingid=t1.Pay_mode_transaction_no and Admission_no=t1.Addmission_no";
        string monthname = "Select top 1 month from Payment_transaction_process where ordertrackingid=t1.Pay_mode_transaction_no and Admission_no=t1.Addmission_no";
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
                        Session["SlipBkSn"] = "MN4";
                        Session["reprintadmission"] = "3";
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
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        hd_session.Value = My.get_session_id();
                        hd_sessions.Value = My.get_session();
                        ViewState["flag"] = "0";
                        find_all();
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

        private void find_all()
        {
            hd_from_date.Value = "0";
            hd_to_date.Value = "0";
            bind_grd_view(" select t1.*,(" + razorpay_payment_id + ") as razorpay_payment_id,(" + monthname + ") as monthname,convert(float, t1.Amount) as Paid_amt,t2.rollnumber,t2.studentname,t2.session,t2.father_mob,t2.class,t2.Session_id from  Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session  where t1.Type='Monthly'   and t2.Session_id='" + hd_session.Value + "' and t1.mode='Online' order by t1.id desc");
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
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose to date", "warning");
                    txt_e_date.Focus();
                }
                else
                {
                    ViewState["flag"] = "1";
                    find_by_date();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_date()
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
                final_find_report_by_date(idate, idate2);
            }
        }

        private void final_find_report_by_date(int idate, int idate2)
        {
            hd_from_date.Value = idate.ToString();
            hd_to_date.Value = idate2.ToString();

            bind_grd_view("select t1.*,(" + razorpay_payment_id + ") as razorpay_payment_id,(" + monthname + ") as monthname,convert(float, t1.Amount) as Paid_amt,t2.rollnumber,t2.studentname,t2.session,t2.father_mob,t2.class,t2.Session_id from  Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session  where t1.Type='Monthly' and t1.mode='Online' and t2.Session_id='" + hd_session.Value + "' and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.Adjust_type is null order by t1.id desc");
        }



        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
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

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }


                string qryS = ""; string qrySS = "";
                if (ViewState["flag"].ToString() == "0")
                {
                    qryS = "select t1.*,(" + razorpay_payment_id + ") as razorpay_payment_id,(" + monthname + ") as monthname,convert(float, t1.Amount) as Paid_amt,t2.rollnumber,t2.studentname,t2.session,t2.father_mob,t2.class,t2.Session_id from  Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session   where t1.Type='Monthly' and t1.mode='Online' and t2.Session_id='" + hd_session.Value + "' and t1.Adjust_type is null";


                    qrySS = "select sum(isnull(convert(float, Amount),0)) as Paid_amt,mode from Student_Payment_History where Type='Monthly' and mode='Online' and Session='" + hd_sessions.Value + "'  group by mode";
                }
                if (ViewState["flag"].ToString() == "1")
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
                    qryS = "select t1.*,(" + razorpay_payment_id + ") as razorpay_payment_id,(" + monthname + ") as monthname,convert(float, t1.Amount) as Paid_amt,t2.rollnumber,t2.studentname,t2.session,t2.father_mob,t2.class,t2.Session_id from  Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session   where t1.Type='Monthly'   and t2.Session_id='" + hd_session.Value + "' and t1.Idate>='" + idate + "' and t1.Idate<='" + idate2 + "' and t1.Adjust_type is null and t1.mode='Online' order by t1.id desc";


                }
                DataTable dtS = mycode.FillData(qryS);
                if (dtS.Rows.Count == 0)
                {
                    lbl_fnl_payble.Text = "00";

                }
                else
                { 
                    String Fnl_paid_amt = Convert.ToDouble(dtS.Compute("SUM(Paid_amt)", string.Empty)).ToString("0.00");
                    lbl_fnl_payble.Text = Fnl_paid_amt; 
                } 
            }
        }




        protected string Getamount_comma_seperated(string amount)
        {
            try
            {
                string amt = String.Format("{0:n}", Convert.ToDouble(amount));
                return amt;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            { 
                if (ViewState["Is_Print"].ToString() == "1")
                { 
                    ((Panel)e.Item.FindControl("Panel2_print")).Visible = true; 
                }
                else
                {
                    ((Panel)e.Item.FindControl("Panel2_print")).Visible = false; 
                }
                string value1 = ((Label)e.Item.FindControl("lbl_Amount1")).Text;

                decimal value;
                if (decimal.TryParse(value1, out value))
                {
                    ((Label)e.Item.FindControl("lbl_Amount1")).Text = value.ToString("0.00");
                }
            }
        }

        //===========================



        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_grid.RenderControl(hw);
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