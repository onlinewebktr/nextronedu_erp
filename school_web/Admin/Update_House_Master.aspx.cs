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
    public partial class Update_House_Master : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) ");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_ddl(ddl_secion, "Select distinct Section from admission_registor order by Section");
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
                My.submitException(ex, "StudentList");
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
        protected void btn_fnd_Click(object sender, EventArgs e)
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
                else if (ddl_secion.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    find_by_c_s_a();

                }
            }
            catch
            {
            }
        }

        private void find_by_c_s_a()
        {
            bind_grd_view("select admissionserialnumber,studentname,Section,Session_id,Class_id,house,id,rollnumber from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_secion.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT') and    Status='1' and StudentStatus='AV' order by rollnumber asc");
        }

        private void bind_grd_view(string query)
        {
            ViewState["flag"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_save.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                btn_save.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }



        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (ViewState["Is_add"].ToString() == "1")
            {
                ViewState["msg"] = "0";
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
                else if (ddl_secion.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;

                    for (int i = 0; i < growcount; i++)
                    {
                        Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");

                        DropDownList ddl_house_name = (DropDownList)GrdView.Rows[i].FindControl("ddl_house_name");

                        mycode.executequery("update admission_registor set house='" + ddl_house_name.SelectedValue + "' where   id='" + lbl_id.Text + "'");
                        ViewState["msg"] = "1";
                    }

                    if (ViewState["msg"].ToString() == "1")
                    {
                        Alertme("House name has been updated sucessfully", "success");
                        bind_grd_view(ViewState["flag"].ToString());
                    }
                }
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                ViewState["msg"] = "0";
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
                else if (ddl_secion.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;

                    for (int i = 0; i < growcount; i++)
                    {
                        Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                        DropDownList ddl_house_name = (DropDownList)GrdView.Rows[i].FindControl("ddl_house_name");
                        mycode.executequery("update admission_registor set house='" + ddl_house_name.SelectedValue + "' where   id='" + lbl_id.Text + "'");
                        ViewState["msg"] = "1";
                    }

                    if (ViewState["msg"].ToString() == "1")
                    {
                        Alertme("House name has been updated sucessfully", "success");
                        bind_grd_view(ViewState["flag"].ToString());
                    }
                }

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_current_housename = (Label)e.Row.FindControl("lbl_current_housename");
                Label lbl_housename = (Label)e.Row.FindControl("lbl_housename");
                bind_house_name(lbl_current_housename.Text, lbl_housename);

                DropDownList ddl_house_name = (DropDownList)e.Row.FindControl("ddl_house_name");

                mycode.bind_all_ddl_with_id(ddl_house_name, "select house_name,house_id from dbo.[house_master]");
                ddl_house_name.SelectedValue = lbl_current_housename.Text;
            }
        }

        private void bind_house_name(string houseid, Label lbl_housename)
        {
            DataTable dt = mycode.FillData("Select house_name from house_master where house_id='" + houseid + "'");
            if (dt.Rows.Count == 0)
            {
                lbl_housename.Text = "N/A";
            }
            else
            {
                lbl_housename.Text = dt.Rows[0]["house_name"].ToString();
            }
        }
    }
}