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
    public partial class Application_List_For_Career : System.Web.UI.Page
    {
        My mycode = new My();
        string session_name = "Select top 1 Session from session_details where session_id=Employee_Online_Apply.Session_id";
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
                        if (Session["msG"] != null)
                        {
                            Alertme(Session["msG"].ToString(), "success");
                            Session["msG"] = null;
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Application_List_For_Career.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();



                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id_onlinereg();

                        mycode.bind_all_ddl_with_id(ddl_hearing_by, "Select Hiring_name,Hiring_id from Employees_Create_Hiring order by Hiring_name asc");

                        mycode.bind_ddlall(ddl_post_applied_for, "Select distinct Employee_Hiring_For from Employee_Hiring_Fee_and_Seat order by Employee_Hiring_For");


                        Bind_all_data();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Application_List_For_Career");
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
        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                Bind_all_data();
            }
        }
        private void Bind_all_data()
        {
            string query = "Select *,(" + session_name + ") as session_name from Employee_Online_Apply where Payment_Status='Paid' and Seat_Remarks='OK' and Session_id=" + ddlsession.SelectedValue + " order by idate desc ";
            bind_grid_data(query);
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session name", "warning");
            }
            else if (ddl_hearing_by.SelectedItem.Text == "Select")
            {
                Alertme("Please select hiring By", "warning");
            }
            else
            {
                bind_data_apply_post();
            }

        }
        private void bind_grid_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                grid_data.DataSource = null;
                grid_data.DataBind();
                btn_excels.Visible = false;
            }
            else
            {
                btn_excels.Visible = true;

                rd_view.DataSource = dt;
                rd_view.DataBind();
                grid_data.DataSource = dt;
                grid_data.DataBind();

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
        protected void ddl_hearing_by_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Sorry select session", "warning");
            }
            else
            {
                mycode.bind_ddlall(ddl_post_applied_for, "Select distinct Apply_for from Employee_Online_Apply where  Session_id=" + ddlsession.SelectedValue + " and Hiring_id=" + ddl_hearing_by.SelectedValue + " order by Apply_for");

                string query = "Select *,(" + session_name + ") as session_name from Employee_Online_Apply where Payment_Status='Paid' and Seat_Remarks='OK' and Session_id=" + ddlsession.SelectedValue + " and Hiring_id=" + ddl_hearing_by.SelectedValue + " order by idate desc ";
                bind_grid_data(query);
            }

        }

        protected void ddl_post_applied_for_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_data_apply_post();
                 
           
        }

        private void bind_data_apply_post()
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Sorry select session", "warning");
            }
            else if (ddl_hearing_by.SelectedItem.Text == "Select")
            {
                Alertme("Sorry select hiring by", "warning");
            }
            else

            {
                if (ddl_post_applied_for.Text == "ALL")
                {
                    string query = "Select *,(" + session_name + ") as session_name from Employee_Online_Apply where Payment_Status='Paid' and Seat_Remarks='OK' and Session_id=" + ddlsession.SelectedValue + " and Hiring_id=" + ddl_hearing_by.SelectedValue + " order by idate desc ";
                    bind_grid_data(query);
                }
                else
                {
                    string query = "Select *,(" + session_name + ") as session_name from Employee_Online_Apply where Payment_Status='Paid' and Seat_Remarks='OK' and Session_id=" + ddlsession.SelectedValue + " and Hiring_id=" + ddl_hearing_by.SelectedValue + " and Apply_for='" + ddl_post_applied_for.Text + "'  order by idate desc ";
                    bind_grid_data(query);

                }

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
                        grid_data.RenderControl(hw);
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