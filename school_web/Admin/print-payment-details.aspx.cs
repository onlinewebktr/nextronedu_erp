using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class print_payment_details : System.Web.UI.Page
    {
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
                        ViewState["IsPlusTwoChecked"] = "NO";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Id-card-print.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();

                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        bind_Section();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void bind_Section()
        {
            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ddlsession.SelectedValue + "' order by Section");
        }

        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select * from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + "  and Status='1' order by rollnumber asc";
            }
            else
            {
                qry = "select * from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.SelectedItem.Text + "'  and Status='1' order by rollnumber asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "ALL")
                {
                    Alertme("Please select section", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    Response.Redirect("slip/payment-details.aspx?clssid=" + ddlclass.SelectedValue + "&ssionid=" + ddlsession.SelectedValue + "&ssionname=" + ddlsession.SelectedItem.Text + "&admNo=0&section=" + ddl_section.SelectedItem.Text, false);
                    //string adm_ids = "";
                    //int growcount = rd_view.Items.Count;
                    //int k = 0;
                    //for (int i = 0; i < growcount; i++)
                    //{
                    //    CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                    //    if (chk.Checked == true)
                    //    {
                    //        Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                    //        adm_ids = adm_ids += lbl_id.Text + ",";
                    //    }
                    //    else
                    //    {
                    //        k++;
                    //    }
                    //}

                    //if (k == growcount)
                    //{
                    //    if (lbl_is_check.Text == "1")
                    //    {
                    //        if (chk_is_ckeck.Checked == true)
                    //        {
                    //            Response.Redirect("slip/payment-details.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                    //        }
                    //        else
                    //        {
                    //            Response.Redirect("slip/payment-details.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Response.Redirect("slip/payment-details.aspx?clssid=" + ddlclass.SelectedValue + "&ssionid=" + ddlsession.SelectedValue + "&ssionname=" + ddlsession.SelectedItem.Text + "&admNo=0&section=" + ddl_section.SelectedItem.Text, false);
                    //    }
                    //}
                    //else
                    //{
                    //    string reslink = "slip/payment-details.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + ddlclass.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                    //    Response.Redirect(reslink, false);
                    //}
                }

            }
            catch (Exception ex)
            {
            }
        }







        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_Section();
        }
    }
}