using school_web.AppCode;
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
    public partial class Report_Total_Month_Fee_Collection_Today : System.Web.UI.Page
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
                        txt_date1.Text = mycode.date();


                       // mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by session_id asc");
                        mycode.bind_all_ddl_with_id_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");

                        try
                        {
                            if (Request.QueryString["sessionid"] != null)
                            {
                                backbtns.Visible = true;
                                ViewState["sessionid"] = Request.QueryString["sessionid"];
                                ViewState["classid"] = Request.QueryString["classid"];
                                txt_date1.Text = Request.QueryString["date"];
                               // ddlsession.SelectedValue = ViewState["sessionid"].ToString();
                                ddlclass.SelectedValue = ViewState["classid"].ToString();

                                ViewState["flag"] = "0";
                                Bind_data();
                            }
                            else
                            {
                                ViewState["flag"] = "1";
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Report_Total_Month_Fee_Collection_Today");
            }
        }


        private void Bind_data()
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                bind_grd_view_data("select t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount,t1.Type,t1.Date from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.session=t2.session where t1.Type='Monthly' and t1.Idate=" + mycode.ConvertStringToiDate(txt_date1.Text) + "  order by t1.Idate desc");
            }
            else
            {
                bind_grd_view_data("select t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount,t1.Type,t1.Date from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.session=t2.session where     t1.Class_id='" + ddlclass.SelectedValue + "'  and t1.Type='Monthly' and t1.Idate=" + mycode.ConvertStringToiDate(txt_date1.Text) + "  order by t1.Idate desc");
            }
        }

        private void bind_grd_view_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                ViewState["query"] = "0";
                Alertme("Sorry there are no data list exist", "warning");
                grd_fee.DataSource = null;
                grd_fee.DataBind();

            }
            else
            {
                lbl_print_headS.Text = "Monthly Fees Collection Report of Date : " + txt_date1.Text + " and Class : " + ddlclass.SelectedItem.Text;
                lbl_print_dateS.Text = "(Print on : " + mycode.date() + ")";
                btn_excels.Visible = true;
                ViewState["query"] = dt;
                grd_fee.DataSource = dt;
                grd_fee.DataBind();
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
        double total = 0;
        protected void grd_fee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_payable = (Label)e.Row.FindControl("lbl_Amount");
                if (lbl_payable.Text != "")
                {
                    total = total + Convert.ToDouble(lbl_payable.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_totaldiscount = (Label)e.Row.FindControl("lbl_totalamount");

                lbl_totaldiscount.Text = total.ToString("0.00");

            }

        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                ViewState["flag"] = "0";
                Bind_data();
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            // DataTable dtCurrentTable = (DataTable)ViewState["query"];
            // export_to_excel(dtCurrentTable, "Total_Collection");

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Total_Month_Fee_Collection_Today" + "_" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd_fee.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}