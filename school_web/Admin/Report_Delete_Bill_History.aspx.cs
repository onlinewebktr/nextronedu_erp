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
    public partial class Report_Delete_Bill_History : System.Web.UI.Page
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
                        string pagename_current = "Report_Delete_Bill_History.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];



                        ViewState["flag"] = "0";

                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                        bind_all_data();
                        lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text;

                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_all_data()
        {
            bind_grd_view("select ar.studentname,ar.admissionserialnumber,ar.class,ar.Section,ar.rollnumber,format(arc.Date_time, 'dd/MM/yyyy') as date1,(Select top 1 Course_Name from Add_course_table where course_id=arc.Old_Class_id) as Course_Nameold,arc.Roll_no_old,arc.Old_Section_name,arc.Session_id,arc.Slip_no    from admission_registor ar join  admission_registor_Change_admission_no_history arc on arc.Current_admission_no=ar.admissionserialnumber and  arc.Session_id=ar.Session_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT' or ar.Transfer_Status='Transferred') and ar.Status='1'   and ar.Session_id='" + ddlsession.SelectedValue + "' and arc.Change_type in ('Monthly Fees Delete','Annual Fees Delete','Admission Fees Delete') order by ar.rollnumber asc");
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
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                print1.Visible = false;
                Alertme("Sorry there are no  any change list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                btn_excels.Visible = true;

                GrdView.DataSource = dt;
                GrdView.DataBind();

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

                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            lbl_class22.Text = "";
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
                    lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text;
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

            if (ddl_section.Text == "ALL")
            {

                bind_grd_view("select ar.studentname,ar.admissionserialnumber,ar.class,ar.Section,ar.rollnumber,arc.Old_admission_no,format(arc.Date_time, 'dd/MM/yyyy') as date1,(Select top 1 Course_Name from Add_course_table where course_id=arc.Old_Class_id) as Course_Nameold,arc.Roll_no_old,arc.Old_Section_name,arc.Session_id,arc.Slip_no      from admission_registor ar join  admission_registor_Change_admission_no_history arc on arc.Current_admission_no=ar.admissionserialnumber and  arc.Session_id=ar.Session_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT' or ar.Transfer_Status='Transferred') and ar,Status='1'  and ar.Session_id='" + ddlsession.SelectedValue + "' and   Class_id='" + ddlclass.SelectedValue + "' and arc.Change_type in ('Monthly Fees Delete','Annual Fees Delete','Admission Fees Delete') order by ar.rollnumber asc");




            }
            else if (ddl_section.Text != "ALL")
            {
                bind_grd_view("select ar.studentname,ar.admissionserialnumber,ar.class,ar.Section,ar.rollnumber,arc.Old_admission_no,format(arc.Date_time, 'dd/MM/yyyy') as date1,(Select top 1 Course_Name from Add_course_table where course_id=arc.Old_Class_id) as Course_Nameold,arc.Roll_no_old,arc.Old_Section_name,arc.Session_id,arc.Slip_no     from admission_registor ar join  admission_registor_Change_admission_no_history arc on arc.Current_admission_no=ar.admissionserialnumber and  arc.Session_id=ar.Session_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT' or ar.Transfer_Status='Transferred') and ar.Status='1'   and ar.Session_id='" + ddlsession.SelectedValue + "' and   Class_id='" + ddlclass.SelectedValue + "' and   Section='" + ddl_section.Text + "'  and arc.Change_type in ('Monthly Fees Delete','Annual Fees Delete','Admission Fees Delete') order by ar.rollnumber asc");


            }



        }

        protected void btn_fnd_by_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please  enter admission no.", "warning");
                txt_admission_no.Focus();
            }
            else
            {
                string sessioid = My.get_session_id();
                lbl_class22.Text = "Admission No : " + txt_admission_no.Text;
                bind_grd_view("select ar.studentname,ar.admissionserialnumber,ar.class,ar.Section,ar.rollnumber,arc.Old_admission_no,format(arc.Date_time, 'dd/MM/yyyy') as date1,(Select top 1 Course_Name from Add_course_table where course_id=arc.Old_Class_id) as Course_Nameold,arc.Roll_no_old,arc.Old_Section_name,arc.Session_id,arc.Slip_no      from admission_registor ar join  admission_registor_Change_admission_no_history arc on arc.Current_admission_no=ar.admissionserialnumber and  arc.Session_id=ar.Session_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT' or ar.Transfer_Status='Transferred')   and ar.Session_id='" + sessioid + "'  and    ar.admissionserialnumber='" + txt_admission_no.Text + "' and ar.Status='1'  and arc.Change_type in ('Monthly Fees Delete','Annual Fees Delete','Admission Fees Delete') order by ar.rollnumber asc");
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                ddlsession.Focus();
            }
            else
            {
                lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text;
                bind_grd_view(" select ar.studentname,ar.admissionserialnumber,ar.class,ar.Section,ar.rollnumber,arc.Old_admission_no,format(arc.Date_time, 'dd/MM/yyyy') as date1,(Select top 1 Course_Name from Add_course_table where course_id=arc.Old_Class_id) as Course_Nameold,arc.Roll_no_old,arc.Old_Section_name,arc.Session_id,arc.Slip_no    from admission_registor ar join  admission_registor_Change_admission_no_history arc on arc.Current_admission_no=ar.admissionserialnumber and  arc.Session_id=ar.Session_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT' or ar.Transfer_Status='Transferred') and ar,Status='1'   and ar.Session_id='" + ddlsession.SelectedValue + "' and arc.Change_type in ('Monthly Fees Delete','Annual Fees Delete','Admission Fees Delete') order by ar.rollnumber asc");
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
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
            else
            {
                lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text;
                bind_grd_view("select ar.studentname,ar.admissionserialnumber,ar.class,ar.Section,ar.rollnumber,arc.Old_admission_no,format(arc.Date_time, 'dd/MM/yyyy') as date1,(Select top 1 Course_Name from Add_course_table where course_id=arc.Old_Class_id) as Course_Nameold,arc.Roll_no_old,arc.Old_Section_name,arc.Session_id,arc.Slip_no      from admission_registor ar join  admission_registor_Change_admission_no_history arc on arc.Current_admission_no=ar.admissionserialnumber and  arc.Session_id=ar.Session_id where (ar.Transfer_Status='New' or ar.Transfer_Status='NT' or ar.Transfer_Status='Transferred') and ar.Status='1'   and ar.Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and arc.Change_type in ('Monthly Fees Delete','Annual Fees Delete','Admission Fees Delete') order by ar.rollnumber asc");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["classchange"] = "1";
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
            Label lbl_Slip_no = (Label)row.FindControl("lbl_Slip_no");
            Label lbl_currentadmission_no = (Label)row.FindControl("lbl_currentadmission_no");

            string path = "slip/Print_delete_bill.aspx?admissionno=" + lbl_currentadmission_no.Text + "&Slip_no=" + lbl_Slip_no.Text + "&sessionid=" + lbl_Session_id.Text;
            Response.Redirect(path, false);

        }
    }
}