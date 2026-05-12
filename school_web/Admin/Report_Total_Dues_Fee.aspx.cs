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
    public partial class Report_Total_Dues_Fee : System.Web.UI.Page
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
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by session_id asc");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");

                        try
                        {
                            if (Request.QueryString["sessionid"] != null)
                            {

                                ViewState["sessionid"] = Request.QueryString["sessionid"];
                                ViewState["classid"] = Request.QueryString["classid"];

                                ddlsession.SelectedValue = ViewState["sessionid"].ToString();
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
                My.submitException(ex, "StudentList");
            }
        }



        private void Bind_data()
        {
            bind_grd_view_data("select *,(cast(totaldues as float)-cast(paid as float)) netdues  from(select      admissionserialnumber,studentname,session,class,(select (classfee-isnull(discount,0)) netfee from (select sum (cast(amount as  float)) as classfee, (isnull((select sum(cast( disc_amount as float)) from dbo.[Discount_Master] where admission_no=ad.admissionserialnumber and session=ad.session and category_id=ad.Category_id and sub_category_id=ad.SubCategory_id),(select sum(cast( disc_amount as float)) from dbo.[Discount_Master] where Class_id=ad.Class_id and admission_no='All'   and session=ad.session  and category_id=ad.Category_id and sub_category_id=ad.SubCategory_id))) as discount    from dbo.[Fee_master_content_wise] where class_id=ad.Class_id and session_id=ad.Session_id)t) as totaldues ,( Select  sum(cast(Amount as float)) totalpaid from Student_Payment_History where Addmission_no=ad.admissionserialnumber and Session=ad.session and Class_id=ad.Class_id) as paid  from dbo.[admission_registor] ad where ad.Class_id='" + ddlclass.SelectedValue + "' and ad.Session_id='" + ddlsession.SelectedValue + "')p");
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
                lbl_print_headS.Text = "Dues List Summary Report of Session : " + ddlsession.SelectedItem.Text + " and Class : " + ddlclass.SelectedItem.Text;
                lbl_print_dateS.Text = "(Print on : " + mycode.date() + ")";
                btn_excels.Visible = true;
                ViewState["query"] = dt;
                grd_fee.DataSource = dt;
                grd_fee.DataBind();

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
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "Select")
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
            if (ViewState["Is_Download"].ToString() == "1")
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Total_Dues" + "_" + DateTime.Now + ".xls");
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
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}