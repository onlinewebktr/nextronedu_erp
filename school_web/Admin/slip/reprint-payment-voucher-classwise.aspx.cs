using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class reprint_payment_voucher_classwise : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Session"] != null || Request.QueryString["Class"] != null || Request.QueryString["Section"] != null || Request.QueryString["month"] != null)
                    {
                        ViewState["sessionid"] = Request.QueryString["Session"];
                        ViewState["classid"] = Request.QueryString["Class"];
                        ViewState["Section"] = Request.QueryString["Section"];
                        ViewState["Month"] = Request.QueryString["month"];
                        ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());
                        bind_vouchers();
                    }
                    else
                    {
                        Response.Redirect("../home.aspx", false);
                    }
                }
            }
        }

        private void bind_vouchers()
        {
            string qrys = "";
            if (ViewState["Section"].ToString() == "0")
            {
                if (ViewState["Month"].ToString() == "0")
                {
                    qrys = "select t1.*,t3.Affiliation,t3.school_no,t3.logo,t3.address1,t3.email,t3.website,t3.contact_no,t3.firm_name,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 join Firm_Details t3 on t1.Firm_id=t3.firm_id where t1.Session_id='" + ViewState["sessionid"].ToString() + "' and t1.Class_id='" + ViewState["classid"].ToString() + "'";
                }
                else
                {
                    qrys = "select t1.*,t3.Affiliation,t3.school_no,t3.logo,t3.address1,t3.email,t3.website,t3.contact_no,t3.firm_name,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 join Firm_Details t3 on t1.Firm_id=t3.firm_id where t1.Session_id='" + ViewState["sessionid"].ToString() + "' and t1.Class_id='" + ViewState["classid"].ToString() + "' and  t1.Months='" + ViewState["Month"].ToString() + "'";
                }
            }
            else
            {
                if (ViewState["Month"].ToString() == "0")
                {
                    qrys = "select t1.*,t3.Affiliation,t3.school_no,t3.logo,t3.address1,t3.email,t3.website,t3.contact_no,t3.firm_name,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 join Firm_Details t3 on t1.Firm_id=t3.firm_id where t1.Session_id='" + ViewState["sessionid"].ToString() + "' and t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Section='" + ViewState["Section"].ToString() + "'";
                }
                else
                {
                    qrys = "select t1.*,t3.Affiliation,t3.school_no,t3.logo,t3.address1,t3.email,t3.website,t3.contact_no,t3.firm_name,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as class,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as rollnumber,(select top 1 session from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as session,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as studentname,(select top 1 Old_Admission_Date from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as Old_Admission_Date,(select top 1 dateofadmission from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as dateofadmission,(select top 1 fathername from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as fathername,(select top 1 mobilenumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and admissionserialnumber=t1.Admission_no) as mobilenumber from Payment_voucher_slip t1 join Firm_Details t3 on t1.Firm_id=t3.firm_id where t1.Session_id='" + ViewState["sessionid"].ToString() + "' and t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Section='" + ViewState["Section"].ToString() + "' and  t1.Months='" + ViewState["Month"].ToString() + "'";
                }
            }
            DataTable dt = mycode.FillData(qrys);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }




        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../reprint-payment-voucher.aspx", false);
        }


        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rd_viewsss = ((Repeater)e.Item.FindControl("rd_viewsss")) as Repeater;
                Label lbl_rd_viewsss = ((Label)e.Item.FindControl("lbl_rd_viewsss")) as Label;
                Label lbl_session = ((Label)e.Item.FindControl("lbl_session")) as Label;

                DataTable dt = mycode.FillData("select * from Slip_note where Type_id='1'");

                Label lbl_fee_months = ((Label)e.Item.FindControl("lbl_fee_months")) as Label;
                Label lbl_fee_rupee = ((Label)e.Item.FindControl("lbl_fee_rupee")) as Label;
                Label lbl_fee_rupee_dis = ((Label)e.Item.FindControl("lbl_fee_rupee_dis")) as Label;


                Label lbl_prev_dues_dis = ((Label)e.Item.FindControl("lbl_prev_dues_dis")) as Label;
                Label lbl_lbl_ttl_amts_dis = ((Label)e.Item.FindControl("lbl_lbl_ttl_amts_dis")) as Label;

                Label lbl_Session_id = ((Label)e.Item.FindControl("lbl_Session_id")) as Label;
                Label lbl_aadmissionno = ((Label)e.Item.FindControl("lbl_aadmissionno")) as Label;
                string prevdues = get_data_preview(lbl_fee_months.Text, lbl_aadmissionno.Text, lbl_Session_id.Text, lbl_session.Text);

                Label lbl_prevous_dues_month = ((Label)e.Item.FindControl("lbl_prevous_dues_month")) as Label;
                HtmlGenericControl Previous_month_val = e.Item.FindControl("Previous_month_val") as HtmlGenericControl;
                HtmlGenericControl Previous_month_level = e.Item.FindControl("Previous_month_level") as HtmlGenericControl;
                if (My.toDouble(lbl_prevous_dues_month.Text) > 0)
                {
                    Previous_month_val.Visible = true;
                    Previous_month_level.Visible = true;
                    lbl_prevous_dues_month.Text = My.toDouble(lbl_prevous_dues_month.Text).ToString("0.00");

                }
                else
                {
                    Previous_month_val.Visible = false;
                    Previous_month_level.Visible = false;
                    lbl_prevous_dues_month.Text = "0.00";
                }

                try
                {
                    string[] stringSeparators = new string[] { "," };
                    string[] arr = prevdues.Split(stringSeparators, StringSplitOptions.None);
                    prevdues = arr[0];
                    string type = arr[1];
                    double prev_dues = My.toDouble(prevdues);

                    lbl_prev_dues_dis.Text = prev_dues.ToString("0.00");
                    HtmlGenericControl Previous_year_level = e.Item.FindControl("Previous_year_level") as HtmlGenericControl;
                    HtmlGenericControl Previous_year_val = e.Item.FindControl("Previous_year_val") as HtmlGenericControl;

                    if (My.toDouble(lbl_prev_dues_dis.Text) > 0)
                    {
                        Previous_year_level.Visible = true;
                        Previous_year_val.Visible = true;

                    }
                    else
                    {
                        Previous_year_level.Visible = false;
                        Previous_year_val.Visible = false;

                    }


                    if (type == "oldyear")//Previous_Year_Dues
                    {

                        double total = prev_dues + My.toDouble(lbl_fee_rupee.Text)+ My.toDouble(lbl_prevous_dues_month.Text);

                        lbl_fee_rupee_dis.Text = lbl_fee_rupee.Text;

                        lbl_lbl_ttl_amts_dis.Text = total.ToString("0.00");
                    }
                    else
                    {

                        double Amount = My.toDouble(lbl_fee_rupee.Text);
                        double monthfee = Amount - prev_dues;

                        double total = prev_dues + monthfee+ My.toDouble(lbl_prevous_dues_month.Text);
                        lbl_lbl_ttl_amts_dis.Text = total.ToString("0.00");
                        lbl_fee_rupee_dis.Text = monthfee.ToString("0.00");
                    }


                }
                catch
                {

                }



                if (dt.Rows.Count == 0)
                {
                    rd_viewsss.DataSource = null;
                    rd_viewsss.DataBind();

                    lbl_rd_viewsss.Visible = false;
                }
                else
                {
                    rd_viewsss.DataSource = dt;
                    rd_viewsss.DataBind();

                    lbl_rd_viewsss.Visible = true;
                }
            }
        }


        private string get_data_preview(string Months, string Admission_no, string Session_id, string session)
        {
            string monthnew = Months.TrimEnd(',');
            DataTable dt = mycode.FillData("select Dues_Amount as amountdata from Previous_Year_Dues where Session_id='" + Session_id + "' and AdmissionNumber='" + Admission_no + "' and Status='Unpaid'");
            if (dt.Rows.Count == 0)
            {
                DataTable dt_m = mycode.FillData("select   * from dbo.[Monthly_Fee_Collection_Slip] where adno = '" + Admission_no + "' and Content = 'Previous Year Dues' and session = '" + session + "'");
                if (dt_m.Rows.Count == 0)
                {
                    DataTable dt1 = mycode.FillData("select Amount as amountdata from Misc_Fee_Master_Studentwise where Session_id='" + Session_id + "' and Admission_No='" + Admission_no + "' and Old_year_Dues_Type='Yes' and Month like '%" + monthnew + "%' ");
                    if (dt1.Rows.Count == 0)
                    {
                        return "0.00" + ",oldyearmonth";
                    }
                    else
                    {
                        return dt1.Rows[0]["amountdata"].ToString() + ",oldyearmonth";
                    }
                }
                else
                {
                   

                    return "0.00" + ",oldyearmonth";
                }
                
            }
            else
            {
                DataTable dt_m = mycode.FillData("select   * from dbo.[Monthly_Fee_Collection_Slip] where adno = '" + Admission_no + "' and Content = 'Previous Year Dues' and session = '" + session + "'");
                if (dt_m.Rows.Count == 0)
                {
                    return dt_m.Rows[0]["amountdata"].ToString() + ",oldyear";
                }
                else
                {
                    return "0.00" + ",oldyear";
                }


            }
        }

    }
}