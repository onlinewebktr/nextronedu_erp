using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class student_list : System.Web.UI.Page
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
                        if (Request.QueryString["adm"] != null)
                        {
                            backbtns.HRef = "admission-report.aspx";
                        }


                        if (Session["MsgeS"] != null)
                        {
                            Alertme(Session["MsgeS"].ToString(), "success");
                            Session["MsgeS"] = null;
                        }

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


                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        ViewState["flag"] = "0";
                        //find_firm_details();
                        bind_session();
                        ddlsession.SelectedValue = My.get_session_id();
                        hd_session.Value = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        hd_class.Value = "ALL";
                        ddl_student_status.SelectedValue = "1";
                        fetch_section();

                        //bind_all_data();
                        //bind_data(); 

                        if (Request.QueryString["hostel"] != null)
                        {
                            lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text + " Class " + ddlclass.SelectedItem.Text;
                            ddlclass.SelectedValue = "0";
                            ddl_section.Text = "ALL";
                            ddl_studenttype.Text = "ALL";
                            hd_class.Value = "0";
                            hd_section.Value = "ALL";
                            hd_student_Type.Value = "ALL";
                            ddl_days_hostel.SelectedValue = "1";
                            find_by_admission_in();

                        }
                        else if (Request.QueryString["dayScholar"] != null)
                        {
                            lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text + " Class " + ddlclass.SelectedItem.Text;
                            hd_class.Value = "0";
                            hd_section.Value = "ALL";
                            hd_student_Type.Value = "ALL";

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

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            { 
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

                ViewState["Is_new_form"] = "0";
                try
                {
                    if (dt.Rows[0]["Is_new_form"].ToString() == "True")
                    {
                        ViewState["Is_new_form"] = "1";
                    }
                }
                catch (Exception ex)
                {
                } 
            }
        }
        private void bind_all_data()
        {
            hd_session.Value = ddlsession.SelectedValue;
            hd_class.Value = ddlclass.SelectedValue;
            hd_section.Value = ddl_section.Text;
            hd_student_Type.Value = ddl_studenttype.SelectedValue;


            lbl_class22.Text = ddlclass.SelectedItem.Text;
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_student_status.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'   order by ct.Position,rollnumber asc");
                    string query2 = "select Transfer_Status,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'   group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'   and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'  and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'   union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  ";
                    total_count_grid_list(query2);
                }
                else
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                    string query2 = "select Transfer_Status,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'   group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'   and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'  and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'   union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  ";
                    total_count_grid_list(query2);
                }

            }
            else
            {
                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");


                string query2 = "select Transfer_Status,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + "  ";
                total_count_grid_list(query2);

            }


            lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text;

        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_by_class(); fetch_section();
                ViewState["flag"] = "3";
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_section()
        {
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "'   order by Section");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section");
            }

        }

        private void find_by_class()
        {
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                find_by_session();
                ViewState["flag"] = "2";
            }
            else
            {
                hd_session.Value = ddlsession.SelectedValue;
                hd_class.Value = ddlclass.SelectedValue;
                hd_section.Value = "0";
                hd_student_Type.Value = "ALL";


                lbl_class22.Text = "";
                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");
                string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " ";
                total_count_grid_list(query2);
                lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text;
            }
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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
        private void bind_grd_view(string qry)
        {
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_total_student.Text = "0";
                ttlStudent.InnerText = "0";
                btn_excels.Visible = false;
            }
            else
            {
                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text + " Section :" + ddl_section.Text + " Student Type :" + ddl_studenttype.SelectedItem.Text;

                rd_view.DataSource = dt;
                rd_view.DataBind();
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

        public void total_count_grid_list(string query2)
        {
            try
            {
                ttlStudent.InnerText = "0";

                lbl_total_student.Text = "0";


                total_act_inactive.InnerText = "0";
                total_inactive.InnerText = "0";

                ttlnewStudent.InnerText = "0";
                ttloldStudent.InnerText = "0";
                ttlHostelStudent.InnerText = "0";
                ttlBusStudent.InnerText = "0";
                ttlTransferStudent.InnerText = "0";

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
                            ttlnewStudent.InnerText = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "NT")
                        {
                            lbltotal_readmission.Text = dr["total"].ToString();
                            ttloldStudent.InnerText = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "Hostel")
                        {
                            ttlHostelStudent.InnerText = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "Bus")
                        {
                            ttlBusStudent.InnerText = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "TotalStudent")
                        {
                            total_act_inactive.InnerText = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "TotalinactiveStudent")
                        {
                            total_inactive.InnerText = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "TotalactiveStudent")
                        {
                            ttlStudent.InnerText = dr["total"].ToString();
                            lbl_total_student.Text = dr["total"].ToString();
                        }


                        else
                        {
                            ttlTransferStudent.InnerText = dr["total"].ToString();
                            lbl_total_trasfer_tonextsession.Text = dr["total"].ToString();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                hd_id.Value = lbl_Id.Text;
                Response.Redirect("admission.aspx?stdid=" + lbl_Id.Text + "&admno=" + lbl_admissionserialnumber.Text, false);
            }
            catch
            {

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
                    lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
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
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = "0";
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;
                    if (ddl_student_status.SelectedValue == "ALL")
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");

                    }
                    else
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                    }



                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'   ";
                    total_count_grid_list(query2);
                }
                else
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = "0";
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;

                    if (ddl_student_status.Text == "ALL")
                    {

                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,rollnumber asc");
                    }
                    else
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                    }

                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' group by Transfer_Status  union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' ";
                    total_count_grid_list(query2);
                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;

                    if (ddl_student_status.Text == "ALL")
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");
                    }
                    else
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                    }
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor   where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    ";
                    total_count_grid_list(query2);


                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = ddl_section.Text;
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;

                    if (ddl_student_status.Text == "ALL")
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");

                    }
                    else
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                    }


                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and   Section='" + ddl_section.Text + "'  and Class_id='" + ddlclass.SelectedValue + "'   ";
                    total_count_grid_list(query2);
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = ddl_section.Text;
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;
                    if (ddl_student_status.Text == "ALL")
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by ct.Position,rollnumber asc");


                    }
                    else
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                    }

                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "'  and  Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.Text + "' union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and   Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  ";
                    total_count_grid_list(query2);
                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;
                    if (ddl_student_status.Text == "ALL")
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    order by ct.Position,rollnumber asc");
                    }
                    else
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'     order by ct.Position,rollnumber asc");
                    }
                    query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "'  and  Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and   Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and   Class_id='" + ddlclass.SelectedValue + "'   ";
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
                    mycode.bind_all_ddl_with_id_cap_All(ddlclass, "  Select  Course_Name, course_id  from Add_course_table order by  Position");
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
            hd_session.Value = ddlsession.SelectedValue;
            hd_class.Value = "0";
            hd_section.Value = "0";
            hd_student_Type.Value = "ALL";
            if (ddl_student_status.Text == "ALL")
            {
                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')    and  Session_id='" + ddlsession.SelectedValue + "'  order by ct.Position,rollnumber asc");
            }
            else
            {
                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')    and  Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
            }



            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  and  Session_id='" + ddlsession.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  and  Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  and  Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  ";
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
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_studenttype.SelectedItem.Text == "ALL")
                {
                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }



                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,'0' as total from admission_registor where  Session_id='" + ddlsession.SelectedValue + "' and   hosteltaken='Yes'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'   ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'   ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  ";
                        total_count_grid_list(query2);
                    }
                    else // BUS
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                        }

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'   ";
                        total_count_grid_list(query2);
                    }
                }
                else
                {
                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,'0' as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'   ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'    ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                        }

                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'    ";
                        total_count_grid_list(query2);
                    }
                    else // BUS
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                        }



                        string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'   ";
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
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");

                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all select 'Bus' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes'   union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "'  ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            if (ddl_student_status.Text == "ALL")
                            {

                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "'  ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "'  ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' ";
                            total_count_grid_list(query2);
                        }
                    }
                    else
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all select 'Bus' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");

                            }
                            else
                            {

                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "'  ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status = '" + ddl_student_status.SelectedValue + "'  and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "'  ";
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
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' union all select 'Bus' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "'  ";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");

                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'    union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Status='" + ddl_student_status.SelectedValue + "'  and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'";
                            total_count_grid_list(query2);
                        }
                    }
                    else
                    {
                        if (ddl_days_hostel.SelectedValue == "1") //Hostel
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' union all select 'Bus' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'";
                            total_count_grid_list(query2);
                        }
                        else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,rollnumber asc");
                            }

                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' ";
                            total_count_grid_list(query2);
                        }
                        else // BUS
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }
                            else
                            {
                                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Status='" + ddl_student_status.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,rollnumber asc");
                            }
                            string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' group by Transfer_Status union all select 'Hostel' as Transfer_Status,'0' as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' Transfer_Status ='" + ddl_studenttype.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "'   ";
                            total_count_grid_list(query2);
                        }
                    }
                }
            }
        }






        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string Class_id, string Section, string Student_type)
        {
            string sections = get_sections(Session, Class_id, Section, Student_type);
            string query = "";
            if (Class_id == "0")
            {
                if (Student_type == "ALL")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and ar.Transfer_Status ='" + Student_type + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                }
            }
            else
            {
                if (Section == "0")
                {
                    if (Student_type == "ALL")
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                    }
                    else
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and ar.Transfer_Status ='" + Student_type + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                    }
                }
                else
                {
                    if (Student_type == "ALL")
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                    }
                    else
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and ar.Transfer_Status ='" + Student_type + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                    }
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Status"].ToString(), Student_type);
                dr["Total"] = total_count;
            }

            List<object> chartData = new List<object>();
            List<string> countries = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Section")).Distinct().ToList();

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




        private static int get_total(string session_id, string class_id, string section, string Student_type)
        {
            string query = "";
            if (Student_type == "ALL")
            {
                query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and Status='1'";
            }
            else
            {
                query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Transfer_Status='" + Student_type + "' and Status='1'";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
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

        private static string get_sections(string Session, string Class_id, string Section, string Student_type)
        {
            string query = "";
            if (Class_id == "0")
            {
                if (Student_type == "ALL")
                {
                    query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else
                {
                    query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and Transfer_Status='" + Student_type + "'";
                }
            }
            else
            {
                if (Section == "0" || Section == "ALL")
                {
                    if (Student_type == "ALL")
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                    else
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Transfer_Status='" + Student_type + "'";
                    }
                }
                else
                {
                    if (Student_type == "ALL")
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Section='" + Section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                    else
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Section='" + Section + "' and Transfer_Status='" + Student_type + "'";
                    }
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "'A'";
            }
            else
            {
                string section = "";
                foreach (DataRow dr in dt.Rows)
                {
                    section = section + "'" + dr["Section"].ToString() + "',";
                }

                section = section.Remove(section.Length - 1);
                return section;
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    if (ViewState["Is_new_form"].ToString() == "1")
                    {
                        ((Panel)e.Item.FindControl("pnl_print_student")).Visible = false;
                        ((Panel)e.Item.FindControl("pnl_print_student_new")).Visible = true;
                    }
                    else
                    {
                        ((Panel)e.Item.FindControl("pnl_print_student")).Visible = true;
                        ((Panel)e.Item.FindControl("pnl_print_student_new")).Visible = false;
                    }
                    ((Panel)e.Item.FindControl("pnl_print_student_ladger")).Visible = true;
                }
                else
                {
                    ((Panel)e.Item.FindControl("pnl_print_student")).Visible = false;
                    ((Panel)e.Item.FindControl("pnl_print_student_new")).Visible = false;
                    ((Panel)e.Item.FindControl("pnl_print_student_ladger")).Visible = false;
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = false;
                }


                ////==========================================////
                if (((Label)e.Item.FindControl("lbl_pay_status")).Text.ToUpper() == "PAID")
                {
                    ((Label)e.Item.FindControl("lbl_pament")).Text = "Paid";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_pament")).Text = "Dues";
                }
            }
        }

        protected void lnk_upload_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                ViewState["DocAdmNo"] = lbl_admissionserialnumber.Text;
                ViewState["DocSessionId"] = lbl_session_id.Text;
                bind_doc_type();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
            }
            catch
            {

            }
        }

        private void bind_doc_type()
        {
            DataTable dt = mycode.FillData("select * from Upload_document_type order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rd_view_docs.DataSource = null;
                rd_view_docs.DataBind();
            }
            else
            {
                rd_view_docs.DataSource = dt;
                rd_view_docs.DataBind();
            }
        }

        private void fetch_saved_images(HtmlImage Image1, Label lbl_column_name, HtmlAnchor stdimgateg)
        {
            DataTable dt = mycode.FillData("select Image_path from Student_image_new where Admission_no='" + ViewState["DocAdmNo"].ToString() + "' and Image_Type='" + lbl_column_name.Text + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                Image1.Src = dt.Rows[0]["Image_path"].ToString();
                stdimgateg.HRef = dt.Rows[0]["Image_path"].ToString();
            }
        }

        protected void rd_view_docs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor stdimgateg = e.Item.FindControl("stdimgateg") as HtmlAnchor;
                HtmlImage Image1 = e.Item.FindControl("stdimages") as HtmlImage;
                Label lbl_column_name = ((Label)e.Item.FindControl("lbl_column_name")) as Label;
                fetch_saved_images(Image1, lbl_column_name, stdimgateg);
            }
        }



        #region ImageSave
        protected void btn_upload_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                Label lbl_column_name = (Label)row.FindControl("lbl_column_name");
                FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
                save_image(FileUpload1, lbl_Name, lbl_column_name);
                bind_doc_type();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
            }
            catch (Exception ex)
            {
            }
        }


        My mycodeMy = new My();
        private void save_image(FileUpload FileUpload1, Label lbl_Name, Label lbl_column_name)
        {
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    string files_path = upload_imag(FileUpload1, lbl_column_name.Text);
                    if (files_path == "")
                    {
                    }
                    else
                    {
                        if (mycodeMy.IsUserExist("select Id from Student_image_new where Admission_no='" + ViewState["DocAdmNo"].ToString() + "' and Image_type='" + lbl_column_name.Text + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'"))
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["DocAdmNo"].ToString());
                            cmd.Parameters.AddWithValue("@Image_name", lbl_Name.Text);
                            cmd.Parameters.AddWithValue("@Image_type", lbl_column_name.Text);
                            cmd.Parameters.AddWithValue("@Image_path", files_path);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["DocSessionId"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "Update Student_image_new set Image_path=@Image_path where Admission_no='" + ViewState["DocAdmNo"].ToString() + "' and Image_type='" + lbl_column_name.Text + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Image_path", files_path);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }


                        if (lbl_column_name.Text == "Student_image")
                        {
                            mycode.executequery("update admission_registor set studentimagepath='" + files_path + "' where admissionserialnumber='" + ViewState["DocAdmNo"].ToString() + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
                        }
                        if (lbl_column_name.Text == "Parent_Sign")
                        {
                            mycode.executequery("update admission_registor set signatureimagepath='" + files_path + "' where admissionserialnumber='" + ViewState["DocAdmNo"].ToString() + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
                        }
                        if (lbl_column_name.Text == "DOB_image")
                        {
                            mycode.executequery("update admission_registor set dobproof='" + files_path + "' where admissionserialnumber='" + ViewState["DocAdmNo"].ToString() + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
                    Alertme("Please Reduce or compress size of  " + lbl_Name.Text + " max(200kb).", "warning");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
                Alertme("Please upload " + lbl_Name.Text, "warning");
            }
        }

        private string upload_imag(FileUpload file, string column_name)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = file.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".PNG", ".jpg", ".JPG", ".JPEG", ".jpeg" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;
                    break;
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    file.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception exe)
                {
                    FileSaved = false;
                    Alertme("File has not save.", "warning");
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                String originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                string[] New_originalPath1 = originalPath2.Split('?');
                string originalPath1 = New_originalPath1[0].ToString();

                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilePath;
        }

        #endregion

        protected void tn_find_by_date_Click(object sender, EventArgs e)
        {
            lbl_class22.Text = "";
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (txt_from_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_from_date.Focus();
                }
                else if (txt_to_date.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_to_date.Focus();
                }
                else
                {
                    int idate1 = My.DateConvertToIdate(txt_from_date.Text);
                    int idate2 = My.DateConvertToIdate(txt_to_date.Text);
                    if (idate1 < idate2)
                    {
                        lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
                        find_by_date();
                        ViewState["flag"] = "15";
                    }
                    else
                    {
                        Alertme("To date can not be less than from date.", "warning");
                        txt_to_date.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_date()
        {
            int idate1 = My.DateConvertToIdate(txt_from_date.Text);
            int idate2 = My.DateConvertToIdate(txt_to_date.Text);
            string query2 = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_studenttype.Text == "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + " and Status='" + ddl_student_status.SelectedValue + "'   and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");

                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  and payment_status='Paid' union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and payment_status='Paid' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and payment_status='Paid'";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {

                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");


                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
                else
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");

                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'   Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "   ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and payment_status='Paid'   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and payment_status='Paid' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'   Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " payment_status='Paid' ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;


                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");

                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and (payment_status='Unpaid' or payment_status='Dues')   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'   Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + " and Status='" + ddl_student_status.SelectedValue + "'   and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "   union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where  (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   Session_id='" + ddlsession.SelectedValue + "'  and   Class_id='" + ddlclass.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " (payment_status='Unpaid' or payment_status='Dues') ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");

                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "   union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status = 'New' or Transfer_Status = 'NT' or Transfer_Status = 'Transferred') and Status = '1'   Session_id = '" + ddlsession.SelectedValue + "'  and Class_id = '" + ddlclass.SelectedValue + "'  and admission_idate>= " + idate1 + "  and admission_idate<= " + idate2 + "(payment_status = 'Paid' ) ";



                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + " and Status='" + ddl_student_status.SelectedValue + "'   and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "   union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status = 'New' or Transfer_Status = 'NT' or Transfer_Status = 'Transferred') and Status = '1'   Session_id = '" + ddlsession.SelectedValue + "'  and Class_id = '" + ddlclass.SelectedValue + "'  and admission_idate>= " + idate1 + "  and admission_idate<= " + idate2 + "(payment_status = 'Unpaid' or payment_status = 'Dues')";
                        total_count_grid_list(query2);
                    }
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;


                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='1'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "'";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");

                        }



                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Paid' )   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Paid' )  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='1'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' and (payment_status='Paid' ) ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='1'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues' )";
                        total_count_grid_list(query2);
                    }
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }



                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Status='1'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "'";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }



                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Paid' )   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Paid' )  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Status='1'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "' and (payment_status='Paid' )";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and Status='" + ddl_student_status.SelectedValue + "'  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')   and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Section='" + ddl_section.Text + "'";
                        total_count_grid_list(query2);
                    }
                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "' order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and    Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and    Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  and payment_status='Paid' union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and    Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and payment_status='Paid' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and    Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and payment_status='Paid'";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and transportationtaken='Yes' union all select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    and Class_id='" + ddlclass.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + "  and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and    Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and    Status='0'    and   Class_id='" + ddlclass.SelectedValue + "' Session_id='" + ddlsession.SelectedValue + "' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
            }
        }

        protected void btn_find_by_pay_status_Click(object sender, EventArgs e)
        {
            try
            {
                find_by_payment_status();
                ViewState["flag"] = "14";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_payment_status()
        {
            string query2 = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_studenttype.Text == "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' ";
                        total_count_grid_list(query2);
                    }

                    else if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and payment_status='Paid'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and payment_status='Paid'  union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and payment_status='Paid' ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') ";
                        total_count_grid_list(query2);


                    }
                }
                else
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");

                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' ";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' order by ct.Position,rollnumber asc");
                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and payment_status='Paid'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and payment_status='Paid' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and payment_status='Paid'";
                        total_count_grid_list(query2);
                    }
                    else if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = "0";
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");

                        }



                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "'    and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and payment_status='Paid'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and payment_status='Paid' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and payment_status='Paid' ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid' and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid' and transportationtaken='Yes'  union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and payment_status='Paid'  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and payment_status='Paid' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and payment_status='Paid'";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status = '" + ddl_student_status.SelectedValue + "'   order by ct.Position, rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;

                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid'  order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,rollnumber asc");
                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid'  group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Paid'   )  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Paid' ) union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Paid' )";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    if (ddl_pay_status.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by ct.Position,rollnumber asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'    order by ct.Position,rollnumber asc");
                        }


                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  ";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Paid")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,rollnumber asc");
                        }
                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and payment_status='Paid' group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and payment_status='Paid' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and payment_status='Paid' and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (payment_status='Paid' )  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (payment_status='Paid'  ) union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (payment_status='Paid'  )";
                        total_count_grid_list(query2);
                    }
                    if (ddl_pay_status.Text == "Dues")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_student_Type.Value = ddl_studenttype.SelectedValue;
                        if (ddl_student_status.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");

                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,rollnumber asc");

                        }

                        query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and (payment_status='Unpaid' or payment_status='Dues') group by Transfer_Status union all select 'Hostel' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and (payment_status='Unpaid' or payment_status='Dues') and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1' and (payment_status='Unpaid' or payment_status='Dues') and transportationtaken='Yes' union all  select 'TotalStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues')  union all  select 'TotalinactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='0'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues') union all  select 'TotalactiveStudent' as Transfer_Status ,count(Id) as total  from admission_registor   where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'    and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues')";
                        total_count_grid_list(query2);
                    }
                }
            }
        }



        #region ExcelDownloaD
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Download"].ToString() == "1")
            {
                try
                {
                    string query2 = "";
                    string house = "Select top 1 house_name from house_master where house_id=admission_registor.house";
                    string query = "select session as [Session],CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Day Scholar' END AS [Student type],CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS [Status],admissionserialnumber as [Admission no], class as [Class],Section,rollnumber as [Roll No],studentname as [Student Name],gender as [Gender],dob as [DOB],aadharno as [Aadhar no],blood_group as [B_group],mobilenumber as [Mobile no],email_id as [Email id],fathername as [Father name],father_mob as [Father mobile no],mothername as [Mother name],guardianname as [Guardian name],parentincome as [Parent Income],Admission_no_date as [Old Admission No],fatherqualification as [Father Qualification],motherqualifiaction as [Mother_qualifiaction], dateofadmission as [Date of Admission],religion as [Religion],cast as [Cast],careof as [Address],city as [City],postoffice as [Post Office],policestation as [Police Station],district as [District],pin as [Pincode],(" + house + ") as House, Pwd as [App login password] from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id ";
                    if (ViewState["flag"].ToString() == "0")
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            query2 = query + " where(Transfer_Status = 'New' or Transfer_Status = 'NT'   or Transfer_Status = 'Transferred') and StudentStatus = 'AV' and Session_id = '" + ddlsession.SelectedValue + "'   order by ct.Position, Section, rollnumber asc";
                            DataTable dt = mycode.FillData(query2);
                            export_to_excel(dt, "Student_list");
                        }
                        else
                        {
                            query2 = query + " where(Transfer_Status = 'New' or Transfer_Status = 'NT'   or Transfer_Status = 'Transferred') and StudentStatus = 'AV' and Session_id = '" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                            DataTable dt = mycode.FillData(query2);
                            export_to_excel(dt, "Student_list");
                        }
                    }

                    if (ViewState["flag"].ToString() == "1")
                    {
                        //if (ddl_student_status.Text == "ALL")
                        //{
                        //    query2 = query + " where(Transfer_Status = 'New' or Transfer_Status = 'NT'   or Transfer_Status = 'Transferred') and StudentStatus = 'AV' and Session_id = '" + ddlsession.SelectedValue + "'   order by ct.Position, Section, rollnumber asc";
                        //    DataTable dt = mycode.FillData(query2);
                        //    export_to_excel(dt, "Student_list");
                        //}
                        //else
                        //{
                        //    query2 = query + " where(Transfer_Status = 'New' or Transfer_Status = 'NT'   or Transfer_Status = 'Transferred') and StudentStatus = 'AV' and Session_id = '" + ddlsession.SelectedValue + "'   and   Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                        //    DataTable dt = mycode.FillData(query2);
                        //    export_to_excel(dt, "Student_list");
                        //}
                        //if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                        //{
                        //    if (ddl_student_status.Text == "ALL")
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred')    order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    else
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and Status = '" + ddl_student_status.SelectedValue + "'    and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred')    order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    DataTable dt = mycode.FillData(query2);
                        //    export_to_excel(dt, "Student_list");
                        //}
                        //else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                        //{
                        //    if (ddl_student_status.Text == "ALL")
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred')   order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    else
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and  Status='" + ddl_student_status.SelectedValue + "'(Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred')   order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    DataTable dt = mycode.FillData(query2);
                        //    export_to_excel(dt, "Student_list");

                        //}
                        //else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                        //{
                        //    if (ddl_student_status.Text == "ALL")
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and    Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    else
                        //    {
                        //        query2 = query + "  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and    Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    DataTable dt = mycode.FillData(query2);
                        //    export_to_excel(dt, "Student_list");
                        //}
                        //else if (ddlclass.SelectedItem.Text.ToUpper() == "ALL" && ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                        //{
                        //    if (ddl_student_status.Text == "ALL")
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    order by ct.Position,rollnumber asc";
                        //    }
                        //    else
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'    order by ct.Position,rollnumber asc ";
                        //    }
                        //    DataTable dt = mycode.FillData(query2);
                        //    export_to_excel(dt, "Student_list");
                        //}
                        //else if (ddlclass.SelectedItem.Text.ToUpper() != "ALL" && ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                        //{
                        //    if (ddl_student_status.Text == "ALL")
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and    Transfer_Status ='" + ddl_studenttype.SelectedValue + "'    order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    else
                        //    {
                        //        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and    Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,Section,rollnumber asc";
                        //    }
                        //    DataTable dt = mycode.FillData(query);
                        //    export_to_excel(dt, "Student_list");
                        //}










                        if (ddlclass.SelectedItem.Text == "ALL")
                        {
                            if (ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_student_status.SelectedValue == "ALL")
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else
                            {
                                if (ddl_student_status.Text == "ALL")
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                        }
                        else
                        {
                            if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_student_status.Text == "ALL")
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_student_status.Text == "ALL")
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "' order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                            {
                                if (ddl_student_status.Text == "ALL")
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "' order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                            {
                                if (ddl_student_status.Text == "ALL")
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else
                                {
                                    query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "' order by ct.Position, Section, rollnumber asc";
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                        }
                    }


                    if (ViewState["flag"].ToString() == "2")
                    {
                        if (ddl_student_status.Text == "ALL")
                        {
                            query2 = query + "  where Session_id='" + ddlsession.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   order by ct.Position,Section,rollnumber asc";
                        }
                        else
                        {
                            query2 = query + "  where Session_id='" + ddlsession.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='" + ddl_student_status.SelectedValue + "'   order by ct.Position,Section,rollnumber asc";
                        }
                        DataTable dt = mycode.FillData(query2);
                        export_to_excel(dt, "Student_list");
                    }
                    if (ViewState["flag"].ToString() == "3")
                    {
                        if (ddlclass.SelectedValue == "0")
                        {

                            if (ddl_student_status.Text == "ALL")
                            {
                                query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  order by ct.Position,Section,rollnumber asc";
                            }
                            else
                            {
                                query2 = query + "  where Session_id='" + ddlsession.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,Section,rollnumber asc";
                            }
                            DataTable dt = mycode.FillData(query2);
                            export_to_excel(dt, "Student_list");
                        }
                        else
                        {
                            if (ddl_student_status.Text == "ALL")
                            {
                                query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')    order by ct.Position,Section,rollnumber asc";
                            }
                            else
                            {
                                query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,Section,rollnumber asc";
                            }
                            DataTable dt = mycode.FillData(query2);
                            export_to_excel(dt, "Student_list");
                        }
                    }
                    if (ViewState["flag"].ToString() == "4")
                    {
                        if (ddlclass.SelectedItem.Text == "ALL")
                        {
                            if (ddl_studenttype.SelectedItem.Text == "ALL")
                            {
                                if (ddl_days_hostel.SelectedValue == "1") //Hostel
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + "  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by ct.Position,Section,rollnumber asc";
                                    }
                                    else
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and  Status='" + ddl_student_status.SelectedValue + "' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by ct.Position,Section,rollnumber asc";

                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                    }
                                    else
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else // BUS
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "' and hosteltaken ='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);



                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else
                            {
                                if (ddl_days_hostel.SelectedValue == "1") //Hostel
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + "  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "' and hosteltaken ='Yes' and Session_id='" + ddlsession.SelectedValue + "' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);



                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' union all select 'Bus' as Transfer_Status,count(Id) as total from admission_registor where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);



                                    export_to_excel(dt, "Student_list");
                                }
                                else // BUS
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
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
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);



                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + "  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);



                                        export_to_excel(dt, "Student_list");
                                    }
                                    else // BUS
                                    {

                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                }
                                else
                                {
                                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + "  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + "  where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                                    {

                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);



                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);

                                        export_to_excel(dt, "Student_list");
                                    }
                                    else // BUS
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);


                                        export_to_excel(dt, "Student_list");
                                    }
                                }
                            }
                            else
                            {
                                if (ddl_studenttype.SelectedItem.Text == "ALL")
                                {
                                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')     and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);


                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                                    {

                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);



                                        export_to_excel(dt, "Student_list");
                                    }
                                    else // BUS
                                    {

                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);


                                        export_to_excel(dt, "Student_list");
                                    }
                                }
                                else
                                {
                                    if (ddl_days_hostel.SelectedValue == "1") //Hostel
                                    {

                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and  hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='Yes' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "2") //Day Scholar
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and admissionserialnumber not in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                    else if (ddl_days_hostel.SelectedValue == "3") //Day Scholar with lunch
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber in (select Admission_no from Student_mapping_with_boarding_with_lunch where Admission_no=admission_registor.admissionserialnumber and Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id) order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);
                                        export_to_excel(dt, "Student_list");
                                    }
                                    else // BUS
                                    {
                                        if (ddl_student_status.Text == "ALL")
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";

                                        }
                                        else
                                        {
                                            query2 = query + " where Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and hosteltaken='No' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Section='" + ddl_section.SelectedItem.Text + "' and transportationtaken='Yes' order by ct.Position,Section,rollnumber asc";
                                        }
                                        DataTable dt = mycode.FillData(query2);


                                        export_to_excel(dt, "Student_list");
                                    }
                                }
                            }
                        }
                    }


                    ///====================
                    /// PAYMENT STATUS
                    if (ViewState["flag"].ToString() == "14")
                    {
                        if (ddlclass.SelectedItem.Text == "ALL")
                        {
                            if (ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and(Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + "  where Session_id='" + ddlsession.SelectedValue + "'       and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + "  where Session_id='" + ddlsession.SelectedValue + "'  and Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Paid")
                                {


                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);



                                    export_to_excel(dt, "Student_list");
                                }
                            }
                        }
                        else
                        {
                            if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   and(Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "' and(Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'      and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and  Status='" + ddl_student_status.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and  Status='" + ddl_student_status.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='" + ddl_student_status.SelectedValue + "'  and(Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='" + ddl_student_status.SelectedValue + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='" + ddl_student_status.SelectedValue + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and payment_status='Paid' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='" + ddl_student_status.SelectedValue + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   payment_status='Paid' order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and payment_status='Paid' order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'     and (payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and(payment_status='Unpaid' or payment_status='Dues') order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                            }
                        }
                    }

                    ///====================
                    /// Admission DATE
                    if (ViewState["flag"].ToString() == "15")
                    {
                        int idate1 = My.DateConvertToIdate(txt_from_date.Text);
                        int idate2 = My.DateConvertToIdate(txt_to_date.Text);

                        if (ddlclass.SelectedItem.Text == "ALL")
                        {
                            if (ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'       and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'  and Status='" + ddl_student_status.SelectedValue + "' and(Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where  Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where  Session_id='" + ddlsession.SelectedValue + "'  and Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid' and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'     and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'  and  Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'   and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Paid")
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                else if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "'  and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                            }
                        }
                        else
                        {
                            if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and  Status='" + ddl_student_status.SelectedValue + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and  (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and (payment_status='Unpaid' or payment_status='Dues') and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'      and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='" + ddl_student_status.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='" + ddl_student_status.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='" + ddl_student_status.SelectedValue + "'  and(Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  and (payment_status='Dues' or payment_status='Unpaid')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {

                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='" + ddl_student_status.SelectedValue + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='" + ddl_student_status.SelectedValue + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and payment_status='Paid'  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='" + ddl_student_status.SelectedValue + "'   and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                            else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                            {
                                if (ddl_pay_status.Text == "ALL")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and   admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);

                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Paid")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "'  and payment_status='Paid' and admission_idate>=" + idate1 + " and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);
                                    export_to_excel(dt, "Student_list");
                                }
                                if (ddl_pay_status.Text == "Dues")
                                {
                                    if (ddl_student_status.Text == "ALL")
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";

                                    }
                                    else
                                    {
                                        query2 = query + " where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='" + ddl_student_status.SelectedValue + "' and (payment_status='Unpaid' or payment_status='Dues')  and admission_idate>=" + idate1 + "  and admission_idate<=" + idate2 + " order by ct.Position,Section,rollnumber asc";
                                    }
                                    DataTable dt = mycode.FillData(query2);


                                    export_to_excel(dt, "Student_list");
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void export_to_excel(DataTable dt, string file)
        {
            string FileName = file + DateTime.Now + ".xls";
            string attachment = "attachment; filename=" + FileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }

            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        #endregion
    }
}