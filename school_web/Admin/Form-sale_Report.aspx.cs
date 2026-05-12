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
    public partial class Form_sale_Report : System.Web.UI.Page
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

                        string pagename_current = "daily-closing-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        lbl_datetime.Text = mycode.date() + "/" + mycode.time();

                        lbl_useridandname.Text = My.get_empname(ViewState["Userid"].ToString()) + "-(" + ViewState["Userid"].ToString() + ")";
                        find_firm_details();


                        mycode.bind_ddlall(ddlclass, "Select Course_Name  from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        txt_e_date.Text = mycode.date();
                        txt_s_date.Text = mycode.date();

                        Bind_data();




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Report_Today_Collection_New");
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
        private void Bind_data()
        {
            string sdate = txt_s_date.Text;
            string sday = sdate.Substring(0, 2);
            string smonth = sdate.Substring(3, 2);
            string syear = sdate.Substring(6, 4);

            string edate = txt_e_date.Text;
            string eday = edate.Substring(0, 2);
            string emonth = edate.Substring(3, 2);
            string eyear = edate.Substring(6, 4);

            int idate1 = Convert.ToInt32(syear + smonth + sday);
            int idate21 = Convert.ToInt32(eyear + emonth + eday);

            if (idate1 > idate21)
            {
                Alertme("End date cannot be less than start date.", "warning");
            }
            else
            {
                string query = "Select * from Form_sale_details where idate>='" + idate1 + "' and Idate<='" + idate21 + "'";
                Bind_final_data(query);
            }

        }

        private void Bind_final_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                print1.Visible = false;
                ViewState["query"] = "0";
                Alertme("Sorry there are no data list exist", "warning");
                grd_fee.DataSource = null;
                grd_fee.DataBind();

            }
            else
            {
               
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


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            // DataTable dtCurrentTable = (DataTable)ViewState["query"];
            // export_to_excel(dtCurrentTable, "Total_Collection");
            if (ViewState["Is_Download"].ToString() == "1")
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Formsale" + "_" + DateTime.Now + ".xls");
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please enter start date", "warning");
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please enter end date", "warning");

            }
            else
            {
                string query = "";
                string sdate = txt_s_date.Text;
                string sday = sdate.Substring(0, 2);
                string smonth = sdate.Substring(3, 2);
                string syear = sdate.Substring(6, 4);

                string edate = txt_e_date.Text;
                string eday = edate.Substring(0, 2);
                string emonth = edate.Substring(3, 2);
                string eyear = edate.Substring(6, 4);

                int idate1 = Convert.ToInt32(syear + smonth + sday);
                int idate21 = Convert.ToInt32(eyear + emonth + eday);

                if (idate1 > idate21)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    if (ddlclass.Text == "ALL")
                    {
                        query = "Select * from Form_sale_details where idate>='" + idate1 + "' and Idate<='" + idate21 + "' order by  idate";
                    }
                    else
                    {
                        query = "Select * from Form_sale_details where idate>='" + idate1 + "' and Idate<='" + idate21 + "' and class='" + ddlclass.Text + "' order by  idate";
                    }
                    Bind_final_data(query);
                }

            }
        }


    }
}