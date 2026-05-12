using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class print_label : System.Web.UI.Page
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
                         
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        ViewState["flag"] = "0";
                        bind_session();
                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();


                        //bind_all_data();  
                        if (Request.QueryString["hostel"] != null)
                        {
                            ddlclass.SelectedValue = "0";
                            ddl_section.Text = "ALL";
                            ddl_studenttype.Text = "ALL";
                            ddl_days_hostel.SelectedValue = "1";
                            find_by_admission_in();

                        }
                        else if (Request.QueryString["dayScholar"] != null)
                        {
                            ddlclass.SelectedValue = "0";
                            ddl_section.Text = "ALL";
                            ddl_studenttype.Text = "ALL";

                            ddl_days_hostel.SelectedValue = "2";
                            find_by_admission_in();
                        }

                        else
                        {
                            bind_all_data();
                        }
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
            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.Text + "' order by rollnumber asc");

        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_by_class();
                ViewState["flag"] = "3";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_class()
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                find_by_session();
                ViewState["flag"] = "2";
            }
            else
            {
                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   group by Transfer_Status ";
            }
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
        }


        #region CountDataA

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
        #endregion

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
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
            string query2 = "";
            if (ddlclass.SelectedItem.Text == "All")
            {
                if (ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");

                }
                else
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by rollnumber asc");

                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");

                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");

                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by rollnumber asc");

                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by rollnumber asc");

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
                    find_by_session();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_session()
        {
            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  and  Session_id='" + ddlsession.SelectedValue + "'  order by rollnumber asc");

        }

        protected void btn_fnd_by_days_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddl_days_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select admission in.", "warning");
                    ddl_days_hostel.Focus();
                }
                else
                {
                    find_by_admission_in();
                    ViewState["flag"] = "4";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission_in()
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                if (ddl_studenttype.SelectedItem.Text == "ALL")
                {
                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by rollnumber asc");

                    }
                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                    }
                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                    }
                    else // BUS
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                    }
                }
                else
                {
                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by rollnumber asc");

                    }
                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                    }
                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                    }
                    else // BUS
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                    }
                }
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddl_studenttype.SelectedItem.Text == "ALL")
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                        }
                    }
                    else
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                        }
                    }
                }
                else
                {
                    if (ddl_studenttype.SelectedItem.Text == "ALL")
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by rollnumber asc");

                        }
                    }
                    else
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by rollnumber asc");

                        }
                    }
                }
            }
        }






        //===========================




        protected void lnk_print_label_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "All")
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
                    string adm_ids = "";
                    int growcount = rd_view.Items.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {
                            Label lbl_session = (Label)rd_view.Items[i].FindControl("lbl_session");
                            Session["SessionS"] = lbl_session.Text;
                            Label lbl_admissionserialnumber = (Label)rd_view.Items[i].FindControl("lbl_admissionserialnumber");
                            adm_ids = adm_ids += lbl_admissionserialnumber.Text + ",";
                            string reslink = "slip/student-label.aspx?printfrom=0&type=0&adm_no=" + adm_ids;
                            Response.Redirect(reslink, false);
                        }
                        else
                        {
                            k++;
                        }
                    }

                    if (k == growcount)
                    {
                        Response.Redirect("slip/student-label.aspx?printfrom=0&type=1&session_id=" + ddlsession.SelectedValue + "&class_id=" + ddlclass.SelectedValue + "&adm_no=0&section=" + ddl_section.SelectedValue + "&prinTtype=2", false);
                    }

                } 
            }
            catch (Exception ex)
            {
            }
        }
    }
}