using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class Active_Inactive_Student : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        bind_all_data();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Active_Inactive_Student");
            }
        }

        private void bind_all_data()
        {
            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where  Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + "   order by rollnumber asc");
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



        private void bind_grd_view(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
                btn_active.Visible = false;
                btn_inactive.Visible = false;
            }
            else
            {
                btn_active.Visible = true;
                btn_inactive.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }


        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_istatus = (Label)e.Row.FindControl("lbl_istatus");
                Label lbl_status = (Label)e.Row.FindControl("lbl_status");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (lbl_istatus.Text == "")
                {
                    lbl_status.Text = "Inactive";
                    lnkEdit.Text = "Active";
                    lnkEdit.BackColor = System.Drawing.Color.Green;
                    lnkEdit.ForeColor = System.Drawing.Color.White;
                }
                else if (lbl_istatus.Text == "0")
                {
                    lbl_status.Text = "Inactive";
                    lnkEdit.Text = "Active";
                    lnkEdit.BackColor = System.Drawing.Color.Green;
                    lnkEdit.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    lbl_status.Text = "Active";
                    lnkEdit.Text = "Inactive";
                    lnkEdit.BackColor = System.Drawing.Color.Red;
                    lnkEdit.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
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
                mycode.bind_ddlall(ddl_Section, "Select distinct Section from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " order by Section ");

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
                if (ddl_Section.Text == "ALL")
                {
                    bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + "  order by rollnumber asc");
                }
                else
                {
                    bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "' order by rollnumber asc");
                }
            }

        }
        protected void ddl_Section_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_istatus = (Label)row.FindControl("lbl_istatus");

                if (lbl_istatus.Text == "")
                {
                    mycode.executequery("update admission_registor set Status='1' where id=" + lbl_id.Text + "");
                    Alertme("This student has been successfully activated", "success");
                }
                else if (lbl_istatus.Text == "0")
                {
                    mycode.executequery("update admission_registor set Status='1' where id=" + lbl_id.Text + "");
                    Alertme("This student has been successfully activated", "success");
                }
                else
                {
                    mycode.executequery("update admission_registor set Status='0',Inactive_date='" + mycode.date() + "',Inactive_idate='" + mycode.idate() + "',Inactive_time='" + mycode.time() + "' where id=" + lbl_id.Text + "");
                    Alertme("This student has been successfully inactive", "success");
                }

                bind_grd_view(ViewState["query"].ToString());
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_istatus = (Label)row.FindControl("lbl_istatus");

                if (lbl_istatus.Text == "")
                {
                    mycode.executequery("update admission_registor set Status='1' where id=" + lbl_id.Text + "");

                    Alertme("This student has been successfully activated", "success");

                }

                else if (lbl_istatus.Text == "0")
                {
                    mycode.executequery("update admission_registor set Status='1' where id=" + lbl_id.Text + "");
                    Alertme("This student has been successfully activated", "success");
                }
                else
                {
                    mycode.executequery("update admission_registor set Status='0',Inactive_date='" + mycode.date() + "',Inactive_idate='" + mycode.idate() + "',Inactive_time='" + mycode.time() + "' where id=" + lbl_id.Text + "");
                    Alertme("This student has been successfully inactive", "success");
                }
                bind_grd_view(ViewState["query"].ToString());

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_inactive_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Is_add"].ToString() == "1")
                {

                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update admission_registor set Status='0',Inactive_date='" + mycode.date() + "',Inactive_idate='" + mycode.idate() + "',Inactive_time='" + mycode.time() + "' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {
                        Alertme("Student has been successfully inactive", "success");
                        bind_grd_view(ViewState["query"].ToString());
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {
                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update admission_registor set Status='0',Inactive_date='" + mycode.date() + "',Inactive_idate='" + mycode.idate() + "',Inactive_time='" + mycode.time() + "' where id=" + lbl_id.Text + "");
                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {
                        Alertme("Student has been successfully inactive", "success");
                        bind_grd_view(ViewState["query"].ToString());
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

        protected void btn_active_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update admission_registor set Status='1' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {
                        Alertme("Student has been successfully activated", "success");
                        bind_grd_view(ViewState["query"].ToString());
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update admission_registor set Status='1' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {
                        Alertme("Student has been successfully activated", "success");
                        bind_grd_view(ViewState["query"].ToString());
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