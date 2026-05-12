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
    public partial class discount_reports : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "select Course_Name,course_id from Add_course_table order by Position asc");
                        ddl_class.SelectedValue = My.get_top_one_class();
                        bind_section();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();

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
                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            lnk_excel.Visible = true;
                        }
                        else
                        {
                            lnk_excel.Visible = false;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_group_master");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                get_discount_report();
            }
            catch (Exception ex)
            {
            }
        }



        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_section();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_section()
        {
            My.bind_ddl_all_Cap(ddl_section, "select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Section asc");
        }


        private void get_discount_report()
        {
            int fromidate = My.DateConvertToIdate(txt_from_date.Text);
            int toidate = My.DateConvertToIdate(txt_to_date.Text);

            string qry = "";
            if (ddl_class.SelectedItem.Text.ToUpper() == "ALL")
            {
                qry = "select t1.Admission_no,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t2.mobilenumber,t1.Bill_no,t1.Created_date,(select top 1 name from user_details where user_id=t1.Created_by) as Discount_by,sum(convert(float, Amount)) as Bill_amt,sum(convert(float, Discount_amt)) as Discount_amt from Discount_master_report t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Created_idate>='" + fromidate + "' and t1.Created_idate<='" + toidate + "' group by Bill_no,t1.Admission_no,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t2.mobilenumber,t1.Created_date,t1.Created_by,t1.Created_idate order by t1.Created_idate asc";
            }
            else
            {
                if (ddl_section.SelectedItem.Text.ToUpper() == "ALL")
                {
                    qry = "select t1.Admission_no,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t2.mobilenumber,t1.Bill_no,t1.Created_date,(select top 1 name from user_details where user_id=t1.Created_by) as Discount_by,sum(convert(float, Amount)) as Bill_amt,sum(convert(float, Discount_amt)) as Discount_amt from Discount_master_report t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + ddl_class.SelectedValue + "' and t1.Created_idate>='" + fromidate + "' and t1.Created_idate<='" + toidate + "' group by Bill_no,t1.Admission_no,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t2.mobilenumber,t1.Created_date,t1.Created_by,t1.Created_idate order by t1.Created_idate asc";
                }
                else
                {
                    qry = "select t1.Admission_no,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t2.mobilenumber,t1.Bill_no,t1.Created_date,(select top 1 name from user_details where user_id=t1.Created_by) as Discount_by,sum(convert(float, Amount)) as Bill_amt,sum(convert(float, Discount_amt)) as Discount_amt from Discount_master_report t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + ddl_class.SelectedValue + "' and t2.Section='" + ddl_section.Text + "' and t1.Created_idate>='" + fromidate + "' and t1.Created_idate<='" + toidate + "' group by Bill_no,t1.Admission_no,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t2.mobilenumber,t1.Created_date,t1.Created_by,t1.Created_idate order by t1.Created_idate asc";
                }
            }
           
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                lbl_total_disc.Text = Convert.ToInt32(dt.Compute("SUM(Discount_amt)", string.Empty)).ToString();
            }
            else
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
        }


        protected void lnk_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=discount-report.xls");
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