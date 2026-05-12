using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class castewise_student : System.Web.UI.Page
    {
        compLN comP = new compLN();
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
                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                      
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
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_All_New(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();

                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();

                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        comP.bind_ddlALL(ddl_cast, "select Category_name from Cast_category"); 
                        find_by_caste(); 
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
        protected void btn_find_Click(object sender, EventArgs e)
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
                    find_by_caste();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_by_caste()
        {
            if (ddl_cast.SelectedItem.Text == "ALL")
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = "0";
                    hd_section.Value = "0";
                    hd_caste.Value = ddl_cast.SelectedItem.Text; 
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' order by rollnumber asc");
                }
                else
                {
                    if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_caste.Value = ddl_cast.SelectedItem.Text;

                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.SelectedItem.Text;
                        hd_caste.Value = ddl_cast.SelectedItem.Text;

                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                    }
                }
            }
            else
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = "0";
                    hd_section.Value = "0";
                    hd_caste.Value = ddl_cast.SelectedItem.Text; 
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1' and Session_id='" + ddlsession.SelectedValue + "' and cast='" + ddl_cast.Text + "' order by rollnumber asc");
                }
                else
                {
                    if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = "0";
                        hd_caste.Value = ddl_cast.SelectedItem.Text;

                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and cast='" + ddl_cast.Text + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        hd_session.Value = ddlsession.SelectedValue;
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.SelectedItem.Text;
                        hd_caste.Value = ddl_cast.SelectedItem.Text;

                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and cast='" + ddl_cast.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                    }
                }
            }
        }



        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            btn_excels.Visible = false;
            print1.Visible = false;
            lbl_class222.Text = "";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                lbl_class222.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section :" + ddl_section.Text+" Caste : "+ ddl_cast.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();

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

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Is_Download"].ToString() == "1")
                {
                    excels_download();

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }



            }
            catch (Exception ex)
            {
            }
        }

        private void excels_download()
        {
            DataTable dt = new DataTable();

            if (ddl_cast.SelectedItem.Text != "ALL")
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    dt = mycode.FillData("Select admissionserialnumber,Old_Admission_Date as  Admission_Date,dateofadmission as Current_Session_Date,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,cast as caste,class,session,Section,rollnumber,studentname,fathername from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and cast='" + ddl_cast.SelectedItem.Text + "' order by rollnumber asc");
                }
                else
                {
                    if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        dt = mycode.FillData("Select admissionserialnumber,Old_Admission_Date as  Admission_Date,dateofadmission as Current_Session_Date,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,cast as caste,class,session,Section,rollnumber,studentname,fathername from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and cast='" + ddl_cast.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        dt = mycode.FillData("Select admissionserialnumber,Old_Admission_Date as  Admission_Date,dateofadmission as Current_Session_Date,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,cast as caste,class,session,Section,rollnumber,studentname,fathernamefrom admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and cast='" + ddl_cast.SelectedItem.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                    }
                }
            }
            else
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {
                    dt = mycode.FillData("Select admissionserialnumber,Old_Admission_Date as  Admission_Date,dateofadmission as Current_Session_Date,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,cast as caste,class,session,Section,rollnumber,studentname,fathernamee from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'   order by rollnumber asc");
                }
                else
                {
                    if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        dt = mycode.FillData("Select admissionserialnumber,Old_Admission_Date as  Admission_Date,dateofadmission as Current_Session_Date,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,cast as caste,class,session,Section,rollnumber,studentname,fathername from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'   and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc");
                    }
                    else
                    {
                        dt = mycode.FillData("Select admissionserialnumber,Old_Admission_Date as  Admission_Date,dateofadmission as Current_Session_Date,CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,cast as caste,class,session,Section,rollnumber,studentname,fathername from admission_registor  where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "'     and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc");
                    }
                }


            }
            export_to_excel(dt, "Student_list");
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


        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string Class_id, string Section, string Caste)
        {
            string caste = get_caste(Session, Class_id, Section, Caste);
            string query = "";

            if (Class_id == "0")
            {
                if (Caste == "ALL")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status as Caste,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "'  and sm.Type='3' order by ad.Position asc";//and  sm.Status in (" + caste + ")
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status as Caste,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "'  and sm.Type='3' and  sm.Status in (" + caste + ") order by ad.Position asc";
                }
            }
            else
            {
                if (Section == "0")
                {
                    if (Caste == "ALL")
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status as Caste,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "'  and sm.Type='3' order by ad.Position asc";//and  sm.Status in (" + Caste + ")
                    }
                    else
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status as Caste,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and sm.Type='3' and  sm.Status in (" + Caste + ") order by ad.Position asc";
                    }
                }
                else
                {
                    if (Caste == "ALL")
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status as Caste,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "'  and sm.Type='3' order by ad.Position asc";//and  sm.Status in (" + Caste + ")
                    }
                    else
                    {
                        query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status as Caste,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "'  and sm.Type='3' and  sm.Status in (" + Caste + ") order by ad.Position asc";
                    }
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Section, dr["Caste"].ToString());
                dr["Total"] = total_count;
            }

            List<object> chartData = new List<object>();
            List<string> countries = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Caste")).Distinct().ToList();

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




        private static int get_total(string session_id, string class_id, string section, string Caste)
        {
            string query = "";
            if (section == "0")
            {
                query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and cast='" + Caste + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and Status='1'";
            }
            else
            {
                query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and cast='" + Caste + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New') and Status='1'";
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


        private static string get_caste(string Session, string Class_id, string Section, string Caste)
        {
            string query = "";
            if (Class_id == "0")
            {
                if (Caste == "ALL")
                {
                    query = "select DISTINCT cast from admission_registor where Session_id='" + Session + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else
                {
                    query = "select DISTINCT cast from admission_registor where Session_id='" + Session + "' and cast='" + Caste + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
            }
            else
            {
                if (Section == "0")
                {
                    if (Caste == "ALL")
                    {
                        query = "select DISTINCT cast from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                    else
                    {
                        query = "select DISTINCT cast from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and cast='" + Caste + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                }
                else
                {
                    if (Caste == "ALL")
                    {
                        query = "select DISTINCT cast from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Section='" + Section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                    else
                    {
                        query = "select DISTINCT cast from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Section='" + Section + "'  and cast='" + Caste + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "A";
            }
            else
            {
                string caste = "";
                foreach (DataRow dr in dt.Rows)
                {
                    caste = caste + "'" + dr["cast"].ToString() + "',";
                }
                caste = caste.Remove(caste.Length - 1);
                return caste;
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section");
        }
    }
}