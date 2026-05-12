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
using System.Web.UI.DataVisualization.Charting;
using System.Web.Services;
namespace school_web.Admin
{
    public partial class Active_Inactive_Student_List : System.Web.UI.Page
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
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }


                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All_New(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();

                        mycode.bind_ddlall(ddl_Section, "Select distinct Section from admission_registor order by Section ");

                        ddl_Section.Text = My.get_top_one_section();

                        
                        if (Request.QueryString["inactive"] != null)
                        {
                            if (Request.QueryString["inactive"].ToString() == "0")
                            {
                                ddl_Status.SelectedValue = "2";
                                find_data();
                            } 
                        }
                        else
                        {
                            find_data();
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Active_Inactive_Student");
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
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                hd_session.Value = ddlsession.SelectedValue;
                hd_class.Value = "0";
                hd_section.Value = "0";
                hd_status.Value = ddl_Status.SelectedValue;

                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + "    order by rollnumber asc");

                string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                total_count_grid_list(query2);
            }
            else
            {
                hd_session.Value = ddlsession.SelectedValue;
                hd_class.Value = ddlclass.SelectedValue;
                hd_section.Value = "0";
                hd_status.Value = ddl_Status.SelectedValue;

                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text + "Class: " + ddlclass.SelectedItem.Text;
                bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + "  order by rollnumber asc");

                string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and (Transfer_Status='New' or Transfer_Status='NT' )    group by Status ";
                total_count_grid_list(query2);
            }

        }
        public void total_count_grid_list(string query2)
        {
            try
            {
                lbltotal_active.Text = "0";
                lbltotal_inactive.Text = "0";

                DataSet ds = mycode.Fill_Data_set(query2);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Status"].ToString() == "1")
                        {
                            lbltotal_active.Text = dr["total"].ToString();
                        }
                        else if (dr["total"].ToString() == "0")
                        {
                            lbltotal_inactive.Text = dr["total"].ToString();
                        }
                        else
                        {
                            lbltotal_inactive.Text = dr["total"].ToString();
                        }
                    }

                    int total = My.toint(lbltotal_active.Text) + My.toint(lbltotal_inactive.Text);

                    lbl_total_student.Text = total.ToString();
                }
            }
            catch
            {
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
        private void bind_grd_view(string query)
        {
            lbl_class22.Text = "";
            print1.Visible = false;
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();

                lbl_class222.Text = "";


            }
            else
            {
                lbl_class222.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_Section.SelectedItem.Text+" Status : "+ ddl_Status.SelectedItem.Text;
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



        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_istatus = (Label)e.Row.FindControl("lbl_istatus");
                Label lbl_status = (Label)e.Row.FindControl("lbl_status");

                if (lbl_istatus.Text == "")
                {
                    lbl_status.Text = "Inactive";

                }

                else if (lbl_istatus.Text == "0")
                {
                    lbl_status.Text = "Inactive";

                }
                else
                {
                    lbl_status.Text = "Active";

                }
            }
        }


         
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                find_data();
            }
            catch (Exception ex)
            {
            }

        }

        private void find_data()
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }


            else
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    if (ddl_Section.SelectedItem.Text == "ALL")
                    {
                        if (ddl_Status.SelectedValue == "0")
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = "0";
                            hd_status.Value = ddl_Status.SelectedValue;

                            lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + "  order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                            total_count_grid_list(query2);
                        }
                        else
                        {
                            if (ddl_Status.SelectedValue == "1")
                            {
                                hd_session.Value = ddlsession.SelectedValue;
                                hd_class.Value = ddlclass.SelectedValue;
                                hd_section.Value = "0";
                                hd_status.Value = ddl_Status.SelectedValue;

                                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                                bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + "  and Status='1'  order by rollnumber asc");
                                string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                                total_count_grid_list(query2);
                            }
                            else
                            {
                                hd_session.Value = ddlsession.SelectedValue;
                                hd_class.Value = ddlclass.SelectedValue;
                                hd_section.Value = "0";
                                hd_status.Value = ddl_Status.SelectedValue;

                                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                                bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + "  and Status='0'  order by rollnumber asc");
                                string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                                total_count_grid_list(query2);
                            }
                        }
                    }
                    else
                    {
                        if (ddl_Status.SelectedValue == "0")
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = ddlsession.SelectedValue;
                            hd_status.Value = ddl_Status.SelectedValue;

                            lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Section='" + ddl_Section.SelectedItem.Text + "' order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                            total_count_grid_list(query2);
                        }
                        else
                        {
                            if (ddl_Status.SelectedValue == "1")
                            {
                                hd_session.Value = ddlsession.SelectedValue;
                                hd_class.Value = ddlclass.SelectedValue;
                                hd_section.Value = ddlsession.SelectedValue;
                                hd_status.Value = ddl_Status.SelectedValue;
                                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                                bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + "  and Status='1'  and Section='" + ddl_Section.SelectedItem.Text + "' order by rollnumber asc");
                                string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                                total_count_grid_list(query2);
                            }
                            else
                            {
                                hd_session.Value = ddlsession.SelectedValue;
                                hd_class.Value = ddlclass.SelectedValue;
                                hd_section.Value = ddlsession.SelectedValue;
                                hd_status.Value = ddl_Status.SelectedValue;
                                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text;
                                bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + "  and Status='0'  and Section='" + ddl_Section.SelectedItem.Text + "' order by rollnumber asc");
                                string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                                total_count_grid_list(query2);
                            }
                        }
                    }
                }
                else
                {
                    if (ddl_Section.Text == "ALL")
                    {
                        if (ddl_Status.SelectedValue == "0")// ALL
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = "0";
                            hd_status.Value = ddl_Status.SelectedValue;

                            lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text + " Section: " + ddl_Section.Text + " Status: " + ddl_Status.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + "  order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and (Transfer_Status='New' or Transfer_Status='NT' )    group by Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_Status.SelectedValue == "2")// inactive
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = "0";
                            hd_status.Value = ddl_Status.SelectedValue;
                            lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text + " Section: " + ddl_Section.Text + " Status: " + ddl_Status.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Status=0  order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and (Transfer_Status='New' or Transfer_Status='NT')    group by Status ";
                            total_count_grid_list(query2);

                        }
                        else
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = "0";
                            hd_status.Value = ddl_Status.SelectedValue;
                            lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text + " Section: " + ddl_Section.Text + " Status: " + ddl_Status.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Status=1  order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and (Transfer_Status='New' or Transfer_Status='NT' )    group by Status ";
                            total_count_grid_list(query2);
                        }

                    }
                    else
                    {
                        if (ddl_Status.SelectedValue == "0")// ALL
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = ddl_Section.Text;
                            hd_status.Value = ddl_Status.SelectedValue;
                            lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text + " Section: " + ddl_Section.Text + " Status: " + ddl_Status.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "'  order by rollnumber asc");

                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "' and  (Transfer_Status='New' or Transfer_Status='NT' )    group by Status ";
                            total_count_grid_list(query2);

                        }
                        else if (ddl_Status.SelectedValue == "2")// inactive
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = ddl_Section.Text;
                            hd_status.Value = ddl_Status.SelectedValue;
                            lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text + " Section: " + ddl_Section.Text + " Status: " + ddl_Status.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "' and Status=0  order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "' and  (Transfer_Status='New' or Transfer_Status='NT' )    group by Status ";
                            total_count_grid_list(query2);
                        }
                        else
                        {
                            hd_session.Value = ddlsession.SelectedValue;
                            hd_class.Value = ddlclass.SelectedValue;
                            hd_section.Value = ddl_Section.Text;
                            hd_status.Value = ddl_Status.SelectedValue;
                            lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text + " Section: " + ddl_Section.Text + " Status: " + ddl_Status.SelectedItem.Text;
                            bind_grd_view("select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "' and Status=1  order by rollnumber asc");
                            string query2 = "select  Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id=" + ddlclass.SelectedValue + " and Section='" + ddl_Section.SelectedValue + "' and  (Transfer_Status='New' or Transfer_Status='NT' )    group by Status ";
                            total_count_grid_list(query2);
                        }
                    }
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




        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string Class_id, string Section, string Statuss)
        {
            string query = "";
            if (Class_id == "0")
            {
                if (Statuss == "0")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and  sm.Status in ('Active','Inactive') order by ad.Position asc";
                }
                else if (Statuss == "1")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and  sm.Status in ('Active') order by ad.Position asc";
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and  sm.Status in ('Inactive') order by ad.Position asc";
                }
            }
            else
            {
                if (Statuss == "0")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "'  and ar.Class_id='" + Class_id + "' and  sm.Status in ('Active','Inactive') order by ad.Position asc";
                }
                else if (Statuss == "1")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and ar.Class_id='" + Class_id + "' and  sm.Status in ('Active') order by ad.Position asc";
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and ar.Class_id='" + Class_id + "' and  sm.Status in ('Inactive') order by ad.Position asc";
                }

            }


            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Section, dr["Status"].ToString());
                dr["Total"] = total_count;
            }




            List<object> chartData = new List<object>();
            List<string> countries = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Status")).Distinct().ToList();

            countries.Insert(0, "Status");

            //Add the Countries Array to the Chart Array.
            chartData.Add(countries.ToArray());


            //Get the DISTINCT Date.
            List<string> years = (from p in dt.AsEnumerable()
                                  select p.Field<string>("Class")).Distinct().ToList();

            //Loop through the Date.
            foreach (string year in years)
            {

                //Get the Total of Orders for each Status for the Date.
                List<object> totals = (from p in dt.AsEnumerable()
                                       where p.Field<string>("Class") == year
                                       select p.Field<Int32>("Total")).Cast<object>().ToList();

                //Insert the Year value as Label in First position.
                totals.Insert(0, year.ToString());

                //Add the Years Array to the Chart Array.
                chartData.Add(totals.ToArray());
            }

            return chartData;
        }




        private static int get_total(string session_id, string class_id, string Section, string status)
        {
            string query = "";
            if (Section == "0")
            {
                if (status == "0")
                {
                    query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else if (status == "Active")
                {
                    query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else
                {
                    query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Status='0' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
            }
            else
            {
                if (status == "0")
                {
                    query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + Section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else if (status == "Active")
                {
                    query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + Section + "' and Status='1' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else
                {
                    query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + Section + "' and Status='0' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                int count = My.toIntS(dt.Rows[0][0].ToString());
                return count;
            }
        }

    }
}