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
    public partial class Approved_Rejected_Income_Certificate : System.Web.UI.Page
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

                        string pagename_current = "Approved_Rejected_Income_Certificate.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        find_firm_details();

                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        Bind_all_pendig_data();
                    }
                }
            }
            catch (Exception ex)
            {

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
        private void Bind_all_pendig_data()
        {
            ViewState["flag"] = "1";
            string query = "Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') Reply_datetime1 ,ar.admissionserialnumber,ar.studentname,ar.rollnumber,ar.Section,ar.class from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ddlsession.SelectedValue + "  and aft.Apply_For='Income Certificate' and aft.Status='" + ddl_status.Text + "' order by id,ar.rollnumber asc";
            bind_data_grid_data(query);
        }

        private void bind_data_grid_data(string query)
        {
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                lbl_class22.Text = " Session: " + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text + " Section :" + ddl_section.SelectedItem.Text;

                btn_excels.Visible = true;

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
                rd_view.DataSource = dt;
                rd_view.DataBind();
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

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
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
                    ViewState["flag"] = "1";
                    Bind_all_pendig_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    class_wise_serch();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void class_wise_serch()
        {
            ViewState["flag"] = "2";

            if (ddlclass.SelectedItem.Text == "ALL")
            {
                string query = "Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') Reply_datetime1  ,ar.admissionserialnumber,ar.studentname,ar.rollnumber,ar.Section,ar.class from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ddlsession.SelectedValue + " and aft.Apply_For='Income Certificate' and aft.Status='" + ddl_status.Text + "' order by id,ar.rollnumber asc";
                bind_data_grid_data(query);
            }
            else
            {
                string query = "Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') Reply_datetime1  ,ar.admissionserialnumber,ar.studentname,ar.rollnumber,ar.Section,ar.class from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ddlsession.SelectedValue + " and ar.Class_Id=" + ddlclass.SelectedValue + " and aft.Apply_For='Income Certificate' and aft.Status='" + ddl_status.Text + "' order by id,ar.rollnumber asc";
                bind_data_grid_data(query);

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                ddlsession.Focus();
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddlclass.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();
            }
            else
            {
                bind_data_search();
            }
        }

        private void bind_data_search()
        {
            if (ddlclass.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
            {

                ViewState["flag"] = "3";

                string query = "Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') Reply_datetime1  ,ar.admissionserialnumber,ar.studentname,ar.rollnumber,ar.Section,ar.class from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ddlsession.SelectedValue + " and aft.Status='" + ddl_status.Text + "'   and aft.Apply_For='Income Certificate' order by id,ar.rollnumber asc";
                bind_data_grid_data(query);

            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
            {
                ViewState["flag"] = "3";
                string query = "Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') Reply_datetime1  ,ar.admissionserialnumber,ar.studentname,ar.rollnumber,ar.Section,ar.class from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ddlsession.SelectedValue + " and ar.Class_Id=" + ddlclass.SelectedValue + " and aft.Apply_For='Income Certificate' and aft.Status='" + ddl_status.Text + "' order by id,ar.rollnumber asc";
                bind_data_grid_data(query);
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
            {
                ViewState["flag"] = "3";
                string query = "Select aft.*,format(aft.Apply_date_time, 'dd/MM/yyyy hh:mm:ss tt') Apply_date_time1,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') Reply_datetime1  ,ar.admissionserialnumber,ar.studentname,ar.rollnumber,ar.Section,ar.class from Apply_For_TC aft join admission_registor ar on aft.Admission_no=ar.admissionserialnumber and aft.Session_id=ar.Session_Id where aft.Session_id=" + ddlsession.SelectedValue + " and ar.Class_Id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.Text + "'  and aft.Apply_For='Income Certificate' and aft.Status='" + ddl_status.Text + "' order by id,ar.rollnumber asc";
                bind_data_grid_data(query);
            }
        }

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
                        Panel11.RenderControl(hw);
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