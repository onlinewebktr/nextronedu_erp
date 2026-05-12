using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class sectionwise_student : System.Web.UI.Page
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
                        if (Request.QueryString["adm"] != null)
                        {
                            backbtns.HRef = "admission-report.aspx";
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_All_New(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        //ddlclass.SelectedValue = My.get_top_one_class();

                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        //ddl_section.Text = My.get_top_one_section();

                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;


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


                        find_by_section();
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
                    find_by_section();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_by_section()
        {
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                hd_session.Value = ddlsession.SelectedValue;
                hd_class.Value = "0";
                hd_section.Value = "0";
                bind_grd_view("select t1.Session_id,t1.Class_id,t1.session,t2.Course_Name as Class,t1.Section,count(t1.Id) as Total,t2.Position from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Status='1' group by t2.Course_Name,t1.Section,t2.Position,t1.session,t1.Session_id,t1.Class_id  order by t2.Position asc");
            }
            else
            {
                if (ddl_section.SelectedItem.Text == "ALL")
                {
                    if (ddlclass.SelectedItem.Text == "ALL")
                    {
                        ddlclass.Focus();
                        Alertme("Please select class.", "warning");
                        return;
                    }

                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = "0";
                    bind_grd_view("select t1.Session_id,t1.Class_id,t1.session,t2.Course_Name as Class,t1.Section,count(t1.Id) as Total,t2.Position from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "'   and t1.Status='1'  group by t2.Course_Name,t1.Section,t2.Position,t1.session,t1.Session_id,t1.Class_id  order by t2.Position asc");
                }
                else
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = ddl_section.SelectedItem.Text;
                    bind_grd_view("select t1.Session_id,t1.Class_id,t1.session,t2.Course_Name as Class,t1.Section,count(t1.Id) as Total,t2.Position from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "'  and t1.Class_id='" + ddlclass.SelectedValue + "' and t1.Section='" + ddl_section.SelectedItem.Text + "'  and t1.Status='1'  group by t2.Course_Name,t1.Section,t2.Position,t1.session,t1.Session_id,t1.Class_id  order by t2.Position asc");
                }
            }
        }



        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            print1.Visible = false;
            btn_excels.Visible = false;
            lbl_class22.Text = "";
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
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text + " Section :" + ddl_section.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                        pnl_grid.RenderControl(hw);
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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string session_id = ((Label)e.Item.FindControl("lbl_session_id")).Text;
                string class_id = ((Label)e.Item.FindControl("lbl_class_id")).Text;
                string section = ((Label)e.Item.FindControl("lbl_section")).Text;


                Label lbl_new_admission = ((Label)e.Item.FindControl("lbl_new_admission"));
                Label lbl_old_admission = ((Label)e.Item.FindControl("lbl_old_admission"));
                Label lbl_ttl_admission = ((Label)e.Item.FindControl("lbl_ttl_admission"));

                Label lbl_male = ((Label)e.Item.FindControl("lbl_male"));
                Label lbl_female = ((Label)e.Item.FindControl("lbl_female"));
                Label lbl_transgender = ((Label)e.Item.FindControl("lbl_transgender"));
                Label lbl_total_genderwise = ((Label)e.Item.FindControl("lbl_total_genderwise"));
                DataTable dt = mycode.FillData("select count(Id) as ttl_students from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and Transfer_Status='New'  and Status='1'  UNION all  select count(Id) as ttl_students from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  UNION all select count(Id) as ttl_students from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')  and Status='1'  UNION all select count(Id) as ttl_students from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and gender in('Male','MALE','male')  and Status='1'  UNION all select count(Id) as ttl_students from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and gender in('Female','FEMALE','female')  and Status='1'  UNION all select count(Id) as ttl_students from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "'  and Status='1'  and gender in('TRANSGENDER','transgender','Transgender')");

                if (dt.Rows.Count != 0)
                {
                    lbl_new_admission.Text = dt.Rows[0]["ttl_students"].ToString();
                    lbl_old_admission.Text = dt.Rows[1]["ttl_students"].ToString();
                    lbl_ttl_admission.Text = dt.Rows[2]["ttl_students"].ToString();

                    lbl_male.Text = dt.Rows[3]["ttl_students"].ToString();
                    lbl_female.Text = dt.Rows[4]["ttl_students"].ToString();
                    lbl_transgender.Text = dt.Rows[5]["ttl_students"].ToString();
                    lbl_total_genderwise.Text = (My.toDouble(dt.Rows[3]["ttl_students"].ToString()) + My.toDouble(dt.Rows[4]["ttl_students"].ToString()) + My.toDouble(dt.Rows[5]["ttl_students"].ToString())).ToString();
                }
                else
                {
                    lbl_new_admission.Text = "00";
                    lbl_old_admission.Text = "00";
                    lbl_ttl_admission.Text = "00";

                    lbl_male.Text = "00";
                    lbl_female.Text = "00";
                    lbl_transgender.Text = "00";
                    lbl_total_genderwise.Text = "00";
                }
            }
        }


        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string Class_id, string Section)
        {
            string sections = get_sections(Session, Class_id, Section);


            string query = "";
            if (Class_id == "0")
            {
                query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
            }
            else
            {
                if (Section == "0")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and Class_id='" + Class_id + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Status"].ToString());
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




        private static int get_total(string session_id, string class_id, string section)
        {
            string query = "select count(Id) as Count from admission_registor where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Section='" + section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
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

        private static string get_sections(string Session, string Class_id, string Section)
        {
            string query = "";
            if (Class_id == "0")
            {
                query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
            }
            else
            {
                if (Section == "0")
                {
                    query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and Class_id='" + Class_id + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else
                {
                    query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and Class_id='" + Class_id + "' and Section='" + Section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
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
                string section = "";
                foreach (DataRow dr in dt.Rows)
                {
                    section = section + "'" + dr["Section"].ToString() + "',";
                }

                section = section.Remove(section.Length - 1);
                return section;
            }
        }
    }
}