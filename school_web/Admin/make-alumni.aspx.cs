using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class make_alumni : System.Web.UI.Page
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


                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                        mycode.bind_all_ddl_with_id_All_New(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();

                        ddlsession.SelectedValue = My.get_session_id();

                        string pagename_current = Path.GetFileName(Request.Path);
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

                        find_by_c_s_a();
                        ViewState["flag"] = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                //  My.saveExceptionDetails(ex, "StudentList");
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
            find_by_c_s_a();
            //if (ddlclass.SelectedItem.Text == "ALL")
            //{
            //    find_by_session();
            //    ViewState["flag"] = "2";
            //}
            //else
            //{
            //    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
            //    string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   group by Transfer_Status ";
            //    total_count_grid_list(query2);
            //}
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



        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_total_student.Text = "0";
            }
            else
            {
                lbl_class22.Text = "Session : "+ddlsession.SelectedItem.Text+ " Class : "+ddlclass.SelectedItem.Text+ " Section :"+ ddl_section.Text+ " Student Type :"+ ddl_studenttype.SelectedItem.Text;
                lbl_total_student.Text = dt.Rows.Count.ToString();
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        public void total_count_grid_list(string query2)
        {
            try
            {
                lbl_newadmission.Text = "0";
                lbltotal_readmission.Text = "0";
                lbl_total_trasfer_tonextsession.Text = "0";
                DataSet ds = mycode.Fill_Data_set(query2);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Transfer_Status"].ToString() == "New")
                        {
                            lbl_newadmission.Text = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "NT")
                        {
                            lbltotal_readmission.Text = dr["total"].ToString();
                        }
                        else
                        {
                            lbl_total_trasfer_tonextsession.Text = dr["total"].ToString();
                        }
                    }
                }
            }
            catch
            {
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
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  group by Transfer_Status ";
                    total_count_grid_list(query2);
                }
                else
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by rollnumber asc");

                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  group by Transfer_Status ";
                    total_count_grid_list(query2);
                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   group by Transfer_Status ";
                    total_count_grid_list(query2);


                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   group by Transfer_Status ";
                    total_count_grid_list(query2);
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by rollnumber asc");
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where   where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   group by Transfer_Status ";
                    total_count_grid_list(query2);
                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by rollnumber asc");
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  group by Transfer_Status ";
                    total_count_grid_list(query2);
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

            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='1'  group by Transfer_Status ";
            total_count_grid_list(query2);

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

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                    else // BUS
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and transportationtaken='Yes' group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                }
                else
                {
                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                        total_count_grid_list(query2);
                    }
                    else // BUS
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and transportationtaken='Yes' group by Transfer_Status ";
                        total_count_grid_list(query2);
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

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Class_id='" + ddlclass.SelectedValue + "'  group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and transportationtaken='Yes' group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                    }
                    else
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and Class_id='" + ddlclass.SelectedValue + "'  group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and transportationtaken='Yes' group by Transfer_Status ";
                            total_count_grid_list(query2);
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

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and transportationtaken='Yes' group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                    }
                    else
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by rollnumber asc");

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and   hosteltaken='Yes'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and transportationtaken='Yes' group by Transfer_Status ";
                            total_count_grid_list(query2);
                        }
                    }

                }
            }
        }

        protected void lnk_make_alimni_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_admission_no = (Label)row.FindControl("lbl_admissionserialnumber");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");

                    ViewState["rowID"] = Id.Text;
                    ViewState["AdmNO"] = lbl_admission_no.Text;
                    ViewState["sessionID"] = lbl_session_id.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_admission_no = (Label)row.FindControl("lbl_admissionserialnumber");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");

                    ViewState["rowID"] = Id.Text;
                    ViewState["AdmNO"] = lbl_admission_no.Text;
                    ViewState["sessionID"] = lbl_session_id.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }
            }
            catch (Exception ex)
            { }
        }

        protected void lnk_submit_alumni_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_remark.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    txt_remark.Focus();
                    Alertme("Please enter remarks.", "warning");
                }
                else
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Alumni_history (Session_id,Admission_no,Remark,Created_by,Created_date,Created_idate) values (@Session_id,@Admission_no,@Remark,@Created_by,@Created_date,@Created_idate)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionID"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", ViewState["AdmNO"].ToString());
                    cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    if (My.InsertUpdateData(cmd))
                    {
                        string qry = "update admission_registor set Transfer_Status='ALUMNI', StudentStatus='0',Status='0' where Id=" + ViewState["rowID"].ToString() + "";
                        My.exeSql(qry);
                        Alertme("Record has been updated successfully", "success");
                        if (ViewState["flag"].ToString() == "0")
                        {
                            find_by_c_s_a();
                        }
                        if (ViewState["flag"].ToString() == "1")
                        {
                            find_by_c_s_a();
                        }
                        if (ViewState["flag"].ToString() == "2")
                        {
                            find_by_session();
                        }
                        if (ViewState["flag"].ToString() == "3")
                        {
                            find_by_class();
                        }
                        if (ViewState["flag"].ToString() == "4")
                        {
                            find_by_admission_in();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                        Panel1.RenderControl(hw);
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