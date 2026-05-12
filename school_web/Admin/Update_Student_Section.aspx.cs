using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin
{
    public partial class Update_Student_Section : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by Use_mode desc ");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update_Student_Section");
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
            bind_grd_view("select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'      and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1' and StudentStatus='AV' order by studentname asc");
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
            
            else
            {
                int growcount = GrdView.Rows.Count;
                int k = 0;

                for (int i = 0; i < growcount; i++)
                {
                    Label lbl_admission_no = (Label)GrdView.Rows[i].FindControl("lbl_admission_no");
                    Label lbl_Session_id = (Label)GrdView.Rows[i].FindControl("lbl_Session_id");
                    Label lbl_Class_id = (Label)GrdView.Rows[i].FindControl("lbl_Class_id");
                     
                    TextBox txt_Section = (TextBox)GrdView.Rows[i].FindControl("txt_Section");
                    mycode.executequery("update admission_registor set Section='" + txt_Section.Text + "' where admissionserialnumber='" + lbl_admission_no.Text + "'    and Class_id='" + lbl_Class_id.Text + "' and Session_id='" + lbl_Session_id.Text + "'");
                    ViewState["msg"] = "1";
                }

                if (ViewState["msg"].ToString() == "1")
                {
                    Alertme("Section has been updated successfully", "success");
                    bind_grd_view(ViewState["flag"].ToString());
                }
            }
        }
    }
}