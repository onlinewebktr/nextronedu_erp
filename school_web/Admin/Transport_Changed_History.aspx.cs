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
    public partial class Transport__Changed_History : System.Web.UI.Page
    {
        string path = "Select top 1 Pathname from TransportationPath where TransportationPath_id=t1.TransportPath_id";
        string pathold = "Select top 1 Pathname from TransportationPath where TransportationPath_id=t1.Old_Transpotid";
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
                        ViewState["branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Transport_Changed_History.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();


                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor");

                        ViewState["sessniD"] = My.get_session_id();
                        ddlsession.SelectedValue = ViewState["sessniD"].ToString();
                        find_firm_details();
                        bind_all_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Transport__Changed_History");
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
        private void bind_all_data()
        {
            bind_grd_view("select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,(" + path + ") as Pathname_Pathnamenew,(" + pathold + ") as Pathnameold from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id  where t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.branch_id='" + ViewState["branch_id"].ToString() + "' and t1.Changed_type in ('Month Changed','Transport Changed','Both Changed') order by t1.Id desc");

        }
        private void Alertme(string msg, string panel)
        {
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
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
        private void bind_grd_view(string qry)
        {
            btn_excels.Visible = false;
            print1.Visible = false;
            ViewState["query"] = qry;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
               

                GrdView.DataSource = dt;
                GrdView.DataBind();

                btn_excels.Visible = true;
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
                    mycode.bind_all_ddl_with_id(ddlclass, "  Select  Course_Name, course_id  from Add_course_table order by  Position");
                    bind_all_data();

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
                }
                else if (ddlclass.SelectedItem.Text == "")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    find_by_class();
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void find_by_class()
        {
            bind_grd_view(" select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,(" + path + ") as Pathname_Pathnamenew,(" + pathold + ") as Pathnameold from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.branch_id='" + ViewState["branch_id"].ToString() + "' and t1.Class_id='" + ddlclass.SelectedValue + "'  and   t1.Changed_type in ('Month Changed','Transport Changed','Both Changed') order by t1.Id desc");

        }
        protected void btn_find_Click(object sender, EventArgs e)
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
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    find_by_c_s_a();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void find_by_c_s_a()
        {
            bind_grd_view("select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,(" + path + ") as Pathname_Pathnamenew,(" + pathold + ") as Pathnameold from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.branch_id='" + ViewState["branch_id"].ToString() + "' and t1.Class_id='" + ddlclass.SelectedValue + "' and   t2.Section='" + ddl_section.SelectedItem.Text + "'  and t1.Changed_type in ('Month Changed','Transport Changed','Both Changed') order by t1.Id desc");
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
                        GrdView.RenderControl(hw);
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
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission No.", "warning");
            }
            else
            {
                bind_grd_view("select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,(" + path + ") as Pathname_Pathnamenew,(" + pathold + ") as Pathnameold from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t1.Session_id='" + ddlsession.SelectedValue + "' and t1.branch_id='" + ViewState["branch_id"].ToString() + "' and t1.Admission_no='" + txt_admission_no.Text + "'    and t1.Changed_type in ('Month Changed','Transport Changed','Both Changed') order by t1.Id desc");
            }
        }
    }
}