using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class print_bus_pass : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
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



        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else
            {
                string query = "";
                if (ddl_section.Text.ToUpper() == "ALL")
                {
                    query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Status='1' and transportationtaken='Yes' order by rollnumber asc";
                }
                else
                {
                    query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and transportationtaken='Yes' order by rollnumber asc";
                }
                bind_grid_data(query);
            }
        }





        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Please select class", "warning");
                }
                else
                {
                    string adm_ids = "";
                    int growcount = rd_view.Items.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                        if (chk.Checked == true)
                        {
                            Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                            adm_ids = adm_ids += lbl_id.Text + ",";
                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        string reslink = "";
                        if (ddl_section.SelectedItem.Text.ToUpper() == "ALL")
                        {
                            reslink = "slip/print-bus-pass.aspx?session_Id=" + ddlsession.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&section=0&admno=0&checked=0";
                        }
                        else
                        {
                            reslink = "slip/print-bus-pass.aspx?session_Id=" + ddlsession.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&admno=0&checked=0";
                        }
                        Response.Redirect(reslink, false);
                    }
                    else
                    {
                        string reslink = "";
                        if (ddl_section.SelectedItem.Text.ToUpper() == "ALL")
                        {
                            reslink = "slip/print-bus-pass.aspx?session_Id=" + ddlsession.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&section=0&admno=" + adm_ids + "&checked=1";
                        }
                        else
                        {
                            reslink = "slip/print-bus-pass.aspx?session_Id=" + ddlsession.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&admno=" + adm_ids + "&checked=1";
                        }
                        Response.Redirect(reslink, false);
                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "print-bus-pass.aspx");
            }
        }
    }
}